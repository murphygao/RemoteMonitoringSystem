﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace RMS.Agent.Proxy.AutoUpdateProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://schemas.datacontract.org/2004/07/RMS.Centralize.WebService.Model")]
    [System.SerializableAttribute()]
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
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AutoUpdateProxy.IAutoUpdateService")]
    public interface IAutoUpdateService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAutoUpdateService/TestConnection", ReplyAction="http://tempuri.org/IAutoUpdateService/TestConnectionResponse")]
        void TestConnection();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAutoUpdateService/TestConnection", ReplyAction="http://tempuri.org/IAutoUpdateService/TestConnectionResponse")]
        System.Threading.Tasks.Task TestConnectionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAutoUpdateService/AddAutoUpdateLog", ReplyAction="http://tempuri.org/IAutoUpdateService/AddAutoUpdateLogResponse")]
        RMS.Agent.Proxy.AutoUpdateProxy.Result AddAutoUpdateLog(string clientCode, string appName, string currentVersion, string updateVersion, bool isComplete, string errorMessage);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAutoUpdateService/AddAutoUpdateLog", ReplyAction="http://tempuri.org/IAutoUpdateService/AddAutoUpdateLogResponse")]
        System.Threading.Tasks.Task<RMS.Agent.Proxy.AutoUpdateProxy.Result> AddAutoUpdateLogAsync(string clientCode, string appName, string currentVersion, string updateVersion, bool isComplete, string errorMessage);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAutoUpdateServiceChannel : RMS.Agent.Proxy.AutoUpdateProxy.IAutoUpdateService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AutoUpdateServiceClient : System.ServiceModel.ClientBase<RMS.Agent.Proxy.AutoUpdateProxy.IAutoUpdateService>, RMS.Agent.Proxy.AutoUpdateProxy.IAutoUpdateService {
        
        public AutoUpdateServiceClient() {
        }
        
        public AutoUpdateServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AutoUpdateServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AutoUpdateServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AutoUpdateServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void TestConnection() {
            base.Channel.TestConnection();
        }
        
        public System.Threading.Tasks.Task TestConnectionAsync() {
            return base.Channel.TestConnectionAsync();
        }
        
        public RMS.Agent.Proxy.AutoUpdateProxy.Result AddAutoUpdateLog(string clientCode, string appName, string currentVersion, string updateVersion, bool isComplete, string errorMessage) {
            return base.Channel.AddAutoUpdateLog(clientCode, appName, currentVersion, updateVersion, isComplete, errorMessage);
        }
        
        public System.Threading.Tasks.Task<RMS.Agent.Proxy.AutoUpdateProxy.Result> AddAutoUpdateLogAsync(string clientCode, string appName, string currentVersion, string updateVersion, bool isComplete, string errorMessage) {
            return base.Channel.AddAutoUpdateLogAsync(clientCode, appName, currentVersion, updateVersion, isComplete, errorMessage);
        }
    }
}