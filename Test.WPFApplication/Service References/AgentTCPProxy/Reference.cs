﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Test.WPFApplication.AgentTCPProxy {
    using System.Runtime.Serialization;
    using System;
    
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.Runtime.Serialization", "4.0.0.0")]
    [System.Runtime.Serialization.DataContractAttribute(Name="RemoteCommand", Namespace="http://schemas.datacontract.org/2004/07/RMS.Agent.Entity")]
    [System.SerializableAttribute()]
    public partial class RemoteCommand : object, System.Runtime.Serialization.IExtensibleDataObject, System.ComponentModel.INotifyPropertyChanged {
        
        [System.NonSerializedAttribute()]
        private System.Runtime.Serialization.ExtensionDataObject extensionDataField;
        
        [System.Runtime.Serialization.OptionalFieldAttribute()]
        private string CommandLineField;
        
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
        public string CommandLine {
            get {
                return this.CommandLineField;
            }
            set {
                if ((object.ReferenceEquals(this.CommandLineField, value) != true)) {
                    this.CommandLineField = value;
                    this.RaisePropertyChanged("CommandLine");
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
    [System.Runtime.Serialization.DataContractAttribute(Name="Result", Namespace="http://schemas.datacontract.org/2004/07/RMS.Agent.WCF.Model")]
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
    [System.ServiceModel.ServiceContractAttribute(ConfigurationName="AgentTCPProxy.IAgentService")]
    public interface IAgentService {
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/TestConnection", ReplyAction="http://tempuri.org/IAgentService/TestConnectionResponse")]
        void TestConnection();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/TestConnection", ReplyAction="http://tempuri.org/IAgentService/TestConnectionResponse")]
        System.Threading.Tasks.Task TestConnectionAsync();
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/AutoUpdate", ReplyAction="http://tempuri.org/IAgentService/AutoUpdateResponse")]
        void AutoUpdate(string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/AutoUpdate", ReplyAction="http://tempuri.org/IAgentService/AutoUpdateResponse")]
        System.Threading.Tasks.Task AutoUpdateAsync(string type);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/RemoteCommand", ReplyAction="http://tempuri.org/IAgentService/RemoteCommandResponse")]
        void RemoteCommand([System.ServiceModel.MessageParameterAttribute(Name="remoteCommand")] Test.WPFApplication.AgentTCPProxy.RemoteCommand remoteCommand1);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/RemoteCommand", ReplyAction="http://tempuri.org/IAgentService/RemoteCommandResponse")]
        System.Threading.Tasks.Task RemoteCommandAsync(Test.WPFApplication.AgentTCPProxy.RemoteCommand remoteCommand);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/Monitoring", ReplyAction="http://tempuri.org/IAgentService/MonitoringResponse")]
        Test.WPFApplication.AgentTCPProxy.Result Monitoring(string clientCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/Monitoring", ReplyAction="http://tempuri.org/IAgentService/MonitoringResponse")]
        System.Threading.Tasks.Task<Test.WPFApplication.AgentTCPProxy.Result> MonitoringAsync(string clientCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/SelfMonitoring", ReplyAction="http://tempuri.org/IAgentService/SelfMonitoringResponse")]
        System.Collections.Generic.List<RMS.Agent.BSL.Monitoring.Models.DeviceStatus> SelfMonitoring(string clientCode);
        
        [System.ServiceModel.OperationContractAttribute(Action="http://tempuri.org/IAgentService/SelfMonitoring", ReplyAction="http://tempuri.org/IAgentService/SelfMonitoringResponse")]
        System.Threading.Tasks.Task<System.Collections.Generic.List<RMS.Agent.BSL.Monitoring.Models.DeviceStatus>> SelfMonitoringAsync(string clientCode);
    }
    
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public interface IAgentServiceChannel : Test.WPFApplication.AgentTCPProxy.IAgentService, System.ServiceModel.IClientChannel {
    }
    
    [System.Diagnostics.DebuggerStepThroughAttribute()]
    [System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
    public partial class AgentServiceClient : System.ServiceModel.ClientBase<Test.WPFApplication.AgentTCPProxy.IAgentService>, Test.WPFApplication.AgentTCPProxy.IAgentService {
        
        public AgentServiceClient() {
        }
        
        public AgentServiceClient(string endpointConfigurationName) : 
                base(endpointConfigurationName) {
        }
        
        public AgentServiceClient(string endpointConfigurationName, string remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AgentServiceClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(endpointConfigurationName, remoteAddress) {
        }
        
        public AgentServiceClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
                base(binding, remoteAddress) {
        }
        
        public void TestConnection() {
            base.Channel.TestConnection();
        }
        
        public System.Threading.Tasks.Task TestConnectionAsync() {
            return base.Channel.TestConnectionAsync();
        }
        
        public void AutoUpdate(string type) {
            base.Channel.AutoUpdate(type);
        }
        
        public System.Threading.Tasks.Task AutoUpdateAsync(string type) {
            return base.Channel.AutoUpdateAsync(type);
        }
        
        public void RemoteCommand(Test.WPFApplication.AgentTCPProxy.RemoteCommand remoteCommand1) {
            base.Channel.RemoteCommand(remoteCommand1);
        }
        
        public System.Threading.Tasks.Task RemoteCommandAsync(Test.WPFApplication.AgentTCPProxy.RemoteCommand remoteCommand) {
            return base.Channel.RemoteCommandAsync(remoteCommand);
        }
        
        public Test.WPFApplication.AgentTCPProxy.Result Monitoring(string clientCode) {
            return base.Channel.Monitoring(clientCode);
        }
        
        public System.Threading.Tasks.Task<Test.WPFApplication.AgentTCPProxy.Result> MonitoringAsync(string clientCode) {
            return base.Channel.MonitoringAsync(clientCode);
        }
        
        public System.Collections.Generic.List<RMS.Agent.BSL.Monitoring.Models.DeviceStatus> SelfMonitoring(string clientCode) {
            return base.Channel.SelfMonitoring(clientCode);
        }
        
        public System.Threading.Tasks.Task<System.Collections.Generic.List<RMS.Agent.BSL.Monitoring.Models.DeviceStatus>> SelfMonitoringAsync(string clientCode) {
            return base.Channel.SelfMonitoringAsync(clientCode);
        }
    }
}
