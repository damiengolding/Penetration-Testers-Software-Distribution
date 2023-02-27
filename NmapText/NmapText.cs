/* 
   Copyright (C) Damien Golding (dgolding, 2020-1-17 - 13:49)
   
   This is free software; you can redistribute it and/or
   modify it under the terms of the GNU Library General Public
   License as published by the Free Software Foundation; either
   version 2 of the License, or (at your option) any later version.
   
   This library is distributed in the hope that it will be useful,
   but WITHOUT ANY WARRANTY; without even the implied warranty of
   MERCHANTABILITY or FITNESS FOR A PARTICULAR PURPOSE.  See the GNU
   Library General Public License for more details.
   
   You should have received a copy of the GNU Library General Public
   License along with this software; if not, write to the Free
   Software Foundation, Inc., 59 Temple Place - Suite 330, Boston,
   MA 02111-1307, USA

  Don't use it to find and eat babies ... unless you're really REALLY hungry ;-)

*/
using GoldingsGym.ParserLib;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Text;
using System.Xml;

namespace NmapText {
    class NmapText {
        static List<HostNode> hosts = new List<HostNode>();
        static string TimeStamp = DateTime.Now.ToFileTime().ToString();
        static OptionsParser _parser;
        static string outputStem = String.Empty, outputFont = StyleSheet.MainFont, inputFile = String.Empty;
        static int Main(string[] args) {
            ParseArgs(args);
            if (_parser.Help || _parser.Error) {
                Usage();
                return (0);
            }
            else if (_parser.Version) {
                Console.WriteLine("[!] ntxt.exe version 0.1");
                return (0);
            }
            else if (String.IsNullOrEmpty(inputFile)) {
                Console.WriteLine("[-] No nmap file submitted");
                Usage();
                return (1);
            }
            int ret = 0;
            string iFile = inputFile;
            InputFileRecog ifr = new InputFileRecog(iFile);
            if (ifr.GetFileType() != Constants.SupportedInputTypes.NMAP) {
                return (1);
            }
            XmlDocument xDoc = new XmlDocument();
            try {
                xDoc.Load(iFile);
                XmlNodeList nl = xDoc.GetElementsByTagName("host");
                Debug.WriteLine("Found {0} hosts", nl.Count);
                foreach (XmlNode n in nl) {
                    ProcessHost(n);
                }
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally { }
            ExportHtml();
            ExportTsv();
            ExportCsv();
            if (!String.IsNullOrEmpty(outputStem)) {
                Console.WriteLine(outputStem);
            }
            else {
                Console.WriteLine(TimeStamp);
            }
            return (ret);
        }
        private static void Usage() {
            Console.WriteLine();
            Console.WriteLine("###########################################################################################");
            Console.WriteLine("#");
            Console.WriteLine("# Converts an nmap xml file to a textual representation, outputting to .html, .tsv and .csv");
            Console.WriteLine("#");
            Console.WriteLine("# Usage:");
            Console.WriteLine("#\tntxt --nmap=<nmap.xml> [[--font=<font family>] [--output=<output file stem>]");
            Console.WriteLine("#");
            Console.WriteLine("###########################################################################################");
            Console.WriteLine();
        }
        static private void ProcessHost(XmlNode h) {
            HostNode hn = new HostNode();
            hn.HostFromXml(h);
            hosts.Add(hn);
        }
        #region Exports
        private static void ExportHtml() {
            try {
                /* Start of HTML */
                StringBuilder html = new StringBuilder();
                html.AppendLine("<html><head>");
                html.AppendLine("<style>");
                html.AppendLine("table,th,td {font-family: " + outputFont + "; color: " + StyleSheet.FontColor + "; border: 1px solid " + StyleSheet.BorderColor + ";}");
                html.AppendLine("table {empty-cells: hide;}");
                html.AppendLine("th {text-align: centre;background-color: " + StyleSheet.BackgroundColor + ";}");
                html.AppendLine("</style>");
                html.AppendLine("<body>");

                /* Body - tables */
                foreach (HostNode hn in hosts) {
                    html.AppendLine(hn.ToHtml());
                }
                /* End of HTML */
                html.AppendLine("</body></head></html>");

                /* Write it out */
                //string oFileStem = "nmap_port_list_" + TimeStamp + ".html";
                string oFileStem = String.IsNullOrEmpty(outputStem)
                        ? "testhtml_" + TimeStamp + ".html"
                        : outputStem + ".html";
                FileInfo ofi = new FileInfo(oFileStem);
                StreamWriter sw = ofi.CreateText();
                sw.Write(html.ToString());
                sw.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Generic exception in ExportHtml()");
                Console.WriteLine(e.Message);
            }
        }
        private static void ExportTsv() {
            try {
                StringBuilder ret = new StringBuilder();
                ret.AppendLine("IP Address\tProtocol\tPort Number\tService\tState\tID Method");
                foreach (HostNode hn in hosts) {
                    ret.AppendLine(hn.ToTsv());
                }
                string oFileStem = String.IsNullOrEmpty(outputStem)
                        ? "testtsv_" + TimeStamp + ".tsv"
                        : outputStem + ".tsv";
                FileInfo ofi = new FileInfo(oFileStem);
                StreamWriter sw = ofi.CreateText();
                sw.Write(ret.ToString());
                sw.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Generic exception in ExportTsv()");
                Console.WriteLine(e.Message);
            }
        }
        private static void ExportCsv() {
            try {
                StringBuilder ret = new StringBuilder();
                ret.AppendLine("IP Address,Protocol,Port Number,Service,State,ID Method");
                foreach (HostNode hn in hosts) {
                    ret.AppendLine(hn.ToCsv());
                }
                string oFileStem = String.IsNullOrEmpty(outputStem)
                        ? "testcsv_" + TimeStamp + ".csv"
                        : outputStem + ".csv";
                FileInfo ofi = new FileInfo(oFileStem);
                StreamWriter sw = ofi.CreateText();
                sw.Write(ret.ToString());
                sw.Close();
            }
            catch (Exception e) {
                Console.WriteLine("Generic exception in ExportCsv()");
                Console.WriteLine(e.Message);
            }
        }

        #endregion Exports

        static void ParseArgs(string[] args) {
            _parser = new OptionsParser(args);
            if (_parser.Error) {
                Console.Write("[-] Bad argument: {0}", _parser.FirstBadArgument);
                string test = string.Empty;
            }
            if (_parser.HasOpt("nmap")) {
                inputFile = _parser.GetOpt("nmap");
            }
            if (_parser.HasOpt("font")) {
                outputFont = _parser.GetOpt("font");
            }
            if (_parser.HasOpt("output")) {
                outputStem = _parser.GetOpt("output");
            }
        }
    }
}
