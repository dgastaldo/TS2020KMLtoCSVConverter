using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace TS2020KMLtoCSVConverter
{   
    public static class Globalvariables
    {
        public static String Destinationpath = Path.Combine(Directory.GetCurrentDirectory(), "CSV");
        public static String Csvfilenameroot = "";
        public static String Logfilepath = Directory.GetCurrentDirectory();
        public static String Logfilename = "log.txt";
        public static String Originpath = Path.Combine(Directory.GetCurrentDirectory(), "KML");
        public static String Originfilenamefilter = @"*.kml";
        public static String Verbosity = "low";
        public static String Logfile = Path.Combine(Globalvariables.Logfilepath, Globalvariables.Logfilename);

        public static String[] GlobalVariablesValues()
        {
            String[] Values = { $"This log file is saved in the path {Globalvariables.Logfilepath} with the name {Globalvariables.Logfilename}.",
                $"This log file verbosity is set to {Globalvariables.Verbosity}.",
                $"Kml files origin path = {Globalvariables.Originpath}.",
                $"Kml files name filter = {Globalvariables.Originfilenamefilter}.",
                $"Csv files destination path = {Globalvariables.Destinationpath}.",
                $"Csv files name root = {Globalvariables.Csvfilenameroot}."
            };
            return Values;
        }

        public static void RefreshDependents()
        {
            Globalvariables.Logfile = Path.Combine(Globalvariables.Logfilepath, Globalvariables.Logfilename);
        }
    }
    class Program
    {
        static void Main(String[] Args)
        {
            foreach (String Argument in Args)
            {
                String[] Splitargument = Argument.Split("=");
                switch (Splitargument[0])
                {
                    case "-D":
                    case "--pathforcsvfiles":
                        Globalvariables.Destinationpath = Splitargument[1];
                        break;
                    case "-d":
                    case "--csvfilenameroot":
                        Globalvariables.Csvfilenameroot = Splitargument[1];
                        break;
                    case "-H":
                    case "-h":
                    case "-U":
                    case "-u":
                    case "--help":
                    case "--usage":
                        Program.Writetoconsole(Program.VersionString());
                        Program.Writetoconsole(Program.UsageString());
                        Environment.Exit(0);
                        break;
                    case "-L":
                    case "--logfilepath":
                        Globalvariables.Logfilepath = Splitargument[1];
                        break;
                    case "-l":
                    case "--logfilename":
                        Globalvariables.Logfilename = Splitargument[1];
                        break;
                    case "-O":
                    case "--pathtokmlfiles":
                        Globalvariables.Originpath = Splitargument[1];
                        break;
                    case "-o":
                    case "--originfilenamefilter":
                        Globalvariables.Originfilenamefilter = Splitargument[1];
                        break;
                    case "-V":
                    case "--verbosity":
                        Globalvariables.Verbosity = Splitargument[1];
                        break;
                    case "-v":
                    case "--version":
                        Program.Writetoconsole(Program.VersionString());
                        Environment.Exit(0);
                        break;
                    default:
                        Program.Writetoconsole(Program.VersionString());
                        Program.Writetoconsole(Program.UsageString());
                        Environment.Exit(0);
                        break;
                }
            }
            Globalvariables.RefreshDependents();
            if (File.Exists(Globalvariables.Logfile))
            {
                File.Delete(Globalvariables.Logfile);
            }
            Program.Writetoconsole(Program.VersionString());
            Program.Writetolog(Program.VersionString());
            if (Globalvariables.Verbosity == "all" || Globalvariables.Verbosity == "debug")
            {
                Program.Writetoconsole(Program.UsageString());
                Program.Writetolog(Program.UsageString());
                Program.Writetoconsole(Globalvariables.GlobalVariablesValues());
                Program.Writetolog(Globalvariables.GlobalVariablesValues());
            }
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
            //Console.ReadKey();
        }

        static String[] VersionString()
        {
            String[] Version =  { "Train Simulator 2020 KML to CSV Converter",
                @"A C# Console Application by Daniele Gastaldo",
                "for Train Simulator 2020 Downloadable Content (DLC) developers.",
                "Version 1.0",
                "For further informations, feature requests and bug reports",
                "please contact the author at the e-mail adress",
                @"daniele.gastaldo.1991@outlook.it"
            };
            return Version;
        }
        static String[] UsageString()
        {
            String[] Usage = { "Application usage help",
                "An \"=\" symbol after the command means that an argument is required.",
                "-D=, --pathforcsvfiles=: csv files output path;",
                "-d=, --csvfilenameroot=: common csv filenames root;",
                "-H, -h, -U, -u, --help, --usage: print this help;",
                "-L=, --logfilepath=: log file path",
                "-l=, --logfilename=: log file name. Must be completed with the .txt extension;",
                "-O=, --pathtokmlfiles=: kml files input path;",
                "-o=, --originfilenamefilter=: string used as a filter for the original kml files to be analyzed;",
                "-V=, --verbosity=: log file verbosity. Must be one between \"low\" (default), \"debug\", \"benchmark\" or \"all\";",
                "-v, --version: print version and author contact informations.",
            };
            return Usage;
        }
        static void Writetolog(String[] content)
        {
            if (File.Exists(Globalvariables.Logfile))
                File.AppendAllLines(Globalvariables.Logfile, content);
            else
                File.WriteAllLines(Globalvariables.Logfile, content);
        }
        static void Writetoconsole(String[] content)
        {
            foreach (String line in content)
            {
                Console.WriteLine(line);
            }
        }
    }
}
