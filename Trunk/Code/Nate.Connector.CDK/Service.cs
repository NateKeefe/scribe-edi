using System;
using System.IO;
using System.Collections.Generic;
using System.Reflection;
using System.Collections.Concurrent;
using indice.Edi;
using indice.Edi.Tests.Models;

using Scribe.Core.ConnectorApi;
using Scribe.Core.ConnectorApi.Actions;
using Scribe.Core.ConnectorApi.Metadata;
using Scribe.Core.ConnectorApi.Query;
using Scribe.Connector.Common.Reflection.Data;
using Scribe.Core.ConnectorApi.Exceptions;

using CDK.Objects;
using CDK.packages;
using CDK.EDIDocs;
using Scribe.Core.ConnectorApi.Logger;
using CDK.EDI_Docs;

namespace CDK
{
    class ConnectorService
    {
        #region Instaniation 
        private Reflector reflector;
        public bool IsConnected { get; set; }
        public Guid ConnectorTypeId { get; }
        public void Dispose()
        {
            throw new NotImplementedException();
        }
        //Will be used for H-Data Messages
        private static ConcurrentDictionary<Guid, object>
            notificationCache = new ConcurrentDictionary<Guid, object>();
        private static ConcurrentDictionary<DateTime, Guid>
            notificationTimes = new ConcurrentDictionary<DateTime, Guid>();
        
        #endregion

        #region Connection
        public enum SupportedActions
        {
            Query,
            Create,
            CreateWith
        }

        public void Connect(ConnectionHelper.ConnectionProperties connectionProps)
        {
            reflector = new Reflector(Assembly.GetExecutingAssembly());
            IsConnected = true;
        }

        public void Disconnect()
        {
            IsConnected = false;
        }

        #endregion 

        #region Messaging

        public IEnumerable<DataEntity> ProcessNotification(string entityName, string message)
        {
            throw new ApplicationException("Execute does not support object type: " + entityName);
        }

        #endregion 

        #region Operations
        public OperationResult Create(DataEntity dataEntity)
        {
            var entityName = dataEntity.ObjectDefinitionFullName;
            var operationResult = new OperationResult();
            var output = new DataEntity(entityName);

            switch (entityName)
            {
                case EntityNames.X12_Invoice_810:
                    var invoice810 = ToScribeModel<Invoice_810>(dataEntity);
                    var grammar = EdiGrammar.NewX12();
                    using (var textWriter = new StreamWriter(File.Open(@"c:\temp\invoice.edi", FileMode.Create)))
                    {
                        using (var ediWriter = new EdiTextWriter(textWriter, grammar))
                        {
                            new EdiSerializer().Serialize(ediWriter, invoice810);
                        }
                    }
                        break;
                case "OrderTest":
                    var order = ToScribeModel<EDI_Docs.OrderTest>(dataEntity);
                    var grammar2 = EdiGrammar.NewEdiFact();
                    //grammar2.SetAdvice("+","+",":","","?","",".");
                    using (var textWriter = new StreamWriter(File.Open(@"c:\temp\order.edi", FileMode.Create)))
                    {
                        using (var ediWriter = new EdiTextWriter(textWriter, grammar2))
                        {
                            new EdiSerializer().Serialize(ediWriter, order);
                        }
                    }
                    break;
                default:
                    throw new ArgumentException($"{entityName} is not supported for Create.");
            }
            operationResult = new OperationResult
            {
                ErrorInfo = new ErrorResult[] { null },
                ObjectsAffected = new[] { 1 },
                Output = new[] { output },
                Success = new[] { true }
            };
            return operationResult;
        }

        private T ToScribeModel<T>(DataEntity input) where T : new()
        {
            T scribeModel;
            try
            {
                scribeModel = reflector.To<T>(input);
            }
            catch (Exception e)
            {
                throw new ArgumentException("Error translating from DataEntity to ScribeModel: " + e.Message, e);
            }
            return scribeModel;
        }

        #endregion

        #region Query
        public IEnumerable<DataEntity> ExecuteQuery(Query query)
        {
            var entityName = query.RootEntity.ObjectDefinitionFullName;
            var rawData = ReadFile(query);

            //deserialize file
            switch (entityName)
            {
                case EntityNames.X12_PurchaseOrder_850: return ReadEDI<PurchaseOrder_850>(rawData, query, reflector, EdiGrammar.NewX12());
                case EntityNames.X12_Invoice_810: return ReadEDI<Invoice_810>(rawData, query, reflector, EdiGrammar.NewX12());
                case "S_ORDRSP": return ReadEDI<ORDRSP>(rawData, query, reflector, EdiGrammar.NewEdiFact());

                default:
                    throw new InvalidExecuteQueryException(
                        $"The {entityName} entity is not supported for query.");
            }
        }

        private static Dictionary<string, object> BuildConstraintDictionary(Expression queryExpression)
        {
            var constraints = new Dictionary<string, object>();

            if (queryExpression == null)
                return constraints;

            if (queryExpression.ExpressionType == ExpressionType.Comparison)
            {
                // only 1 filter
                addCompEprToConstraints(queryExpression as ComparisonExpression, ref constraints);
            }
            else if (queryExpression.ExpressionType == ExpressionType.Logical)
            {
                // Multiple filters
                addLogicalEprToConstraints(queryExpression as LogicalExpression, ref constraints);
            }
            else
                throw new InvalidExecuteQueryException("Unsupported filter type: " + queryExpression.ExpressionType.ToString());

            return constraints;
        }

        private static void addLogicalEprToConstraints(LogicalExpression exp, ref Dictionary<string, object> constraints)
        {
            if (exp.Operator != LogicalOperator.And)
                throw new InvalidExecuteQueryException("Unsupported operator in filter: " + exp.Operator.ToString());

            if (exp.LeftExpression.ExpressionType == ExpressionType.Comparison)
                addCompEprToConstraints(exp.LeftExpression as ComparisonExpression, ref constraints);
            else if (exp.LeftExpression.ExpressionType == ExpressionType.Logical)
                addLogicalEprToConstraints(exp.LeftExpression as LogicalExpression, ref constraints);
            else
                throw new InvalidExecuteQueryException("Unsupported filter type: " + exp.LeftExpression.ExpressionType.ToString());

            if (exp.RightExpression.ExpressionType == ExpressionType.Comparison)
                addCompEprToConstraints(exp.RightExpression as ComparisonExpression, ref constraints);
            else if (exp.RightExpression.ExpressionType == ExpressionType.Logical)
                addLogicalEprToConstraints(exp.RightExpression as LogicalExpression, ref constraints);
            else
                throw new InvalidExecuteQueryException("Unsupported filter type: " + exp.RightExpression.ExpressionType.ToString());
        }

        private static void addCompEprToConstraints(ComparisonExpression exp, ref Dictionary<string, object> constraints)
        {
            if (exp.Operator != ComparisonOperator.Equal)
                throw new InvalidExecuteQueryException(string.Format(StringMessages.OnlyEqualsOperatorAllowed, exp.Operator.ToString(), exp.LeftValue.Value));

            var constraintKey = exp.LeftValue.Value.ToString();
            if (constraintKey.LastIndexOf(".") > -1)
            {
                // need to remove "objectname." if present
                constraintKey = constraintKey.Substring(constraintKey.LastIndexOf(".") + 1);
            }
            constraints.Add(constraintKey, exp.RightValue.Value.ToString());
        }

        public static string ReadFile(Query query)
        {
            string rawData = "";
            var entityName = query.RootEntity.ObjectDefinitionFullName;
            var constraints = BuildConstraintDictionary(query.Constraints);

            constraints.TryGetValue("Filename", out var filename);
                if (filename == null) { throw new InvalidExecuteQueryException("Missing Filename filter."); }
            constraints.TryGetValue("Folder", out var folder);
                if (folder == null) { throw new InvalidExecuteQueryException("Missing Folder filter."); }
                if (folder.ToString().EndsWith("\\") == false) { folder = folder + "\\".ToString(); }
            try
            {
                rawData = File.ReadAllText(folder.ToString() + filename.ToString());
            }
            catch (Exception exp)
            {
                Logger.Write(Logger.Severity.Error,
                    $"Cannot find Folder or File when querying entity: {entityName}.", exp.InnerException.Message);
                throw new InvalidExecuteQueryException("Cannot find Folder or File: " + exp.Message);
            }
            return rawData;
        }

        public static IEnumerable<DataEntity> ReadEDI<T>(string raw, Query query, Reflector r, IEdiGrammar grammar)
        {
            var n = default(T);
            using (TextReader sr = new StringReader(raw)) { n = new EdiSerializer().Deserialize<T>(sr, grammar); }
            return r.ToDataEntities(new[] { n }, query.RootEntity);
        }
        #endregion

        #region Metadata

        public IMetadataProvider GetMetadataProvider()
        {
            return reflector.GetMetadataProvider();
        }

        public IEnumerable<IActionDefinition> RetrieveActionDefinitions()
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IObjectDefinition> RetrieveObjectDefinitions(bool shouldGetProperties = false, bool shouldGetRelations = false)
        {
            throw new NotImplementedException();
        }

        public IObjectDefinition RetrieveObjectDefinition(string objectName, bool shouldGetProperties = false,
            bool shouldGetRelations = false)
        {
            throw new NotImplementedException();
        }

        public IEnumerable<IMethodDefinition> RetrieveMethodDefinitions(bool shouldGetParameters = false)
        {
            throw new NotImplementedException();
        }

        public IMethodDefinition RetrieveMethodDefinition(string objectName, bool shouldGetParameters = false)
        {
            throw new NotImplementedException();
        }

        public void ResetMetadata()
        {
            throw new NotImplementedException();
        }

        #endregion
    }
}
