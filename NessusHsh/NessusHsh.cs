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

namespace NessusHsh
{
    class NessusHsh
    {
        static List<HostVuln> hosts = new List<HostVuln>();
        static string TimeStamp = DateTime.Now.ToFileTime().ToString();
        static int Main(string[] args)
        {
            int ret = 0;
            if (args.Length != 1 || !File.Exists(args[0]))
            {
                Usage();
                return (1);
            }
            string iFile = args[0];
            InputFileRecog ifr = new InputFileRecog(iFile);
            if (ifr.GetFileType() != Constants.SupportedInputTypes.NESSUS)
            {
                return (1);
            }

            XmlDocument xDoc = new XmlDocument();
            try
            {
                xDoc.Load(iFile);
                XmlNodeList nl = xDoc.GetElementsByTagName("Report");
                if (nl.Count != 1)
                {
                    return (1);
                }
                var repDoc = new XmlDocument();
                repDoc.LoadXml(nl[0].OuterXml); // report host
                nl = repDoc.GetElementsByTagName("ReportHost");
                Debug.WriteLine("Found {0} report hosts", nl.Count);
                foreach (XmlNode n in nl)
                {
                    ProcessHost(n);
                }
            }
            catch (Exception e) { Debug.WriteLine(e.Message); }
            finally { }
            WriteOutput();
            Console.WriteLine(TimeStamp);
            return (ret);
        }

        private static void Usage()
        {
            Console.WriteLine();
            Console.WriteLine("###########################################################################################");
            Console.WriteLine("#");
            Console.WriteLine("# Converts a nessus xml file to a textual representation of highest severity");
            Console.WriteLine("# vulnerabilities per host");
            Console.WriteLine("#");
            Console.WriteLine("# Output is used in conjunction with ntop.exe");
            Console.WriteLine("#");
            Console.WriteLine("# Usage:");
            Console.WriteLine("#\vhsh <nessus.xml>");
            Console.WriteLine("#");
            Console.WriteLine("###########################################################################################");
            Console.WriteLine();
        }

        static private void ProcessHost(XmlNode h)
        {
            try
            {
                HostVuln hv = new HostVuln();
                hv.HostFromXml(h);
                hosts.Add(hv);
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic exception in ProcessHost");
                Debug.WriteLine(e.Message);
            }
            finally { }
        }

        static private void WriteOutput()
        {
            StringBuilder sb = new StringBuilder();
            foreach (HostVuln hv in hosts)
            {
                string str = hv.IpAddress.IpAddress + "=" + SupportFunctions.StringToNIS(hv.HighestSeverity.ToString());
                sb.AppendLine(str);
            }
            try
            {
                string fn = "nessus_" + TimeStamp + ".hsh";
                FileInfo ofi = new FileInfo(fn);
                StreamWriter sw = ofi.CreateText();
                sw.Write(sb.ToString());
                sw.Close();
            }
            catch (Exception e)
            {
                Debug.WriteLine("Generic exception in WriteOutput");
                Debug.WriteLine(e.Message);
            }
            finally { }
        }
    }
}
