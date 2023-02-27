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
	public static class Constants {
		/**
		<summary>The list of input file types supported by PenTesterUtils.</summary>
			<list type="bullet">
				<listheader>
					<term>SupportedInputTypes</term>
					<description>Supported input file types. This is a Flags enum, so will accept the logical operators and, or operators</description>
				</listheader>
				<item>
					<term>NONE</term>
					<description>The file is not supported or does not exist</description>
				</item>
				<item>
					<term>NMAP</term>
					<description>Nmap XML file; the recommended usage of nmap includes the ip-forwarding.nse script, e.g. --script=ip-forwarding --script-args='target=www.google.com'</description>
				</item>
				<item>
					<term>MASSCAN</term>
					<description>Output from the masscan network scanner tool.</description>
				</item>
				<item>
					<term>NESSUS</term>
					<description>Output exported from the Nessus vulnerability scanner tool in the *.nessus format.</description>
				</item>
				<item>
					<term>OPENVAS_XML</term>
					<description>Standard XML output from the Open Source Vulnerability Assessment tool.</description>
				</item>
				<item>
					<term>OPENVAS_ANONYMOUS_XML</term>
					<description>Anonymous XML output format from the Open Source Vulnerability Assessment tool.</description>
				</item>
				<item>
					<term>BURP_PROJECT</term>
					<description>Issues generated from the Burp web application assessment/attack tool.</description>
				</item>
				<item>
					<term>OWASPZAP</term>
					<description>Site-tree generated from the OWASP Zed Attack Proxy</description>
				</item>
				<item>
					<term>AXFR_DIG</term>
					<description>Output from a Dig zone transfer operation</description>
				</item>
				<item>
					<term>SSLSCAN</term>
					<description>Output from an SSLScan</description>
				</item>
				<item>
					<term>METASPLOIT</term>
					<description>Output from a Metasploit database export</description>
				</item>
				<item>
					<term>HASHES_PWDUMP</term>
					<description>Hashes in pwdump output format</description>
				</item>
			</list>
		*/
		public enum SupportedInputTypes {
			NONE,
			NMAP,
			NETDISCOVER,
			MASSCAN,
			NESSUS,
			NESSUS_HSH,
			OPENVAS_XML,
			OPENVAS_ANONYMOUS_XML,
			BURP_PROJECT,
			OWASPZAP,
			AXFR_DIG,
			AXFR_NS_WIN,
			AXFR_NS_LIN,
			SSLSCAN,
			METASPLOIT,
			HASHES_PWDUMP,
			NUM_SUPPORTED_TYPES
		};
		public enum NmapNodeRoles {
			NONE,
			ROOT,
			SUBNET,
			ACLASS,
			BCLASS,
			CCLASS,
			LEAF,
			NUM_ROLES
		}
		public enum ProcessorStates {
			NONE,
			START_STATE_CREATED
		}
		public enum NessusIssueSeverity {
			UNKNOWN,
			INFO,
			LOW,
			MEDUIM,
			HIGH,
			CRITICAL,
			NUM_SEVERITIES
		}
		public enum NessusPluginCategory {
			UNKNOWN,
			WINDOWS,
			LINUX,
			NETWORK_DEVICES,
			POLICY,
			DATABASE,
			WEB,
			SERVICES,
			MOBILE,
			P2P,
			SCADA,
			DOS,
			REMOTE_CONTROL,
			MISCELLANEOUS,
			NUM_CATEGORIES
		}
	}
}
