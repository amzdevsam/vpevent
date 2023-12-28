using System;
using System.Xml.Serialization;

namespace GetOrdersEvents.Function.Domain.Models.Akanea
{
    [XmlRoot(ElementName = "Emet")]
    public class Emet
    {

        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "Nom")]
        public string Nom { get; set; }
    }

    [XmlRoot(ElementName = "Dest")]
    public class Dest
    {

        [XmlElement(ElementName = "Code")]
        public string Code { get; set; }

        [XmlElement(ElementName = "Nom")]
        public string Nom { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

        [XmlText]
        public string Text { get; set; }
    }

    [XmlRoot(ElementName = "Dests")]
    public class Dests
    {

        [XmlElement(ElementName = "Dest")]
        public Dest Dest { get; set; }
    }

    [XmlRoot(ElementName = "Entete")]
    public class Entete
    {

        [XmlElement(ElementName = "TypeFlux")]
        public string TypeFlux { get; set; }

        [XmlElement(ElementName = "Emet")]
        public Emet Emet { get; set; }

        [XmlElement(ElementName = "NomFichier")]
        public string NomFichier { get; set; }

        [XmlElement(ElementName = "Dests")]
        public Dests Dests { get; set; }
    }

    [XmlRoot(ElementName = "Doc")]
    public class Doc
    {

        [XmlElement(ElementName = "Valeur")]
        public string Valeur { get; set; }

        [XmlAttribute(AttributeName = "Type")]
        public string Type { get; set; }

    }

    [XmlRoot(ElementName = "Docs")]
    public class Docs
    {

        [XmlElement(ElementName = "Doc")]
        public Doc Doc { get; set; }
    }

    [XmlRoot(ElementName = "OT")]
    public class OT
    {

        [XmlElement(ElementName = "Ref1")]
        public string Ref1 { get; set; }

        [XmlElement(ElementName = "Ref2")]
        public string Ref2 { get; set; }

        [XmlElement(ElementName = "Agence")]
        public string Agence { get; set; }

        [XmlElement(ElementName = "NomDonneur")]
        public string NomDonneur { get; set; }

        [XmlElement(ElementName = "DptDest")]
        public string DptDest { get; set; }

        [XmlElement(ElementName = "CpDest")]
        public string CpDest { get; set; }

        [XmlElement(ElementName = "TelDest")]
        public string TelDest { get; set; }

        [XmlElement(ElementName = "PortableDest")]
        public string PortableDest { get; set; }

        [XmlElement(ElementName = "DateDebutRdv")]
        public DateTime DateDebutRdv { get; set; }

        [XmlElement(ElementName = "DateFinRdv")]
        public DateTime DateFinRdv { get; set; }

        [XmlElement(ElementName = "DateCreation")]
        public DateTime DateCreation { get; set; }

        [XmlElement(ElementName = "DatePrepa")]
        public DateTime DatePrepa { get; set; }

        [XmlElement(ElementName = "DirectionDistrib")]
        public string DirectionDistrib { get; set; }

        [XmlElement(ElementName = "DateLiv")]
        public DateTime DateLiv { get; set; }

        [XmlElement(ElementName = "ChampLibre1")]
        public string ChampLibre1 { get; set; }

        [XmlElement(ElementName = "ChampLibre2")]
        public string ChampLibre2 { get; set; }

        [XmlElement(ElementName = "ChampLibre3")]
        public string ChampLibre3 { get; set; }

        [XmlElement(ElementName = "SiretDonneur")]
        public string SiretDonneur { get; set; }
    }

    [XmlRoot(ElementName = "Evenement")]
    public class Evenement
    {

        [XmlElement(ElementName = "RefEDI")]
        public string RefEDI { get; set; }

        [XmlElement(ElementName = "CodeSitu")]
        public string CodeSitu { get; set; }

        [XmlElement(ElementName = "CodeJustif")]
        public string CodeJustif { get; set; }

        [XmlElement(ElementName = "DateEvt")]
        public DateTime DateEvt { get; set; }

        [XmlElement(ElementName = "DateRdv")]
        public DateTime DateRdv { get; set; }

        [XmlElement(ElementName = "TexteInfo")]
        public string TexteInfo { get; set; }

        [XmlElement(ElementName = "UrlRecep")]
        public string UrlRecep { get; set; }

        [XmlElement(ElementName = "Docs")]
        public Docs Docs { get; set; }

        [XmlElement(ElementName = "OT")]
        public OT OT { get; set; }
    }

    [XmlRoot(ElementName = "Evenements")]
    public class Evenements
    {
        [XmlElement(ElementName = "Evenement")]
        public List<Evenement> Evenement { get; set; }
    }

    [XmlRoot(ElementName = "Flux")]
    public class Flux
    {

        [XmlElement(ElementName = "Entete")]
        public Entete Entete { get; set; }

        [XmlElement(ElementName = "Evenements")]
        public Evenements Evenements { get; set; }

        [XmlAttribute(AttributeName = "xsd")]
        public string Xsd { get; set; }

        [XmlAttribute(AttributeName = "xsi")]
        public string Xsi { get; set; }

        [XmlText]
        public string Text { get; set; }
    }



}
