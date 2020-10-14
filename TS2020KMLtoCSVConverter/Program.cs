using System;
using System.IO;
using System.Xml.Linq;
using System.Collections.Generic;
using System.Linq;

namespace TS2020KMLtoCSVConverter
{   
    class Program
    {
        /*
         * These are the global variables that can be modified by the application of the appropriate command line argument
         */
        static String Destinationpath = Path.Combine(Directory.GetCurrentDirectory(), "CSV");
        static String Csvfilenameroot = "";
        static String Logfilepath = Directory.GetCurrentDirectory();
        static String Logfilename = "log.txt";
        static String Originpath = Path.Combine(Directory.GetCurrentDirectory(), "KML");
        static String Originfilenamefilter = @"*.kml";
        static String Verbosity = "low";
        /*
         * These are global variables that need refresh after the command line argument parsing
         */
        static String Logfile = "";
        /*
         * These are instead service variables, used to pass collections of datas from one block to another in the code.
         */
        static List<String> Directorylist = new List<String>();
        static List<String> Filelist = new List<String>();
        static void Main(String[] Args)
        {
            if (Args.Any())
            {
                foreach (String Argument in Args)
                {
                    String[] Splitargument = Argument.Split("=");
                    switch (Splitargument[0])
                    {
                        case "-D":
                        case "--pathforcsvfiles":
                            Destinationpath = Splitargument[1];
                            break;
                        case "-d":
                        case "--csvfilenameroot":
                            Csvfilenameroot = Splitargument[1];
                            break;
                        case "-H":
                        case "-h":
                        case "-U":
                        case "-u":
                        case "--help":
                        case "--usage":
                            Writetoconsole(VersionString());
                            Writetoconsole(UsageString());
                            Environment.Exit(0);
                            break;
                        case "-L":
                        case "--logfilepath":
                            Logfilepath = Splitargument[1];
                            break;
                        case "-l":
                        case "--logfilename":
                            Logfilename = Splitargument[1];
                            break;
                        case "-O":
                        case "--pathtokmlfiles":
                            Originpath = Splitargument[1];
                            break;
                        case "-o":
                        case "--originfilenamefilter":
                            Originfilenamefilter = Splitargument[1];
                            break;
                        case "-V":
                        case "--verbosity":
                            Verbosity = Splitargument[1];
                            break;
                        case "-v":
                        case "--version":
                            Writetoconsole(VersionString());
                            Environment.Exit(0);
                            break;
                        default:
                            Writetoconsole(VersionString());
                            Writetoconsole(UsageString());
                            Environment.Exit(0);
                            break;
                    }
                }
            }
            CalculateDerivedGlobalValues();
            if (File.Exists(Logfile))
            {
                File.Delete(Logfile);
            }
            Writetoconsole(VersionString());
            Writetolog(VersionString());
            if (Verbosity == "all" || Verbosity == "debug")
            {
                Writetoconsole(UsageString());
                Writetolog(UsageString());
                Writetoconsole(GlobalString());
                Writetolog(GlobalString());
            }
            EnumerateFoldersTask();
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

        static void CalculateDerivedGlobalValues()
        {
            Logfile = Path.Combine(Logfilepath, Logfilename);
            Directorylist.Add(Originpath);
        }

        static String[] GlobalString()
        {
            String[] Values = { $"This log file is saved in the path {Logfilepath} with the name {Logfilename}.",
                $"This log file verbosity is set to {Verbosity}.",
                $"Kml files origin path = {Originpath}.",
                $"Kml files name filter = {Originfilenamefilter}.",
                $"Csv files destination path = {Destinationpath}.",
                $"Csv files name root = {Csvfilenameroot}."
            };
            return Values;
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
            if (File.Exists(Logfile))
                File.AppendAllLines(Logfile, content);
            else
                File.WriteAllLines(Logfile, content);
        }
        static void Writetoconsole(String[] content)
        {
            foreach (String line in content)
            {
                Console.WriteLine(line);
            }
        }
        static void EnumerateFoldersTask()
        {
            for (int counter = 0; counter < Directorylist.Count(); counter++)
            {
                EnumerateFolders(Directorylist[counter]);
            }
        }
        static void EnumerateFolders(String path)
            {
                IEnumerable<String> newfolders = Directory.EnumerateDirectories(path);
                foreach (String newfolder in newfolders)
                {
                    Directorylist.Add(newfolder);
                }
            }
    }
}
