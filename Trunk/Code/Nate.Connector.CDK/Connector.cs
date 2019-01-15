using System;
using System.Collections.Generic;

using Scribe.Core.ConnectorApi;
using Scribe.Core.ConnectorApi.Actions;
using Scribe.Core.ConnectorApi.Query;
using Scribe.Core.ConnectorApi.Exceptions;
using Scribe.Core.ConnectorApi.Logger;
using Scribe.Connector.Common;
using CDK.packages;

namespace CDK
{
    #region Connector Attributes

    [ScribeConnector(
    ConnectorTypeIdAsString,
    ConnectorTypeName,
    ConnectorTypeDescription,
    typeof(Connector),
    "", // SettingsUITypeName (obsolete)
    "", // SettingsUIVersion (obsolete)
    ConnectionUITypeName,
    ConnectionUIVersion,
    "", // XapFileName (obsolete)
    new[] { "Scribe.IS2.Target", "Scribe.MS2.Target", "Scribe.IS2.Source", "Scribe.MS2.Source", "Scribe.IS2.Message" },
    SupportsCloud,
    ConnectorVersion)]
    #endregion

    public class Connector : IConnector, IDisposable, ISupportMessage //ISupportOAuth, ISupportProcessNotifications
    {
        #region Constants

        internal const string ConnectorTypeName = "Scribe Labs - EDI";
        internal const string ConnectorTypeDescription = "Connector that captures EDI messages";
        internal const string ConnectorVersion = "1.0.0";
        internal const string ConnectorTypeIdAsString = "{BC0BDE70-AC3D-47AB-B3A0-AB4022B1E61C}";
        internal const string CryptoKey = "{D553D40C-FEEE-404D-AD29-FCC4DAEBD26C}";
        internal const string CompanyName = "Scribe Labs";
        internal const bool SupportsCloud = true;
        internal const string ConnectionUITypeName = "ScribeOnline.GenericConnectionUI";
        internal const string ConnectionUIVersion = "1.0";

        private MethodInfo methodInfo;
        private ConnectorService service;
        private IMetadataProvider metadataProvider;

        public Connector()
        {
            clearLocals();
            methodInfo = new MethodInfo(GetType().Name);
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            clearLocals();
        }

        private void clearLocals()
        {
            if (metadataProvider != null)
            {
                metadataProvider.Dispose();
                metadataProvider = null;
            }

            if (service != null)
            {
                service.Disconnect();
                service = null;
            }
        }

        internal static void unhandledExecptionHandler(string methodName, Exception exception)
        {
            var msg = string.Format("Unhandled exception caught in {0}: {1}\n\n", methodName, exception.Message);
            var details = string.Format("Details: {0}", exception.ToString());
            Logger.Write(Logger.Severity.Error, ConnectorTypeDescription, msg + details);
            throw new ApplicationException(msg, exception);
        }

        #endregion

        #region IConnector implimentation

        public Guid ConnectorTypeId => Guid.Parse(ConnectorTypeIdAsString);
        public bool IsConnected
        {
            get
            {
                if (service == null) return false;
                return service.IsConnected;
            }
        }

        public string PreConnect(IDictionary<string, string> properties)
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                try
                {
                    var uiDef = ConnectionHelper.GetConnectionFormDefintion();
                    return uiDef.Serialize();
                }
                catch (Exception exception)
                {
                    unhandledExecptionHandler(methodInfo.GetCurrentMethodName(), exception);
                }
            }
            return "";
        }

        public void Connect(IDictionary<string, string> properties)
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                try
                {
                    // validate & get connection properties
                    var connectionProps = ConnectionHelper.GetConnectionProperties(properties);

                    if (service == null)
                        service = new ConnectorService();

                    service.Connect(connectionProps);
                }
                catch (InvalidConnectionException)
                {
                    clearLocals();
                    throw;
                }
                catch (Exception exception)
                {
                    clearLocals();
                    unhandledExecptionHandler(methodInfo.GetCurrentMethodName(), exception);
                }
            }
        }

        public void Disconnect()
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                try
                {
                    clearLocals();
                }
                catch (Exception exception)
                {
                    unhandledExecptionHandler(methodInfo.GetCurrentMethodName(), exception);
                }
            }
        }

        public IMetadataProvider GetMetadataProvider()
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                if (service.GetMetadataProvider() == null)
                    throw new ApplicationException("Must connect before calling " + methodInfo.GetCurrentMethodName());

                return service.GetMetadataProvider();
            }
        }

        public IEnumerable<DataEntity> ExecuteQuery(Query query)
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                try
                {
                    if (service == null || service.IsConnected == false)
                        throw new ApplicationException("Must connect before calling " + methodInfo.GetCurrentMethodName());

                    if (query == null)
                        throw new ArgumentNullException(nameof(query));

                    return service.ExecuteQuery(query);
                }
                catch (InvalidExecuteQueryException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    unhandledExecptionHandler(methodInfo.GetCurrentMethodName(), exception);
                }
            }

            return null;
        }

        public IEnumerable<DataEntity> ProcessMessage(string entityName, string message)
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                try
                {
                    if (service == null || service.IsConnected == false)
                        throw new ApplicationException("Must connect before calling " + methodInfo.GetCurrentMethodName());

                    return service.ProcessNotification(entityName, message);
                }
                catch (InvalidOperationException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    unhandledExecptionHandler(methodInfo.GetCurrentMethodName(), exception);
                    throw;
                }
            }
        }

        public OperationResult ExecuteOperation(OperationInput input)
        {
            using (new LogMethodExecution(ConnectorTypeDescription, methodInfo.GetCurrentMethodName()))
            {
                try
                {
                    if (service == null || service.IsConnected == false)
                        throw new ApplicationException("Must connect before calling " + methodInfo.GetCurrentMethodName());

                    if (input == null)
                        throw new ArgumentNullException(nameof(input));

                    if (input.Input == null)
                        throw new ArgumentException(StringMessages.InputPropertyCannotBeNull, nameof(input));

                    if (!Enum.TryParse(input.Name, out ConnectorService.SupportedActions action))
                        throw new InvalidExecuteOperationException("Unsupported operation: " + input.Name);

                    if (input.Input.Length < 1)
                        throw new ArgumentException(StringMessages.InputNeedsAtLeastOneEntity, nameof(input));

                    switch (action)
                    {
                        case ConnectorService.SupportedActions.Create:
                            return service.Create(input.Input[0]);
                        default:
                            throw new InvalidExecuteOperationException("Unsupported operation: " + input.Name);
                    }
                }
                catch (InvalidExecuteOperationException)
                {
                    throw;
                }
                catch (Exception exception)
                {
                    unhandledExecptionHandler(methodInfo.GetCurrentMethodName(), exception);
                }
            }
            return null;
        }

        public MethodResult ExecuteMethod(MethodInput input)
        {
            throw new NotImplementedException();
        }

        public void ProcessStarted(Guid processId)
        {
            throw new NotImplementedException();
        }

        public void ProcessEnded(Guid processId, bool success)
        {
            throw new NotImplementedException();
        }
    }
        #endregion 
}
