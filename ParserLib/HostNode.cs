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
using System.Collections.Generic;
using System.Diagnostics;
using System.Text;
using System.Xml;

namespace GoldingsGym.ParserLib {
    public class HostNode {

        public HostNode() {
            IsGateway = false;
            HasGeoLocation = false;
            PortSpecs = new List<PortSpecification>();
            Scripts = new Dictionary<string, string>();
        }

        #region HostFromXml
        public void HostFromXml(XmlNode h) {
            try {
                var pDoc = new XmlDocument();
                pDoc.LoadXml(h.OuterXml);

                //Process basic host attributes
                StartTime = h.Attributes.GetNamedItem("starttime").Value;
                EndTime = h.Attributes.GetNamedItem("endtime").Value;
                Debug.WriteLine("Start: {0} End: {1}", StartTime, EndTime);

                //Process each child node:
                XmlNodeList cnList = h.ChildNodes;
                foreach (XmlNode n in cnList) {
                    //	Addresses
                    if (n.Name.Equals("address")) {
                        Debug.WriteLine("Is this addr type?: {0}", n.Attributes.GetNamedItem("addrtype").Value);
                        string adType = n.Attributes.GetNamedItem("addrtype").Value; ;
                        AddrType = adType;
                        if (adType.Equals("mac")) {
                            MacAddress = n.Attributes.GetNamedItem("addr").Value;
                            MacVendor = n.Attributes.GetNamedItem("vendor").Value;
                        }
                        else {
                            IpAddress = new IPAddress(n.Attributes.GetNamedItem("addr").Value);
                        }
                    }

                    //	Host names
                    else if (n.Name.Equals("hostnames")) {
                        XmlNodeList hnl = n.ChildNodes;
                        foreach (XmlNode hn in hnl) {
                            if (hn.Name.Equals("hostname")) {
                                HostName = hn.Attributes.GetNamedItem("name").Value;
                                RecordType = hn.Attributes.GetNamedItem("type").Value;
                            }
                        }
                    }

                    //	Ports
                    else if (n.Name.Equals("ports")) {
                        Debug.WriteLine("Ports");
                        XmlNodeList pnl = n.ChildNodes;
                        foreach (XmlNode pn in pnl) {
                            if (pn.Name.Equals("port")) {
                                PortSpecification ps = new PortSpecification();
                                Debug.WriteLine("\tPort: {0}", pn.Attributes.GetNamedItem("portid").Value);
                                ps.Protocol = pn.Attributes.GetNamedItem("protocol").Value;
                                ps.PortNumber = pn.Attributes.GetNamedItem("portid").Value;
                                /* Get state and service children */
                                XmlNodeList pChildren = pn.ChildNodes;
                                foreach (XmlNode pChild in pChildren) {
                                    if (pChild.Name.Equals("state")) {
                                        Debug.WriteLine("Reason: {0}", pChild.Attributes.GetNamedItem("reason").Value);
                                        ps.State = pChild.Attributes.GetNamedItem("state").Value;
                                        ps.Reason = pChild.Attributes.GetNamedItem("reason").Value;
                                        ps.TTL = pChild.Attributes.GetNamedItem("reason_ttl").Value;
                                    }
                                    else if (pChild.Name.Equals("service")) {
                                        Debug.WriteLine("Service: {0}", pChild.Attributes.GetNamedItem("name").Value);
                                        ps.Service = pChild.Attributes.GetNamedItem("name").Value;
                                        ps.IdMethod = pChild.Attributes.GetNamedItem("method").Value;
                                        ps.Confidence = pChild.Attributes.GetNamedItem("conf").Value;
                                    }
                                }
                                PortSpecs.Add(ps);
                            }
                        }
                    }

                    //	OS guesses
                    else if (n.Name.Equals("os")) {
                        Debug.WriteLine("OS tag");
                        XmlNodeList sChildren = n.ChildNodes;
                        foreach (XmlNode sChild in sChildren) {
                            if (sChild.Name.Equals("osmatch")) {
                                OsMatch = sChild.Attributes.GetNamedItem("name").Value;
                                OsMatchConfidence = sChild.Attributes.GetNamedItem("accuracy").Value;
                                Debug.WriteLine("OS Match: {0}", OsMatch);
                                break;
                            }
                        }
                    }
                    //	Host scripts
                    else if (n.Name.Equals("hostscript")) {
                        Debug.WriteLine("Hostscript");
                        XmlNodeList sChildren = n.ChildNodes;
                        foreach (XmlNode sChild in sChildren) {
                            if (sChild.Name.Equals("script")) {
                                string scr = sChild.Attributes.GetNamedItem("id").Value;
                                string res = sChild.Attributes.GetNamedItem("output").Value;
                                Scripts.Add(scr, res);

                                if (scr.Equals("ip-geolocation-ipinfodb")) {
                                    Debug.WriteLine("Tested for geo-location: {0}", res);
                                    HasGeoLocation = true;

                                    /* Parse Longitude and Latitude */

                                    string lat, lng, combined, split1;
                                    string[] resArr = res.Split('\n');
                                    combined = resArr[2];
                                    Debug.WriteLine("Split: {0}", combined);
                                    split1 = combined.Split(':')[1].Trim(' ');
                                    Debug.WriteLine("Split1: {0}", split1);
                                    lat = split1.Split(',')[0];
                                    lng = split1.Split(',')[1];

                                    Debug.WriteLine("Latitude: {0}", lat);
                                    Debug.WriteLine("Longitude: {0}", lng);
                                    Latitude = lat;
                                    Longitude = lng;
                                }

                                if (scr.Equals("ip-forwarding")) {
                                    if (res.Contains("The host has ip forwarding enabled")) {
                                        Debug.WriteLine("\tThe host is a gateway");
                                        IsGateway = true;
                                    }
                                }
                            }
                        }
                    }
                }


                XmlNodeList psList = pDoc.GetElementsByTagName("ports");
                if (psList.Count == 0) {
                    Debug.WriteLine("No ports in list");
                    return;
                }
                Debug.WriteLine("Port list size: {0}", psList.Count.ToString());
                var psDoc = new XmlDocument();
                psDoc.LoadXml(psList[0].OuterXml);
                XmlNodeList pList = psDoc.GetElementsByTagName("port");
            }
            catch (Exception e) {
                Debug.WriteLine("Exception in process host");
                Debug.WriteLine(e.Message);
            }
        }

        #endregion HostFromXml

        #region Exports
        public string ToTsv() {
            StringBuilder ret = new StringBuilder();
            //ret.AppendLine("IP Address\tProtocol\tPort Number\tService\tState\tID Method");
            foreach (PortSpecification p in PortSpecs) {
                ret.AppendFormat("{0}\t{1}\t{2}\t{3}\t{4}\t{5}\r\n",
                    IpAddress.IpAddress,
                    p.Protocol,
                    p.PortNumber,
                    p.Service,
                    p.State,
                    p.IdMethod
                    );
            }
            return (ret.ToString());
        }

        public string ToCsv() {
            StringBuilder ret = new StringBuilder();
            //ret.AppendLine("IP Address\tProtocol\tPort Number\tService\tState\tID Method");
            foreach (PortSpecification p in PortSpecs) {
                ret.AppendFormat("{0},{1},{2},{3},{4},{5}\r\n",
                    IpAddress.IpAddress,
                    p.Protocol,
                    p.PortNumber,
                    p.Service,
                    p.State,
                    p.IdMethod
                    );
            }
            return (ret.ToString());
        }
        public string ToHtml() {
            StringBuilder html = new StringBuilder();

            html.AppendLine("<br><table style=\"width:100%\">");
            html.AppendFormat("<tr><th colspan=\"5\">{0}</th><th colspan=\"2\">{1}</th></tr>",
                IpAddress.IpAddress,
                HostName
                );
            html.AppendLine("<tr><th>Port</th><th>Protocol</th><th>State</th><th>Service</th><th>Discovery method</th></th></tr>");
            if (PortSpecs.Count == 0) {
                html.AppendLine("<tr><td colspan=\"5\">No ports were found</td></tr></table>");
                return (html.ToString());
            }

            foreach (PortSpecification ps in PortSpecs) {
                html.AppendFormat("<tr><td>{0}</td><td>{1}</td><td>{2}</td><td>{3}</td><td>{4}</td></tr>",
                    ps.PortNumber,
                    ps.Protocol,
                    ps.State,
                    ps.Service,
                    ps.IdMethod
                    );
            }
            html.AppendLine("</table><br>");

            return (html.ToString());
        }
        //TODO Implement ToXml
        public string ToXml() {
            StringBuilder ret = new StringBuilder();

            return (ret.ToString());
        }

        #endregion Exports

        #region Properties and fields
        public IPAddress IpAddress { get; set; }
        public string AddrType = "";
        public string MacAddress = "";
        public string MacVendor = "";

        public string RecordType = "";
        public string HostName = "";

        public string OsMatch = "";
        public string OsMatchConfidence = "";
        public int HostIndex { get; set; }
        public string StartTime = "";
        public string EndTime = "";

        public Dictionary<string, string> Scripts { get; set; }
        public bool IsGateway { get; set; }
        public bool HasGeoLocation { get; set; }
        public string Longitude = "";
        public string Latitude = "";
        public List<PortSpecification> PortSpecs { get; set; }

        #endregion Properties and fields
    }
}
