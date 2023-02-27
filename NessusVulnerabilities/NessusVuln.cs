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
using System.Drawing;
using System.IO;
using System.Text;
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace NessusVulnerabilities {
    class NessusVuln {
        static List<HostVuln> hosts = new List<HostVuln>();
        static string TimeStamp = DateTime.Now.ToFileTime().ToString();
        static OptionsParser _parser;
        static string outputStem = String.Empty, outputFont = StyleSheet.MainFont, inputFile = String.Empty;
        static bool processInfo = false;
        static int Main(string[] args) {
            ParseArgs(args);
            if (_parser.Help || _parser.Error) {
                Usage();
                return (0);
            }
            else if (_parser.Version) {
                Console.WriteLine("[!] vvln.exe version 0.1");
                return (0);
            }
            else if (String.IsNullOrEmpty(inputFile)) {
                Console.WriteLine("[-] No nessus file submitted");
                Usage();
                return (1);
            }
            int ret = 0;
            string iFile = inputFile;
            InputFileRecog ifr = new InputFileRecog(iFile);
            if (ifr.GetFileType() != Constants.SupportedInputTypes.NESSUS) {
                return (1);
            }

            XmlDocument xDoc = new XmlDocument();
            try {
                xDoc.Load(iFile);
                XmlNodeList nl = xDoc.GetElementsByTagName("Report");
                if (nl.Count != 1) {
                    return (1);
                }
                var repDoc = new XmlDocument();
                repDoc.LoadXml(nl[0].OuterXml); // report host
                nl = repDoc.GetElementsByTagName("ReportHost");
                Debug.WriteLine("Found {0} report hosts", nl.Count);
                foreach (XmlNode n in nl) {
                    ProcessHost(n);
                }
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally { }
            CreateCharts();
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
            Console.WriteLine("# Converts a nessus xml file to a visual representation, outputting charts showing");
            Console.WriteLine("# vulnerability severities per host");
            Console.WriteLine("#");
            Console.WriteLine("# Usage:");
            Console.WriteLine("#\tvvln --nessus=<nessus.xml> [[--font=<font family>] [--output=<output file stem>]");
            Console.WriteLine("#\t\t[--info=<true|false> (process informational plugins; default = false) ]]");
            Console.WriteLine("#");
            Console.WriteLine("###########################################################################################");
            Console.WriteLine();
        }
        static private void ProcessHost(XmlNode h) {
            try {
                HostVuln hv = new HostVuln();
                hv.HostFromXml(h);
                hosts.Add(hv);
            }
            catch (Exception e) {
                Debug.WriteLine("Generic exception in ProcessHost");
                Debug.WriteLine(e.Message);
            }
            finally { }
        }
        static private void CreateCharts() {
            foreach (HostVuln hv in hosts) {
                try {
                    Chart c = new Chart();
                    (c).BeginInit();
                    ChartArea ca = new ChartArea();
                    ca.Name = "ca";
                    ca.AxisX.LabelStyle.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    ca.AxisY.LabelStyle.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));

                    c.ChartAreas.Add(ca);
                    Series s1 = new Series("s1");
                    s1.Color = Color.White;
                    s1.BorderColor = Color.Black;
                    Legend l1 = new Legend("l1");

                    DataPoint dp0 = new DataPoint(0D, hv.CriticalCount);
                    dp0.AxisLabel = "Critical";
                    dp0.Color = System.Drawing.Color.Firebrick;
                    dp0.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    DataPoint dp1 = new DataPoint(1D, hv.HighCount);
                    dp1.AxisLabel = "High";
                    dp1.Color = System.Drawing.Color.OrangeRed;
                    dp1.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    DataPoint dp2 = new DataPoint(2D, hv.MediumCount);
                    dp2.AxisLabel = "Medium";
                    dp2.Color = System.Drawing.Color.DarkGreen;
                    dp2.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    DataPoint dp3 = new DataPoint(3D, hv.LowCount);
                    dp3.AxisLabel = "Low";
                    dp3.Color = System.Drawing.Color.DarkBlue;
                    dp3.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    DataPoint dp4 = new DataPoint(4D, hv.InfoCount);
                    dp4.AxisLabel = "Info";
                    dp4.Color = System.Drawing.Color.CornflowerBlue;
                    dp4.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));

                    s1.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    s1.Points.Add(dp0);
                    s1.Points.Add(dp1);
                    s1.Points.Add(dp2);
                    s1.Points.Add(dp3);
                    if (processInfo) {
                        s1.Points.Add(dp4);
                    }

                    c.Series.Add(s1);
                    c.Width = 800;
                    c.Height = 500;
                    StringBuilder title = new StringBuilder();
                    title.AppendFormat("Vulnerabilities for host {0}", hv.IpAddress.IpAddress);
                    c.Titles.Add(title.ToString());

                    StringBuilder subTitle = new StringBuilder();
                    subTitle.AppendFormat("Scan completed (plugin 19506 found): {0}", hv.Has19506);
                    c.Titles.Add(subTitle.ToString());

                    c.Titles[0].Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                    c.Titles[1].Font = new Font(outputFont, (Convert.ToInt32(StyleSheet.TitleFontSize)) - 4);

                    (c).EndInit();
                    string fn = String.IsNullOrEmpty(outputStem)
                        ? hv.IpAddress.IpAddress + "_" + TimeStamp + ".png"
                        : outputStem + "_" + hv.IpAddress.IpAddress + ".png";
                    c.SaveImage(fn, ChartImageFormat.Png);
                }
                catch (Exception e) {
                    Debug.WriteLine("Generic exception in CreateCharts()");
                    Debug.WriteLine(e.Message);
                }
            }
        }
        static void ParseArgs(string[] args) {
            _parser = new OptionsParser(args);
            if (_parser.Error) {
                Console.Write("[-] Bad argument: {0}", _parser.FirstBadArgument);
                string test = string.Empty;
            }
            if (_parser.HasOpt("info")) {
                if (_parser.GetOpt("info").ToLower() == "true") {
                    processInfo = true;
                }
                else {
                    processInfo = false;
                }
            }
            if (_parser.HasOpt("nessus")) {
                inputFile = _parser.GetOpt("nessus");
            }
            if (_parser.HasOpt("font")) {
                outputFont = _parser.GetOpt("font");
            }
            if (_parser.HasOpt("output")) {
                outputStem = _parser.GetOpt("output");
            }
        }

    }
    #region Fields
    #endregion Fields
}

