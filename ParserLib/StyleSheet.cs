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
    public class StyleSheet {

        public static StyleSheet instance = null;
        private StyleSheet() { }
        public static StyleSheet GetInstance() {
            if (instance == null) {
                instance = new StyleSheet();
            }
            return (instance);
        }
        /* Presentation defaults */
        public static string MainFont = "Arial";//"Courier New";
        public static string MainFontSize = "10";
        public static string TitleFontSize = "15";
        public static string FontColor = "Black";
        public static string HighLightColor = "Black";
        public static string BorderColor = "Black";
        public static string BackgroundColor = "White";

        /* Severity colors - X11 strings */
        //public static string ColorCritical = "firebrick1";
        //public static string ColorHigh = "orangered1";
        //public static string ColorMedium = "gold2";
        //public static string ColorLow = "navy";
        //public static string ColorInfo = "navy";

        /* Severity colors - .Net */
        public static Color NetColorCritical = Color.Firebrick;
        public static Color NetColorHigh = Color.OrangeRed;
        public static Color NetColorMedium = Color.DarkSalmon;
        public static Color NetColorLow = Color.DarkGreen;
        public static Color NetColorInfo = Color.Navy;

        /* Vulnerability category colors - .Net */
        public static Color NetColorWindows = Color.Navy;
        public static Color NetColorLinux = Color.Goldenrod;
        public static Color NetColorNetworkDevices = Color.Firebrick;
        public static Color NetColorPolicy = Color.CadetBlue;
        public static Color NetColorRDBMS = Color.Coral;
        public static Color NetColorWebApplication = Color.CornflowerBlue;
        public static Color NetColorServices = Color.DarkOrange;
        public static Color NetColorMobile = Color.Salmon;
        public static Color NetColorPeer2Peer = Color.DodgerBlue;
        public static Color NetColorScada = Color.Gainsboro;
        public static Color NetColorVirtualisation = Color.SlateGray;
        public static Color NetColorDenialOfService = Color.Black;
        public static Color NetColorRemoteControl = Color.Crimson;
        public static Color NetColorMiscellaneous = Color.LightBlue;

        public static string CompanyName = "Golding's Gym";
        public static string CompanyLogo = "";

        private string inputFile = "";

        public void LoadFromFile(string f) {
            //try {
            //    if (!File.Exists(f)) {
            //        Debug.WriteLine("Style sheet definition file {0} doesn't exist", f);
            //        return;
            //    }
            //    inputFile = f;
            //    FileInfo fi = new FileInfo(inputFile);
            //    StreamReader sr = fi.OpenText();
            //    string line = "";
            //    while (!sr.EndOfStream) {
            //        line = sr.ReadLine();
            //        if (line.StartsWith("#")) {
            //            continue;
            //        }
            //        string[] pr = line.Split('=');
            //        if (pr.Length != 2)
            //            continue;
            //        if (pr[0].Equals("mainfont", StringComparison.OrdinalIgnoreCase)) MainFont = pr[1];
            //        else if (pr[0].Equals("mainfontsize", StringComparison.OrdinalIgnoreCase)) MainFontSize = pr[1];
            //        else if (pr[0].Equals("fontcolor", StringComparison.OrdinalIgnoreCase)) FontColor = pr[1];
            //        else if (pr[0].Equals("highlightcolor", StringComparison.OrdinalIgnoreCase)) HighLightColor = pr[1];
            //        else if (pr[0].Equals("BorderColor", StringComparison.OrdinalIgnoreCase)) BorderColor = pr[1];
            //        else if (pr[0].Equals("BackgroundColor", StringComparison.OrdinalIgnoreCase)) BackgroundColor = pr[1];
            //        else if (pr[0].Equals("ColorCritical", StringComparison.OrdinalIgnoreCase)) ColorCritical = pr[1];
            //        else if (pr[0].Equals("ColorHigh", StringComparison.OrdinalIgnoreCase)) ColorHigh = pr[1];
            //        else if (pr[0].Equals("ColorMedium", StringComparison.OrdinalIgnoreCase)) ColorMedium = pr[1];
            //        else if (pr[0].Equals("ColorLow", StringComparison.OrdinalIgnoreCase)) ColorLow = pr[1];
            //        else if (pr[0].Equals("ColorInfo", StringComparison.OrdinalIgnoreCase)) ColorInfo = pr[1];
            //        else if (pr[0].Equals("CompanyName", StringComparison.OrdinalIgnoreCase)) CompanyName = pr[1];
            //        else if (pr[0].Equals("CompanyLogo", StringComparison.OrdinalIgnoreCase)) CompanyLogo = pr[1];
            //        //else if (pr[0].Equals("",StringComparison.OrdinalIgnoreCase))  = pr[1];
            //    }

            //}
            //catch (Exception e) { Debug.WriteLine("General exception in StyleSheet.LoadFromFile: {0}", e.ToString()); }
            //finally { }
        }
        public void FileConfirm(string f) {
            //    try {

            //    }
            //    catch (Exception e) { Debug.WriteLine("[-] General exception in StyleSheet.LoadFromFile: {0}", e.ToString()); }
            //    finally { }
            //}
        }
    }
}
