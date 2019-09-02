﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace DarwinCoreUtility.KML
{
    [XmlRoot("kml")]
    public class KMLFile
    {
        [XmlElement("Document")]
        public Document Document { get; set; }
    }

    public class Document
    {
        [XmlElement("Style")]
        public List<Style> Styles { get; set; }

        [XmlElement("Placemark")]
        public List<Placemark> Placemarks { get; set; }
    }

    public class Style
    {
        [XmlAttribute("id")]
        public string ID { get; set; }

        [XmlElement("IconStyle")]
        public IconStyle IconStyle { get; set; }
    }
    public class IconStyle
    {
        [XmlElement("color")]
        public string Color { get; set; }

        [XmlElement("colorMode")]
        public string ColorMode { get; set; }

        [XmlElement("href")]
        public string Href { get; set; }
    }

    public class Placemark
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("styleUrl")]
        public string StyleURL { get; set; }

        [XmlElement("description")]
        public string Description { get; set; }

        [XmlElement("Point")]
        public PlacemarkPoint Point { get; set; }

    }
    public class PlacemarkPoint
    {
        [XmlElement("coordinates")]
        public string Coordinates { get; set; }
    }

}
