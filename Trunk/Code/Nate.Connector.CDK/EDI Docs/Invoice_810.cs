using System;
using System.Collections.Generic;

using indice.Edi.Serialization;

using Scribe.Connector.Common.Reflection;
using Scribe.Connector.Common.Reflection.Actions;
using CDK.Objects;

namespace indice.Edi.Tests.Models
{
    [ObjectDefinition(Hidden = false, Name = EntityNames.Invoice_810)]
    [CreateWith]
    [Query]
    public class Invoice_810
    {
        #region Scribe Filters
        [PropertyDefinition(UsedInActionOutput = false, UsedInQueryConstraint = true, UsedInQuerySelect = false)]
        public string Folder { get; set; }

        [PropertyDefinition(UsedInActionOutput = false, UsedInQueryConstraint = true, UsedInQuerySelect = false)]
        public string Filename { get; set; }

        #endregion

        #region ISA and IEA
        [PropertyDefinition(Description = "ISA01 - Authorization Information Qualifier")]
        [EdiValue("9(2)", Path = "ISA/0", Description = "ISA01 - Authorization Information Qualifier")]
        public int AuthorizationInformationQualifier { get; set; }

        [PropertyDefinition(Description = "ISA02 - Authorization Information")] 
        [EdiValue("X(10)", Path = "ISA/1", Description = "ISA02 - Authorization Information")]
        public string AuthorizationInformation { get; set; }

        [PropertyDefinition(Description = "ISA03 - Security Information Qualifier")]
        [EdiValue("9(2)", Path = "ISA/2", Description = "ISA03 - Security Information Qualifier")]
        public string Security_Information_Qualifier { get; set; }

        [PropertyDefinition(Description = "ISA04 - Security Information")]
        [EdiValue("X(10)", Path = "ISA/3", Description = "ISA04 - Security Information")]
        public string Security_Information { get; set; }

        [PropertyDefinition(Description = "ISA05 - Interchange ID Qualifier")]
        [EdiValue("9(2)", Path = "ISA/4", Description = "ISA05 - Interchange ID Qualifier")]
        public string ID_Qualifier { get; set; }

        [PropertyDefinition(Description = "ISA06 - Interchange Sender ID")]
        [EdiValue("X(15)", Path = "ISA/5", Description = "ISA06 - Interchange Sender ID")]
        public string Sender_ID { get; set; }

        [PropertyDefinition(Description = "ISA07 - Interchange ID Qualifier")]
        [EdiValue("9(2)", Path = "ISA/6", Description = "ISA07 - Interchange ID Qualifier")]
        public string ID_Qualifier2 { get; set; }

        [PropertyDefinition(Description = "ISA08 - Interchange Receiver ID")]
        [EdiValue("X(15)", Path = "ISA/7", Description = "ISA08 - Interchange Receiver ID")]
        public string Receiver_ID { get; set; }

        [PropertyDefinition(Description = "I09 - Interchange Date format \'yyMMdd\'. I10 - Interchange Time format \'HHmm\'.")]
        [EdiValue("9(6)", Path = "ISA/8", Format = "yyMMdd", Description = "I09 - Interchange Date")]
        [EdiValue("9(4)", Path = "ISA/9", Format = "HHmm", Description = "I10 - Interchange Time")]
        public DateTime Date { get; set; }

        [PropertyDefinition(Description = "ISA11 - Interchange Control Standards ID")]
        [EdiValue("X(1)", Path = "ISA/10", Description = "ISA11 - Interchange Control Standards ID")]
        public string Control_Standards_ID { get; set; }

        [PropertyDefinition(Description = "ISA12 - Interchange Control Version Num")]
        [EdiValue("9(5)", Path = "ISA/11", Description = "ISA12 - Interchange Control Version Num")]
        public int ControlVersion { get; set; }

        [PropertyDefinition(Description = "ISA13 - Interchange Control Number")]
        [EdiValue("9(9)", Path = "ISA/12", Description = "ISA13 - Interchange Control Number")]
        public int ControlNumber { get; set; }

        [PropertyDefinition(Description = "ISA14 - Acknowledgement Requested")]
        [EdiValue("9(1)", Path = "ISA/13", Description = "ISA14 - Acknowledgement Requested")]
        public bool? AcknowledgementRequested { get; set; }

        [PropertyDefinition(Description = "ISA15 - Usage Indicator")]
        [EdiValue("X(1)", Path = "ISA/14", Description = "ISA15 - Usage Indicator")]
        public string Usage_Indicator { get; set; }

        [PropertyDefinition(Description = "ISA16 - Component Element Separator")]
        [EdiValue("X(1)", Path = "ISA/15", Description = "ISA16 - Component Element Separator")]
        public char? Component_Element_Separator { get; set; }

        [PropertyDefinition(Description = "IEA01 - Num of Included Functional Grps")]
        [EdiValue("9(1)", Path = "IEA/0", Description = "IEA01 - Num of Included Functional Grps")]
        public int GroupsCount { get; set; }

        [PropertyDefinition(Description = "IEA02 - Interchange Control Number")]
        [EdiValue("9(9)", Path = "IEA/1", Description = "IEA02 - Interchange Control Number")]
        public int TrailerControlNumber { get; set; }

        #endregion

        [PropertyDefinition(Name = EntityNames.Invoice_Groups)]
        public List<FunctionalGroup> Groups { get; set; }

        [ObjectDefinition(Name = EntityNames.Invoice_Groups)]
        [EdiGroup]
        public class FunctionalGroup
        {
            [PropertyDefinition(Description = "GS01 - Functional Identifier Code")]
            [EdiValue("X(2)", Path = "GS/0", Description = "GS01 - Functional Identifier Code")]
            public string FunctionalIdentifierCode { get; set; }

            [PropertyDefinition(Description = "GS02 - Application Sender's Code")]
            [EdiValue("X(15)", Path = "GS/1", Description = "GS02 - Application Sender's Code")]
            public string ApplicationSenderCode { get; set; }

            [PropertyDefinition(Description = "GS03 - Application Receiver's Code")]
            [EdiValue("X(15)", Path = "GS/2", Description = "GS03 - Application Receiver's Code")]
            public string ApplicationReceiverCode { get; set; }

            [PropertyDefinition(Description = "GS04 - Date format \'yyyyMMdd\'. GS05 - Time format \'HHmm\'.")]
            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "GS04 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "GS05 - Time")]
            public DateTime Date { get; set; }

            [PropertyDefinition(Description = "GS06 - Group Control Number format \'HHmm\'.")]
            [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "GS06 - Group Control Number")]
            public int GroupControlNumber { get; set; }

            [PropertyDefinition(Description = "GS07 Responsible Agency Code format \'HHmm\'.")]
            [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "GS07 Responsible Agency Code")]
            public string AgencyCode { get; set; }

            [PropertyDefinition(Description = "GS08 Version / Release / Industry Identifier Code format \'HHmm\'.")]
            [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "GS08 Version / Release / Industry Identifier Code")]
            public string Version { get; set; }

            [PropertyDefinition]
            public Invoice Invoice { get; set; }

            [PropertyDefinition(Description = "97 Number of Transaction Sets Included")]
            [EdiValue("9(1)", Path = "GE/0", Description = "97 Number of Transaction Sets Included")]
            public int TransactionsCount { get; set; }

            [PropertyDefinition(Description = "28 Group Control Number")]
            [EdiValue("9(9)", Path = "GE/1", Description = "28 Group Control Number")]
            public int GroupTrailerControlNumber { get; set; }
        }

        [ObjectDefinition]
        [EdiMessage]
        public class Invoice
        {
            #region Header Trailer
            [PropertyDefinition(Description = "ST01 - Transaction set ID code")]
            [EdiValue("X(3)", Path = "ST/0", Description = "ST01 - Transaction set ID code")]
            public string TransactionSetCode { get; set; }

            [PropertyDefinition(Description = "ST02 - Transaction set control number")]
            [EdiValue("X(9)", Path = "ST/1", Description = "ST02 - Transaction set control number")]
            public string TransactionSetControlNumber { get; set; }

            [PropertyDefinition(Description = "SE01 - Number of included segments")]
            [EdiValue(Path = "SE/0", Description = "SE01 - Number of included segments")]
            public int SegmentsCouts { get; set; }

            [PropertyDefinition(Description = "SE02 - Transaction set control number (same as ST02)")]
            [EdiValue("X(9)", Path = "SE/1", Description = "SE02 - Transaction set control number (same as ST02)")]
            public string TrailerTransactionSetControlNumber { get; set; }
            #endregion

            [PropertyDefinition]
            [EdiCondition("P", Path = "BAL/0"), EdiCondition("TP", Path = "BAL/1")]
            public Balance TotalPaymentsAndRefunds { get; set; }

            [PropertyDefinition]
            [EdiCondition("M", Path = "BAL/0"), EdiCondition("YB", Path = "BAL/1")]
            public Balance TotalOutstandingBalance { get; set; }

            [PropertyDefinition]
            [EdiCondition("P", Path = "BAL/0"), EdiCondition("YB", Path = "BAL/1")]
            public Balance PriorBalance { get; set; }
        }

        [ObjectDefinition]
        [EdiSegment]
        public class Balance
        {
            [PropertyDefinition(Description = "Decimal Number")]
            [EdiValue("9(9)", Path = "BAL/2", Description = "Decimal Number")]
            public decimal Value { get; set; }
        }
    }
}