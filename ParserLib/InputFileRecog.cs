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
    public class InputFileRecog {
        public InputFileRecog(string inputFile) {
            iFile = inputFile;
            FileRecog();
        }

        private string iFile;
        //private Constants.SupportedInputTypes FileType;
        public string FileTypeString = "Does not exist";
        public string ExceptionMessage = "";
        public Type ExceptionType;
        public Constants.SupportedInputTypes GetFileType() {
            //get {
            if (FileTypeString.ToLower() == "nmap") {
                return (Constants.SupportedInputTypes.NMAP);
            }
            else if (FileTypeString.ToLower() == "masscan") {
                return (Constants.SupportedInputTypes.MASSCAN);
            }
            else if (FileTypeString.ToLower() == "netdiscover") {
                return (Constants.SupportedInputTypes.NETDISCOVER);
            }
            else if (FileTypeString.ToLower() == "nessus") {
                return (Constants.SupportedInputTypes.NESSUS);
            }
            else if (FileTypeString.ToLower() == "nslookup axfr win") {
                return (Constants.SupportedInputTypes.AXFR_NS_WIN);
            }
            else if (FileTypeString.ToLower() == "nslookup axfr lin") {
                return (Constants.SupportedInputTypes.AXFR_NS_LIN);
            }
            else if (FileTypeString.ToLower() == "hashes") {
                return (Constants.SupportedInputTypes.HASHES_PWDUMP);
            }
            else if (FileTypeString.ToLower() == "sslscan") {
                return (Constants.SupportedInputTypes.SSLSCAN);
            }
            else if (FileTypeString.ToLower() == "burpsuite issues") {
                return (Constants.SupportedInputTypes.BURP_PROJECT);
            }
            else if (FileTypeString.ToLower() == "metasploit") {
                return (Constants.SupportedInputTypes.METASPLOIT);
            }
            else if (FileTypeString.ToLower() == "openvas xml") {
                return (Constants.SupportedInputTypes.OPENVAS_XML);
            }
            else if (FileTypeString.ToLower() == "openvas anonymous xml") {
                return (Constants.SupportedInputTypes.OPENVAS_ANONYMOUS_XML);
            }
            else if (FileTypeString.ToLower() == "owasp zap") {
                return (Constants.SupportedInputTypes.OWASPZAP);
            }
            else if (FileTypeString.ToLower() == "nslookup axfr lin") {
                return (Constants.SupportedInputTypes.AXFR_NS_LIN);
            }
            else if (FileTypeString.ToLower() == "nslookup axfr win") {
                return (Constants.SupportedInputTypes.AXFR_NS_WIN);
            }
            /*else if (FileTypeString.ToLower() == "") {
                return (Constants.SupportedInputTypes);
            }
            */
            else {
                return (Constants.SupportedInputTypes.NONE);
            }
        }

        void FileRecog() {
            if (!ReadXml()) {
                ReadTextFile();
            }
        }
        bool ReadTextFile() {
            bool ret = true;
            Debug.WriteLine("Entering ReadTextFile");
            if (!File.Exists(iFile)) {
                FileTypeString = "Does not exist";
                return (false);
            }
            try {
                string[] lines = File.ReadAllLines(iFile);
                if (lines.Length < 2) {
                    FileTypeString = "Not recognised";
                    return (false);
                }
                string l0 = lines[0], l1 = lines[1];
                if (l0.StartsWith("[[") && l0.EndsWith("]]")) {
                    FileTypeString = "nslookup axfr win";
                }
                else if (l0.ToLower().StartsWith("server:") && l1.ToLower().StartsWith("address:")) {
                    FileTypeString = "nslookup axfr lin";
                }
            }
            catch (Exception ex) {
                Console.WriteLine("[-] Exception caught in ReadTextFile: {0}", ex.Message);
                return (false);
            }
            return (ret);
        }
        bool ReadXml() {
            bool ret = true;
            Debug.WriteLine("Entering ReadXml");
            if (!File.Exists(iFile)) {
                FileTypeString = "Does not exist";
                return (false);
            }
            try {
                XmlReaderSettings settings = new XmlReaderSettings();
                settings.DtdProcessing = System.Xml.DtdProcessing.Ignore;// DtdProcessing.Parse;
                XmlReader reader = XmlReader.Create(iFile, settings);
                while (reader.Read()) {
                    if (reader.NodeType == XmlNodeType.Element) {
                        Debug.WriteLine("Element name: {0}", reader.Name);
                        if (reader.Name.ToLower() == "nmaprun") {
                            FileTypeString = "nmaprun";
                            if (reader.HasAttributes) {
                                string scanner = reader.GetAttribute("scanner").ToLower();
                                if (scanner.Equals("nmap")) {
                                    FileTypeString = "Nmap";
                                    Debug.WriteLine("Confirmed as Nmap");
                                    break;
                                }
                                else if (scanner.Equals("masscan")) {
                                    FileTypeString = "Masscan";
                                    Debug.WriteLine("Confirmed as masscan");
                                    break;
                                }
                            }
                            break;
                        }
                        else if (reader.Name.ToLower() == "nessusclientdata_v2") {
                            FileTypeString = "Nessus";
                            Debug.WriteLine("Confirmed as nessus");
                            break;
                        }
                        else if (reader.Name.ToLower() == "ssltest") {
                            FileTypeString = "SSLScan";
                            Debug.WriteLine("Confirmed as SSLScan");
                            break;
                        }
                        else if (reader.Name.ToLower() == "issues") {
                            FileTypeString = "BurpSuite Issues";
                            Debug.WriteLine("Confirmed as BurpSuite issues export");
                            break;
                        }
                        else if (reader.Name.ToLower() == "items") {
                            FileTypeString = "BurpSuite Items";
                            Debug.WriteLine("Confirmed as BurpSuite site tree export");
                            break;
                        }
                        else if (reader.Name.ToLower().StartsWith("metasploit")) {
                            FileTypeString = "Metasploit";
                            Debug.WriteLine("Confirmed as Metasploit");
                            break;
                        }
                        else if (reader.Name.ToLower() == "site") {
                            if (reader.GetAttribute("name").Length > 0) {
                                FileTypeString = "OWASP ZAP";
                                Debug.WriteLine("Confirmed as OWASP ZAP");
                                break;
                            }
                        }
                        else if (reader.Name.ToLower() == "report") {
                            string nm = "", val = "";
                            while (reader.Read()) {
                                nm = reader.ToString().ToLower();
                                val = reader.ReadString().ToLower();
                                if (val.Equals("xml")) {
                                    FileTypeString = "OpenVAS XML";
                                    Debug.WriteLine("Confirmed as OpenVAS Standard XML");
                                    break;
                                }
                                else if (val.Equals("anonymous xml")) {
                                    FileTypeString = "OpenVAS Anonymous XML";
                                    Debug.WriteLine("Confirmed as OpenVAS Anonymous XML");
                                    break;
                                }
                            }
                            break;
                        }
                    }
                }
            }
            catch (System.Xml.XmlException xe) { // Not xml - test for AXFR or hashes
                Debug.WriteLine("XML related exception in FileRecog: " + xe.Message);
                ExceptionMessage = xe.Message;
                ExceptionType = xe.GetType();
                return (false);
                //System.IO.StreamReader sr = new System.IO.StreamReader(iFile);
                //string line;
                //while ((line = sr.ReadLine()) != null) {
                //    if (line.Length == 0) {
                //        continue;
                //    }
                //    else {
                //        string ltl = line.ToLower();
                //        if (ltl.Contains("; <<>> dig") && ltl.Contains("axfr")) { // it's an AXFR DiG file
                //            Debug.WriteLine("Confirmed as DiG AXFR");
                //            FileTypeString = "AXFR";
                //            break;
                //        }
                //        else {
                //            int counter = 0;
                //            foreach (char c in ltl) {
                //                if (c == ':') {
                //                    counter++;
                //                }
                //            }
                //            if (counter == 6) {
                //                Debug.WriteLine("Confirmed as Pwdump hashes");
                //                FileTypeString = "hashes";
                //                break;
                //            }
                //        }
                //    }
                //}
                //sr.Close();

            }
            catch (Exception e) {
                Debug.WriteLine("Non-XML related exception in FileRecog: " + e.Message);
                ExceptionMessage = e.Message;
                ExceptionType = e.GetType();
            }
            finally { }
            return (ret);
        }
    }
}
