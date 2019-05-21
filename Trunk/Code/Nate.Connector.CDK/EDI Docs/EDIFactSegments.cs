//using System;
//using System.Collections.Generic;
//using CDK.Objects;
//using indice.Edi.Serialization;
//using indice.Edi.Utilities;
//using Scribe.Connector.Common.Reflection;
//using Scribe.Connector.Common.Reflection.Actions;

//namespace CDK.EDIDocs
//{
//    class EdiFact01_Segments
//    {
//        [ObjectDefinition(Hidden = false, Name = EntityNames.EdiFact_ORDER)]
//        [CreateWith]
//        [Query]
//        public class Interchange_Multi_Message
//        {
//            [PropertyDefinition(UsedInActionOutput = false, UsedInQueryConstraint = true, UsedInQuerySelect = false)]
//            public string Folder { get; set; }

//            [PropertyDefinition(UsedInActionOutput = false, UsedInQueryConstraint = true, UsedInQuerySelect = false)]
//            public string Filename { get; set; }

//            [PropertyDefinition(Name = EntityNames.EdiFact_ORDER_UNB_Header)]
//            public UNB Header { get; set; }

//            [PropertyDefinition(Name = EntityNames.EdiFact_ORDER_UNB_Footer)]
//            public UNZ Footer { get; set; }

//            [PropertyDefinition(Name = EntityNames.EdiFact_ORDER_UNB_Order)]
//            public List<Order> Message { get; set; }
//        }

//        [ObjectDefinition(Hidden = false, Name = EntityNames.EdiFact_ORDER_UNB_Order)]
//        [EdiMessage]
//        public class Order
//        {
//            [PropertyDefinition]
//            public UNH_Segment Header { get; set; }

//            [PropertyDefinition]
//            public BGM_Segment BGM { get; set; }

//            [PropertyDefinition]
//            public DTM_Segment DTM { get; set; }

//            [PropertyDefinition]
//            public CUX_Segment CUX { get; set; }

//            [PropertyDefinition]
//            public List<NAD> NAD { get; set; }

//            [PropertyDefinition]
//            public LOC_Segment LOC { get; set; }

//            [PropertyDefinition]
//            public List<LineItem> Lines { get; set; }

//            [PropertyDefinition]
//            public UNS_Segment UNS { get; set; }

//            [PropertyDefinition]
//            public UNT_Segment Trailer { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_UNB_Header)]
//        [EdiSegment, EdiPath("UNB")]
//        public class UNB
//        {
//            [PropertyDefinition]
//            [EdiValue("X(4)", Mandatory = true, Path = "UNB/0")]
//            public string SyntaxIdentifier { get; set; }

//            [PropertyDefinition]
//            [EdiValue("9(1)", Path = "UNB/0/1", Mandatory = true)]
//            public int SyntaxVersion { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
//            public string SenderId { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(4)", Path = "UNB/1/1", Mandatory = true)]
//            public string PartnerIDCodeQualifier { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
//            public string ReverseRoutingAddress { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
//            public string RecipientId { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = true)]
//            public string ParterIDCode { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
//            public string RoutingAddress { get; set; }

//            [PropertyDefinition]
//            [EdiValue("9(6)", Path = "UNB/3/0", Format = "ddMMyy", Description = "Date of Preparation")]
//            [EdiValue("9(4)", Path = "UNB/3/1", Format = "HHmm", Description = "Time or Prep")]
//            public DateTime DateOfPreparation { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(14)", Path = "UNB/4", Mandatory = true)]
//            public string ControlRef { get; set; }

//            [PropertyDefinition]
//            [EdiValue("9(1)", Path = "UNB/8", Mandatory = false)]
//            public int AckRequest { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_Header)]
//        [EdiSegment, EdiPath("UNH")]
//        public class UNH_Segment
//        {
//            [PropertyDefinition]
//            [EdiValue("X(14)", Path = "UNH/0/0")]
//            public string MessageRef { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(6)", Path = "UNH/1/0")]
//            public string MessageType { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "UNH/1/1")]
//            public string Version { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "UNH/1/2")]
//            public string ReleaseNumber { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(2)", Path = "UNH/1/3")]
//            public string ControllingAgency { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(6)", Path = "UNH/1/4")]
//            public string AssociationAssignedCode { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(35)", Path = "UNH/2/0")]
//            public string CommonAccessRef { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_UNB_Footer)]
//        [EdiSegment, EdiPath("UNZ")]
//        public class UNZ
//        {
//            [PropertyDefinition]
//            [EdiValue("X(1)", Path = "UNZ/0")]
//            public int TrailerControlCount { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(14)", Path = "UNZ/1")]
//            public string TrailerControlReference { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_BGM)]
//        [EdiSegment, EdiPath("BGM")]
//        public class BGM_Segment
//        {
//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "BGM/0/0")]
//            public string MessageName { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(35)", Path = "BGM/1/0")]
//            public string DocumentNumber { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "BGM/2/0", Mandatory = false)]
//            public string MessageFunction { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "BGM/3/0")]
//            public string ResponseType { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_DTM_Segment)]
//        [EdiSegment, EdiPath("DTM")]
//        public class DTM_Segment
//        {
//            [PropertyDefinition]
//            [EdiCondition("137", Path = "DTM/0/0")]
//            public DTM MessageDate { get; set; }

//            [PropertyDefinition]
//            [EdiCondition("163", Path = "DTM/0/0")]
//            public DTM ProcessingStartDate { get; set; }

//            [PropertyDefinition]
//            [EdiCondition("164", Path = "DTM/0/0")]
//            public DTM ProcessingEndDate { get; set; }

//            [PropertyDefinition]
//            public UTCOffset UTCOffset { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_CUX)]
//        [EdiSegment, EdiPath("CUX")]
//        public class CUX_Segment
//        {
//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "CUX/0/0")]
//            public string CurrencyQualifier { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "CUX/0/1")]
//            public string ISOCurrency { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_LOC)]
//        [EdiSegment, EdiPath("LOC")]
//        public class LOC_Segment
//        {
//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "LOC/0/0")]
//            public string LocationQualifier { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "LOC/1/0")]
//            public string LocationId { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(3)", Path = "LOC/1/2")]
//            public string LocationResponsibleAgency { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_UNS)]
//        [EdiSegment, EdiPath("UNS")]
//        public class UNS_Segment
//        {
//            [PropertyDefinition]
//            [EdiValue("X(1)", Path = "UNS/0/0")]
//            public char? UNS { get; set; }
//        }

//        [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_UNT)]
//        [EdiSegment, EdiPath("UNT")]
//        public class UNT_Segment
//        {
//            [PropertyDefinition]
//            [EdiValue("X(1)", Path = "UNT/0")]
//            public int TrailerMessageSegmentsCount { get; set; }

//            [PropertyDefinition]
//            [EdiValue("X(14)", Path = "UNT/1")]
//            public string TrailerMessageReference { get; set; }
//        }
//    }

//    [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_DTM)]
//    [EdiElement, EdiPath("DTM/0")]
//    public class DTM
//    {
//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "DTM/0/0")]
//        public int ID { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(12)", Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
//        public DateTime DateTime { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "DTM/0/2")]
//        public int Code { get; set; }

//        public override string ToString()
//        {
//            return DateTime.ToString();
//        }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_DTM_UTCOffset)]
//    [EdiElement, EdiPath("DTM/0"), EdiCondition("ZZZ", Path = "DTM/0/0")]
//    public class UTCOffset
//    {
//        [PropertyDefinition]
//        [EdiValue("X(3)", Path = "DTM/0/0")]
//        public int? ID { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(1)", Path = "DTM/0/1")]
//        public int Hours { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "DTM/0/2")]
//        public int Code { get; set; }

//        public override string ToString()
//        {
//            return Hours.ToString();
//        }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_NAD)]
//    [EdiSegment, EdiPath("NAD")]
//    public class NAD
//    {
//        [PropertyDefinition]
//        [EdiValue("X(3)", Path = "NAD/0/0")]
//        public string PartyQualifier { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(35)", Path = "NAD/1/0")]
//        public string PartyId { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(3)", Path = "NAD/1/2")]
//        public string ResponsibleAgency { get; set; }
//    }

//    [ObjectDefinition(Name = EntityNames.EdiFact_ORDER_Order_Lines)]
//    [EdiSegment, EdiSegmentGroup("LIN", SequenceEnd = "UNS")]
//    public class LineItem
//    {
//        [PropertyDefinition]
//        [EdiValue("X(1)", Path = "LIN/0/0")]
//        public int LineNumber { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "LIN/1/0")]
//        public string Code { get; set; }

//        [PropertyDefinition]
//        public ItemNumber NumberIdentification { get; set; }

//        [PropertyDefinition]
//        public Period Period { get; set; }

//        [PropertyDefinition]
//        public List<PriceDetails> Prices { get; set; }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_ItemNumber)]
//    [EdiElement, EdiPath("LIN/2")]
//    public class ItemNumber
//    {
//        [PropertyDefinition]
//        [EdiValue("X(1)", Path = "LIN/2/0")]
//        public string Number { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "LIN/2/1")]
//        public string Type { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "LIN/2/2")]
//        public string CodeListQualifier { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "LIN/2/3")]
//        public string CodeListResponsibleAgency { get; set; }

//        public override string ToString()
//        {
//            return $"{Number} {Type} {CodeListQualifier}";
//        }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_Period)]
//    [EdiElement, EdiPath("DTM/0"), EdiCondition("324", Path = "DTM/0/0")]
//    public class Period
//    {
//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "DTM/0/0")]
//        public int ID { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(24)", Path = "DTM/0/1")]
//        public DTMPeriod Date { get; set; }

//        [PropertyDefinition]
//        [EdiValue("9(3)", Path = "DTM/0/2")]
//        public int Code { get; set; }

//        public override string ToString()
//        {
//            return $"{Date.From} | {Date.To}";
//        }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_Period_DTMPeriod)]
//    public struct DTMPeriod
//    {
//        [PropertyDefinition]
//        public readonly DateTime From;

//        [PropertyDefinition]
//        public readonly DateTime To;

//        public DTMPeriod(DateTime from, DateTime to)
//        {
//            From = from;
//            To = to;
//        }

//        public static DTMPeriod Parse(string text)
//        {
//            var textFrom = text?.Substring(0, 12);
//            var textTo = text?.Substring(12, 12);
//            return new DTMPeriod(
//                    textFrom.ParseEdiDate("yyyyMMddHHmm"),
//                    textTo.ParseEdiDate("yyyyMMddHHmm")
//                );
//        }

//        public override string ToString()
//        {
//            return $"{From:yyyyMMddHHmm}{To:yyyyMMddHHmm}";
//        }

//        public static implicit operator string(DTMPeriod value)
//        {
//            return value.ToString();
//        }

//        // With a cast operator from string --> MyClass or MyStruct 
//        // we can convert any edi component value to our custom implementation.
//        public static explicit operator DTMPeriod(string value)
//        {
//            return DTMPeriod.Parse(value);
//        }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_PriceDetails)]
//    [EdiSegment, EdiSegmentGroup("PRI")]
//    public class PriceDetails
//    {
//        [PropertyDefinition]
//        public Price Price { get; set; }

//        [PropertyDefinition]
//        public Range Range { get; set; }
//    }
    
//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_PriceDetails_Price)]
//    [EdiElement, EdiPath("PRI/0")]
//    public class Price
//    {
//        [PropertyDefinition]
//        [EdiValue("X(3)", Path = "PRI/0/0")]
//        public string Code { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(15)", Path = "PRI/0/1")]
//        public decimal? Amount { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(3)", Path = "PRI/0/2")]
//        public string Type { get; set; }
//    }

//    [ObjectDefinition (Name = EntityNames.EdiFact_ORDER_Order_PriceDetails_Range)]
//    [EdiSegment, EdiPath("RNG")]
//    public class Range
//    {
//        [PropertyDefinition]
//        [EdiValue("X(3)", Path = "RNG/0/0")]
//        public string MeasurementUnitCode { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(18)", Path = "RNG/1/0")]
//        public decimal? Minimum { get; set; }

//        [PropertyDefinition]
//        [EdiValue("X(18)", Path = "RNG/1/1")]
//        public decimal? Maximum { get; set; }
//    }
//}
