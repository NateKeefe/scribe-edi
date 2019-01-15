using System;
using System.Collections.Generic;

using Scribe.Core.ConnectorApi.Exceptions;
using Scribe.Core.ConnectorApi.ConnectionUI;
using Scribe.Core.ConnectorApi.Cryptography;

namespace CDK
{
    internal static class ConnectionHelper
    {
        internal class ConnectionProperties
        {
            public string SourcePath { get; set; }
        }

        #region Constants

        internal static class ConnectionPropertyKeys
        {
            public const string SourcePath = "SourcePath";
        }

        internal static class ConnectionPropertyLabels
        {
            public const string SourcePath = "Source Path";
        }

        private const string HelpLink = "https://dev.scribesoft.com/";

        #endregion

        internal static ConnectionProperties GetConnectionProperties(IDictionary<string, string> propDictionary)
        {
            if (propDictionary == null)
                throw new InvalidConnectionException("Connection Properties are NULL");

            var connectorProps = new ConnectionProperties();
            connectorProps.SourcePath = getRequiredPropertyValue(propDictionary, ConnectionPropertyKeys.SourcePath, ConnectionPropertyLabels.SourcePath);

            // re-check unencrypted password
            if (string.IsNullOrEmpty(connectorProps.SourcePath))
                throw new InvalidConnectionException(string.Format("A value is required for '{0}'", ConnectionPropertyLabels.SourcePath));

            return connectorProps;
        }

        private static string getRequiredPropertyValue(IDictionary<string, string> properties, string key, string label)
        {
            var value = getPropertyValue(properties, key);
            if (string.IsNullOrEmpty(value))
                throw new InvalidConnectionException(string.Format("A value is required for '{0}'", label));

            return value;
        }

        private static string getPropertyValue(IDictionary<string, string> properties, string key)
        {
            var value = "";
            properties.TryGetValue(key, out value);
            return value;
        }

        internal static FormDefinition GetConnectionFormDefintion()
        {

            var formDefinition = new FormDefinition
            {
                CompanyName = Connector.CompanyName,
                CryptoKey = Connector.CryptoKey,
                HelpUri = new Uri(HelpLink)
            };
            formDefinition.Add(BuildTokenDefinition(0));

            return formDefinition;
        }

        private static EntryDefinition BuildTokenDefinition(int order)
        {
            var entryDefinition = new EntryDefinition
            {
                InputType = InputType.Text,
                IsRequired = true,
                Label = ConnectionPropertyLabels.SourcePath,
                PropertyName = ConnectionPropertyKeys.SourcePath,
                Order = order,
            };

            return entryDefinition;
        }
    }
}
