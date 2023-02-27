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
    public partial class NessusClientData_v2 : IInputFilter {

        private NessusPolicy policyField;

        private NessusReport reportField;

        /// <remarks/>
        public NessusPolicy Policy {
            get {
                return this.policyField;
            }
            set {
                this.policyField = value;
            }
        }

        /// <remarks/>
        public NessusReport Report {
            get {
                return this.reportField;
            }
            set {
                this.reportField = value;
            }
        }

        public IInputFilter PopulateFromFile(string fileName) {

            return (this);
        }

    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusPolicy {

        private string policyNameField;

        private NessusPolicyPreferences preferencesField;

        private NessusPolicyFamilyItem[] familySelectionField;

        private NessusPolicyPluginItem[] individualPluginSelectionField;

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
        public NessusPolicyPreferences Preferences {
            get {
                return this.preferencesField;
            }
            set {
                this.preferencesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("FamilyItem", IsNullable = false)]
        public NessusPolicyFamilyItem[] FamilySelection {
            get {
                return this.familySelectionField;
            }
            set {
                this.familySelectionField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("PluginItem", IsNullable = false)]
        public NessusPolicyPluginItem[] IndividualPluginSelection {
            get {
                return this.individualPluginSelectionField;
            }
            set {
                this.individualPluginSelectionField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusPolicyPreferences {

        private NessusPolicyPreferencesPreference[] serverPreferencesField;

        private NessusPolicyPreferencesItem[] pluginsPreferencesField;

        /// <remarks/>
        [XmlArrayItemAttribute("preference", IsNullable = false)]
        public NessusPolicyPreferencesPreference[] ServerPreferences {
            get {
                return this.serverPreferencesField;
            }
            set {
                this.serverPreferencesField = value;
            }
        }

        /// <remarks/>
        [XmlArrayItemAttribute("item", IsNullable = false)]
        public NessusPolicyPreferencesItem[] PluginsPreferences {
            get {
                return this.pluginsPreferencesField;
            }
            set {
                this.pluginsPreferencesField = value;
            }
        }
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusPolicyPreferencesPreference {

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
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusPolicyPreferencesItem {

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
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusPolicyFamilyItem {

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
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusPolicyPluginItem {

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
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusReport {

        private NessusReportReportHost[] reportHostField;

        private string nameField;

        /// <remarks/>
        [XmlElementAttribute("ReportHost")]
        public NessusReportReportHost[] ReportHost {
            get {
                return this.reportHostField;
            }
            set {
                this.reportHostField = value;
            }
        }

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
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusReportReportHost {

        private NessusReportReportHostTag[] hostPropertiesField;

        private NessusReportReportHostReportItem[] reportItemField;

        private string nameField;

        /// <remarks/>
        [XmlArrayItemAttribute("tag", IsNullable = false)]
        public NessusReportReportHostTag[] HostProperties {
            get {
                return this.hostPropertiesField;
            }
            set {
                this.hostPropertiesField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ReportItem")]
        public NessusReportReportHostReportItem[] ReportItem {
            get {
                return this.reportItemField;
            }
            set {
                this.reportItemField = value;
            }
        }

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
    }

    /// <remarks/>
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusReportReportHostTag {

        private string nameField;

        private string valueField;

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
        [XmlTextAttribute()]
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
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusReportReportHostReportItem {

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
        [XmlElementAttribute("agent", typeof(string))]
        [XmlElementAttribute("attachment", typeof(NessusReportReportHostReportItemAttachment))]
        [XmlElementAttribute("bid", typeof(uint))]
        [XmlElementAttribute("cert", typeof(uint))]
        [XmlElementAttribute("cert-cc", typeof(string))]
        [XmlElementAttribute("cisco-bug-id", typeof(string))]
        [XmlElementAttribute("cisco-sa", typeof(string))]
        [XmlElementAttribute("cpe", typeof(string))]
        [XmlElementAttribute("cve", typeof(string))]
        [XmlElementAttribute("cvss_base_score", typeof(decimal))]
        [XmlElementAttribute("cvss_temporal_score", typeof(decimal))]
        [XmlElementAttribute("cvss_temporal_vector", typeof(string))]
        [XmlElementAttribute("cvss_vector", typeof(string))]
        [XmlElementAttribute("cwe", typeof(ushort))]
        [XmlElementAttribute("description", typeof(string))]
        [XmlElementAttribute("exploit_available", typeof(bool))]
        [XmlElementAttribute("exploitability_ease", typeof(string))]
        [XmlElementAttribute("exploited_by_nessus", typeof(bool))]
        [XmlElementAttribute("fname", typeof(string))]
        [XmlElementAttribute("iavb", typeof(string))]
        [XmlElementAttribute("in_the_news", typeof(bool))]
        [XmlElementAttribute("osvdb", typeof(uint))]
        [XmlElementAttribute("patch_publication_date", typeof(string))]
        [XmlElementAttribute("plugin_modification_date", typeof(string))]
        [XmlElementAttribute("plugin_name", typeof(string))]
        [XmlElementAttribute("plugin_output", typeof(string))]
        [XmlElementAttribute("plugin_publication_date", typeof(string))]
        [XmlElementAttribute("plugin_type", typeof(string))]
        [XmlElementAttribute("risk_factor", typeof(string))]
        [XmlElementAttribute("script_version", typeof(string))]
        [XmlElementAttribute("see_also", typeof(string))]
        [XmlElementAttribute("solution", typeof(string))]
        [XmlElementAttribute("stig_severity", typeof(string))]
        [XmlElementAttribute("synopsis", typeof(string))]
        [XmlElementAttribute("unsupported_by_vendor", typeof(bool))]
        [XmlElementAttribute("vuln_publication_date", typeof(string))]
        [XmlElementAttribute("xref", typeof(string))]
        [XmlChoiceIdentifierAttribute("ItemsElementName")]
        public object[] Items {
            get {
                return this.itemsField;
            }
            set {
                this.itemsField = value;
            }
        }

        /// <remarks/>
        [XmlElementAttribute("ItemsElementName")]
        [XmlIgnoreAttribute()]
        public ItemsChoiceType[] ItemsElementName {
            get {
                return this.itemsElementNameField;
            }
            set {
                this.itemsElementNameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public ushort port {
            get {
                return this.portField;
            }
            set {
                this.portField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string svc_name {
            get {
                return this.svc_nameField;
            }
            set {
                this.svc_nameField = value;
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
        public byte severity {
            get {
                return this.severityField;
            }
            set {
                this.severityField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public uint pluginID {
            get {
                return this.pluginIDField;
            }
            set {
                this.pluginIDField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
        public string pluginName {
            get {
                return this.pluginNameField;
            }
            set {
                this.pluginNameField = value;
            }
        }

        /// <remarks/>
        [XmlAttributeAttribute()]
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
    [SerializableAttribute()]
    [DesignerCategoryAttribute("code")]
    [XmlTypeAttribute(AnonymousType = true)]
    public partial class NessusReportReportHostReportItemAttachment {

        private string nameField;

        private string typeField;

        private string valueField;

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

        /// <remarks/>
        [XmlTextAttribute()]
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
    [SerializableAttribute()]
    [XmlTypeAttribute(IncludeInSchema = false)]
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
        [XmlEnumAttribute("cert-cc")]
        certcc,

        /// <remarks/>
        [XmlEnumAttribute("cisco-bug-id")]
        ciscobugid,

        /// <remarks/>
        [XmlEnumAttribute("cisco-sa")]
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
}
