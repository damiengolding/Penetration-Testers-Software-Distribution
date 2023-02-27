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
using System;
using System.IO;
using System.Xml;
using System.Diagnostics;
using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Runtime.Serialization.Formatters.Binary;


namespace GoldingsGym.ParserLib {
    public class HostVuln {
        public HostVuln() {
            VulnSpecs = new List<VulnSpecification>();
        }

        #region HostFromXml
        public void HostFromXml(XmlNode n) {
            try {
                IpAddress = new IPAddress(n.Attributes.GetNamedItem("name").Value);
                Debug.WriteLine("IP Address: {0}", IpAddress.IpAddress);
                var riDoc = new XmlDocument();// Report host
                riDoc.LoadXml(n.OuterXml);
                XmlNodeList xnl = riDoc.GetElementsByTagName("ReportItem");// Individual vulns/items

                foreach (XmlNode ri in xnl) {
                    VulnSpecification vs = new VulnSpecification();

                    /* Attributes */
                    vs.Port = ri.Attributes.GetNamedItem("port").Value;
                    vs.ServiceName = ri.Attributes.GetNamedItem("svc_name").Value;
                    vs.Protocol = ri.Attributes.GetNamedItem("protocol").Value;

                    /* Special case - severity */
                    vs.Severity = ri.Attributes.GetNamedItem("severity").Value;
                    vs.SeverityInt = Convert.ToInt32(vs.Severity);
                    vs.SeverityEnum = SupportFunctions.StringToNIS(vs.Severity);
                    ProcessSeverity(vs.SeverityInt);

                    /* Special case - plugins */
                    vs.PluginFamily = ri.Attributes.GetNamedItem("pluginFamily").Value;
                    vs.PluginName = ri.Attributes.GetNamedItem("pluginName").Value;
                    ProcessPlugin(vs.PluginFamily);

                    /* Special case - 19506 scan completed OK */
                    string test = ri.Attributes.GetNamedItem("pluginID").Value;
                    if (test.Equals("19506")) {
                        _has19506 = true;
                        vs.ScanCompleted = true;
                    }

                    /* Child text elements */
                    XmlNode tmpNode;
                    var childDoc = new XmlDocument();
                    childDoc.LoadXml(ri.OuterXml);
                    XmlNodeList cl = childDoc.GetElementsByTagName("synopsis");
                    if (cl.Count == 1) {
                        tmpNode = cl[0];
                        vs.Synopsis = tmpNode.InnerText;
                    }

                    cl = childDoc.GetElementsByTagName("solution");
                    if (cl.Count == 1) {
                        tmpNode = cl[0];
                        vs.Solution = tmpNode.InnerText;
                    }
                    Debug.WriteLine("Synopsis: {0}", vs.Synopsis);
                    Debug.WriteLine("Solution: {0}", vs.Solution);
                    VulnSpecs.Add(vs);
                }

            }
            catch (Exception e) {

            }
            finally { }
        }

        private void ProcessPlugin(string pl) {
            string plg = pl.ToLower();
            Debug.WriteLine("Plugin: {0}", pl);
            if (plg.ToLower().Trim().Contains("windows")) {
                WindowsCategory++;
            }
            else if (plg.Contains("cisco")
                    || plg.ToLower().Trim().Contains("junos")
                    || plg.ToLower().Trim().Contains("firewalls")
                    || plg.ToLower().Trim().Contains("netware")
                    || plg.ToLower().Trim().Contains("huawei")
                    || plg.ToLower().Trim().Contains("f5 networks")
                    || plg.ToLower().Trim().Contains("palo alto")
                    ) {
                NetworkDevicesCategory++;
            }
            else if (plg.ToLower().Trim().Contains("policy compliance")) {
                PolicyCategory++;
            }
            else if (plg.ToLower().Trim().Contains("databases")) {
                RDBMSCategory++;
            }
            else if (plg.ToLower().Trim().Contains("cgi") || plg.Contains("web servers")) {
                WebApplicationCategory++;
            }
            else if (plg.ToLower().Trim().Contains("dns")
                || plg.Contains("ftp")
                || plg.Contains("smtp")
                || plg.Contains("snmp")
                || plg.Contains("service detection")) {
                ServicesCategory++;
            }
            else if (plg.ToLower().Trim().Contains("mobile devices")) {
                MobileCategory++;
            }
            else if (plg.ToLower().Trim().Contains("peer-to-peer file sharing")) {
                Peer2PeerCategory++;
            }
            else if (plg.ToLower().Trim().Contains("scada")) {
                ScadaCategory++;
            }
            else if (plg.ToLower().Trim().Contains("denial of service")) {
                DenialOfServiceCategory++;
            }
            else if (plg.ToLower().Trim().Contains("gain a shell remotely")
                || plg.Contains("rpc")
                || plg.Contains("back doors")
                || plg.Contains("brute force attacks")
                ) {
                RemoteControlCategory++;
            }
            else if (plg.ToLower().Trim().Contains("oraclevm")
                || plg.ToLower().Trim().Contains("vmware esx")
                || plg.ToLower().Trim().Contains("virtuozzo")
                ) {
                VirtualisationCategory++;
            }
            else if (plg.ToLower().Trim().Contains("local security checks")
                || plg.ToLower().Trim().Contains("default unix accounts")
                ) {
                LinuxCategory++;
            }
            else {
                MiscellaneousCategory++;
            }
        }

        private void ProcessSeverity(int s) {
            if (s > HighestSeverity) {
                HighestSeverity = s;
            }
            switch (s) {
                case 4:
                    CriticalCount++;
                    break;
                case 3:
                    HighCount++;
                    break;
                case 2:
                    MediumCount++;
                    break;
                case 1:
                    LowCount++;
                    break;
                case 0:
                    InfoCount++;
                    break;
                default:
                    UnknownCount++;
                    break;
            }
        }

        #endregion HostFromXml

        #region Properties
        public List<VulnSpecification> VulnSpecs { get; set; }
        public IPAddress IpAddress { get; set; }

        bool _has19506 = false;
        public bool Has19506 {
            get {
                return (_has19506);
            }
            set {
                _has19506 = value;
            }
        }
        /* Severity count */
        public int HighestSeverity = 0;
        public int CriticalCount = 0;
        public int HighCount = 0;
        public int MediumCount = 0;
        public int LowCount = 0;
        public int InfoCount = 0;
        public int UnknownCount = 0;

        /* Plugin family/category count */
        public int WindowsCategory = 0;
        public int LinuxCategory = 0;
        public int NetworkDevicesCategory = 0;
        public int PolicyCategory = 0;
        public int RDBMSCategory = 0;
        public int WebApplicationCategory = 0;
        public int ServicesCategory = 0;
        public int MobileCategory = 0;
        public int Peer2PeerCategory = 0;
        public int ScadaCategory = 0;
        public int DenialOfServiceCategory = 0;
        public int RemoteControlCategory = 0;
        public int VirtualisationCategory = 0;
        public int MiscellaneousCategory = 0;

        #endregion Properties
    }
}
