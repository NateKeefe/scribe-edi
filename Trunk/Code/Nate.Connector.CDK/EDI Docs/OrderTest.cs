﻿using indice.Edi.Serialization;
using Scribe.Connector.Common.Reflection;
using Scribe.Connector.Common.Reflection.Actions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CDK.EDI_Docs
{
    //Symbol  Description       ExamplePicture      Component               c# result
    //9	      Numeric	        9(3)	            013	                    int v = 13;
    //X       Alphanumeric      X(20)               This is alphanumeric    string v = "This is alphanumeric";
    //V       Implicit Decimal	9(3)V9(2)           01342	                decimal v = 13.42M;

    [ObjectDefinition]
    [CreateWith]
    public class OrderTest
    {
        //#region Scribe Filters
        //[PropertyDefinition(UsedInActionOutput = false, UsedInQueryConstraint = true, UsedInQuerySelect = false)]
        //public string Folder { get; set; }

        //[PropertyDefinition(UsedInActionOutput = false, UsedInQueryConstraint = true, UsedInQuerySelect = false)]
        //public string Filename { get; set; }
        //#endregion

        [PropertyDefinition]
        public UNB Header { get; set; }

        [PropertyDefinition] //We want to be able to support multiple Messages
        public List<Order> Message { get; set; }

        [ObjectDefinition]
        [EdiSegment, EdiPath("UNB")]
        public class UNB
        {
            [PropertyDefinition]
            [EdiValue("X(4)", Mandatory = true, Path = "UNB/0")]
            public string SyntaxIdentifier { get; set; }

            [PropertyDefinition]
            [EdiValue("9(1)", Path = "UNB/0/1", Mandatory = true)]
            public int SyntaxVersion { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "UNB/1/0", Mandatory = true)]
            public string SenderId { get; set; }

            [PropertyDefinition]
            [EdiValue("X(4)", Path = "UNB/1/1", Mandatory = true)]
            public string PartnerIDCodeQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(14)", Path = "UNB/1/2", Mandatory = false)]
            public string ReverseRoutingAddress { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "UNB/2/0", Mandatory = true)]
            public string RecipientId { get; set; }

            [PropertyDefinition]
            [EdiValue("X(4)", Path = "UNB/2/1", Mandatory = true)]
            public string ParterIDCode { get; set; }

            [PropertyDefinition]
            [EdiValue("X(14)", Path = "UNB/2/2", Mandatory = false)]
            public string RoutingAddress { get; set; }

            [PropertyDefinition]
            [EdiValue("9(6)", Path = "UNB/3/0", Format = "ddMMyy", Description = "Date of Preparation")]
            [EdiValue("9(4)", Path = "UNB/3/1", Format = "HHmm", Description = "Time or Prep")]
            public DateTime DateOfPreparation { get; set; }

            [PropertyDefinition]
            [EdiValue("X(14)", Path = "UNB/4", Mandatory = true)]
            public string ControlRef { get; set; }

            [PropertyDefinition]
            [EdiValue("9(1)", Path = "UNB/8", Mandatory = false)]
            public int AckRequest { get; set; }
        }

        [ObjectDefinition(Name = "Order_Message")]
        [EdiMessage]
        public class Order
        {
            [PropertyDefinition]
            public UNH_Segment Header { get; set; }

            [PropertyDefinition]
            public BGM_Segment BGM { get; set; }

            [PropertyDefinition]
            public List<DTM_Segment> Order_DTM { get; set; }

            [PropertyDefinition]
            public FTX_Segment FTX { get; set; }

            [PropertyDefinition]
            public RFF_Segment RFF { get; set; }

            [PropertyDefinition]
            public List<NAD_BD> NAD_Buyer_Deliver { get; set; }

            [PropertyDefinition]
            public List<NAD_SS> NAD_Supplier_Seller { get; set; }

            [PropertyDefinition]
            public List<LineItem> Lines { get; set; }

            [PropertyDefinition]
            public List<PIA_Segment> PIA { get; set; }

            [PropertyDefinition]
            public List<IMD_Segment> IMD { get; set; }

            [PropertyDefinition]
            public List<QTY_Segment> QTY { get; set; }

            //Get this down further
            [PropertyDefinition]
            public UNS_Segment UNS { get; set; }

            [PropertyDefinition]
            public UNT_Segment UNT { get; set; }

            [PropertyDefinition]
            public UNZ_Segment UNZ { get; set; }
        }

        [ObjectDefinition(Name = "UNH_Segment")]
        [EdiSegment, EdiPath("UNH")]
        public class UNH_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(14)", Path = "UNH/0/0")]
            public string MessageRef { get; set; }

            [PropertyDefinition]
            [EdiValue("X(6)", Path = "UNH/1/0")]
            public string MessageType { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "UNH/1/1")]
            public string Version { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "UNH/1/2")]
            public string ReleaseNumber { get; set; }

            [PropertyDefinition]
            [EdiValue("X(2)", Path = "UNH/1/3")]
            public string ControllingAgency { get; set; }

            [PropertyDefinition]
            [EdiValue("X(6)", Path = "UNH/1/4")]
            public string AssociationAssignedCode { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "UNH/2/0")]
            public string CommonAccessRef { get; set; }
        }

        [ObjectDefinition(Name = "BGM_Segment")]
        [EdiSegment, EdiPath("BGM")]
        public class BGM_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "BGM/0/0")]
            public string MessageName { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "BGM/1/0")]
            public string DocumentNumber { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "BGM/2/0", Mandatory = false)]
            public string MessageFunction { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "BGM/3/0")]
            public string ResponseType { get; set; }

            [PropertyDefinition]
            public List<DTM_Segment> BGM_DTM { get; set; }
        }

        [ObjectDefinition(Name = "DMT_Segment")]
        [EdiSegment, EdiPath("DTM")]
        public class DTM_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "DTM/0/0")]
            public int ID { get; set; }

            [PropertyDefinition]
            [EdiValue("X(12)", Path = "DTM/0/1", Format = "yyyyMMddHHmm")]
            public DateTime DateTime { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "DTM/0/2")]
            public int Code { get; set; }

            public override string ToString()
            {
                return DateTime.ToString();
            }

            //[PropertyDefinition]
            //public UTCOffset UTCOffset { get; set; }
        }

        //[ObjectDefinition(Name = "UTCOffset1")]
        //[EdiElement, EdiPath("DTM/0"), EdiCondition("ZZZ", Path = "DTM/0/0")]
        //public class UTCOffset
        //{
        //    [PropertyDefinition]
        //    [EdiValue("X(3)", Path = "DTM/0/0")]
        //    public int? ID { get; set; }

        //    [PropertyDefinition]
        //    [EdiValue("9(1)", Path = "DTM/0/1")]
        //    public int Hours { get; set; }

        //    [PropertyDefinition]
        //    [EdiValue("9(3)", Path = "DTM/0/2")]
        //    public int Code { get; set; }

        //    public override string ToString()
        //    {
        //        return Hours.ToString();
        //    }
        //}

        [ObjectDefinition(Name = "FTX_Segment")]
        [EdiSegment, EdiPath("FTX")]
        public class FTX_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "FTX/0/0")]
            public string SubjectQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(1)", Path = "FTX/1/0")]
            public string TextFunction { get; set; }

            [PropertyDefinition]
            [EdiValue("9(4)", Path = "FTX/2/0")]
            public string TextReference { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "FTX/3/0")]
            public string TextLiteral { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "FTX/4/0")]
            public string Language { get; set; }
        }

        [ObjectDefinition(Name = "RFF_Segment")]
        [EdiSegment, EdiPath("RFF")]
        public class RFF_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "RFF/0/0")]
            public string ReferenceQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "RFF/1/0")]
            public string ReferenceNumber { get; set; }

            [PropertyDefinition]
            public DTM_Segment RFF_DTM { get; set; }
        }

        [ObjectDefinition(Name = "NAD_Buyer_Delivery")]
        [EdiSegment, EdiPath("NAD")]
        public class NAD_BD
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "NAD/0/0")]
            public string PartyQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "NAD/1/0")]
            public string PartyId { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "NAD/1/2")]
            public string ResponsibleAgency { get; set; } //9

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "NAD/2/0", Mandatory = false)]
            public string NameAndAddress { get; set; } // blank

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "NAD/3/0", Mandatory = false)]
            public string PartyName { get; set; } //cbs

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "NAD/4/0", Mandatory = false)] //im
            public string Street { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "NAD/5/0", Mandatory = false)]
            public string CityName { get; set; }

            [PropertyDefinition]
            [EdiValue("X(9)", Path = "NAD/6/0", Mandatory = false)]
            public string CountrySubEntityId { get; set; }

            [PropertyDefinition]
            [EdiValue("X(9)", Path = "NAD/7/0", Mandatory = false)]
            public string PostalCode { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "NAD/8/0", Mandatory = false)]
            public string CountryCode { get; set; }
        }

        [ObjectDefinition(Name = "NAD_Supplier_Seller")]
        [EdiSegment, EdiPath("NAD")]
        public class NAD_SS
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "NAD/0/0")]
            public string PartyQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "NAD/1/0")]
            public string PartyId { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "NAD/1/2")]
            public string ResponsibleAgency { get; set; }
        }

        [ObjectDefinition(Name = "LIN1")]
        [EdiSegment, EdiPath("LIN")]
        public class LineItem
        {
            [PropertyDefinition]
            [EdiValue("X(6)", Path = "LIN/0/0")]
            public string LineItemNumber { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "LIN/1/0")]
            public string ActionRequestCode { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "LIN/2/0")]
            public string ItemNumber { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "LIN/2/1")]
            public string ItemType { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "LIN/2/2")]
            public string CodeListQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "LIN/2/3")]
            public string CodeListResponsibleAgency { get; set; }
        }

        [ObjectDefinition(Name = "PIA_Segment")]
        [EdiSegment, EdiPath("PIA")]
        public class PIA_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "PIA/0/0")]
            public string ProductFunctionQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "PIA/1/0")]
            public string ItemNumber { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "PIA/1/1")]
            public string ItemNumberType { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "PIA/1/2")]
            public string CodeList { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "PIA/1/3")]
            public string CodeListResponsibleAgency { get; set; }
        }

        [ObjectDefinition(Name = "IMD_Segment")]
        [EdiSegment, EdiPath("IMD")]
        public class IMD_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(3)", Path = "IMD/0/0")]
            public string ItemDescriptionTypeCode { get; set; } //F

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "IMD/1/0")]
            public string ItemCharacteristicCode { get; set; }

            [PropertyDefinition]
            [EdiValue("X(17)", Path = "IMD/2/0")]
            public string ItemDescriptionId { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "IMD/2/1")]
            public string CodeListQualifier { get; set; }

            [PropertyDefinition]
            [EdiValue("X(3)", Path = "IMD/2/2")]
            public string CodeListResponsibleAgency { get; set; }

            [PropertyDefinition]
            [EdiValue("X(35)", Path = "IMD/2/3")]
            public string ItemDescription { get; set; } //Product 123
        }

        [ObjectDefinition(Name = "QTY_Segment")]
        [EdiSegment, EdiPath("QTY")]
        public class QTY_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(10)", Path = "QTY/0/0")]
            public string Details { get; set; } //21

            [PropertyDefinition]
            [EdiValue("X(10)", Path = "QTY/0/1")]
            public string Qualifier { get; set; } //56.00

            [PropertyDefinition(Description = "PCE = Piece; CMT = Centimeter; DMT = Decimeter; MTR = Meter; MTK = Square meter; MTQ = Cubic meter; LTR = Liter; GRM = Gram; KGM = Kilogram; TNE = Ton(metric)")]
            [EdiValue("X(3)", Path = "QTY/0/2")]
            public string Quantity { get; set; } //PCE

            [PropertyDefinition]
            public List<DTM_Segment> QTY_DTM { get; set; }
        }

        [ObjectDefinition(Name = "UNS_Segment")]
        [EdiSegment, EdiPath("UNS")]
        public class UNS_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(1)", Path = "UNS/0/0")]
            public char? UNS { get; set; }
        }

        [ObjectDefinition]
        [EdiSegment, EdiPath("UNT")]
        public class UNT_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(1)", Path = "UNT/0")]
            public int TrailerMessageSegmentsCount { get; set; }

            [PropertyDefinition]
            [EdiValue("X(14)", Path = "UNT/1")]
            public string TrailerMessageReference { get; set; }
        }

        [ObjectDefinition]
        [EdiSegment, EdiPath("UNZ")]
        public class UNZ_Segment
        {
            [PropertyDefinition]
            [EdiValue("X(1)", Path = "UNZ/0")]
            public int TrailerControlCount { get; set; }

            [PropertyDefinition]
            [EdiValue("X(14)", Path = "UNZ/1")]
            public string TrailerControlReference { get; set; }
        }
    }
}
