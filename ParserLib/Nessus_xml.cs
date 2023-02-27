/* 
   Copyright (C) Damien Golding (dgolding, 2020-1-20 - 07:35)
   
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
/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
[System.Xml.Serialization.XmlRootAttribute(Namespace = "", IsNullable = false)]
public partial class NessusRun {

    private NessusClientData_v2Policy policyField;

    private NessusClientData_v2Report reportField;

    /// <remarks/>
    public NessusClientData_v2Policy Policy {
        get {
            return this.policyField;
        }
        set {
            this.policyField = value;
        }
    }

    /// <remarks/>
    public NessusClientData_v2Report Report {
        get {
            return this.reportField;
        }
        set {
            this.reportField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2Policy {

    private string policyNameField;

    private NessusClientData_v2PolicyPreferences preferencesField;

    private NessusClientData_v2PolicyFamilyItem[] familySelectionField;

    private NessusClientData_v2PolicyPluginItem[] individualPluginSelectionField;

    /// <remarks/>
    public string policyName {
        get {
            return this.policyNameField;
        }
        set {
            this.policyNameField = value;
        }
    }

    /// <remarks/>
    public NessusClientData_v2PolicyPreferences Preferences {
        get {
            return this.preferencesField;
        }
        set {
            this.preferencesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("FamilyItem", IsNullable = false)]
    public NessusClientData_v2PolicyFamilyItem[] FamilySelection {
        get {
            return this.familySelectionField;
        }
        set {
            this.familySelectionField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("PluginItem", IsNullable = false)]
    public NessusClientData_v2PolicyPluginItem[] IndividualPluginSelection {
        get {
            return this.individualPluginSelectionField;
        }
        set {
            this.individualPluginSelectionField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2PolicyPreferences {

    private NessusClientData_v2PolicyPreferencesPreference[] serverPreferencesField;

    private NessusClientData_v2PolicyPreferencesItem[] pluginsPreferencesField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("preference", IsNullable = false)]
    public NessusClientData_v2PolicyPreferencesPreference[] ServerPreferences {
        get {
            return this.serverPreferencesField;
        }
        set {
            this.serverPreferencesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("item", IsNullable = false)]
    public NessusClientData_v2PolicyPreferencesItem[] PluginsPreferences {
        get {
            return this.pluginsPreferencesField;
        }
        set {
            this.pluginsPreferencesField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2PolicyPreferencesPreference {

    private string nameField;

    private string valueField;

    /// <remarks/>
    public string name {
        get {
            return this.nameField;
        }
        set {
            this.nameField = value;
        }
    }

    /// <remarks/>
    public string value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2PolicyPreferencesItem {

    private string pluginNameField;

    private uint pluginIdField;

    private string fullNameField;

    private string preferenceNameField;

    private string preferenceTypeField;

    private string preferenceValuesField;

    private string selectedValueField;

    /// <remarks/>
    public string pluginName {
        get {
            return this.pluginNameField;
        }
        set {
            this.pluginNameField = value;
        }
    }

    /// <remarks/>
    public uint pluginId {
        get {
            return this.pluginIdField;
        }
        set {
            this.pluginIdField = value;
        }
    }

    /// <remarks/>
    public string fullName {
        get {
            return this.fullNameField;
        }
        set {
            this.fullNameField = value;
        }
    }

    /// <remarks/>
    public string preferenceName {
        get {
            return this.preferenceNameField;
        }
        set {
            this.preferenceNameField = value;
        }
    }

    /// <remarks/>
    public string preferenceType {
        get {
            return this.preferenceTypeField;
        }
        set {
            this.preferenceTypeField = value;
        }
    }

    /// <remarks/>
    public string preferenceValues {
        get {
            return this.preferenceValuesField;
        }
        set {
            this.preferenceValuesField = value;
        }
    }

    /// <remarks/>
    public string selectedValue {
        get {
            return this.selectedValueField;
        }
        set {
            this.selectedValueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2PolicyFamilyItem {

    private string familyNameField;

    private string statusField;

    /// <remarks/>
    public string FamilyName {
        get {
            return this.familyNameField;
        }
        set {
            this.familyNameField = value;
        }
    }

    /// <remarks/>
    public string Status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2PolicyPluginItem {

    private ushort pluginIdField;

    private string pluginNameField;

    private string familyField;

    private string statusField;

    /// <remarks/>
    public ushort PluginId {
        get {
            return this.pluginIdField;
        }
        set {
            this.pluginIdField = value;
        }
    }

    /// <remarks/>
    public string PluginName {
        get {
            return this.pluginNameField;
        }
        set {
            this.pluginNameField = value;
        }
    }

    /// <remarks/>
    public string Family {
        get {
            return this.familyField;
        }
        set {
            this.familyField = value;
        }
    }

    /// <remarks/>
    public string Status {
        get {
            return this.statusField;
        }
        set {
            this.statusField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2Report {

    private NessusClientData_v2ReportReportHost[] reportHostField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ReportHost")]
    public NessusClientData_v2ReportReportHost[] ReportHost {
        get {
            return this.reportHostField;
        }
        set {
            this.reportHostField = value;
        }
    }

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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2ReportReportHost {

    private NessusClientData_v2ReportReportHostTag[] hostPropertiesField;

    private NessusClientData_v2ReportReportHostReportItem[] reportItemField;

    private string nameField;

    /// <remarks/>
    [System.Xml.Serialization.XmlArrayItemAttribute("tag", IsNullable = false)]
    public NessusClientData_v2ReportReportHostTag[] HostProperties {
        get {
            return this.hostPropertiesField;
        }
        set {
            this.hostPropertiesField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ReportItem")]
    public NessusClientData_v2ReportReportHostReportItem[] ReportItem {
        get {
            return this.reportItemField;
        }
        set {
            this.reportItemField = value;
        }
    }

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
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2ReportReportHostTag {

    private string nameField;

    private string valueField;

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
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2ReportReportHostReportItem {

    private object[] itemsField;

    private ItemsChoiceType[] itemsElementNameField;

    private ushort portField;

    private string svc_nameField;

    private string protocolField;

    private byte severityField;

    private uint pluginIDField;

    private string pluginNameField;

    private string pluginFamilyField;

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("agent", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("attachment", typeof(NessusClientData_v2ReportReportHostReportItemAttachment))]
    [System.Xml.Serialization.XmlElementAttribute("bid", typeof(uint))]
    [System.Xml.Serialization.XmlElementAttribute("cert", typeof(uint))]
    [System.Xml.Serialization.XmlElementAttribute("cert-cc", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cisco-bug-id", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cisco-sa", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cpe", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cve", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cvss_base_score", typeof(decimal))]
    [System.Xml.Serialization.XmlElementAttribute("cvss_temporal_score", typeof(decimal))]
    [System.Xml.Serialization.XmlElementAttribute("cvss_temporal_vector", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cvss_vector", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("cwe", typeof(ushort))]
    [System.Xml.Serialization.XmlElementAttribute("description", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("exploit_available", typeof(bool))]
    [System.Xml.Serialization.XmlElementAttribute("exploitability_ease", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("exploited_by_nessus", typeof(bool))]
    [System.Xml.Serialization.XmlElementAttribute("fname", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("iavb", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("in_the_news", typeof(bool))]
    [System.Xml.Serialization.XmlElementAttribute("osvdb", typeof(uint))]
    [System.Xml.Serialization.XmlElementAttribute("patch_publication_date", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("plugin_modification_date", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("plugin_name", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("plugin_output", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("plugin_publication_date", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("plugin_type", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("risk_factor", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("script_version", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("see_also", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("solution", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("stig_severity", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("synopsis", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("unsupported_by_vendor", typeof(bool))]
    [System.Xml.Serialization.XmlElementAttribute("vuln_publication_date", typeof(string))]
    [System.Xml.Serialization.XmlElementAttribute("xref", typeof(string))]
    [System.Xml.Serialization.XmlChoiceIdentifierAttribute("ItemsElementName")]
    public object[] Items {
        get {
            return this.itemsField;
        }
        set {
            this.itemsField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("ItemsElementName")]
    [System.Xml.Serialization.XmlIgnoreAttribute()]
    public ItemsChoiceType[] ItemsElementName {
        get {
            return this.itemsElementNameField;
        }
        set {
            this.itemsElementNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public ushort port {
        get {
            return this.portField;
        }
        set {
            this.portField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string svc_name {
        get {
            return this.svc_nameField;
        }
        set {
            this.svc_nameField = value;
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
    public byte severity {
        get {
            return this.severityField;
        }
        set {
            this.severityField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public uint pluginID {
        get {
            return this.pluginIDField;
        }
        set {
            this.pluginIDField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string pluginName {
        get {
            return this.pluginNameField;
        }
        set {
            this.pluginNameField = value;
        }
    }

    /// <remarks/>
    [System.Xml.Serialization.XmlAttributeAttribute()]
    public string pluginFamily {
        get {
            return this.pluginFamilyField;
        }
        set {
            this.pluginFamilyField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType = true)]
public partial class NessusClientData_v2ReportReportHostReportItemAttachment {

    private string nameField;

    private string typeField;

    private string valueField;

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

    /// <remarks/>
    [System.Xml.Serialization.XmlTextAttribute()]
    public string Value {
        get {
            return this.valueField;
        }
        set {
            this.valueField = value;
        }
    }
}

/// <remarks/>
[System.SerializableAttribute()]
[System.Xml.Serialization.XmlTypeAttribute(IncludeInSchema = false)]
public enum ItemsChoiceType {

    /// <remarks/>
    agent,

    /// <remarks/>
    attachment,

    /// <remarks/>
    bid,

    /// <remarks/>
    cert,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("cert-cc")]
    certcc,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("cisco-bug-id")]
    ciscobugid,

    /// <remarks/>
    [System.Xml.Serialization.XmlEnumAttribute("cisco-sa")]
    ciscosa,

    /// <remarks/>
    cpe,

    /// <remarks/>
    cve,

    /// <remarks/>
    cvss_base_score,

    /// <remarks/>
    cvss_temporal_score,

    /// <remarks/>
    cvss_temporal_vector,

    /// <remarks/>
    cvss_vector,

    /// <remarks/>
    cwe,

    /// <remarks/>
    description,

    /// <remarks/>
    exploit_available,

    /// <remarks/>
    exploitability_ease,

    /// <remarks/>
    exploited_by_nessus,

    /// <remarks/>
    fname,

    /// <remarks/>
    iavb,

    /// <remarks/>
    in_the_news,

    /// <remarks/>
    osvdb,

    /// <remarks/>
    patch_publication_date,

    /// <remarks/>
    plugin_modification_date,

    /// <remarks/>
    plugin_name,

    /// <remarks/>
    plugin_output,

    /// <remarks/>
    plugin_publication_date,

    /// <remarks/>
    plugin_type,

    /// <remarks/>
    risk_factor,

    /// <remarks/>
    script_version,

    /// <remarks/>
    see_also,

    /// <remarks/>
    solution,

    /// <remarks/>
    stig_severity,

    /// <remarks/>
    synopsis,

    /// <remarks/>
    unsupported_by_vendor,

    /// <remarks/>
    vuln_publication_date,

    /// <remarks/>
    xref,
}

