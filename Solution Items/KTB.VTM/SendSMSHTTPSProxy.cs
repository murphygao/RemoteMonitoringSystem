﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.18444
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------



[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ServiceModel.ServiceContractAttribute(Namespace="http://new.webservice.namespace", ConfigurationName="MobileMKTAdaptorSoap")]
public interface MobileMKTAdaptorSoap
{
    
    // CODEGEN: Generating message contract since the operation sendSMS is neither RPC nor document wrapped.
    [System.ServiceModel.OperationContractAttribute(Action="urn:#NewOperation", ReplyAction="*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    sendSMSResponse sendSMS(sendSMSRequest request);
    
    [System.ServiceModel.OperationContractAttribute(Action="urn:#NewOperation", ReplyAction="*")]
    System.Threading.Tasks.Task<sendSMSResponse> sendSMSAsync(sendSMSRequest request);
    
    // CODEGEN: Generating message contract since the operation cancelSMS is neither RPC nor document wrapped.
    [System.ServiceModel.OperationContractAttribute(Action="urn:#NewOperation", ReplyAction="*")]
    [System.ServiceModel.XmlSerializerFormatAttribute()]
    cancelSMSResponse cancelSMS(cancelSMSRequest request);
    
    [System.ServiceModel.OperationContractAttribute(Action="urn:#NewOperation", ReplyAction="*")]
    System.Threading.Tasks.Task<cancelSMSResponse> cancelSMSAsync(cancelSMSRequest request);
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://new.webservice.namespace")]
public partial class sendSMS_Req
{
    
    private string channel_idField;
    
    private string reference_noField;
    
    private string secret_flagField;
    
    private string resend_flagField;
    
    private SmsList_Type[] smslistField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(DataType="integer", Order=0)]
    public string channel_id
    {
        get
        {
            return this.channel_idField;
        }
        set
        {
            this.channel_idField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string reference_no
    {
        get
        {
            return this.reference_noField;
        }
        set
        {
            this.reference_noField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string secret_flag
    {
        get
        {
            return this.secret_flagField;
        }
        set
        {
            this.secret_flagField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public string resend_flag
    {
        get
        {
            return this.resend_flagField;
        }
        set
        {
            this.resend_flagField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute("smslist", Order=4)]
    public SmsList_Type[] smslist
    {
        get
        {
            return this.smslistField;
        }
        set
        {
            this.smslistField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(Namespace="http://new.webservice.namespace")]
public partial class SmsList_Type
{
    
    private string msg_fromField;
    
    private string msg_toField;
    
    private string contentField;
    
    private string langField;
    
    private string priority_noField;
    
    private string schedule_timeField;
    
    private string sender_nameField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string msg_from
    {
        get
        {
            return this.msg_fromField;
        }
        set
        {
            this.msg_fromField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string msg_to
    {
        get
        {
            return this.msg_toField;
        }
        set
        {
            this.msg_toField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string content
    {
        get
        {
            return this.contentField;
        }
        set
        {
            this.contentField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=3)]
    public string lang
    {
        get
        {
            return this.langField;
        }
        set
        {
            this.langField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=4)]
    public string priority_no
    {
        get
        {
            return this.priority_noField;
        }
        set
        {
            this.priority_noField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=5)]
    public string schedule_time
    {
        get
        {
            return this.schedule_timeField;
        }
        set
        {
            this.schedule_timeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=6)]
    public string sender_name
    {
        get
        {
            return this.sender_nameField;
        }
        set
        {
            this.sender_nameField = value;
        }
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://new.webservice.namespace")]
public partial class sendSMS_Resp
{
    
    private string respCodeField;
    
    private string respMsgField;
    
    private string respDetailField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string respCode
    {
        get
        {
            return this.respCodeField;
        }
        set
        {
            this.respCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string respMsg
    {
        get
        {
            return this.respMsgField;
        }
        set
        {
            this.respMsgField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string respDetail
    {
        get
        {
            return this.respDetailField;
        }
        set
        {
            this.respDetailField = value;
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class sendSMSRequest
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://new.webservice.namespace", Order=0)]
    public sendSMS_Req sendSMS_Req;
    
    public sendSMSRequest()
    {
    }
    
    public sendSMSRequest(sendSMS_Req sendSMS_Req)
    {
        this.sendSMS_Req = sendSMS_Req;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class sendSMSResponse
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://new.webservice.namespace", Order=0)]
    public sendSMS_Resp sendSMS_Resp;
    
    public sendSMSResponse()
    {
    }
    
    public sendSMSResponse(sendSMS_Resp sendSMS_Resp)
    {
        this.sendSMS_Resp = sendSMS_Resp;
    }
}

/// <remarks/>
[System.CodeDom.Compiler.GeneratedCodeAttribute("svcutil", "4.0.30319.33440")]
[System.SerializableAttribute()]
[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.ComponentModel.DesignerCategoryAttribute("code")]
[System.Xml.Serialization.XmlTypeAttribute(AnonymousType=true, Namespace="http://new.webservice.namespace")]
public partial class cancelSMS_Resp
{
    
    private string respCodeField;
    
    private string respMsgField;
    
    private string respDetailField;
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=0)]
    public string respCode
    {
        get
        {
            return this.respCodeField;
        }
        set
        {
            this.respCodeField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=1)]
    public string respMsg
    {
        get
        {
            return this.respMsgField;
        }
        set
        {
            this.respMsgField = value;
        }
    }
    
    /// <remarks/>
    [System.Xml.Serialization.XmlElementAttribute(Order=2)]
    public string respDetail
    {
        get
        {
            return this.respDetailField;
        }
        set
        {
            this.respDetailField = value;
        }
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class cancelSMSRequest
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://new.webservice.namespace", Order=0)]
    public sendSMS_Req sendSMS_Req;
    
    public cancelSMSRequest()
    {
    }
    
    public cancelSMSRequest(sendSMS_Req sendSMS_Req)
    {
        this.sendSMS_Req = sendSMS_Req;
    }
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
[System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
[System.ServiceModel.MessageContractAttribute(IsWrapped=false)]
public partial class cancelSMSResponse
{
    
    [System.ServiceModel.MessageBodyMemberAttribute(Namespace="http://new.webservice.namespace", Order=0)]
    public cancelSMS_Resp cancelSMS_Resp;
    
    public cancelSMSResponse()
    {
    }
    
    public cancelSMSResponse(cancelSMS_Resp cancelSMS_Resp)
    {
        this.cancelSMS_Resp = cancelSMS_Resp;
    }
}

[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public interface MobileMKTAdaptorSoapChannel : MobileMKTAdaptorSoap, System.ServiceModel.IClientChannel
{
}

[System.Diagnostics.DebuggerStepThroughAttribute()]
[System.CodeDom.Compiler.GeneratedCodeAttribute("System.ServiceModel", "4.0.0.0")]
public partial class MobileMKTAdaptorSoapClient : System.ServiceModel.ClientBase<MobileMKTAdaptorSoap>, MobileMKTAdaptorSoap
{
    
    public MobileMKTAdaptorSoapClient()
    {
    }
    
    public MobileMKTAdaptorSoapClient(string endpointConfigurationName) : 
            base(endpointConfigurationName)
    {
    }
    
    public MobileMKTAdaptorSoapClient(string endpointConfigurationName, string remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public MobileMKTAdaptorSoapClient(string endpointConfigurationName, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(endpointConfigurationName, remoteAddress)
    {
    }
    
    public MobileMKTAdaptorSoapClient(System.ServiceModel.Channels.Binding binding, System.ServiceModel.EndpointAddress remoteAddress) : 
            base(binding, remoteAddress)
    {
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    sendSMSResponse MobileMKTAdaptorSoap.sendSMS(sendSMSRequest request)
    {
        return base.Channel.sendSMS(request);
    }
    
    public sendSMS_Resp sendSMS(sendSMS_Req sendSMS_Req)
    {
        sendSMSRequest inValue = new sendSMSRequest();
        inValue.sendSMS_Req = sendSMS_Req;
        sendSMSResponse retVal = ((MobileMKTAdaptorSoap)(this)).sendSMS(inValue);
        return retVal.sendSMS_Resp;
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<sendSMSResponse> MobileMKTAdaptorSoap.sendSMSAsync(sendSMSRequest request)
    {
        return base.Channel.sendSMSAsync(request);
    }
    
    public System.Threading.Tasks.Task<sendSMSResponse> sendSMSAsync(sendSMS_Req sendSMS_Req)
    {
        sendSMSRequest inValue = new sendSMSRequest();
        inValue.sendSMS_Req = sendSMS_Req;
        return ((MobileMKTAdaptorSoap)(this)).sendSMSAsync(inValue);
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    cancelSMSResponse MobileMKTAdaptorSoap.cancelSMS(cancelSMSRequest request)
    {
        return base.Channel.cancelSMS(request);
    }
    
    public cancelSMS_Resp cancelSMS(sendSMS_Req sendSMS_Req)
    {
        cancelSMSRequest inValue = new cancelSMSRequest();
        inValue.sendSMS_Req = sendSMS_Req;
        cancelSMSResponse retVal = ((MobileMKTAdaptorSoap)(this)).cancelSMS(inValue);
        return retVal.cancelSMS_Resp;
    }
    
    [System.ComponentModel.EditorBrowsableAttribute(System.ComponentModel.EditorBrowsableState.Advanced)]
    System.Threading.Tasks.Task<cancelSMSResponse> MobileMKTAdaptorSoap.cancelSMSAsync(cancelSMSRequest request)
    {
        return base.Channel.cancelSMSAsync(request);
    }
    
    public System.Threading.Tasks.Task<cancelSMSResponse> cancelSMSAsync(sendSMS_Req sendSMS_Req)
    {
        cancelSMSRequest inValue = new cancelSMSRequest();
        inValue.sendSMS_Req = sendSMS_Req;
        return ((MobileMKTAdaptorSoap)(this)).cancelSMSAsync(inValue);
    }
}
