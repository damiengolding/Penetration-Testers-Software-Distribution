/* 
   Copyright (C) Damien Golding (dgolding, 2020-1-20 - 07:17)
   
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


// NOTE: Generated code may require at least .NET Framework 4.5 or .NET Core/Standard 2.0.
using System;
using System.IO;
using System.Xml.Serialization;
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class NmapRun {

    public static NmapRun FromFile(string nmapFile) {

        try {
            XmlSerializer serializer = new XmlSerializer(typeof(NmapRun));
            NmapRun ret;
            using (Stream reader = new FileStream(nmapFile, FileMode.Open)) {
                ret = (NmapRun)serializer.Deserialize(reader);
            }
            return (ret);
        }
        catch (Exception ex) {
            Console.WriteLine("[-] Exception: {0}", ex.Message);
        }
        return (null);
    }
    public static NmapRun FromString(string nmapXml) {
        try {
            XmlSerializer serializer = new XmlSerializer(typeof(NmapRun));
            NmapRun ret;
            using (StringReader reader = new StringReader(nmapXml)) {
                ret = (NmapRun)serializer.Deserialize(reader);
            }
            return (ret);
        }
        catch (Exception ex) {
            Console.WriteLine("[-] Exception: {0}", ex.Message);
        }
        return (null);
    }

    private nmaprunScaninfo scaninfoField;

    private nmaprunVerbose verboseField;

    private nmaprunDebugging debuggingField;

    private nmaprunHost[] hostField;

    private nmaprunRunstats runstatsField;

    private string scannerField;

    private string argsField;

    private uint startField;

    private string startstrField;

    private decimal versionField;

    private decimal xmloutputversionField;

    /// <remarks/>
    public nmaprunScaninfo scaninfo {
        get {
            return this.scaninfoField;
        }
        set {
            this.scaninfoField = value;
        }
    }

    /// <remarks/>
    public nmaprunVerbose verbose {
        get {
            return this.verboseField;
        }
        set {
            this.verboseField = value;
        }
    }

    /// <remarks/>
    public nmaprunDebugging debugging {
        get {
            return this.debuggingField;
        }
        set {
            this.debuggingField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("host")]
    public nmaprunHost[] host {
        get {
            return this.hostField;
        }
        set {
            this.hostField = value;
        }
    }

    /// <remarks/>
    public nmaprunRunstats runstats {
        get {
            return this.runstatsField;
        }
        set {
            this.runstatsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string scanner {
        get {
            return this.scannerField;
        }
        set {
            this.scannerField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string args {
        get {
            return this.argsField;
        }
        set {
            this.argsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint start {
        get {
            return this.startField;
        }
        set {
            this.startField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string startstr {
        get {
            return this.startstrField;
        }
        set {
            this.startstrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal version {
        get {
            return this.versionField;
        }
        set {
            this.versionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunScaninfo {

    private string typeField;

    private string protocolField;

    private ushort numservicesField;

    private string servicesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string type {
        get {
            return this.typeField;
        }
        set {
            this.typeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string protocol {
        get {
            return this.protocolField;
        }
        set {
            this.protocolField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort numservices {
        get {
            return this.numservicesField;
        }
        set {
            this.numservicesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunVerbose {

    private byte levelField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunDebugging {

    private byte levelField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHost {

    private nmaprunHostStatus statusField;

    private nmaprunHostAddress addressField;

    private nmaprunHostHostnames hostnamesField;

    private nmaprunHostPorts portsField;

    private nmaprunHostHostscript[] hostscriptField;

    private nmaprunHostTimes[] timesField;

    private uint starttimeField;

    private uint endtimeField;

    /// <remarks/>
    public nmaprunHostStatus status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }

    /// <remarks/>
    public nmaprunHostAddress address {
        get {
            return this.addressField;
        }
        set {
            this.addressField = value;
        }
    }

    /// <remarks/>
    public nmaprunHostHostnames hostnames {
        get {
            return this.hostnamesField;
        }
        set {
            this.hostnamesField = value;
        }
    }

    /// <remarks/>
    public nmaprunHostPorts ports {
        get {
            return this.portsField;
        }
        set {
            this.portsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("hostscript")]
    public nmaprunHostHostscript[] hostscript {
        get {
            return this.hostscriptField;
        }
        set {
            this.hostscriptField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("times")]
    public nmaprunHostTimes[] times {
        get {
            return this.timesField;
        }
        set {
            this.timesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint starttime {
        get {
            return this.starttimeField;
        }
        set {
            this.starttimeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostStatus {

    private string stateField;

    private string reasonField;

    private byte reason_ttlField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string state {
        get {
            return this.stateField;
        }
        set {
            this.stateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string reason {
        get {
            return this.reasonField;
        }
        set {
            this.reasonField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostAddress {

    private string addrField;

    private string addrtypeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string addr {
        get {
            return this.addrField;
        }
        set {
            this.addrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostHostnames {

    private nmaprunHostHostnamesHostname hostnameField;

    /// <remarks/>
    public nmaprunHostHostnamesHostname hostname {
        get {
            return this.hostnameField;
        }
        set {
            this.hostnameField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostHostnamesHostname {

    private string nameField;

    private string typeField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostPorts {

    private nmaprunHostPortsExtraports extraportsField;

    private nmaprunHostPortsPort[] portField;

    /// <remarks/>
    public nmaprunHostPortsExtraports extraports {
        get {
            return this.extraportsField;
        }
        set {
            this.extraportsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("port")]
    public nmaprunHostPortsPort[] port {
        get {
            return this.portField;
        }
        set {
            this.portField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostPortsExtraports {

    private nmaprunHostPortsExtraportsExtrareasons extrareasonsField;

    private string stateField;

    private ushort countField;

    /// <remarks/>
    public nmaprunHostPortsExtraportsExtrareasons extrareasons {
        get {
            return this.extrareasonsField;
        }
        set {
            this.extrareasonsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string state {
        get {
            return this.stateField;
        }
        set {
            this.stateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostPortsExtraportsExtrareasons {

    private string reasonField;

    private ushort countField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string reason {
        get {
            return this.reasonField;
        }
        set {
            this.reasonField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostPortsPort {

    private nmaprunHostPortsPortState stateField;

    private nmaprunHostPortsPortService serviceField;

    private string protocolField;

    private ushort portidField;

    /// <remarks/>
    public nmaprunHostPortsPortState state {
        get {
            return this.stateField;
        }
        set {
            this.stateField = value;
        }
    }

    /// <remarks/>
    public nmaprunHostPortsPortService service {
        get {
            return this.serviceField;
        }
        set {
            this.serviceField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string protocol {
        get {
            return this.protocolField;
        }
        set {
            this.protocolField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostPortsPortState {

    private string stateField;

    private string reasonField;

    private byte reason_ttlField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string state {
        get {
            return this.stateField;
        }
        set {
            this.stateField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string reason {
        get {
            return this.reasonField;
        }
        set {
            this.reasonField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostPortsPortService {

    private string nameField;

    private string methodField;

    private byte confField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string method {
        get {
            return this.methodField;
        }
        set {
            this.methodField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostHostscript {

    private nmaprunHostHostscriptScript scriptField;

    /// <remarks/>
    public nmaprunHostHostscriptScript script {
        get {
            return this.scriptField;
        }
        set {
            this.scriptField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostHostscriptScript {

    private string idField;

    private string outputField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string id {
        get {
            return this.idField;
        }
        set {
            this.idField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunHostTimes {

    private uint srttField;

    private ushort rttvarField;

    private uint toField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint srtt {
        get {
            return this.srttField;
        }
        set {
            this.srttField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort rttvar {
        get {
            return this.rttvarField;
        }
        set {
            this.rttvarField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunRunstats {

    private nmaprunRunstatsFinished finishedField;

    private nmaprunRunstatsHosts hostsField;

    /// <remarks/>
    public nmaprunRunstatsFinished finished {
        get {
            return this.finishedField;
        }
        set {
            this.finishedField = value;
        }
    }

    /// <remarks/>
    public nmaprunRunstatsHosts hosts {
        get {
            return this.hostsField;
        }
        set {
            this.hostsField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunRunstatsFinished {

    private uint timeField;

    private string timestrField;

    private decimal elapsedField;

    private string summaryField;

    private string exitField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint time {
        get {
            return this.timeField;
        }
        set {
            this.timeField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string timestr {
        get {
            return this.timestrField;
        }
        set {
            this.timestrField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public decimal elapsed {
        get {
            return this.elapsedField;
        }
        set {
            this.elapsedField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string summary {
        get {
            return this.summaryField;
        }
        set {
            this.summaryField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
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
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class nmaprunRunstatsHosts {

    private byte upField;

    private byte downField;

    private byte totalField;

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte up {
        get {
            return this.upField;
        }
        set {
            this.upField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte down {
        get {
            return this.downField;
        }
        set {
            this.downField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public byte total {
        get {
            return this.totalField;
        }
        set {
            this.totalField = value;
        }
    }
}

