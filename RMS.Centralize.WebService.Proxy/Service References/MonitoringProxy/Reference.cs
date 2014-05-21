﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Centralize.WebService.Proxy.MonitoringProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RmsReportMonitoringRaw", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.DAL")]
    [System.SerializableAttribute()]
    public partial class RmsReportMonitoringRaw : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        private long IdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string ClientIpAddressField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string DeviceCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageGroupCodeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> MessageDateTimeField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string MessageRemarkField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<int> MonitoringProfileDeviceIdField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private System.Nullable<System.DateTime> CreatedDateField;
        
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=1)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=2)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=3)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=4)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=5)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=6)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=7)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=8)]
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
        
        [System.Runtime.Serialization.DataMemberAttribute(Order=9)]
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
        
        public event System.ComponentModel.PropertyChangedEventHandler PropertyChanged;
        
        protected void RaisePropertyChanged(string propertyName) {
            System.ComponentModel.PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
            if ((propertyChanged != null)) {
                propertyChanged(this, new System.ComponentModel.PropertyChangedEventArgs(propertyName));
            }
        }
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="MonitoringProxy.IMonitoringService")]
    public interface IMonitoringService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/AddMessage", ReplyAction="http://tempuri.org/IMonitoringService/AddMessageResponse")]
        void AddMessage(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/AddMessage", ReplyAction="http://tempuri.org/IMonitoringService/AddMessageResponse")]
        System.Threading.Tasks.Task AddMessageAsync(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/AddMessages", ReplyAction="http://tempuri.org/IMonitoringService/AddMessagesResponse")]
        void AddMessages(System.Collections.Generic.List<RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw> lRawMessages);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/AddMessages", ReplyAction="http://tempuri.org/IMonitoringService/AddMessagesResponse")]
        System.Threading.Tasks.Task AddMessagesAsync(System.Collections.Generic.List<RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw> lRawMessages);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/AddBusinessMessage", ReplyAction="http://tempuri.org/IMonitoringService/AddBusinessMessageResponse")]
        void AddBusinessMessage(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/AddBusinessMessage", ReplyAction="http://tempuri.org/IMonitoringService/AddBusinessMessageResponse")]
        System.Threading.Tasks.Task AddBusinessMessageAsync(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/StartMonitoringEngine", ReplyAction="http://tempuri.org/IMonitoringService/StartMonitoringEngineResponse")]
        void StartMonitoringEngine();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IMonitoringService/StartMonitoringEngine", ReplyAction="http://tempuri.org/IMonitoringService/StartMonitoringEngineResponse")]
        System.Threading.Tasks.Task StartMonitoringEngineAsync();
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IMonitoringServiceChannel : RMS.Centralize.WebService.Proxy.MonitoringProxy.IMonitoringService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class MonitoringServiceClient : System.ServiceModel.ClientBase<RMS.Centralize.WebService.Proxy.MonitoringProxy.IMonitoringService>, RMS.Centralize.WebService.Proxy.MonitoringProxy.IMonitoringService {
        
        public MonitoringServiceClient() {
        }
        
        public MonitoringServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public MonitoringServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitoringServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public MonitoringServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void AddMessage(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage) {
            base.Channel.AddMessage(rawMessage);
        }
        
        public System.Threading.Tasks.Task AddMessageAsync(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage) {
            return base.Channel.AddMessageAsync(rawMessage);
        }
        
        public void AddMessages(System.Collections.Generic.List<RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw> lRawMessages) {
            base.Channel.AddMessages(lRawMessages);
        }
        
        public System.Threading.Tasks.Task AddMessagesAsync(System.Collections.Generic.List<RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw> lRawMessages) {
            return base.Channel.AddMessagesAsync(lRawMessages);
        }
        
        public void AddBusinessMessage(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage) {
            base.Channel.AddBusinessMessage(rawMessage);
        }
        
        public System.Threading.Tasks.Task AddBusinessMessageAsync(RMS.Centralize.WebService.Proxy.MonitoringProxy.RmsReportMonitoringRaw rawMessage) {
            return base.Channel.AddBusinessMessageAsync(rawMessage);
        }
        
        public void StartMonitoringEngine() {
            base.Channel.StartMonitoringEngine();
        }
        
        public System.Threading.Tasks.Task StartMonitoringEngineAsync() {
            return base.Channel.StartMonitoringEngineAsync();
        }
    }
}
