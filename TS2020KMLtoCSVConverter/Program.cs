using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace TS2020KMLtoCSVConverter
{   
    class Program
    {
        static void Main(string[] args)
        {
            //String OriginPath = @"C:\Users\afr\OneDrive\PROGETTO NODO DI TORINO\KML";
            //String KmlFileName = "Località servite.kml";
            //String Destinationpath = @"C:\Users\afr\OneDrive\PROGETTO NODO DI TORINO\CSV";
            //var KmlFile = Path.Combine(OriginPath, KmlFileName);
            //Console.WriteLine($"Working on file {KmlFile}.");
            //XElement KmlContent = XElement.Load(KmlFile);
            //XNamespace KmlNameSpace = KmlContent.Attribute(XNamespace.Xmlns + "kml").Value;
            //IEnumerable<XElement> PointPlacemarks = from placemark in KmlContent.Element(KmlNameSpace + "Document").Element(KmlNameSpace + "Folder").Elements(KmlNameSpace + "Placemark")
            //                                        where placemark.Elements(KmlNameSpace + "Point").Any()
            //                                        select placemark;
            //IEnumerable<XElement> LinearPlacemarks = from placemark in KmlContent.Element(KmlNameSpace + "Document").Element(KmlNameSpace + "Folder").Elements(KmlNameSpace + "Placemark")
            //                                         where placemark.Elements(KmlNameSpace + "LineString").Any()
            //                                         select placemark;
            //IEnumerable<XElement> PolygonPlacemarks = from placemark in KmlContent.Element(KmlNameSpace + "Document").Element(KmlNameSpace + "Folder").Elements(KmlNameSpace + "Placemark")
            //                                         where placemark.Elements(KmlNameSpace + "Polygon").Any()
            //                                         select placemark;
            //if (PointPlacemarks.Any())
            //{
            //    Console.WriteLine("Elenco marker puntuali");
            //    foreach (XElement element in PointPlacemarks)
            //    {
            //        Console.WriteLine(element.Element(KmlNameSpace + "name").Value);
            //    }
            //}
            //if (LinearPlacemarks.Any())
            //{
            //    Console.WriteLine("Elenco marker lineari");
            //    foreach (XElement element in LinearPlacemarks)
            //    {
            //        Console.WriteLine(element.Element(KmlNameSpace + "name").Value);
            //    }
            //}
            //if (PolygonPlacemarks.Any())
            //{
            //    Console.WriteLine("Elenco marker poligonali");
            //    foreach (XElement element in PolygonPlacemarks)
            //    {
            //        Console.WriteLine(element.Element(KmlNameSpace + "name").Value);
            //    }
            //}
            //List<String> Pointnames = new List<string>();
            //List<String> Pointcoordinates = new List<string>();
            //List<String> Seriesnames = new List<string>();
            //List<String> Seriescoordinates = new List<string>();
            //List<String> Polygonnames = new List<string>();
            //List<String> Polygoncoordinates = new List<string>();
            //foreach (XElement Placemark in Placemarks)
            //{
            //    String Placemarkname = Placemark.Element($"{Xmlns}name").Value.ToString();
            //    Console.WriteLine($"Working on placemark {Placemarkname}.");
            //    if (Placemark.Elements($"{Xmlns}Point").Any())
            //    {
            //        Console.WriteLine($"Placemark {Placemarkname} is a Point");
            //        continue;
            //    }
            //    else if (Placemark.Elements($"{Xmlns}Polygon").Any())
            //    {
            //        Console.WriteLine($"Placemark {Placemarkname} is a Polygon");
            //        continue;
            //    }
            //    else if (Placemark.Elements($"{Xmlns}LineString").Any())
            //    {
            //        Console.WriteLine($"Placemark {Placemarkname} is a Series");
            //        continue;
            //    }
            //    else
            //    {
            //        Console.WriteLine($"Placemark {Placemarkname} .was not recognized. Program will stop NOW!");
            //        continue;
            //    }
            //}
            Console.ReadKey();
        }
    }
}
