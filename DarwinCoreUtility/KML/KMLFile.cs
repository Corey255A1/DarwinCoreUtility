using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;
using DarwinCoreUtility.Utils;
namespace DarwinCoreUtility.KML
{
    [XmlRoot("kml", Namespace="http://www.opengis.net/kml/2.2")]
    public class KMLFile
    {
        [XmlElement("Document")]
        public Document Document { get; set; }

        public static void Save(KMLFile file, string path)
        {
            var namespaces = new XmlSerializerNamespaces();
            namespaces.Add("gx", "http://www.google.com/kml/ext/2.2");
            namespaces.Add("atom", "http://www.w3.org/2005/Atom");
            XmlUtils.Save(file, path, namespaces);
        }

        public static KMLFile Load(string path)
        {
            return XmlUtils.Load<KMLFile>(path);
        }

    }

    public class Document
    {
        [XmlElement("Style")]
        public List<Style> Styles { get; set; }

        [XmlElement("Placemark")]
        public List<Placemark> Placemarks { get; set; }

        [XmlElement("Folder")]
        public List<Folder> Folders { get; set; }
    }
    public class Folder
    {
        [XmlElement("name")]
        public string Name { get; set; }

        [XmlElement("Folder")]
        public List<Folder> Folders { get; set; }

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

        public PlacemarkPoint(string lat, string lon)
        {
            Coordinates = String.Format("{0},{1},0", lon, lat);
        }

        public PlacemarkPoint() { }
    }

}
