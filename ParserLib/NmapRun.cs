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
using System.ComponentModel;
using System.Xml.Serialization;

namespace GoldingsGym.ParserLib {

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    [XmlRootAttribute(Namespace = "", IsNullable = false)]
    public partial class NmapRun : IInputFilter {

        private NmapRunScaninfo scaninfoField;

        private NmapRunVerbose verboseField;

        private NmapRunDebugging debuggingField;

        private NmapRunHost[] hostField;

        private NmapRunRunstats runstatsField;

        private string scannerField;

        private string argsField;

        private uint startField;

        private string startstrField;

        private decimal versionField;

        private decimal xmloutputversionField;

        public IInputFilter PopulateFromFile(string fileName) {

            return (this);
        }

        /// <remarks/>
        public NmapRunScaninfo scaninfo {
            get {
                return this.scaninfoField;
            }
            set {
                this.scaninfoField = value;
            }
        }

        /// <remarks/>
        public NmapRunVerbose verbose {
            get {
                return this.verboseField;
            }
            set {
                this.verboseField = value;
            }
        }

        /// <remarks/>
        public NmapRunDebugging debugging {
            get {
                return this.debuggingField;
            }
            set {
                this.debuggingField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("host")]
        public NmapRunHost[] host {
            get {
                return this.hostField;
            }
            set {
                this.hostField = value;
            }
        }

        /// <remarks/>
        public NmapRunRunstats runstats {
            get {
                return this.runstatsField;
            }
            set {
                this.runstatsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string scanner {
            get {
                return this.scannerField;
            }
            set {
                this.scannerField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string args {
            get {
                return this.argsField;
            }
            set {
                this.argsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint start {
            get {
                return this.startField;
            }
            set {
                this.startField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string startstr {
            get {
                return this.startstrField;
            }
            set {
                this.startstrField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public decimal version {
            get {
                return this.versionField;
            }
            set {
                this.versionField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public decimal xmloutputversion {
            get {
                return this.xmloutputversionField;
            }
            set {
                this.xmloutputversionField = value;
            }
        }

    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunScaninfo {

        private string typeField;

        private string protocolField;

        private ushort numservicesField;

        private string servicesField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string protocol {
            get {
                return this.protocolField;
            }
            set {
                this.protocolField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public ushort numservices {
            get {
                return this.numservicesField;
            }
            set {
                this.numservicesField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string services {
            get {
                return this.servicesField;
            }
            set {
                this.servicesField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunVerbose {

        private byte levelField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte level {
            get {
                return this.levelField;
            }
            set {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunDebugging {

        private byte levelField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte level {
            get {
                return this.levelField;
            }
            set {
                this.levelField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHost {

        private NmapRunHostStatus statusField;

        private NmapRunHostAddress addressField;

        private NmapRunHostHostnames hostnamesField;

        private NmapRunHostPorts portsField;

        private NmapRunHostHostscript[] hostscriptField;

        private NmapRunHostTimes[] timesField;

        private uint starttimeField;

        private uint endtimeField;

        /// <remarks/>
        public NmapRunHostStatus status {
            get {
                return this.statusField;
            }
            set {
                this.statusField = value;
            }
        }

        /// <remarks/>
        public NmapRunHostAddress address {
            get {
                return this.addressField;
            }
            set {
                this.addressField = value;
            }
        }

        /// <remarks/>
        public NmapRunHostHostnames hostnames {
            get {
                return this.hostnamesField;
            }
            set {
                this.hostnamesField = value;
            }
        }

        /// <remarks/>
        public NmapRunHostPorts ports {
            get {
                return this.portsField;
            }
            set {
                this.portsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("hostscript")]
        public NmapRunHostHostscript[] hostscript {
            get {
                return this.hostscriptField;
            }
            set {
                this.hostscriptField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("times")]
        public NmapRunHostTimes[] times {
            get {
                return this.timesField;
            }
            set {
                this.timesField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint starttime {
            get {
                return this.starttimeField;
            }
            set {
                this.starttimeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint endtime {
            get {
                return this.endtimeField;
            }
            set {
                this.endtimeField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostStatus {

        private string stateField;

        private string reasonField;

        private byte reason_ttlField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte reason_ttl {
            get {
                return this.reason_ttlField;
            }
            set {
                this.reason_ttlField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostAddress {

        private string addrField;

        private string addrtypeField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string addr {
            get {
                return this.addrField;
            }
            set {
                this.addrField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string addrtype {
            get {
                return this.addrtypeField;
            }
            set {
                this.addrtypeField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostHostnames {

        private NmapRunHostHostnamesHostname hostnameField;

        /// <remarks/>
        public NmapRunHostHostnamesHostname hostname {
            get {
                return this.hostnameField;
            }
            set {
                this.hostnameField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostHostnamesHostname {

        private string nameField;

        private string typeField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string type {
            get {
                return this.typeField;
            }
            set {
                this.typeField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostPorts {

        private NmapRunHostPortsExtraports extraportsField;

        private NmapRunHostPortsPort[] portField;

        /// <remarks/>
        public NmapRunHostPortsExtraports extraports {
            get {
                return this.extraportsField;
            }
            set {
                this.extraportsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("port")]
        public NmapRunHostPortsPort[] port {
            get {
                return this.portField;
            }
            set {
                this.portField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostPortsExtraports {

        private NmapRunHostPortsExtraportsExtrareasons extrareasonsField;

        private string stateField;

        private ushort countField;

        /// <remarks/>
        public NmapRunHostPortsExtraportsExtrareasons extrareasons {
            get {
                return this.extrareasonsField;
            }
            set {
                this.extrareasonsField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public ushort count {
            get {
                return this.countField;
            }
            set {
                this.countField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostPortsExtraportsExtrareasons {

        private string reasonField;

        private ushort countField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public ushort count {
            get {
                return this.countField;
            }
            set {
                this.countField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostPortsPort {

        private NmapRunHostPortsPortState stateField;

        private NmapRunHostPortsPortService serviceField;

        private string protocolField;

        private ushort portidField;

        /// <remarks/>
        public NmapRunHostPortsPortState state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }

        /// <remarks/>
        public NmapRunHostPortsPortService service {
            get {
                return this.serviceField;
            }
            set {
                this.serviceField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string protocol {
            get {
                return this.protocolField;
            }
            set {
                this.protocolField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public ushort portid {
            get {
                return this.portidField;
            }
            set {
                this.portidField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostPortsPortState {

        private string stateField;

        private string reasonField;

        private byte reason_ttlField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string state {
            get {
                return this.stateField;
            }
            set {
                this.stateField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string reason {
            get {
                return this.reasonField;
            }
            set {
                this.reasonField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte reason_ttl {
            get {
                return this.reason_ttlField;
            }
            set {
                this.reason_ttlField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostPortsPortService {

        private string nameField;

        private string methodField;

        private byte confField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string name {
            get {
                return this.nameField;
            }
            set {
                this.nameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string method {
            get {
                return this.methodField;
            }
            set {
                this.methodField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte conf {
            get {
                return this.confField;
            }
            set {
                this.confField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostHostscript {

        private NmapRunHostHostscriptScript scriptField;

        /// <remarks/>
        public NmapRunHostHostscriptScript script {
            get {
                return this.scriptField;
            }
            set {
                this.scriptField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostHostscriptScript {

        private string idField;

        private string outputField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string id {
            get {
                return this.idField;
            }
            set {
                this.idField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string output {
            get {
                return this.outputField;
            }
            set {
                this.outputField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunHostTimes {

        private uint srttField;

        private ushort rttvarField;

        private uint toField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint srtt {
            get {
                return this.srttField;
            }
            set {
                this.srttField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public ushort rttvar {
            get {
                return this.rttvarField;
            }
            set {
                this.rttvarField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint to {
            get {
                return this.toField;
            }
            set {
                this.toField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunRunstats {

        private NmapRunRunstatsFinished finishedField;

        private NmapRunRunstatsHosts hostsField;

        /// <remarks/>
        public NmapRunRunstatsFinished finished {
            get {
                return this.finishedField;
            }
            set {
                this.finishedField = value;
            }
        }

        /// <remarks/>
        public NmapRunRunstatsHosts hosts {
            get {
                return this.hostsField;
            }
            set {
                this.hostsField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunRunstatsFinished {

        private uint timeField;

        private string timestrField;

        private decimal elapsedField;

        private string summaryField;

        private string exitField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint time {
            get {
                return this.timeField;
            }
            set {
                this.timeField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string timestr {
            get {
                return this.timestrField;
            }
            set {
                this.timestrField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public decimal elapsed {
            get {
                return this.elapsedField;
            }
            set {
                this.elapsedField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string summary {
            get {
                return this.summaryField;
            }
            set {
                this.summaryField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string exit {
            get {
                return this.exitField;
            }
            set {
                this.exitField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NmapRunRunstatsHosts {

        private byte upField;

        private byte downField;

        private byte totalField;

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte up {
            get {
                return this.upField;
            }
            set {
                this.upField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte down {
            get {
                return this.downField;
            }
            set {
                this.downField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public byte total {
            get {
                return this.totalField;
            }
            set {
                this.totalField = value;
            }
        }
    }
}

