﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Centralize.WebSite.Proxy.SummaryReportProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="JQueryDataTableParamModel", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService.Model")]
    [System.SerializableAttribute()]
    public partial class JQueryDataTableParamModel : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int iColumnsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int iDisplayLengthField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int iDisplayStartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string iSortColumnField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int iSortingColsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string sColumnsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string sEchoField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string sSearchField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int iColumns {
            get {
                return this.iColumnsField;
            }
            set {
                if ((this.iColumnsField.Equals(value) != true)) {
                    this.iColumnsField = value;
                    this.RaisePropertyChanged("iColumns");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int iDisplayLength {
            get {
                return this.iDisplayLengthField;
            }
            set {
                if ((this.iDisplayLengthField.Equals(value) != true)) {
                    this.iDisplayLengthField = value;
                    this.RaisePropertyChanged("iDisplayLength");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int iDisplayStart {
            get {
                return this.iDisplayStartField;
            }
            set {
                if ((this.iDisplayStartField.Equals(value) != true)) {
                    this.iDisplayStartField = value;
                    this.RaisePropertyChanged("iDisplayStart");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string iSortColumn {
            get {
                return this.iSortColumnField;
            }
            set {
                if ((object.ReferenceEquals(this.iSortColumnField, value) != true)) {
                    this.iSortColumnField = value;
                    this.RaisePropertyChanged("iSortColumn");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int iSortingCols {
            get {
                return this.iSortingColsField;
            }
            set {
                if ((this.iSortingColsField.Equals(value) != true)) {
                    this.iSortingColsField = value;
                    this.RaisePropertyChanged("iSortingCols");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string sColumns {
            get {
                return this.sColumnsField;
            }
            set {
                if ((object.ReferenceEquals(this.sColumnsField, value) != true)) {
                    this.sColumnsField = value;
                    this.RaisePropertyChanged("sColumns");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string sEcho {
            get {
                return this.sEchoField;
            }
            set {
                if ((object.ReferenceEquals(this.sEchoField, value) != true)) {
                    this.sEchoField = value;
                    this.RaisePropertyChanged("sEcho");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string sSearch {
            get {
                return this.sSearchField;
            }
            set {
                if ((object.ReferenceEquals(this.sSearchField, value) != true)) {
                    this.sSearchField = value;
                    this.RaisePropertyChanged("sSearch");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService.Model")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RMS.Centralize.WebSite.Proxy.SummaryReportProxy.SummaryMonitoringResult))]
    public partial class Result : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ErrorMessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private bool IsSuccessField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorCode {
            get {
                return this.ErrorCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorCodeField, value) != true)) {
                    this.ErrorCodeField = value;
                    this.RaisePropertyChanged("ErrorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ErrorMessage {
            get {
                return this.ErrorMessageField;
            }
            set {
                if ((object.ReferenceEquals(this.ErrorMessageField, value) != true)) {
                    this.ErrorMessageField = value;
                    this.RaisePropertyChanged("ErrorMessage");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public bool IsSuccess {
            get {
                return this.IsSuccessField;
            }
            set {
                if ((this.IsSuccessField.Equals(value) != true)) {
                    this.IsSuccessField = value;
                    this.RaisePropertyChanged("IsSuccess");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="SummaryMonitoringResult", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService")]
    [System.SerializableAttribute()]
    public partial class SummaryMonitoringResult : RMS.Centralize.WebSite.Proxy.SummaryReportProxy.Result {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ReportSummaryMonitoring> ListSummaryMonitoringsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ReportSummaryMonitoring SummaryMonitoringField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalRecordsField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ReportSummaryMonitoring> ListSummaryMonitorings {
            get {
                return this.ListSummaryMonitoringsField;
            }
            set {
                if ((object.ReferenceEquals(this.ListSummaryMonitoringsField, value) != true)) {
                    this.ListSummaryMonitoringsField = value;
                    this.RaisePropertyChanged("ListSummaryMonitorings");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ReportSummaryMonitoring SummaryMonitoring {
            get {
                return this.SummaryMonitoringField;
            }
            set {
                if ((object.ReferenceEquals(this.SummaryMonitoringField, value) != true)) {
                    this.SummaryMonitoringField = value;
                    this.RaisePropertyChanged("SummaryMonitoring");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public int TotalRecords {
            get {
                return this.TotalRecordsField;
            }
            set {
                if ((this.TotalRecordsField.Equals(value) != true)) {
                    this.TotalRecordsField = value;
                    this.RaisePropertyChanged("TotalRecords");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="ReportSummaryMonitoring", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService.Model")]
    [System.SerializableAttribute()]
    public partial class ReportSummaryMonitoring : RMS.Centralize.WebSite.Proxy.SummaryReportProxy.RmsReportSummaryMonitoring {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ColorCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ColorTagEndField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ColorTagStartField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LevelNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string LocationField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> RowNumField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ColorCode {
            get {
                return this.ColorCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ColorCodeField, value) != true)) {
                    this.ColorCodeField = value;
                    this.RaisePropertyChanged("ColorCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ColorTagEnd {
            get {
                return this.ColorTagEndField;
            }
            set {
                if ((object.ReferenceEquals(this.ColorTagEndField, value) != true)) {
                    this.ColorTagEndField = value;
                    this.RaisePropertyChanged("ColorTagEnd");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ColorTagStart {
            get {
                return this.ColorTagStartField;
            }
            set {
                if ((object.ReferenceEquals(this.ColorTagStartField, value) != true)) {
                    this.ColorTagStartField = value;
                    this.RaisePropertyChanged("ColorTagStart");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string LevelName {
            get {
                return this.LevelNameField;
            }
            set {
                if ((object.ReferenceEquals(this.LevelNameField, value) != true)) {
                    this.LevelNameField = value;
                    this.RaisePropertyChanged("LevelName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string Location {
            get {
                return this.LocationField;
            }
            set {
                if ((object.ReferenceEquals(this.LocationField, value) != true)) {
                    this.LocationField = value;
                    this.RaisePropertyChanged("Location");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> RowNum {
            get {
                return this.RowNumField;
            }
            set {
                if ((this.RowNumField.Equals(value) != true)) {
                    this.RowNumField = value;
                    this.RaisePropertyChanged("RowNum");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RmsReportSummaryMonitoring", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.DAL")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ReportSummaryMonitoring))]
    public partial class RmsReportSummaryMonitoring : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<long> RawIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> ClientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientIpAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> LocationIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> DeviceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceDescriptionField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MessageGroupIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageGroupCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageGroupNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MessageIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> StatusField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> MessageDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageRemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MonitoringProfileDeviceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> SeverityLevelIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> EventDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> CreatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CreatedByField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> UpdatedDateField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string UpdatedByField;
        
        [global::System.ComponentModel.BrowsableAttribute(false)]
        public System.Runtime.Serialization.ExtensionDataObject ExtensionData {
            get {
                return this.extensionDataField;
            }
            set {
                this.extensionDataField = value;
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true)]
        public long Id {
            get {
                return this.IdField;
            }
            set {
                if ((this.IdField.Equals(value) != true)) {
                    this.IdField = value;
                    this.RaisePropertyChanged("Id");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Nullable<long> RawId {
            get {
                return this.RawIdField;
            }
            set {
                if ((this.RawIdField.Equals(value) != true)) {
                    this.RawIdField = value;
                    this.RaisePropertyChanged("RawId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public System.Nullable<int> ClientId {
            get {
                return this.ClientIdField;
            }
            set {
                if ((this.ClientIdField.Equals(value) != true)) {
                    this.ClientIdField = value;
                    this.RaisePropertyChanged("ClientId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string ClientCode {
            get {
                return this.ClientCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientCodeField, value) != true)) {
                    this.ClientCodeField = value;
                    this.RaisePropertyChanged("ClientCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public string ClientIpAddress {
            get {
                return this.ClientIpAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientIpAddressField, value) != true)) {
                    this.ClientIpAddressField = value;
                    this.RaisePropertyChanged("ClientIpAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public System.Nullable<int> LocationId {
            get {
                return this.LocationIdField;
            }
            set {
                if ((this.LocationIdField.Equals(value) != true)) {
                    this.LocationIdField = value;
                    this.RaisePropertyChanged("LocationId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public System.Nullable<int> DeviceId {
            get {
                return this.DeviceIdField;
            }
            set {
                if ((this.DeviceIdField.Equals(value) != true)) {
                    this.DeviceIdField = value;
                    this.RaisePropertyChanged("DeviceId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
        public string DeviceCode {
            get {
                return this.DeviceCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.DeviceCodeField, value) != true)) {
                    this.DeviceCodeField = value;
                    this.RaisePropertyChanged("DeviceCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
        public string DeviceDescription {
            get {
                return this.DeviceDescriptionField;
            }
            set {
                if ((object.ReferenceEquals(this.DeviceDescriptionField, value) != true)) {
                    this.DeviceDescriptionField = value;
                    this.RaisePropertyChanged("DeviceDescription");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
        public System.Nullable<int> MessageGroupId {
            get {
                return this.MessageGroupIdField;
            }
            set {
                if ((this.MessageGroupIdField.Equals(value) != true)) {
                    this.MessageGroupIdField = value;
                    this.RaisePropertyChanged("MessageGroupId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
        public string MessageGroupCode {
            get {
                return this.MessageGroupCodeField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageGroupCodeField, value) != true)) {
                    this.MessageGroupCodeField = value;
                    this.RaisePropertyChanged("MessageGroupCode");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=11)]
        public string MessageGroupName {
            get {
                return this.MessageGroupNameField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageGroupNameField, value) != true)) {
                    this.MessageGroupNameField = value;
                    this.RaisePropertyChanged("MessageGroupName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=12)]
        public System.Nullable<int> MessageId {
            get {
                return this.MessageIdField;
            }
            set {
                if ((this.MessageIdField.Equals(value) != true)) {
                    this.MessageIdField = value;
                    this.RaisePropertyChanged("MessageId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=13)]
        public string Message {
            get {
                return this.MessageField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageField, value) != true)) {
                    this.MessageField = value;
                    this.RaisePropertyChanged("Message");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=14)]
        public System.Nullable<int> Status {
            get {
                return this.StatusField;
            }
            set {
                if ((this.StatusField.Equals(value) != true)) {
                    this.StatusField = value;
                    this.RaisePropertyChanged("Status");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=15)]
        public System.Nullable<System.DateTime> MessageDateTime {
            get {
                return this.MessageDateTimeField;
            }
            set {
                if ((this.MessageDateTimeField.Equals(value) != true)) {
                    this.MessageDateTimeField = value;
                    this.RaisePropertyChanged("MessageDateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=16)]
        public string MessageRemark {
            get {
                return this.MessageRemarkField;
            }
            set {
                if ((object.ReferenceEquals(this.MessageRemarkField, value) != true)) {
                    this.MessageRemarkField = value;
                    this.RaisePropertyChanged("MessageRemark");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=17)]
        public System.Nullable<int> MonitoringProfileDeviceId {
            get {
                return this.MonitoringProfileDeviceIdField;
            }
            set {
                if ((this.MonitoringProfileDeviceIdField.Equals(value) != true)) {
                    this.MonitoringProfileDeviceIdField = value;
                    this.RaisePropertyChanged("MonitoringProfileDeviceId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=18)]
        public System.Nullable<int> SeverityLevelId {
            get {
                return this.SeverityLevelIdField;
            }
            set {
                if ((this.SeverityLevelIdField.Equals(value) != true)) {
                    this.SeverityLevelIdField = value;
                    this.RaisePropertyChanged("SeverityLevelId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=19)]
        public System.Nullable<System.DateTime> EventDateTime {
            get {
                return this.EventDateTimeField;
            }
            set {
                if ((this.EventDateTimeField.Equals(value) != true)) {
                    this.EventDateTimeField = value;
                    this.RaisePropertyChanged("EventDateTime");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=20)]
        public System.Nullable<System.DateTime> CreatedDate {
            get {
                return this.CreatedDateField;
            }
            set {
                if ((this.CreatedDateField.Equals(value) != true)) {
                    this.CreatedDateField = value;
                    this.RaisePropertyChanged("CreatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=21)]
        public string CreatedBy {
            get {
                return this.CreatedByField;
            }
            set {
                if ((object.ReferenceEquals(this.CreatedByField, value) != true)) {
                    this.CreatedByField = value;
                    this.RaisePropertyChanged("CreatedBy");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=22)]
        public System.Nullable<System.DateTime> UpdatedDate {
            get {
                return this.UpdatedDateField;
            }
            set {
                if ((this.UpdatedDateField.Equals(value) != true)) {
                    this.UpdatedDateField = value;
                    this.RaisePropertyChanged("UpdatedDate");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=23)]
        public string UpdatedBy {
            get {
                return this.UpdatedByField;
            }
            set {
                if ((object.ReferenceEquals(this.UpdatedByField, value) != true)) {
                    this.UpdatedByField = value;
                    this.RaisePropertyChanged("UpdatedBy");
                }
            }
        }
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="SummaryReportProxy.ISummaryReportService")]
    public interface ISummaryReportService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISummaryReportService/SearchSummaryMonitoring", ReplyAction="http://tempuri.org/ISummaryReportService/SearchSummaryMonitoringResponse")]
        RMS.Centralize.WebSite.Proxy.SummaryReportProxy.SummaryMonitoringResult SearchSummaryMonitoring(RMS.Centralize.WebSite.Proxy.SummaryReportProxy.JQueryDataTableParamModel param, System.Nullable<System.DateTime> txtStartMessageDate, System.Nullable<System.DateTime> txtEndMessageDate, string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, System.Nullable<bool> ddlMessageStatus);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/ISummaryReportService/SearchSummaryMonitoring", ReplyAction="http://tempuri.org/ISummaryReportService/SearchSummaryMonitoringResponse")]
        System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.SummaryReportProxy.SummaryMonitoringResult> SearchSummaryMonitoringAsync(RMS.Centralize.WebSite.Proxy.SummaryReportProxy.JQueryDataTableParamModel param, System.Nullable<System.DateTime> txtStartMessageDate, System.Nullable<System.DateTime> txtEndMessageDate, string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, System.Nullable<bool> ddlMessageStatus);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface ISummaryReportServiceChannel : RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ISummaryReportService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class SummaryReportServiceClient : System.ServiceModel.ClientBase<RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ISummaryReportService>, RMS.Centralize.WebSite.Proxy.SummaryReportProxy.ISummaryReportService {
        
        public SummaryReportServiceClient() {
        }
        
        public SummaryReportServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public SummaryReportServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SummaryReportServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public SummaryReportServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public RMS.Centralize.WebSite.Proxy.SummaryReportProxy.SummaryMonitoringResult SearchSummaryMonitoring(RMS.Centralize.WebSite.Proxy.SummaryReportProxy.JQueryDataTableParamModel param, System.Nullable<System.DateTime> txtStartMessageDate, System.Nullable<System.DateTime> txtEndMessageDate, string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, System.Nullable<bool> ddlMessageStatus) {
            return base.Channel.SearchSummaryMonitoring(param, txtStartMessageDate, txtEndMessageDate, txtClientCode, txtLocation, ddlMessageGroup, txtMessage, ddlMessageStatus);
        }
        
        public System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.SummaryReportProxy.SummaryMonitoringResult> SearchSummaryMonitoringAsync(RMS.Centralize.WebSite.Proxy.SummaryReportProxy.JQueryDataTableParamModel param, System.Nullable<System.DateTime> txtStartMessageDate, System.Nullable<System.DateTime> txtEndMessageDate, string txtClientCode, string txtLocation, string ddlMessageGroup, string txtMessage, System.Nullable<bool> ddlMessageStatus) {
            return base.Channel.SearchSummaryMonitoringAsync(param, txtStartMessageDate, txtEndMessageDate, txtClientCode, txtLocation, ddlMessageGroup, txtMessage, ddlMessageStatus);
        }
    }
}
