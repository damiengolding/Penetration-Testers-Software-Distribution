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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoldingsGym.ParserLib {
    public class VulnSpecification {
        public string Port = "";
        public string ServiceName = "";
        public string Protocol = "";
        public string PluginName = "";
        public string PluginFamily = "";
        public string Synopsis = "";
        public string Solution = "";
        public string Severity = "";
        public int SeverityInt = 0;
        public bool ScanCompleted = false;
        public Constants.NessusIssueSeverity SeverityEnum = Constants.NessusIssueSeverity.UNKNOWN;
    }
}
