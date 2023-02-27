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
using ptulnis = GoldingsGym.ParserLib.Constants.NessusIssueSeverity;
using System.Text.RegularExpressions;

namespace GoldingsGym.ParserLib {
    public static class SupportFunctions {
        public // Nicked from the interwebs
        static string CollapseSpaces(string value) {
            return Regex.Replace(value, @"\s+", " ");
        }
        public static ptulnis StringToNIS(string sev) {
            ptulnis ret = ptulnis.UNKNOWN;
            if (sev == "4") {
                ret = ptulnis.CRITICAL;
            }
            else if (sev == "3") {
                ret = ptulnis.HIGH;
            }
            else if (sev == "2") {
                ret = ptulnis.MEDUIM;
            }
            else if (sev == "1") {
                ret = ptulnis.LOW;
            }
            else if (sev == "0") {
                ret = ptulnis.INFO;
            }
            else {
                ret = ptulnis.UNKNOWN;
            }
            return (ret);

        }
        public static bool IsValidIpv4Address(string addr) {
            int[] ia = new int[] { 0, 0, 0, 0 };
            if (!ParseIpAddress(addr, ref ia)) {
                return (false);
            }
            foreach (int i in ia) {
                if (i < 0 || i > 255) {
                    return (false);
                }
            }
            return (true);
        }
        public static bool IsReservedAddress(string addr) {
            int[] ia = new int[] { 0, 0, 0, 0 };
            if (!ParseIpAddress(addr, ref ia)) {
                return (false);
            }
            if (ia[0] == 10) {
                return (true);
            }
            else if (ia[0] == 192 && ia[1] == 168) {
                return (true);
            }
            else if (ia[0] == 172 && (ia[1] >= 16 && ia[1] <= 31)) {
                return (true);
            }
            else if (ia[0] == 127) {
                return (true);
            }
            return (false);
        }
        public static bool ParseIpAddress(string addr, ref int[] oa) {
            int[] ia = new int[] { 0, 0, 0, 0 };
            int res;
            string[] tmp = addr.Split('.');
            if (tmp.Count() != 4) {
                return (false);
            }
            /* Test members before assignment */
            foreach (string str in tmp) {
                if (!Int32.TryParse(str, out res)) {
                    return (false);
                }
            }
            /* Assign the ints */
            int c = 0;
            foreach (string str in tmp) {
                oa[c] = Int32.Parse(str);
                c++;
            }
            return (true);
        }
    }
}
