using System;
using System.Collections.Generic;

using indice.Edi.Serialization;

using Scribe.Connector.Common.Reflection;
using Scribe.Connector.Common.Reflection.Actions;
using CDK.Objects;

namespace CDK.EDIDocs
{
    [ObjectDefinition(Hidden = false, Name = EntityNames.PurchaseOrder_850)]
    [CreateWith]
    [Query]
    public class PurchaseOrder_850
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

        [PropertyDefinition(Description = "ISA06 - Interchange Sender ID")]
        [EdiValue("9(2)", Path = "ISA/6", Description = "ISA07 - Interchange ID Qualifier")]
        public string ID_Qualifier2 { get; set; }

        [PropertyDefinition(Description = "ISA08 - Interchange Receiver ID")]
        [EdiValue("X(15)", Path = "ISA/7", Description = "ISA08 - Interchange Receiver ID")]
        public string Receiver_ID { get; set; }

        [PropertyDefinition(Description = "Interchange Date I09 format \'yyMMdd I10\'. Interchange Time format \'HHmm\'.")]
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
        [PropertyDefinition(Name = EntityNames.PurchaseOrder_Groups)]
        public List<FunctionalGroup> Groups { get; set; }

        [ObjectDefinition(Name = EntityNames.PurchaseOrder_Groups)]
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

            [PropertyDefinition(Description = "GS0 4Date format \'yyyyMMdd\'. GS05 Time format \'HHmm\'.")]
            [EdiValue("9(8)", Path = "GS/3", Format = "yyyyMMdd", Description = "GS04 - Date")]
            [EdiValue("9(4)", Path = "GS/4", Format = "HHmm", Description = "GS05 - Time")]
            //was "Date"
            public DateTime FunctionalGDateroupDate { get; set; }

            [PropertyDefinition(Description = "GS06 - Group Control Number")]
            [EdiValue("9(9)", Path = "GS/5", Format = "HHmm", Description = "GS06 - Group Control Number")]
            public int GroupControlNumber { get; set; }

            [PropertyDefinition(Description = "GS07 Responsible Agency Code")]
            [EdiValue("X(2)", Path = "GS/6", Format = "HHmm", Description = "GS07 Responsible Agency Code")]
            public string AgencyCode { get; set; }

            [PropertyDefinition(Description = "GS08 Version / Release / Industry Identifier Code. Format \'HHmm\'.")]
            [EdiValue("X(2)", Path = "GS/7", Format = "HHmm", Description = "GS08 Version / Release / Industry Identifier Code")]
            public string Version { get; set; }

            [PropertyDefinition]
            public List<Order> Orders { get; set; }

            [PropertyDefinition(Description = "97 Number of Transaction Sets Included")]
            [EdiValue("9(1)", Path = "GE/0", Description = "97 Number of Transaction Sets Included")]
            public int TransactionsCount { get; set; }

            [PropertyDefinition(Description = "28 Group Control Number")]
            [EdiValue("9(9)", Path = "GE/1", Description = "28 Group Control Number")]
            public int GroupTrailerControlNumber { get; set; }
        }

        [ObjectDefinition]
        [EdiMessage]
        public class Order
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

            [PropertyDefinition(Description = "BEG01 - Trans. Set Purpose Code")]
            [EdiValue("X(2)", Path = "BEG/0", Description = "BEG01 - Trans. Set Purpose Code")]
            public string TransSetPurposeCode { get; set; }

            [PropertyDefinition(Description = "BEG02 - Purchase Order Type Code")]
            [EdiValue("X(2)", Path = "BEG/1", Description = "BEG02 - Purchase Order Type Code")]
            public string PurchaseOrderTypeCode { get; set; }

            [PropertyDefinition(Description = "BEG03 - Purchase Order Number")]
            [EdiValue(Path = "BEG/2", Description = "BEG03 - Purchase Order Number")]
            public string PurchaseOrderNumber { get; set; }

            [PropertyDefinition(Description = "BEG05 - Purchase Order Date. Format \'yyyyMMdd\'.")]
            [EdiValue("9(8)", Path = "BEG/4", Format = "yyyyMMdd", Description = "BEG05 - Purchase Order Date")]
            public string PurchaseOrderDate { get; set; }

            [PropertyDefinition(Description = "CUR01 - Entity Identifier Code")]
            [EdiValue(Path = "CUR/0", Description = "CUR01 - Entity Identifier Code")]
            public string EntityIdentifierCode { get; set; }

            [PropertyDefinition(Description = "CUR02 - Currency Code")]
            [EdiValue("X(3)", Path = "CUR/1", Description = "CUR02 - Currency Code")]
            public string CurrencyCode { get; set; }

            [PropertyDefinition(Description = "REF01 - Reference Identification Qualifier IA – Vendor Number assigned by Carhartt")]
            [EdiValue(Path = "REF/0", Description = "REF01 - Reference Identification Qualifier IA – Vendor Number assigned by Carhartt")]
            public string ReferenceIdentificationQualifier { get; set; }

            [PropertyDefinition(Description = "REF02 - Reference Identification")]
            [EdiValue(Path = "REF/1", Description = "REF02 - Reference Identification")]
            public string ReferenceIdentification { get; set; }

            [PropertyDefinition(Description = "FOB05 - Transportation Terms code")]
            [EdiValue(Path = "FOB/4", Description = "FOB05 - Transportation Terms code")]
            public string TransportationTermscode { get; set; }

            [PropertyDefinition(Description = "FOB06 - Code identifying type of location KL – Port of loading")]
            [EdiValue(Path = "FOB/5", Description = "FOB06 - Code identifying type of location KL – Port of loading")]
            public string LocationQualifier { get; set; }

            [PropertyDefinition(Description = "ITD01 - Terms Type Code")]
            [EdiValue("X(2)", Path = "ITD/0", Description = "ITD01 - Terms Type Code")]
            public string TermsTypeCode { get; set; }

            [PropertyDefinition(Description = "ITD02 - Terms Basis Date Code")]
            [EdiValue(Path = "ITD/1", Description = "ITD02 - Terms Basis Date Code")]
            public string TermsBasisDateCode { get; set; }

            [PropertyDefinition(Description = "ITD07 - Terms Net Days")]
            [EdiValue(Path = "ITD/6", Description = "ITD07 - Terms Net Days")]
            public string TermsNetDays { get; set; }

            [PropertyDefinition(Description = "TD504 - Transportation Method/Type Code")]
            [EdiValue(Path = "TD5/3", Description = "TD504 - Transportation Method/Type Code")]
            public string TransportationMethod { get; set; }

            [PropertyDefinition(Description = "MSG01 - Message Text")]
            [EdiValue(Path = "MSG/0", Description = "MSG01 - Message Text")]
            public string OrderHeaderMessageText { get; set; }

            [PropertyDefinition(Name = EntityNames.PurchaseOrder_Address)]
            public List<Address> Addresses { get; set; }

            [PropertyDefinition(Name = EntityNames.PurchaseOrder_Details)]
            public List<OrderDetail> Items { get; set; }

            [PropertyDefinition(Description = "AMT02 - Total amount of the Purchase Order")]
            [EdiValue(Path = "AMT/1", Description = "AMT02 - Total amount of the Purchase Order")]
            public string TotalTransactionAmount { get; set; }

        }

        [ObjectDefinition(Name = EntityNames.PurchaseOrder_Details)]
        [EdiSegment, EdiSegmentGroup("PO1", SequenceEnd = "CTT")]
        public class OrderDetail
        {
            [PropertyDefinition(Description = "PO101 - Order Line Number")]
            [EdiValue(Path = "PO1/0", Description = "PO101 - Order Line Number")]
            public string OrderLineNumber { get; set; }

            [PropertyDefinition(Description = "PO102 - Quantity Ordered")]
            [EdiValue(Path = "PO1/1", Description = "PO102 - Quantity Ordered")]
            public decimal QuantityOrdered { get; set; }

            [PropertyDefinition(Description = "PO103 - Unit Of Measurement")]
            [EdiValue(Path = "PO1/2", Description = "PO103 - Unit Of Measurement")]
            public string UnitOfMeasurement { get; set; }

            [PropertyDefinition(Description = "PO104 - Unit Price")]
            [EdiValue(Path = "PO1/3", Description = "PO104 - Unit Price")]
            public decimal UnitPrice { get; set; }

            [PropertyDefinition(Description = "PO109 - Buyer’s Part #.")]
            [EdiValue(Path = "PO1/8", Description = "PO109 - Buyer’s Part #.")]
            public string BuyersPartno { get; set; }

            [PropertyDefinition(Description = "PO111 - Vendor’s Part #.")]
            [EdiValue(Path = "PO1/10", Description = "PO111 - Vendor’s Part #.")]
            public string VendorsPartno { get; set; }

            [PropertyDefinition(Description = "PID05 - Product Description")]
            [EdiValue(Path = "PID/4", Description = "PID05 - Product Description")]
            public string ProductDescription { get; set; }

            [PropertyDefinition(Description = "MEA03 - Measurement Value")]
            [EdiValue(Path = "MEA/2", Description = "MEA03 - Measurement Value")]
            public decimal MeasurementValue { get; set; }

            [PropertyDefinition(Description = "DTM/0/0")]
            [EdiCondition("018", Path = "DTM/0/0")]
            public DTM AvailableFromDate { get; set; }

            [PropertyDefinition(Description = "DTM/0/0")]
            [EdiCondition("067", Path = "DTM/0/0")]
            public DTM ArrivalDate { get; set; }

            [PropertyDefinition(Description = "TC201 - Measurement Value")]
            [EdiValue(Path = "TC2/0", Description = "TC201 - Measurement Value")]
            public string TC201 { get; set; }

            [PropertyDefinition(Name = EntityNames.PurchaseOrder_MSG)]
            public List<MSG> MSG { get; set; }

        }

        [ObjectDefinition(Name = EntityNames.PurchaseOrder_Address)]
        [EdiSegment, EdiSegmentGroup("N1", SequenceEnd = "PO1")]
        public class Address
        {
            [PropertyDefinition(Description = "N101 - Address Code")]
            [EdiValue(Path = "N1/0", Description = "N101 - Address Code")]
            public string AddressCode { get; set; }

            [PropertyDefinition(Description = "N102 - Address Name")]
            [EdiValue(Path = "N1/1", Description = "N102 - Address Name")]
            public string AddressName { get; set; }

            [PropertyDefinition(Description = "N301 - Address Information")]
            [EdiValue(Path = "N3/0", Description = "N301 - Address Information")]
            public string AddressInformation { get; set; }

            [PropertyDefinition(Description = "N401 - City Name")]
            [EdiValue(Path = "N4/0", Description = "N401 - City Name")]
            public string CityName { get; set; }

            [PropertyDefinition(Description = "N404 - Country Code")]
            [EdiValue(Path = "N4/3", Description = "N404 - Country Code")]
            public string CountryCode { get; set; }

        }

        [ObjectDefinition]
        [EdiSegment, EdiPath("DTM")]
        public class DTM
        {
            [PropertyDefinition(Description = "DTM01 - Date/Time Qualifier")]
            [EdiValue(Path = "DTM/0", Description = "DTM01 - Date/Time Qualifier")]
            public string DateTimeQualifier { get; set; }

            [PropertyDefinition(Description = "DTM02 - Date format \'CCYYMMDD\' or \'yyyyMMdd\'.")]
            [EdiValue("9(8)", Path = "DTM/1", Format = "yyyyMMdd", Description = "DTM02 - Date format =CCYYMMDD")]
            public DateTime Date { get; set; }
        }

        [ObjectDefinition(Name = EntityNames.PurchaseOrder_MSG)]
        [EdiElement, EdiPath("MSG/0")]
        public class MSG
        {
            [PropertyDefinition(Description = "MSG01 - Message Text")]
            [EdiValue(Path = "MSG/0", Description = "MSG01 - Message Text")]
            public string MessageText { get; set; }
        }

        #region Edi Enumerations
        #endregion
    }
}