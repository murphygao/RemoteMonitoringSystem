﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy {
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
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult))]
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
    [System.Runtime.Serialization.DataContractAttribute(Name="WebsiteMonitoringResult", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService")]
    [System.SerializableAttribute()]
    public partial class WebsiteMonitoringResult : RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Collections.Generic.List<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringInfo> ListWebsiteMonitoringInfosField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private int TotalRecordsField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringInfo WebsiteMonitoringInfoField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public System.Collections.Generic.List<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringInfo> ListWebsiteMonitoringInfos {
            get {
                return this.ListWebsiteMonitoringInfosField;
            }
            set {
                if ((object.ReferenceEquals(this.ListWebsiteMonitoringInfosField, value) != true)) {
                    this.ListWebsiteMonitoringInfosField = value;
                    this.RaisePropertyChanged("ListWebsiteMonitoringInfos");
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringInfo WebsiteMonitoringInfo {
            get {
                return this.WebsiteMonitoringInfoField;
            }
            set {
                if ((object.ReferenceEquals(this.WebsiteMonitoringInfoField, value) != true)) {
                    this.WebsiteMonitoringInfoField = value;
                    this.RaisePropertyChanged("WebsiteMonitoringInfo");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="WebsiteMonitoringInfo", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService.Model")]
    [System.SerializableAttribute()]
    public partial class WebsiteMonitoringInfo : RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.RmsWebsiteMonitoring {
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientIPAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WebsiteMonitoringProtocolValueField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WebsiteMonitoringTypeValueField;
        
        [System.Runtime.Serialization.DataMemberAttribute()]
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
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string ClientIPAddress {
            get {
                return this.ClientIPAddressField;
            }
            set {
                if ((object.ReferenceEquals(this.ClientIPAddressField, value) != true)) {
                    this.ClientIPAddressField = value;
                    this.RaisePropertyChanged("ClientIPAddress");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebsiteMonitoringProtocolValue {
            get {
                return this.WebsiteMonitoringProtocolValueField;
            }
            set {
                if ((object.ReferenceEquals(this.WebsiteMonitoringProtocolValueField, value) != true)) {
                    this.WebsiteMonitoringProtocolValueField = value;
                    this.RaisePropertyChanged("WebsiteMonitoringProtocolValue");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute()]
        public string WebsiteMonitoringTypeValue {
            get {
                return this.WebsiteMonitoringTypeValueField;
            }
            set {
                if ((object.ReferenceEquals(this.WebsiteMonitoringTypeValueField, value) != true)) {
                    this.WebsiteMonitoringTypeValueField = value;
                    this.RaisePropertyChanged("WebsiteMonitoringTypeValue");
                }
            }
        }
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RmsWebsiteMonitoring", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.DAL")]
    [System.SerializableAttribute()]
    [System.Runtime.Serialization.KnownTypeAttribute(typeof(RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringInfo))]
    public partial class RmsWebsiteMonitoring : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private int WebsiteMonitoringIdField;
        
        private int ClientIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WebsiteMonitoringTypeIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string WebsiteMonitoringProtocolIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> PortNumberField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DomainNameField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<bool> EnableField;
        
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
        public int WebsiteMonitoringId {
            get {
                return this.WebsiteMonitoringIdField;
            }
            set {
                if ((this.WebsiteMonitoringIdField.Equals(value) != true)) {
                    this.WebsiteMonitoringIdField = value;
                    this.RaisePropertyChanged("WebsiteMonitoringId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(IsRequired=true, Order=1)]
        public int ClientId {
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
        public string WebsiteMonitoringTypeId {
            get {
                return this.WebsiteMonitoringTypeIdField;
            }
            set {
                if ((object.ReferenceEquals(this.WebsiteMonitoringTypeIdField, value) != true)) {
                    this.WebsiteMonitoringTypeIdField = value;
                    this.RaisePropertyChanged("WebsiteMonitoringTypeId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
        public string WebsiteMonitoringProtocolId {
            get {
                return this.WebsiteMonitoringProtocolIdField;
            }
            set {
                if ((object.ReferenceEquals(this.WebsiteMonitoringProtocolIdField, value) != true)) {
                    this.WebsiteMonitoringProtocolIdField = value;
                    this.RaisePropertyChanged("WebsiteMonitoringProtocolId");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
        public System.Nullable<int> PortNumber {
            get {
                return this.PortNumberField;
            }
            set {
                if ((this.PortNumberField.Equals(value) != true)) {
                    this.PortNumberField = value;
                    this.RaisePropertyChanged("PortNumber");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
        public string DomainName {
            get {
                return this.DomainNameField;
            }
            set {
                if ((object.ReferenceEquals(this.DomainNameField, value) != true)) {
                    this.DomainNameField = value;
                    this.RaisePropertyChanged("DomainName");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
        public System.Nullable<bool> Enable {
            get {
                return this.EnableField;
            }
            set {
                if ((this.EnableField.Equals(value) != true)) {
                    this.EnableField = value;
                    this.RaisePropertyChanged("Enable");
                }
            }
        }
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=10)]
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="WebsiteMonitoringProxy.IWebsiteMonitoringService")]
    public interface IWebsiteMonitoringService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/TestConnection", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/TestConnectionResponse")]
        void TestConnection();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/TestConnection", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/TestConnectionResponse")]
        System.Threading.Tasks.Task TestConnectionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/ListWebsiteMonitoringsByClient", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/ListWebsiteMonitoringsByClientRespon" +
            "se")]
        RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult ListWebsiteMonitoringsByClient(RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.JQueryDataTableParamModel param, int clientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/ListWebsiteMonitoringsByClient", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/ListWebsiteMonitoringsByClientRespon" +
            "se")]
        System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult> ListWebsiteMonitoringsByClientAsync(RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.JQueryDataTableParamModel param, int clientID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/GetWebsiteMonitoring", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/GetWebsiteMonitoringResponse")]
        RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult GetWebsiteMonitoring(int websiteMonitoringID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/GetWebsiteMonitoring", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/GetWebsiteMonitoringResponse")]
        System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult> GetWebsiteMonitoringAsync(int websiteMonitoringID);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/UpdateWebsiteMonitoring", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/UpdateWebsiteMonitoringResponse")]
        RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result UpdateWebsiteMonitoring(System.Nullable<int> id, string m, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID, System.Nullable<int> portNumber, string domainName, bool enable, string updatedBy);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/UpdateWebsiteMonitoring", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/UpdateWebsiteMonitoringResponse")]
        System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result> UpdateWebsiteMonitoringAsync(System.Nullable<int> id, string m, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID, System.Nullable<int> portNumber, string domainName, bool enable, string updatedBy);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/DeleteWebsiteMonitoring", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/DeleteWebsiteMonitoringResponse")]
        RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result DeleteWebsiteMonitoring(int websiteMonitoringID, string updatedBy);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IWebsiteMonitoringService/DeleteWebsiteMonitoring", ReplyAction="http://tempuri.org/IWebsiteMonitoringService/DeleteWebsiteMonitoringResponse")]
        System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result> DeleteWebsiteMonitoringAsync(int websiteMonitoringID, string updatedBy);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IWebsiteMonitoringServiceChannel : RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.IWebsiteMonitoringService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class WebsiteMonitoringServiceClient : System.ServiceModel.ClientBase<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.IWebsiteMonitoringService>, RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.IWebsiteMonitoringService {
        
        public WebsiteMonitoringServiceClient() {
        }
        
        public WebsiteMonitoringServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public WebsiteMonitoringServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebsiteMonitoringServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public WebsiteMonitoringServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void TestConnection() {
            base.Channel.TestConnection();
        }
        
        public System.Threading.Tasks.Task TestConnectionAsync() {
            return base.Channel.TestConnectionAsync();
        }
        
        public RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult ListWebsiteMonitoringsByClient(RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.JQueryDataTableParamModel param, int clientID) {
            return base.Channel.ListWebsiteMonitoringsByClient(param, clientID);
        }
        
        public System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult> ListWebsiteMonitoringsByClientAsync(RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.JQueryDataTableParamModel param, int clientID) {
            return base.Channel.ListWebsiteMonitoringsByClientAsync(param, clientID);
        }
        
        public RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult GetWebsiteMonitoring(int websiteMonitoringID) {
            return base.Channel.GetWebsiteMonitoring(websiteMonitoringID);
        }
        
        public System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.WebsiteMonitoringResult> GetWebsiteMonitoringAsync(int websiteMonitoringID) {
            return base.Channel.GetWebsiteMonitoringAsync(websiteMonitoringID);
        }
        
        public RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result UpdateWebsiteMonitoring(System.Nullable<int> id, string m, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID, System.Nullable<int> portNumber, string domainName, bool enable, string updatedBy) {
            return base.Channel.UpdateWebsiteMonitoring(id, m, clientID, websiteMonitoringTypeID, websiteMonitoringProtocolID, portNumber, domainName, enable, updatedBy);
        }
        
        public System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result> UpdateWebsiteMonitoringAsync(System.Nullable<int> id, string m, int clientID, string websiteMonitoringTypeID, string websiteMonitoringProtocolID, System.Nullable<int> portNumber, string domainName, bool enable, string updatedBy) {
            return base.Channel.UpdateWebsiteMonitoringAsync(id, m, clientID, websiteMonitoringTypeID, websiteMonitoringProtocolID, portNumber, domainName, enable, updatedBy);
        }
        
        public RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result DeleteWebsiteMonitoring(int websiteMonitoringID, string updatedBy) {
            return base.Channel.DeleteWebsiteMonitoring(websiteMonitoringID, updatedBy);
        }
        
        public System.Threading.Tasks.Task<RMS.Centralize.WebSite.Proxy.WebsiteMonitoringProxy.Result> DeleteWebsiteMonitoringAsync(int websiteMonitoringID, string updatedBy) {
            return base.Channel.DeleteWebsiteMonitoringAsync(websiteMonitoringID, updatedBy);
        }
    }
}