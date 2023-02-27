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
using System.Windows.Forms.DataVisualization.Charting;
using System.Xml;

namespace NessusPlugins {
    class NessusPlg {
        static List<HostVuln> hosts = new List<HostVuln>();
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
                Console.WriteLine("[!] vplg.exe version 0.1");
                return (0);
            }
            else if (String.IsNullOrEmpty(inputFile)) {
                Console.WriteLine("[-] No nessus file submitted");
                Usage();
                return (1);
            }
            int ret = 0;
            //if (args.Length != 1 || !File.Exists(args[0])) {
            //    Usage();
            //    return (1);
            //}
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
            CalculateTotals();
            CreateChart();
            if (!String.IsNullOrEmpty(outputStem)) {
                Console.WriteLine(outputStem);
            }
            else {
                Console.WriteLine(TimeStamp);
            }
            return (ret);
        }

        #region Usage
        private static void Usage() {
            Console.WriteLine();
            Console.WriteLine("###########################################################################################");
            Console.WriteLine("#");
            Console.WriteLine("# Converts a nessus xml file to a visual representation, outputting a chart showing a ");
            Console.WriteLine("# plugin family overview.");
            Console.WriteLine("#");
            Console.WriteLine("# Usage:");
            Console.WriteLine("#\tvplg <nessus.xml>");
            Console.WriteLine("#");
            Console.WriteLine("###########################################################################################");
            Console.WriteLine();
        }
        #endregion Usage

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

        static private void CreateChart() {
            try {
                Chart c = new Chart();
                c.BeginInit();
                ChartArea ca = new ChartArea();
                ca.Name = "ca";
                ca.AxisX.LabelStyle.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                ca.AxisY.LabelStyle.Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));

                c.ChartAreas.Add(ca);
                c.Series.Clear();
                Series s1 = new Series("s1");
                s1.Color = Color.White;
                s1.BorderColor = Color.Black;
                Legend l1 = new Legend("l1");

                DataPoint dp0 = new DataPoint(0D, WindowsCategory);
                dp0.AxisLabel = "Windows (" + WindowsCategory + ")";
                dp0.Color = StyleSheet.NetColorWindows;
                DataPoint dp1 = new DataPoint(1D, LinuxCategory);
                dp1.AxisLabel = "Linux (" + LinuxCategory + ")";
                dp1.Color = StyleSheet.NetColorLinux;
                DataPoint dp2 = new DataPoint(2D, NetworkDevicesCategory);
                dp2.AxisLabel = "Network\r\ndevices (" + NetworkDevicesCategory + ")";
                dp2.Color = StyleSheet.NetColorNetworkDevices;
                DataPoint dp3 = new DataPoint(3D, VirtualisationCategory);
                dp3.AxisLabel = "VM (" + VirtualisationCategory + ")";
                dp3.Color = StyleSheet.NetColorVirtualisation;
                DataPoint dp4 = new DataPoint(4D, PolicyCategory);
                dp4.AxisLabel = "Policy (" + PolicyCategory + ")";
                dp4.Color = StyleSheet.NetColorPolicy;
                DataPoint dp5 = new DataPoint(5D, RDBMSCategory);
                dp5.AxisLabel = "RDBMS (" + RDBMSCategory + ")";
                dp5.Color = StyleSheet.NetColorRDBMS;
                DataPoint dp6 = new DataPoint(6D, WebApplicationCategory);
                dp6.AxisLabel = "Web Apps\r\n(" + WebApplicationCategory + ")";
                dp6.Color = StyleSheet.NetColorWebApplication;
                DataPoint dp7 = new DataPoint(7D, ServicesCategory);
                dp7.AxisLabel = "Services (" + ServicesCategory + ")";
                dp7.Color = StyleSheet.NetColorServices;
                DataPoint dp8 = new DataPoint(8D, MobileCategory);
                dp8.AxisLabel = "Mobile (" + MobileCategory + ")";
                dp8.Color = StyleSheet.NetColorMobile;
                DataPoint dp9 = new DataPoint(9D, Peer2PeerCategory);
                dp9.AxisLabel = "P2P (" + Peer2PeerCategory + ")";
                dp9.Color = StyleSheet.NetColorPeer2Peer;
                DataPoint dp10 = new DataPoint(10D, ScadaCategory);
                dp10.AxisLabel = "SCADA (" + ScadaCategory + ")";
                dp10.Color = StyleSheet.NetColorScada;
                DataPoint dp11 = new DataPoint(11D, DenialOfServiceCategory);
                dp11.AxisLabel = "Denial of\r\nService (" + DenialOfServiceCategory + ")";
                dp11.Color = StyleSheet.NetColorDenialOfService;
                DataPoint dp12 = new DataPoint(12D, RemoteControlCategory);
                dp12.AxisLabel = "Remote\r\nControl (" + RemoteControlCategory + ")";
                dp12.Color = StyleSheet.NetColorRemoteControl;
                DataPoint dp13 = new DataPoint(13D, MiscellaneousCategory);
                dp13.AxisLabel = "Misc (" + MiscellaneousCategory + ")";
                dp13.Color = StyleSheet.NetColorMiscellaneous;

                s1.Points.Add(dp0);
                s1.Points.Add(dp1);
                s1.Points.Add(dp2);
                s1.Points.Add(dp3);
                s1.Points.Add(dp4);
                s1.Points.Add(dp5);
                s1.Points.Add(dp6);
                s1.Points.Add(dp7);
                s1.Points.Add(dp8);
                s1.Points.Add(dp9);
                s1.Points.Add(dp10);
                s1.Points.Add(dp11);
                s1.Points.Add(dp12);
                s1.Points.Add(dp13);
                //Console.WriteLine("[!] Datapoints count: {0}", s1.Points.Count);
                c.Series.Add(s1);
                c.Width = 1250;
                c.Height = 750;
                c.Titles.Add("Plugins by category");
                c.Titles[0].Font = new Font(outputFont, Convert.ToInt32(StyleSheet.TitleFontSize));
                c.EndInit();
                string fn = String.IsNullOrEmpty(outputStem)
                        ? "test_" + TimeStamp + ".png"
                        : outputStem + ".png";
                c.SaveImage(fn, ChartImageFormat.Png);
                //string fn = "test_" + TimeStamp + ".png";
                //c.SaveImage(fn, ChartImageFormat.Png);
            }
            catch (Exception e) {
                Debug.WriteLine("Generic exception in CreateChart()");
                Debug.WriteLine(e.Message);
            }
        }

        static private void CalculateTotals() {
            foreach (HostVuln hv in hosts) {
                WindowsCategory += hv.WindowsCategory;
                LinuxCategory += hv.LinuxCategory;
                NetworkDevicesCategory += hv.NetworkDevicesCategory;
                PolicyCategory += hv.PolicyCategory;
                RDBMSCategory += hv.RDBMSCategory;
                WebApplicationCategory += WebApplicationCategory;
                ServicesCategory += hv.ServicesCategory;
                MobileCategory += hv.MobileCategory;
                Peer2PeerCategory += hv.Peer2PeerCategory;
                ScadaCategory += hv.ScadaCategory;
                DenialOfServiceCategory += hv.DenialOfServiceCategory;
                RemoteControlCategory += hv.RemoteControlCategory;
                MiscellaneousCategory += hv.MiscellaneousCategory;
                VirtualisationCategory += hv.VirtualisationCategory;
            }
        }

        static void ParseArgs(string[] args) {
            _parser = new OptionsParser(args);
            if (_parser.Error) {
                Console.Write("[-] Bad argument: {0}", _parser.FirstBadArgument);
                string test = string.Empty;
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

        #region Fields
        static int WindowsCategory = 0;
        static int LinuxCategory = 0;
        static int NetworkDevicesCategory = 0;
        static int PolicyCategory = 0;
        static int RDBMSCategory = 0;
        static int WebApplicationCategory = 0;
        static int ServicesCategory = 0;
        static int MobileCategory = 0;
        static int Peer2PeerCategory = 0;
        static int ScadaCategory = 0;
        static int DenialOfServiceCategory = 0;
        static int RemoteControlCategory = 0;
        static int MiscellaneousCategory = 0;
        static int VirtualisationCategory = 0;
        #endregion Fields
    }
}
