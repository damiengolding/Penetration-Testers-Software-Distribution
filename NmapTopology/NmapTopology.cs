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
using Newtonsoft.Json;
using GoldingsGym.ParserLib;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading;
using System.Xml;

namespace NmapTopology {
    partial class NmapTopology {
        static List<GoldingsGym.ParserLib.IPAddress> addresses = new List<GoldingsGym.ParserLib.IPAddress>();
        static string TimeStamp = DateTime.Now.ToFileTime().ToString();
        static string nmapFile = String.Empty,
            hshFile = String.Empty,
            axfrFile = String.Empty,
            outputStem = String.Empty,
            glApi = String.Empty,
            subnetString = String.Empty,
            outputFont = StyleSheet.MainFont;
        static bool _hasSubnets = false;
        static bool _hasIpGeoLocInfo = false;
        static bool _hasHsh = false;
        static Hashtable hshValues;
        static OptionsParser _parser;
        static bool _verbose = false;

        static int Main(string[] args) {
            try {
                if (args.Length == 0) {
                    Usage();
                    return (1);
                }
                ParseArgs(args);
                if (_parser.Help || _parser.Error) {
                    Usage();
                    return (0);
                }
                else if (_parser.Version) {
                    Console.WriteLine("[!] ntop.exe version 0.1");
                    return (0);
                }
                else if (String.IsNullOrEmpty(nmapFile)) {
                    Console.WriteLine("[-] No nmap file submitted");
                    Usage();
                    return (1);
                }
                InputFileRecog ifr = new InputFileRecog(nmapFile);
                if (ifr.GetFileType() != Constants.SupportedInputTypes.NMAP) {
                    Console.WriteLine("[-] Not an nmap file or file does not exist: {0}", nmapFile);
                    return (1);
                }
                if (_verbose) {
                    Console.WriteLine("[!] in Verbose output mode");
                }
                XmlDocument xDoc = new XmlDocument();
                try {
                    xDoc.Load(nmapFile);
                    XmlNodeList nl = xDoc.GetElementsByTagName("host");
                    foreach (XmlNode n in nl) {
                        ProcessHost(n);
                    }
                }
                catch (Exception e) {
                    Console.WriteLine("[-] Execption in main (XML Load): {0}", e.Message);
                }
                finally { }
                GetHighSev();
                GetAXFR();
                ExecuteDirg();
                ExecuteUdirg();
                WriteIpGeoLocInfo();
                WriteStats();
                if (String.IsNullOrEmpty(outputStem)) {
                    Console.WriteLine(TimeStamp);
                }
                else {
                    Console.WriteLine(outputStem);
                }
                return (0);
            }
            catch (Exception ex) {
                Console.WriteLine("[-] Exception caught in main: {0}\r\n{1}", ex.StackTrace, ex.Message);
            }
            return (1);
        }
        private static void Usage() {
            Console.WriteLine();
            Console.WriteLine("###########################################################################################");
            Console.WriteLine("#");
            Console.WriteLine("# Converts an nmap xml file to visual network representations.");
            Console.WriteLine("#\tThe ***_model_***.png image shows hosts grouped by network");
            Console.WriteLine("#\tThe ***_layout_***.png image shows a directed graph depicting the layout of the network");
            Console.WriteLine("#");
            Console.WriteLine("#\tThe two .dot file artefacts are the Dot (from GraphViz by AT&T) run control files used");
            Console.WriteLine("#\tby dot.exe to generate the images. They can be modified and the files regenerated afterwards");
            Console.WriteLine("#\tby calling dot.exe <*****.dot> -Tpng -o <imagename.png>");
            Console.WriteLine("#");
            Console.WriteLine("# Usage:");
            Console.WriteLine("#\tntop --nmap=<nmap.xml> [[--hsh=<highest_sev.txt>] [--axfr=<axfr.txt>] [--glapi=<ipinfodb api key>] [--subnet=<subnet>] [--output=<output file stem>] [--font=<font family>]]");
            Console.WriteLine("#");
            Console.WriteLine("###########################################################################################");
            Console.WriteLine();
        }
        static private void ProcessHost(XmlNode h) {
            try {
                GoldingsGym.ParserLib.IPAddress ipAddr;
                string ip = "", dns = "", os = "";
                var hosts = new XmlDocument();
                hosts.LoadXml(h.OuterXml);
                XmlNodeList addlist = hosts.GetElementsByTagName("address");
                foreach (XmlNode an in addlist) {
                    if (an.Attributes.GetNamedItem("addrtype").Value.Equals("ipv4")) {
                        ip = an.Attributes.GetNamedItem("addr").Value;
                    }
                }
                XmlNodeList hnlist = hosts.GetElementsByTagName("hostname");
                foreach (XmlNode hn in hnlist) {
                    if (hn.Attributes.GetNamedItem("type").Value.ToLower().Equals("ptr")) {
                        dns = hn.Attributes.GetNamedItem("name").Value;
                    }
                }

                ipAddr = new GoldingsGym.ParserLib.IPAddress(ip, dns);

                foreach (XmlNode ci in hosts.GetElementsByTagName("osmatch")) {
                    if (!String.IsNullOrEmpty(ci.Attributes.GetNamedItem("name").Value)) {
                        os = ci.Attributes.GetNamedItem("name").Value.Split(',')[0];
                        break;
                    }
                }

                /// Handle scripts - add to the recursive function below
                foreach (XmlNode sn in hosts.GetElementsByTagName("script")) {
                    string scriptName = sn.Attributes.GetNamedItem("id").Value;
                    if (scriptName.Equals("ip-forwarding")) {
                        if (sn.Attributes.GetNamedItem("output").Value.Contains("The host has ip forwarding enabled")) {
                            ipAddr.HasIpForwarding = true;
                        }
                    }
                    else if (scriptName.Equals("ip-geolocation-ipinfodb")) {
                        _hasIpGeoLocInfo = true;
                        string geoInf = sn.Attributes.GetNamedItem("output").Value;
                        geoInf = geoInf.Trim();
                        string _city = geoInf.Split('\n')[2].Split(':')[1].Trim();
                        ipAddr.City = _city;
                        string _latLong = geoInf.Split('\n')[1].Split(':')[1].Trim();
                        string _lat = _latLong.Split(',')[0];
                        string _long = _latLong.Split(',')[1];
                        ipAddr.Latitude = _lat;
                        ipAddr.Longitude = _long;
                        ipAddr.HasGeoLocation = true;
                    }
                }
                ipAddr.FromNmap = true;
                ipAddr.OSGuess = os;
                addresses.Add(ipAddr);
            }
            catch (Exception e) {
                Console.WriteLine("[!] Exception in process host: {0}", e.Message);
            }
        }
        static private void ExecuteDirg() {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine("strict digraph nmap{\r\nrankdir = TB;");
            sb.AppendFormat("graph [fontname=\"{0}\",fontsize=\"{1}\"];",
                outputFont,//StyleSheet.MainFont,
                StyleSheet.TitleFontSize
                );
            sb.AppendFormat("\r\nnode [fontname=\"{0}\",fontsize=\"{1}\"];",
                outputFont,//StyleSheet.MainFont,
                StyleSheet.MainFontSize
                );
            sb.AppendFormat("\r\ncompound = true; labelloc = \"t\";label=\"Nmap network model\";\r\n");

            sb.AppendLine(GetDirgBody());

            sb.Append("}//End of digraph");
            string oFileStem = "";

            if (String.IsNullOrEmpty(outputStem)) {
                oFileStem = "nmap_network_model_" + TimeStamp + ".dot";
            }
            else {
                oFileStem = outputStem + "_network.dot";
            }
            try {
                FileInfo ofi = new FileInfo(oFileStem);
                StreamWriter sw = ofi.CreateText();
                sw.Write(sb.ToString());
                sw.Close();
            }
            catch (Exception e) {
                Console.WriteLine("General exception in ExecuteDirg");
                Console.WriteLine(e.ToString());
            }
            finally { }

            try {
                string pngFileName = oFileStem.Replace(".dot", ".png");
                StringBuilder args = new StringBuilder();
                args.AppendFormat("{0} -o {1} {2}", "-Tpng", pngFileName, oFileStem);
                Debug.WriteLine("Dot args: {0}", args);
                Process p = new Process();
                p.StartInfo.FileName = "dot.exe";
                p.StartInfo.Arguments = args.ToString();
                p.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
            }
            catch (Exception e) {
                Debug.WriteLine("General exception in ExecuteDirg()");
                Debug.WriteLine(e.ToString());
            }
            finally { }
        }
        static string GetDirgBody() {
            StringBuilder sb = new StringBuilder();
            List<string> rtoa = new List<string>();
            List<string> atob = new List<string>();
            List<string> btoc = new List<string>();
            List<string> ctol = new List<string>();

            foreach (GoldingsGym.ParserLib.IPAddress i in addresses) {
                //for (int j = 0; j < addresses.Count; ++j) {
                //    GoldingsGym.ParserLib.IPAddress i = addresses[j];
                //    addresses[j] = GetIpGeoLocInfo(addresses[j]);
                string borderStyle = "solid";

                if (!String.IsNullOrEmpty(axfrFile)) {
                    if (i.FromAXFR && i.FromNmap) {
                        borderStyle = "solid";
                    }
                    else if (i.FromNmap && !i.FromAXFR) {
                        borderStyle = "dotted";
                    }
                    else if (!i.FromNmap && i.FromAXFR) {
                        borderStyle = "dashed";
                    }
                    else {
                        borderStyle = "solid";
                    }
                }
                if (_hasSubnets) {
                    if (IpAddressIsInRange(i.IpAddress)) {
                        string hostDotString = i.CClassToLeafDotEntry().Replace("%COLOUR%", GetSevColour(i.IpAddress));
                        hostDotString = hostDotString.Replace("%STYLE%", borderStyle);
                        hostDotString = hostDotString.Replace("%FONT%", outputFont);
                        ctol.Add(hostDotString);
                        if (!rtoa.Contains(i.RootToAClassDotEntry())) {
                            rtoa.Add(i.RootToAClassDotEntry().Replace("%FONT%", outputFont));
                        }
                        if (!atob.Contains(i.AClassToBClassDotEntry())) {
                            atob.Add(i.AClassToBClassDotEntry().Replace("%FONT%", outputFont));
                        }
                        if (!btoc.Contains(i.BClassToCClassDotEntry().Replace("%FONT%", outputFont))) {
                            btoc.Add(i.BClassToCClassDotEntry());
                        }
                    }
                }
                else {
                    string hostDotString = i.CClassToLeafDotEntry().Replace("%COLOUR%", GetSevColour(i.IpAddress));
                    hostDotString = hostDotString.Replace("%STYLE%", borderStyle);
                    hostDotString = hostDotString.Replace("%FONT%", outputFont);
                    ctol.Add(hostDotString);
                    if (!rtoa.Contains(i.RootToAClassDotEntry())) {
                        rtoa.Add(i.RootToAClassDotEntry().Replace("%FONT%", outputFont));
                    }
                    if (!atob.Contains(i.AClassToBClassDotEntry())) {
                        atob.Add(i.AClassToBClassDotEntry().Replace("%FONT%", outputFont));
                    }
                    if (!btoc.Contains(i.BClassToCClassDotEntry())) {
                        btoc.Add(i.BClassToCClassDotEntry().Replace("%FONT%", outputFont));
                    }
                }
            }
            foreach (string s in rtoa) {
                sb.AppendLine(s);
            }
            foreach (string s in atob) {
                sb.AppendLine(s);
            }
            foreach (string s in btoc) {
                sb.AppendLine(s);
            }
            foreach (string s in ctol) {
                sb.AppendLine(s);
            }
            return (sb.ToString());
        }
        private static List<string> GetSubnets() {
            List<string> ret;// = new List<string>();
            if (string.IsNullOrEmpty(subnetString)) {
                ret = new List<string>();
                foreach (GoldingsGym.ParserLib.IPAddress i in addresses) {
                    string sn = i.ParentSubnet().Trim('*');
                    if (!ret.Contains(sn)) {
                        //Console.WriteLine("[!] Adding subnet {0} from all", sn);
                        ret.Add(sn);
                    }
                }
            }
            else {
                ret = new List<string>(subnetString.Split(','));
            }
            return (ret);
        }
        private static List<GoldingsGym.ParserLib.IPAddress> GetInScopeAddresses(string subnet) {
            //Console.WriteLine("[!] Testing subnet: {0}", subnet);
            List<GoldingsGym.ParserLib.IPAddress> ret = new List<GoldingsGym.ParserLib.IPAddress>();
            if (String.IsNullOrEmpty(subnet)) {
                ret = addresses;
            }
            else {
                foreach (GoldingsGym.ParserLib.IPAddress i in addresses) {
                    if (i.IpAddress.StartsWith(subnet)) {
                        //Console.WriteLine("[!] Adding {0} for subnet {1}", i.IpAddress, subnet);
                        ret.Add(i);
                    }
                }
            }
            return (ret);
        }
        private static void ExecuteUdirg() {
            try {
                /// Build graph header
                StringBuilder sb = new StringBuilder();
                sb.AppendLine("strict digraph g{rankdir = TB;graph[fontname=\"" + outputFont //StyleSheet.MainFont
                    + "\",fontsize=" + StyleSheet.TitleFontSize
                    + "];node[fontname=\"" + outputFont //StyleSheet.MainFont
                    + "\",fontsize=\"" + StyleSheet.MainFontSize
                    + "\"];edge[fontname=\"" + outputFont //StyleSheet.MainFont
                    + "\",fontsize=\"" + StyleSheet.MainFontSize
                    + "\"];compound = true; labelloc = \"t\"; label=\"Nmap layout model\";\r\n");

                /// Get subnets
                /// Build subgraph header
                int c1 = 0;
                int c2 = 0;
                //Console.WriteLine("[!] Subnets size: {0}", GetSubnets().Count);
                foreach (string sn in GetSubnets()) {
                    sb.AppendLine("subgraph cluster" + c1.ToString() + "{ fontsize=" + StyleSheet.MainFontSize + ";label=\"" + sn + "*\"");
                    /// Add in each node
                    string prevNode = String.Empty;
                    foreach (GoldingsGym.ParserLib.IPAddress ip in GetInScopeAddresses(sn)) {
                        string borderStyle = "solid";
                        string sevColor = GetSevColour(ip.IpAddress);
                        string tag = "host" + c2.ToString();
                        GoldingsGym.ParserLib.IPAddress ipa = GetIpGeoLocInfo(ip);
                        if (!String.IsNullOrEmpty(axfrFile)) {
                            if (ipa.FromAXFR && ipa.FromNmap) {
                                borderStyle = "solid";
                            }
                            else if (ipa.FromNmap && !ipa.FromAXFR) {
                                borderStyle = "dotted";
                            }
                            else if (!ipa.FromNmap && ipa.FromAXFR) {
                                borderStyle = "dashed";
                            }
                        }

                        sb.AppendFormat("{0} [shape=box,fontcolor={1},color={1},fontname=\"{2}\",style={3},label=\"{4}\\l({5})\\lOS Guess: {6}\\lIP Forwarding: {7}\\l"
                        , tag
                        , sevColor
                        , outputFont
                        , borderStyle
                        , ipa.IpAddress
                        , String.IsNullOrEmpty(ipa.DNSName) ? ipa.IpAddress : ipa.DNSName
                        , String.IsNullOrEmpty(ipa.OSGuess) ? "No OS Guess" : ipa.OSGuess
                        , ip.HasIpForwarding
                        );

                        if (ipa.HasGeoLocation) {
                            sb.AppendFormat("City: {0}\\lLatitude: {1}\\lLongitude: {2}\\l", ipa.City, ipa.Latitude, ipa.Longitude);
                        }
                        sb.AppendFormat("\"];\r\n");
                        if (String.IsNullOrEmpty(prevNode)) {
                            prevNode = tag;
                        }
                        else {
                            sb.AppendFormat("{0}->{1} [style=invis];\r\n", prevNode, tag);
                            prevNode = tag;
                        }

                        ++c2;
                    }
                    sb.AppendLine("\r\n}//End of subgraph\r\n");
                    ++c1;
                }

                /// Build graph footer
                sb.Append("\r\n}//End of graph");

                /// Create runcontrol
                string oFileStem = "";
                if (String.IsNullOrEmpty(outputStem)) {
                    oFileStem = "nmap_network_layout_" + TimeStamp + ".dot";
                }
                else {
                    oFileStem = outputStem + "_layout.dot";
                }
                FileInfo ofi = new FileInfo(oFileStem);
                StreamWriter sw = ofi.CreateText();
                sw.Write(sb.ToString());
                sw.Close();

                /// Run dot
                string pngFileName = oFileStem.Replace(".dot", ".png");
                StringBuilder args = new StringBuilder();
                args.AppendFormat("{0} -o {1} {2}", "-Tpng", pngFileName, oFileStem);
                Debug.WriteLine("Dot args: {0}", args);
                Process p = new Process();
                p.StartInfo.FileName = "dot.exe";
                p.StartInfo.Arguments = args.ToString();
                p.StartInfo.WorkingDirectory = Directory.GetCurrentDirectory();
                p.StartInfo.CreateNoWindow = true;
                p.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
                p.Start();
            }
            catch (Exception ex) {
                Console.Write("[-] Exception caught: {0}", ex.Message);
            }

        }
        private static bool IpAddressIsInRange(string IpAddress) {
            if (!_hasSubnets) {
                return (true);
            }
            string[] subnets = subnetString.Split(',');
            foreach (string s in subnets) {
                if (IpAddress.StartsWith(s)) {
                    return (true);
                }
            }
            return (false);
        }
        static void GetAXFR() {
            if (String.IsNullOrEmpty(axfrFile)) {
                return;
            }
            try {
                InputFileRecog ifr = new InputFileRecog(axfrFile);
                if (ifr.GetFileType() == Constants.SupportedInputTypes.AXFR_NS_LIN) {
                    ///Console.WriteLine("[!] Linux format AXFR");
                    string[] lines = File.ReadAllLines(axfrFile);
                    string serverAddress = lines[0].Split(':')[1].Trim();
                    if (serverAddress.Split('.').Length < 4) {
                        Console.WriteLine("[!] Ill-formed IP address: {0}", serverAddress);
                    }
                    string aClass = serverAddress.Split('.')[0].Trim();
                    for (int i = 0; i < lines.Length - 1; ++i) {
                        string name, address;
                        string l1 = lines[i].ToLower();
                        string l2 = lines[i + 1].ToLower();
                        if (l1.StartsWith("name:") && l2.StartsWith("address:")) {
                            name = l1.Split(':')[1].Trim();
                            address = l2.Split(':')[1].Trim();
                            if (!address.StartsWith(aClass)) {
                                continue;
                            }
                            else {
                                GoldingsGym.ParserLib.IPAddress ipAddr = new GoldingsGym.ParserLib.IPAddress(address, name);
                                if (address == serverAddress) {
                                    ipAddr.IsNameServer = true;
                                }
                                ipAddr.FromAXFR = true;
                                bool found = false;
                                foreach (GoldingsGym.ParserLib.IPAddress ipa in addresses) {
                                    if (ipa.IpAddress.Equals(address)) {
                                        ipa.FromAXFR = true;
                                        found = true;
                                        break;
                                    }
                                }
                                if (!found) {
                                    addresses.Add(ipAddr);
                                }
                            }
                        }
                    }
                }
                else if (ifr.GetFileType() == Constants.SupportedInputTypes.AXFR_NS_WIN) {
                    ///Console.WriteLine("[!] Windows format AXFR");
                    string[] lines = File.ReadAllLines(axfrFile);
                    string serverAddress = lines[0].Trim('[').Trim(']');
                    string domainPrefix = lines[1].Trim();
                    domainPrefix = SupportFunctions.CollapseSpaces(domainPrefix);
                    domainPrefix = domainPrefix.Split(' ')[0].Trim();
                    foreach (string l1 in lines) {
                        string l = SupportFunctions.CollapseSpaces(l1);
                        l = l.Trim();
                        string[] parts = l.Split(' ');
                        if (parts.Length != 3) {
                            continue;
                        }
                        if (parts[1].ToUpper() != "A") {
                            continue;
                        }
                        if (parts[0].ToUpper().EndsWith("DNSZONES")) {
                            continue;
                        }
                        if (parts[0].Trim().ToUpper() == domainPrefix.ToUpper()) {
                            continue;
                        }
                        if (!GoldingsGym.ParserLib.IPAddress.ArePeersBClass(new GoldingsGym.ParserLib.IPAddress(serverAddress), new GoldingsGym.ParserLib.IPAddress(parts[2]))) {
                            continue;
                        }
                        string name = parts[0].Trim().ToUpper();
                        string address = parts[2].Trim().ToUpper();
                        GoldingsGym.ParserLib.IPAddress ipAddr = new GoldingsGym.ParserLib.IPAddress(address, name);
                        if (address == serverAddress) {
                            ipAddr.IsNameServer = true;
                        }
                        ipAddr.FromAXFR = true;
                        bool found = false;
                        foreach (GoldingsGym.ParserLib.IPAddress ipa in addresses) {
                            if (ipa.IpAddress.Equals(address)) {
                                ipa.FromAXFR = true;
                                found = true;
                                break;
                            }
                        }
                        if (!found) {
                            addresses.Add(ipAddr);
                        }
                    }
                }
                else {
                    Console.WriteLine("[!] Input file type is not a recognised Zone Transfer (AXFR) file");
                }
            }
            catch (Exception ex) {
                Console.WriteLine("[-] Exception caught in GetAXFR: {0}", ex.Message);
            }
        }
        static void GetHighSev() {
            hshValues = new Hashtable();
            if (String.IsNullOrEmpty(hshFile) || !File.Exists(hshFile)) {
                return;
            }
            _hasHsh = true;
            try {
                string[] lines = File.ReadAllLines(hshFile);
                foreach (string l in lines) {
                    if (l.StartsWith("#")) {
                        continue;
                    }
                    string ip = l.Split('=')[0].Trim();
                    string sev = l.Split('=')[1].Trim();
                    hshValues.Add(ip, sev);
                }
            }
            catch (Exception ex) {
                Console.WriteLine("[-] Exception loading high sevs: {0}", ex.Message);
            }
        }
        static string GetSevColour(string ip) {
            try {
                string ret = "black";//_hasHsh ? "blue" : "black";
                string test = "UNKNOWN";
                if (!hshValues.ContainsKey(ip)) {
                    return (ret);
                }
                else {
                    foreach (DictionaryEntry de in hshValues) {
                        if (de.Key.ToString() == ip) {
                            test = de.Value.ToString();
                            break;
                        }
                    }
                }
                if (test.ToUpper() == "CRITICAL") {
                    //ret = "firebrick1";
                    ret = StyleSheet.NetColorCritical.ToString().ToLower().Replace("color [", "").Trim(']');
                }
                else if (test.ToUpper() == "HIGH") {
                    //ret = "orange1";
                    ret = StyleSheet.NetColorHigh.ToString().ToLower().Replace("color [", "").Trim(']');
                }
                else if (test.ToUpper() == "MEDIUM") {
                    //ret = "darkgreen";
                    ret = StyleSheet.NetColorMedium.ToString().ToLower().Replace("color [", "").Trim(']');
                }
                else if (test.ToUpper() == "LOW") {
                    //ret = "navy";
                    ret = StyleSheet.NetColorLow.ToString().ToLower().Replace("color [", "").Trim(']');
                }
                else if (test.ToUpper() == "INFO") {
                    //ret = "navy";
                    ret = StyleSheet.NetColorInfo.ToString().ToLower().Replace("color [", "").Trim(']');
                }
                return (ret);
            }
            catch (Exception ex) {
                Console.WriteLine("[!] Exception in GetSevColour(): {0}", ex.Message);
            }
            return ("black");
        }
        static GoldingsGym.ParserLib.IPAddress GetIpGeoLocInfo(GoldingsGym.ParserLib.IPAddress addr) {
            //Console.WriteLine("[!] IP Address for GeoLocation: {0}", addr.IpAddress);
            ///Todo: combine these after proof
            if (String.IsNullOrEmpty(glApi)) {
                return (addr);
            }
            if (GoldingsGym.ParserLib.IPAddress.IsReservedAddress(addr.IpAddress)) {
                Console.WriteLine("[!] {0} is a reserved address", addr.IpAddress);
                return (addr);
            }
            //Console.WriteLine("[!] GL API Key: {0}", glApi);
            _hasIpGeoLocInfo = true;
            GoldingsGym.ParserLib.IPAddress ret = addr;
            if (glApi.Length < 64) { // IP2Location API Key
                Console.WriteLine("[!] IP2Location DB");
                try {
                    Ip2Location ip2l;
                    string url = "http://api.ip2location.com/v2/?ip=" + addr.IpAddress + "&key=" + glApi + "&package=WS24&addon=continent,country,region,city,geotargeting,country_groupings,time_zone_info";
                    using (WebClient wc = new WebClient()) {
                        string resp = wc.DownloadString(url);
                        if (resp.Contains("Insufficient credits to use the API")) {
                            Console.WriteLine("[!] API credits expired for key: {0}", glApi);
                            ret.HasGeoLocation = false;
                            ret.City = "Unknown city";
                            ret.Longitude = "Unknown long";
                            ret.Latitude = "Unknown lat";
                            return (ret);
                        }
                        //ip2l = JsonConvert.DeserializeObject<Ip2Location>(resp);
                        ip2l = Ip2Location.FromString(resp);
                        ret.HasGeoLocation = true;
                        ret.Latitude = ip2l.latitude;
                        ret.Longitude = ip2l.longitude;
                        ret.City = ip2l.city_name;
                        ret.TimeZone = ip2l.time_zone;
                        return (ret);
                    }
                }
                catch (Exception ex) {
                    Console.WriteLine("[!] Exception caught in GetIpGeoLocInfo: {0}", ex.Message);
                }
            }
            else { // IPInfoDb
                Console.WriteLine("[!] IPInfoDB");
                Thread.Sleep(1000);
                string url = "http://api.ipinfodb.com/v3/ip-city/?key=" + glApi + "&ip=" + addr.IpAddress + "&format=json";
                using (WebClient wc = new WebClient()) {
                    string respStr = wc.DownloadString(url);
                    //var respObj = JsonConvert.DeserializeObject<IPInfoDb>(respStr);
                    var respObj = IPInfoDb.FromString(respStr);
                    if (respObj.statusCode.ToUpper() != "OK") {
                        Console.WriteLine("[!] IPInfoDb query failed for IP Address {0}, with message: {1}", addr.IpAddress, respObj.statusMessage);
                        ret.HasGeoLocation = false;
                        ret.City = "Unknown city";
                        ret.Longitude = "Unknown long";
                        ret.Latitude = "Unknown lat";
                        return (ret);
                    }
                    ret.HasGeoLocation = true;
                    ret.Latitude = respObj.latitude;
                    ret.Longitude = respObj.longitude;
                    ret.City = respObj.cityName;
                    ret.TimeZone = respObj.timeZone;
                    return (ret);
                }
            }

            return (ret);
        }
        static void WriteIpGeoLocInfo() {
            if (!_hasIpGeoLocInfo) {
                return;
            }
            try {
                /// Text output
                StringBuilder sb = new StringBuilder();
                string s = "";
                foreach (GoldingsGym.ParserLib.IPAddress i in addresses) {
                    if (i.HasGeoLocation) {
                        s = i.Latitude + ',' + i.Longitude + ',' + i.DNSName + ',' + i.IpAddress + ',' + GetSevColour(i.IpAddress) + '\n';
                    }
                    sb.Append(s);
                }
                string oFileStem = "";
                if (String.IsNullOrEmpty(outputStem)) {
                    oFileStem = "ipinfo_geoloc_" + TimeStamp + ".txt";
                }
                else {
                    oFileStem = "ipinfo_geoloc_" + outputStem + ".txt";
                }
                FileInfo ofi = new FileInfo(oFileStem);
                StreamWriter sw = ofi.CreateText();
                sw.Write(sb.ToString());
                sw.Close();

                /// KML output
                ///
                string oFileStem2 = "";
                if (String.IsNullOrEmpty(outputStem)) {
                    oFileStem2 = TimeStamp + "_ipinfo_geoloc" + ".kml";
                }
                else {
                    oFileStem2 = outputStem + "_ipinfo_geoloc" + ".kml";
                }
                StringBuilder sb2 = new StringBuilder();
                sb2.Append("<?xml version=\"1.0\" encoding=\"UTF-8\"?>\r\n");
                sb2.Append("<kml xmlns=\"http://www.opengis.net/kml/2.2\">\r\n");
                sb2.Append("<Document>\r\n");
                sb2.AppendFormat("<name>{0}</name>\r\n", oFileStem2);
                sb2.AppendFormat("<description>{0}</description>\r\n", outputStem);
                sb2.AppendFormat(KMLStyleMap() + "\r\n");
                string s2 = "";
                foreach (GoldingsGym.ParserLib.IPAddress i in addresses) {
                    if (i.HasGeoLocation) {
                        s2 = "<Placemark>\r\n";
                        s2 += "<name>" + i.IpAddress + "</name>\r\n";
                        s2 += "<description>"
                            + "<p>DNS Name: " + (String.IsNullOrEmpty(i.DNSName) ? i.IpAddress : i.DNSName) + "</p>"
                            + "<p>City: " + i.City + "</p>"
                            + "<p>TimeZone: " + i.TimeZone + "</p>"
                            + "</description>\r\n";
                        s2 += "<styleUrl>" + KMLIconFromHSH(i.IpAddress) + "</styleUrl>\r\n";
                        s2 += "<Point>\r\n\t";
                        s2 += "<coordinates>" + i.Longitude + "," + i.Latitude + ",0</coordinates>\r\n";
                        s2 += "</Point>\r\n";
                        s2 += "</Placemark>\r\n";
                    }
                    sb2.Append(s2);
                }
                sb2.Append("</Document>\r\n");
                sb2.Append("</kml>");
                FileInfo ofi2 = new FileInfo(oFileStem2);
                StreamWriter sw2 = ofi2.CreateText();
                sw2.Write(sb2.ToString());
                sw2.Close();
            }
            catch (Exception ex) {
                Console.WriteLine("[!] Exception writing Geo-location data: {0}", ex.Message);
            }
        }
        static private void WriteStats() {
            try {
                var tf = addresses.Count;
                int nf = 0, xf = 0, bf = 0, vf = hshValues.Count;

                foreach (GoldingsGym.ParserLib.IPAddress i in addresses) {
                    if (i.FromNmap && i.FromAXFR) {
                        ++xf;
                        ++nf;
                        ++bf;
                    }
                    else if (i.FromAXFR) {
                        ++xf;
                    }
                    else if (i.FromNmap) {
                        ++nf;
                    }
                }
                string fs = "";
                if (String.IsNullOrEmpty(outputStem)) {
                    fs = TimeStamp + "_statistics" + ".txt";
                }
                else {
                    fs = outputStem + "_statistics" + ".txt";
                }
                StringBuilder sb = new StringBuilder();
                sb.AppendFormat("Total addresses found: {0}\r\n", tf);
                sb.AppendFormat("Addresses discovered by Nmap and from AXFR: {0}\r\n", bf);
                sb.AppendFormat("Addresses discovered by Nmap: {0}\r\n", nf);
                sb.AppendFormat("Addresses discovered from AXFR: {0}\r\n", xf);
                sb.AppendFormat("Addresses scanned by Nessus: {0}\r\n", vf);

                FileInfo fi = new FileInfo(fs);
                StreamWriter sw = fi.CreateText();
                sw.Write(sb.ToString());
                sw.Close();

            }
            catch (Exception ex) {
                Console.WriteLine("[-] execption caught writing statistics: {0}", ex.Message);
            }
        }
        static string KMLIconFromHSH(string ip) {
            string ret = "#icon-005";
            try {
                string test = "UNKNOWN";
                if (!hshValues.ContainsKey(ip)) {
                    return (ret);
                }
                else {
                    foreach (DictionaryEntry de in hshValues) {
                        if (de.Key.ToString() == ip) {
                            test = de.Value.ToString();
                            break;
                        }
                    }
                }
                if (test.ToUpper() == "CRITICAL") {
                    ret = "#icon-001";
                }
                else if (test.ToUpper() == "HIGH") {
                    ret = "#icon-002";
                }
                else if (test.ToUpper() == "MEDIUM") {
                    ret = "#icon-003";
                }
                else if (test.ToUpper() == "LOW") {
                    ret = "#icon-004";
                }
                else if (test.ToUpper() == "INFO") {
                    ret = "#icon-004";
                }
                return (ret);

            }
            catch (Exception ex) {
                Console.WriteLine("[!] Exception caught in KmlIconFromHSH: {0}", ex.Message);
            }

            return (ret);
        }
        static void ParseArgs(string[] args) {
            _parser = new OptionsParser(args);
            _verbose = _parser.Verbose;
            nmapFile = _parser.GetOpt("nmap");
            hshFile = _parser.GetOpt("hsh");
            axfrFile = _parser.GetOpt("axfr");
            outputStem = _parser.GetOpt("output");
            subnetString = _parser.GetOpt("subnet");
            glApi = _parser.GetOpt("glapi");

            string f = _parser.GetOpt("font");
            //Console.WriteLine("[!] Chosen font: {0}", f);
            if (!string.IsNullOrEmpty(f)) {
                outputFont = f;
            }

            if (String.IsNullOrEmpty(subnetString)) {
                _hasSubnets = false;
            }
            else {
                _hasSubnets = true;
            }
        }
        static string KMLStyleMap() {
            string ret = "<Style id=\"icon-001-hoveroff\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>ff0000ff</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>0</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<Style id=\"icon-001-hoveron\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>ff0000ff</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>1</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<StyleMap id=\"icon-001\">\r\n" +
                "<Pair>\r\n" +
                "<key>normal</key>\r\n" +
                "<styleUrl>#icon-001-hoveroff</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "<Pair>\r\n" +
                "<key>highlight</key>\r\n" +
                "<styleUrl>#icon-001-hoveron</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "</StyleMap>\r\n" +
                "<Style id=\"icon-002-hoveroff\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>0014b4ff</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>0</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<Style id=\"icon-002-hoveron\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>0014b4ff</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>1</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<StyleMap id=\"icon-002\">\r\n" +
                "<Pair>\r\n" +
                "<key>normal</key>\r\n" +
                "<styleUrl>#icon-002-hoveroff</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "<Pair>\r\n" +
                "<key>highlight</key>\r\n" +
                "<styleUrl>#icon-002-hoveron</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "</StyleMap>\r\n" +
                "<Style id=\"icon-003-hoveroff\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>0014b400</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>0</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<Style id=\"icon-003-hoveron\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>0014b400</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>1</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<StyleMap id=\"icon-003\">\r\n" +
                "<Pair>\r\n" +
                "<key>normal</key>\r\n" +
                "<styleUrl>#icon-003-hoveroff</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "<Pair>\r\n" +
                "<key>highlight</key>\r\n" +
                "<styleUrl>#icon-003-hoveron</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "</StyleMap>\r\n" +
                "<Style id=\"icon-004-hoveroff\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>ffff0000</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>0</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<Style id=\"icon-004-hoveron\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>ffff0000</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>1</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<StyleMap id=\"icon-004\">\r\n" +
                "<Pair>\r\n" +
                "<key>normal</key>\r\n" +
                "<styleUrl>#icon-004-hoveroff</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "<Pair>\r\n" +
                "<key>highlight</key>\r\n" +
                "<styleUrl>#icon-004-hoveron</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "</StyleMap>\r\n" +
                "<Style id=\"icon-005-hoveroff\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>ff000000</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>0</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<Style id=\"icon-005-hoveron\">\r\n" +
                "<IconStyle>\r\n" +
                "<color>ff000000</color>\r\n" +
                "<scale>1</scale>\r\n" +
                "<Icon>\r\n" +
                "<href>http://www.gstatic.com/mapspro/images/stock/503-wht-blank_maps.png</href>\r\n" +
                "</Icon>\r\n" +
                "<hotSpot x=\"32\" xunits=\"pixels\" y=\"64\" yunits=\"insetPixels\"/>\r\n" +
                "</IconStyle>\r\n" +
                "<LabelStyle>\r\n" +
                "<scale>1</scale>\r\n" +
                "</LabelStyle>\r\n" +
                "</Style>\r\n" +
                "<StyleMap id=\"icon-005\">\r\n" +
                "<Pair>\r\n" +
                "<key>normal</key>\r\n" +
                "<styleUrl>#icon-005-hoveroff</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "<Pair>\r\n" +
                "<key>highlight</key>\r\n" +
                "<styleUrl>#icon-005-hoveron</styleUrl>\r\n" +
                "</Pair>\r\n" +
                "</StyleMap>";

            return (ret);
        }
    }
}
