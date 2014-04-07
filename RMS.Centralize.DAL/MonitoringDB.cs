﻿

// This file was automatically generated.
// Do not make changes directly to this file - edit the template instead.
// 
// The following connection settings were used to generate this file
// 
//     Configuration file:     "RMS.Centralize.DAL\App.config"
//     Connection String Name: "MyDbContext"
//     Connection String:      "Data Source=(local);Initial Catalog=DEV_Monitoring;Integrated Security=True;Application Name=MyApp"

// ReSharper disable RedundantUsingDirective
// ReSharper disable DoNotCallOverridableMethodsInConstructor
// ReSharper disable InconsistentNaming
// ReSharper disable PartialTypeWithSinglePart
// ReSharper disable PartialMethodWithSinglePart
// ReSharper disable RedundantNameQualifier

using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Entity;
using System.Data.Entity.ModelConfiguration;
using System.Runtime.Serialization;
//using DatabaseGeneratedOption = System.ComponentModel.DataAnnotations.DatabaseGeneratedOption;

namespace RMS.Centralize.DAL
{
    // ************************************************************************
    // Unit of work
    public interface IReverseDbContext : IDisposable
    {
        IDbSet<RmsActionProfile> RmsActionProfiles { get; set; } // RMS_ActionProfile
        IDbSet<RmsClient> RmsClients { get; set; } // RMS_Client
        IDbSet<RmsClientMonitoring> RmsClientMonitorings { get; set; } // RMS_ClientMonitoring
        IDbSet<RmsClientSeverityAction> RmsClientSeverityActions { get; set; } // RMS_ClientSeverityAction
        IDbSet<RmsClientType> RmsClientTypes { get; set; } // RMS_ClientType
        IDbSet<RmsColorLabel> RmsColorLabels { get; set; } // RMS_ColorLabel
        IDbSet<RmsDevice> RmsDevices { get; set; } // RMS_Device
        IDbSet<RmsDeviceType> RmsDeviceTypes { get; set; } // RMS_DeviceType
        IDbSet<RmsMessage> RmsMessages { get; set; } // RMS_Message
        IDbSet<RmsMessageGroup> RmsMessageGroups { get; set; } // RMS_MessageGroup
        IDbSet<RmsMonitoringProfile> RmsMonitoringProfiles { get; set; } // RMS_MonitoringProfile
        IDbSet<RmsMonitoringProfileDevice> RmsMonitoringProfileDevices { get; set; } // RMS_MonitoringProfileDevice
        IDbSet<RmsReportMonitoringRaw> RmsReportMonitoringRaws { get; set; } // RMS_Report_MonitoringRaw
        IDbSet<RmsReportSummaryMonitoring> RmsReportSummaryMonitorings { get; set; } // RMS_Report_SummaryMonitoring
        IDbSet<RmsSeverityLevel> RmsSeverityLevels { get; set; } // RMS_SeverityLevel
        IDbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams

        int SaveChanges();
    }

    // ************************************************************************
    // Database context
    public partial class ReverseDbContext : DbContext, IReverseDbContext
    {
        public IDbSet<RmsActionProfile> RmsActionProfiles { get; set; } // RMS_ActionProfile
        public IDbSet<RmsClient> RmsClients { get; set; } // RMS_Client
        public IDbSet<RmsClientMonitoring> RmsClientMonitorings { get; set; } // RMS_ClientMonitoring
        public IDbSet<RmsClientSeverityAction> RmsClientSeverityActions { get; set; } // RMS_ClientSeverityAction
        public IDbSet<RmsClientType> RmsClientTypes { get; set; } // RMS_ClientType
        public IDbSet<RmsColorLabel> RmsColorLabels { get; set; } // RMS_ColorLabel
        public IDbSet<RmsDevice> RmsDevices { get; set; } // RMS_Device
        public IDbSet<RmsDeviceType> RmsDeviceTypes { get; set; } // RMS_DeviceType
        public IDbSet<RmsMessage> RmsMessages { get; set; } // RMS_Message
        public IDbSet<RmsMessageGroup> RmsMessageGroups { get; set; } // RMS_MessageGroup
        public IDbSet<RmsMonitoringProfile> RmsMonitoringProfiles { get; set; } // RMS_MonitoringProfile
        public IDbSet<RmsMonitoringProfileDevice> RmsMonitoringProfileDevices { get; set; } // RMS_MonitoringProfileDevice
        public IDbSet<RmsReportMonitoringRaw> RmsReportMonitoringRaws { get; set; } // RMS_Report_MonitoringRaw
        public IDbSet<RmsReportSummaryMonitoring> RmsReportSummaryMonitorings { get; set; } // RMS_Report_SummaryMonitoring
        public IDbSet<RmsSeverityLevel> RmsSeverityLevels { get; set; } // RMS_SeverityLevel
        public IDbSet<Sysdiagram> Sysdiagrams { get; set; } // sysdiagrams

        static ReverseDbContext()
        {
            Database.SetInitializer<ReverseDbContext>(null);
        }

        public ReverseDbContext()
            : base("Name=MyDbContext")
        {
        InitializePartial();
        }

        public ReverseDbContext(string connectionString) : base(connectionString)
        {
        InitializePartial();
        }

        public ReverseDbContext(string connectionString, System.Data.Entity.Infrastructure.DbCompiledModel model) : base(connectionString, model)
        {
        InitializePartial();
        }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Configurations.Add(new RmsActionProfileConfiguration());
            modelBuilder.Configurations.Add(new RmsClientConfiguration());
            modelBuilder.Configurations.Add(new RmsClientMonitoringConfiguration());
            modelBuilder.Configurations.Add(new RmsClientSeverityActionConfiguration());
            modelBuilder.Configurations.Add(new RmsClientTypeConfiguration());
            modelBuilder.Configurations.Add(new RmsColorLabelConfiguration());
            modelBuilder.Configurations.Add(new RmsDeviceConfiguration());
            modelBuilder.Configurations.Add(new RmsDeviceTypeConfiguration());
            modelBuilder.Configurations.Add(new RmsMessageConfiguration());
            modelBuilder.Configurations.Add(new RmsMessageGroupConfiguration());
            modelBuilder.Configurations.Add(new RmsMonitoringProfileConfiguration());
            modelBuilder.Configurations.Add(new RmsMonitoringProfileDeviceConfiguration());
            modelBuilder.Configurations.Add(new RmsReportMonitoringRawConfiguration());
            modelBuilder.Configurations.Add(new RmsReportSummaryMonitoringConfiguration());
            modelBuilder.Configurations.Add(new RmsSeverityLevelConfiguration());
            modelBuilder.Configurations.Add(new SysdiagramConfiguration());
        OnModelCreatingPartial(modelBuilder);
        }

        public static DbModelBuilder CreateModel(DbModelBuilder modelBuilder, string schema)
        {
            modelBuilder.Configurations.Add(new RmsActionProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsClientConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsClientMonitoringConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsClientSeverityActionConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsClientTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsColorLabelConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsDeviceConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsDeviceTypeConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsMessageConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsMessageGroupConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsMonitoringProfileConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsMonitoringProfileDeviceConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsReportMonitoringRawConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsReportSummaryMonitoringConfiguration(schema));
            modelBuilder.Configurations.Add(new RmsSeverityLevelConfiguration(schema));
            modelBuilder.Configurations.Add(new SysdiagramConfiguration(schema));
            return modelBuilder;
        }

        partial void InitializePartial();
        partial void OnModelCreatingPartial(DbModelBuilder modelBuilder);
    }

    // ************************************************************************
    // POCO classes

    // RMS_ActionProfile
    [DataContract]
    public partial class RmsActionProfile
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int ActionProfileId { get; set; } // ActionProfileID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string ActionProfileName { get; set; } // ActionProfileName

        [DataMember(Order = 3, IsRequired = false)]
        public string Email { get; set; } // Email

        [DataMember(Order = 4, IsRequired = false)]
        public string Sms { get; set; } // SMS

        [DataMember(Order = 5, IsRequired = false)]
        public int? CommandLineId { get; set; } // CommandLineID

        [DataMember(Order = 6, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 7, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 8, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 9, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 10, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy

        [DataMember(Order = 11, IsRequired = false)]
        public long? RowNum { get; set; } // RowNum


        // Reverse navigation
        public virtual ICollection<RmsSeverityLevel> RmsSeverityLevels { get; set; } // RMS_SeverityLevel.FK_RMS_SeverityLevel_RMS_ActionProfile

        public RmsActionProfile()
        {
            CreatedDate = System.DateTime.Now;
            RmsSeverityLevels = new List<RmsSeverityLevel>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Client
    [DataContract]
    public partial class RmsClient
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int ClientId { get; set; } // ClientID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public int? ClientTypeId { get; set; } // ClientTypeID

        [DataMember(Order = 3, IsRequired = false)]
        public string ClientCode { get; set; } // ClientCode

        [DataMember(Order = 4, IsRequired = false)]
        public int? ReferenceClientId { get; set; } // ReferenceClientID

        [DataMember(Order = 5, IsRequired = false)]
        public int? LocationId { get; set; } // LocationID

        [DataMember(Order = 6, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 7, IsRequired = false)]
        public bool? Enable { get; set; } // Enable

        [DataMember(Order = 8, IsRequired = false)]
        public DateTime? EffectiveDate { get; set; } // EffectiveDate

        [DataMember(Order = 9, IsRequired = false)]
        public DateTime? ExpiredDate { get; set; } // ExpiredDate

        [DataMember(Order = 10, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 11, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 12, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 13, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Reverse navigation
        public virtual ICollection<RmsClientMonitoring> RmsClientMonitorings { get; set; } // Many to many mapping
        public virtual ICollection<RmsClientSeverityAction> RmsClientSeverityActions { get; set; } // Many to many mapping

        // Foreign keys
        public virtual RmsClientType RmsClientType { get; set; } // FK_RMS_Client_RMS_ClientType

        public RmsClient()
        {
            RmsClientMonitorings = new List<RmsClientMonitoring>();
            RmsClientSeverityActions = new List<RmsClientSeverityAction>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_ClientMonitoring
    [DataContract]
    public partial class RmsClientMonitoring
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int ClientId { get; set; } // ClientID (Primary key)

        [DataMember(Order = 2, IsRequired = true)]
        public int MonitoringProfileId { get; set; } // MonitoringProfileID (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public DateTime? EffectiveDate { get; set; } // EffectiveDate

        [DataMember(Order = 4, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 5, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 6, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 7, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Foreign keys
        public virtual RmsClient RmsClient { get; set; } // FK_RMS_ClientMonitoring_RMS_Client
        public virtual RmsMonitoringProfile RmsMonitoringProfile { get; set; } // FK_RMS_ClientMonitoring_RMS_MonitoringProfile
    }

    // RMS_ClientSeverityAction
    [DataContract]
    public partial class RmsClientSeverityAction
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int ClientId { get; set; } // ClientID (Primary key)

        [DataMember(Order = 2, IsRequired = true)]
        public int SeverityLevelId { get; set; } // SeverityLevelID (Primary key)

        [DataMember(Order = 3, IsRequired = false)]
        public string Email { get; set; } // Email

        [DataMember(Order = 4, IsRequired = false)]
        public string Sms { get; set; } // SMS

        [DataMember(Order = 5, IsRequired = false)]
        public int? CommandLindId { get; set; } // CommandLindID

        [DataMember(Order = 6, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 7, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 8, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 9, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Foreign keys
        public virtual RmsClient RmsClient { get; set; } // FK_RMS_ClientSeverityAction_RMS_Client
        public virtual RmsSeverityLevel RmsSeverityLevel { get; set; } // FK_RMS_ClientSeverityAction_RMS_SeverityLevel
    }

    // RMS_ClientType
    [DataContract]
    public partial class RmsClientType
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int ClientTypeId { get; set; } // ClientTypeID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string Clienttype { get; set; } // Clienttype


        // Reverse navigation
        public virtual ICollection<RmsClient> RmsClients { get; set; } // RMS_Client.FK_RMS_Client_RMS_ClientType

        public RmsClientType()
        {
            RmsClients = new List<RmsClient>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_ColorLabel
    [DataContract]
    public partial class RmsColorLabel
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string ColorCode { get; set; } // ColorCode (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string ColorName { get; set; } // ColorName

        [DataMember(Order = 3, IsRequired = false)]
        public string ColorTagStart { get; set; } // ColorTagStart

        [DataMember(Order = 4, IsRequired = false)]
        public string ColorTagEnd { get; set; } // ColorTagEnd


        // Reverse navigation
        public virtual ICollection<RmsSeverityLevel> RmsSeverityLevels { get; set; } // RMS_SeverityLevel.FK_RMS_SeverityLevel_RMS_ColorLabel

        public RmsColorLabel()
        {
            RmsSeverityLevels = new List<RmsSeverityLevel>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Device
    [DataContract]
    public partial class RmsDevice
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int DeviceId { get; set; } // DeviceID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public int? DeviceTypeId { get; set; } // DeviceTypeID

        [DataMember(Order = 3, IsRequired = false)]
        public string DeviceCode { get; set; } // DeviceCode

        [DataMember(Order = 4, IsRequired = false)]
        public string Brand { get; set; } // Brand

        [DataMember(Order = 5, IsRequired = false)]
        public string Model { get; set; } // Model

        [DataMember(Order = 6, IsRequired = false)]
        public string DeviceManagerName { get; set; } // DeviceManagerName

        [DataMember(Order = 7, IsRequired = false)]
        public string DeviceManagerId { get; set; } // DeviceManagerID

        [DataMember(Order = 8, IsRequired = false)]
        public string StringValue { get; set; } // StringValue

        [DataMember(Order = 9, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 10, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 11, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 12, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 13, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Reverse navigation
        public virtual ICollection<RmsMessage> RmsMessages { get; set; } // Many to many mapping
        public virtual ICollection<RmsMonitoringProfileDevice> RmsMonitoringProfileDevices { get; set; } // RMS_MonitoringProfileDevice.FK_RMS_MonitoringProfileDevice_RMS_Device

        // Foreign keys
        public virtual RmsDeviceType RmsDeviceType { get; set; } // FK_RMS_Device_RMS_DeviceType

        public RmsDevice()
        {
            RmsMonitoringProfileDevices = new List<RmsMonitoringProfileDevice>();
            RmsMessages = new List<RmsMessage>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_DeviceType
    [DataContract]
    public partial class RmsDeviceType
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int DeviceTypeId { get; set; } // DeviceTypeID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string DeviceType { get; set; } // DeviceType


        // Reverse navigation
        public virtual ICollection<RmsDevice> RmsDevices { get; set; } // RMS_Device.FK_RMS_Device_RMS_DeviceType

        public RmsDeviceType()
        {
            RmsDevices = new List<RmsDevice>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Message
    [DataContract]
    public partial class RmsMessage
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int MessageId { get; set; } // MessageID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public int? MessageGroupId { get; set; } // MessageGroupID

        [DataMember(Order = 3, IsRequired = false)]
        public string Message { get; set; } // Message

        [DataMember(Order = 4, IsRequired = false)]
        public int? SeverityLevelId { get; set; } // SeverityLevelID

        [DataMember(Order = 5, IsRequired = false)]
        public bool? ReadOnly { get; set; } // ReadOnly

        [DataMember(Order = 6, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 7, IsRequired = false)]
        public bool? ActiveStatus { get; set; } // ActiveStatus

        [DataMember(Order = 8, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 9, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 10, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 11, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Reverse navigation
        public virtual ICollection<RmsDevice> RmsDevices { get; set; } // Many to many mapping

        // Foreign keys
        public virtual RmsMessageGroup RmsMessageGroup { get; set; } // FK_RMS_Message_RMS_MessageGroup
        public virtual RmsSeverityLevel RmsSeverityLevel { get; set; } // FK_RMS_Message_RMS_SeverityLevel

        public RmsMessage()
        {
            CreatedDate = System.DateTime.Now;
            RmsDevices = new List<RmsDevice>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_MessageGroup
    [DataContract]
    public partial class RmsMessageGroup
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int MessageGroupId { get; set; } // MessageGroupID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string MessageGroupName { get; set; } // MessageGroupName

        [DataMember(Order = 3, IsRequired = false)]
        public string MessageType { get; set; } // MessageType

        [DataMember(Order = 4, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 5, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 6, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 7, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 8, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Reverse navigation
        public virtual ICollection<RmsMessage> RmsMessages { get; set; } // RMS_Message.FK_RMS_Message_RMS_MessageGroup

        public RmsMessageGroup()
        {
            CreatedDate = System.DateTime.Now;
            RmsMessages = new List<RmsMessage>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_MonitoringProfile
    [DataContract]
    public partial class RmsMonitoringProfile
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int MonitoringProfileId { get; set; } // MonitoringProfileID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string ProfileName { get; set; } // ProfileName

        [DataMember(Order = 3, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 4, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 5, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 6, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 7, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Reverse navigation
        public virtual ICollection<RmsClientMonitoring> RmsClientMonitorings { get; set; } // Many to many mapping
        public virtual ICollection<RmsMonitoringProfileDevice> RmsMonitoringProfileDevices { get; set; } // RMS_MonitoringProfileDevice.FK_RMS_MonitoringProfileDevice_RMS_MonitoringProfile

        public RmsMonitoringProfile()
        {
            RmsClientMonitorings = new List<RmsClientMonitoring>();
            RmsMonitoringProfileDevices = new List<RmsMonitoringProfileDevice>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_MonitoringProfileDevice
    [DataContract]
    public partial class RmsMonitoringProfileDevice
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int MonitoringProfileDeviceId { get; set; } // MonitoringProfileDeviceID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public int? MonitoringProfileId { get; set; } // MonitoringProfileID

        [DataMember(Order = 3, IsRequired = false)]
        public int? DeviceId { get; set; } // DeviceID

        [DataMember(Order = 4, IsRequired = false)]
        public decimal? HighThreshold { get; set; } // HighThreshold

        [DataMember(Order = 5, IsRequired = false)]
        public decimal? LowThreshold { get; set; } // LowThreshold

        [DataMember(Order = 6, IsRequired = false)]
        public string ComPort { get; set; } // COMPort

        [DataMember(Order = 7, IsRequired = false)]
        public bool? BooleanValue { get; set; } // BooleanValue

        [DataMember(Order = 8, IsRequired = false)]
        public string StringValue { get; set; } // StringValue


        // Foreign keys
        public virtual RmsDevice RmsDevice { get; set; } // FK_RMS_MonitoringProfileDevice_RMS_Device
        public virtual RmsMonitoringProfile RmsMonitoringProfile { get; set; } // FK_RMS_MonitoringProfileDevice_RMS_MonitoringProfile
    }

    // RMS_Report_MonitoringRaw
    [DataContract]
    public partial class RmsReportMonitoringRaw
    {
        [DataMember(Order = 1, IsRequired = true)]
        public long Id { get; set; } // ID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string ClientCode { get; set; } // ClientCode

        [DataMember(Order = 3, IsRequired = false)]
        public string ClientIpAddress { get; set; } // ClientIPAddress

        [DataMember(Order = 4, IsRequired = false)]
        public string DeviceCode { get; set; } // DeviceCode

        [DataMember(Order = 5, IsRequired = false)]
        public string MessageGroupCode { get; set; } // MessageGroupCode

        [DataMember(Order = 6, IsRequired = false)]
        public string Message { get; set; } // Message

        [DataMember(Order = 7, IsRequired = false)]
        public DateTime? MessageDateTime { get; set; } // MessageDateTime

        [DataMember(Order = 8, IsRequired = false)]
        public string MessageRemark { get; set; } // MessageRemark

        [DataMember(Order = 9, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

    }

    // RMS_Report_SummaryMonitoring
    [DataContract]
    public partial class RmsReportSummaryMonitoring
    {
        [DataMember(Order = 1, IsRequired = true)]
        public long Id { get; set; } // ID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public int? RawId { get; set; } // RawID

        [DataMember(Order = 3, IsRequired = false)]
        public int? ClientId { get; set; } // ClientID

        [DataMember(Order = 4, IsRequired = false)]
        public string ClientCode { get; set; } // ClientCode

        [DataMember(Order = 5, IsRequired = false)]
        public string ClientIpAddress { get; set; } // ClientIPAddress

        [DataMember(Order = 6, IsRequired = false)]
        public int? LocationId { get; set; } // LocationID

        [DataMember(Order = 7, IsRequired = false)]
        public int? DeviceId { get; set; } // DeviceID

        [DataMember(Order = 8, IsRequired = false)]
        public string DeviceCode { get; set; } // DeviceCode

        [DataMember(Order = 9, IsRequired = false)]
        public int? MessageGroupId { get; set; } // MessageGroupID

        [DataMember(Order = 10, IsRequired = false)]
        public string MessageGroupCode { get; set; } // MessageGroupCode

        [DataMember(Order = 11, IsRequired = false)]
        public string MessageGroupName { get; set; } // MessageGroupName

        [DataMember(Order = 12, IsRequired = false)]
        public int? MessageId { get; set; } // MessageID

        [DataMember(Order = 13, IsRequired = false)]
        public string Message { get; set; } // Message

        [DataMember(Order = 14, IsRequired = false)]
        public int? Status { get; set; } // Status

        [DataMember(Order = 15, IsRequired = false)]
        public DateTime? MessageDateTime { get; set; } // MessageDateTime

        [DataMember(Order = 16, IsRequired = false)]
        public string MessageRemark { get; set; } // MessageRemark

        [DataMember(Order = 17, IsRequired = false)]
        public DateTime? EventDateTime { get; set; } // EventDateTime

        [DataMember(Order = 18, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 19, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 20, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 21, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy

    }

    // RMS_SeverityLevel
    [DataContract]
    public partial class RmsSeverityLevel
    {
        [DataMember(Order = 1, IsRequired = true)]
        public int SeverityLevelId { get; set; } // SeverityLevelID (Primary key)

        [DataMember(Order = 2, IsRequired = false)]
        public string LevelCode { get; set; } // LevelCode

        [DataMember(Order = 3, IsRequired = false)]
        public string LevelName { get; set; } // LevelName

        [DataMember(Order = 4, IsRequired = false)]
        public int? OrderList { get; set; } // OrderList

        [DataMember(Order = 5, IsRequired = false)]
        public bool? ActiveList { get; set; } // ActiveList

        [DataMember(Order = 6, IsRequired = false)]
        public int? DefaultActionProfileId { get; set; } // DefaultActionProfileID

        [DataMember(Order = 7, IsRequired = false)]
        public string ColorCode { get; set; } // ColorCode

        [DataMember(Order = 8, IsRequired = false)]
        public DateTime? CreatedDate { get; set; } // CreatedDate

        [DataMember(Order = 9, IsRequired = false)]
        public string CreatedBy { get; set; } // CreatedBy

        [DataMember(Order = 10, IsRequired = false)]
        public DateTime? UpdatedDate { get; set; } // UpdatedDate

        [DataMember(Order = 11, IsRequired = false)]
        public string UpdatedBy { get; set; } // UpdatedBy


        // Reverse navigation
        public virtual ICollection<RmsClientSeverityAction> RmsClientSeverityActions { get; set; } // Many to many mapping
        public virtual ICollection<RmsMessage> RmsMessages { get; set; } // RMS_Message.FK_RMS_Message_RMS_SeverityLevel

        // Foreign keys
        public virtual RmsActionProfile RmsActionProfile { get; set; } // FK_RMS_SeverityLevel_RMS_ActionProfile
        public virtual RmsColorLabel RmsColorLabel { get; set; } // FK_RMS_SeverityLevel_RMS_ColorLabel

        public RmsSeverityLevel()
        {
            CreatedDate = System.DateTime.Now;
            RmsClientSeverityActions = new List<RmsClientSeverityAction>();
            RmsMessages = new List<RmsMessage>();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // sysdiagrams
    [DataContract]
    public partial class Sysdiagram
    {
        [DataMember(Order = 1, IsRequired = true)]
        public string Name { get; set; } // name

        [DataMember(Order = 2, IsRequired = true)]
        public int PrincipalId { get; set; } // principal_id

        [DataMember(Order = 3, IsRequired = true)]
        public int DiagramId { get; set; } // diagram_id (Primary key)

        [DataMember(Order = 4, IsRequired = false)]
        public int? Version { get; set; } // version

        [DataMember(Order = 5, IsRequired = false)]
        public byte[] Definition { get; set; } // definition

    }


    // ************************************************************************
    // POCO Configuration

    // RMS_ActionProfile
    internal partial class RmsActionProfileConfiguration : EntityTypeConfiguration<RmsActionProfile>
    {
        public RmsActionProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_ActionProfile");
            HasKey(x => x.ActionProfileId);

            Property(x => x.ActionProfileId).HasColumnName("ActionProfileID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.ActionProfileName).HasColumnName("ActionProfileName").IsOptional().HasMaxLength(50);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(500);
            Property(x => x.Sms).HasColumnName("SMS").IsOptional().HasMaxLength(500);
            Property(x => x.CommandLineId).HasColumnName("CommandLineID").IsOptional();
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.RowNum).HasColumnName("RowNum").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Client
    internal partial class RmsClientConfiguration : EntityTypeConfiguration<RmsClient>
    {
        public RmsClientConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_Client");
            HasKey(x => x.ClientId);

            Property(x => x.ClientId).HasColumnName("ClientID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ClientTypeId).HasColumnName("ClientTypeID").IsOptional();
            Property(x => x.ClientCode).HasColumnName("ClientCode").IsOptional().HasMaxLength(50);
            Property(x => x.ReferenceClientId).HasColumnName("ReferenceClientID").IsOptional();
            Property(x => x.LocationId).HasColumnName("LocationID").IsOptional();
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.Enable).HasColumnName("Enable").IsOptional();
            Property(x => x.EffectiveDate).HasColumnName("EffectiveDate").IsOptional();
            Property(x => x.ExpiredDate).HasColumnName("ExpiredDate").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasOptional(a => a.RmsClientType).WithMany(b => b.RmsClients).HasForeignKey(c => c.ClientTypeId); // FK_RMS_Client_RMS_ClientType
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_ClientMonitoring
    internal partial class RmsClientMonitoringConfiguration : EntityTypeConfiguration<RmsClientMonitoring>
    {
        public RmsClientMonitoringConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_ClientMonitoring");
            HasKey(x => new { x.ClientId, x.MonitoringProfileId });

            Property(x => x.ClientId).HasColumnName("ClientID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.MonitoringProfileId).HasColumnName("MonitoringProfileID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.EffectiveDate).HasColumnName("EffectiveDate").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.RmsClient).WithMany(b => b.RmsClientMonitorings).HasForeignKey(c => c.ClientId); // FK_RMS_ClientMonitoring_RMS_Client
            HasRequired(a => a.RmsMonitoringProfile).WithMany(b => b.RmsClientMonitorings).HasForeignKey(c => c.MonitoringProfileId); // FK_RMS_ClientMonitoring_RMS_MonitoringProfile
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_ClientSeverityAction
    internal partial class RmsClientSeverityActionConfiguration : EntityTypeConfiguration<RmsClientSeverityAction>
    {
        public RmsClientSeverityActionConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_ClientSeverityAction");
            HasKey(x => new { x.ClientId, x.SeverityLevelId });

            Property(x => x.ClientId).HasColumnName("ClientID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.SeverityLevelId).HasColumnName("SeverityLevelID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Email).HasColumnName("Email").IsOptional().HasMaxLength(500);
            Property(x => x.Sms).HasColumnName("SMS").IsOptional().HasMaxLength(500);
            Property(x => x.CommandLindId).HasColumnName("CommandLindID").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasRequired(a => a.RmsClient).WithMany(b => b.RmsClientSeverityActions).HasForeignKey(c => c.ClientId); // FK_RMS_ClientSeverityAction_RMS_Client
            HasRequired(a => a.RmsSeverityLevel).WithMany(b => b.RmsClientSeverityActions).HasForeignKey(c => c.SeverityLevelId); // FK_RMS_ClientSeverityAction_RMS_SeverityLevel
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_ClientType
    internal partial class RmsClientTypeConfiguration : EntityTypeConfiguration<RmsClientType>
    {
        public RmsClientTypeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_ClientType");
            HasKey(x => x.ClientTypeId);

            Property(x => x.ClientTypeId).HasColumnName("ClientTypeID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.Clienttype).HasColumnName("Clienttype").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_ColorLabel
    internal partial class RmsColorLabelConfiguration : EntityTypeConfiguration<RmsColorLabel>
    {
        public RmsColorLabelConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_ColorLabel");
            HasKey(x => x.ColorCode);

            Property(x => x.ColorCode).HasColumnName("ColorCode").IsRequired().HasMaxLength(50).HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ColorName).HasColumnName("ColorName").IsOptional().HasMaxLength(50);
            Property(x => x.ColorTagStart).HasColumnName("ColorTagStart").IsOptional().HasMaxLength(500);
            Property(x => x.ColorTagEnd).HasColumnName("ColorTagEnd").IsOptional().HasMaxLength(500);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Device
    internal partial class RmsDeviceConfiguration : EntityTypeConfiguration<RmsDevice>
    {
        public RmsDeviceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_Device");
            HasKey(x => x.DeviceId);

            Property(x => x.DeviceId).HasColumnName("DeviceID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DeviceTypeId).HasColumnName("DeviceTypeID").IsOptional();
            Property(x => x.DeviceCode).HasColumnName("DeviceCode").IsOptional().HasMaxLength(20);
            Property(x => x.Brand).HasColumnName("Brand").IsOptional().HasMaxLength(50);
            Property(x => x.Model).HasColumnName("Model").IsOptional().HasMaxLength(50);
            Property(x => x.DeviceManagerName).HasColumnName("DeviceManagerName").IsOptional().HasMaxLength(100);
            Property(x => x.DeviceManagerId).HasColumnName("DeviceManagerID").IsOptional().HasMaxLength(100);
            Property(x => x.StringValue).HasColumnName("StringValue").IsOptional().HasMaxLength(100);
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasOptional(a => a.RmsDeviceType).WithMany(b => b.RmsDevices).HasForeignKey(c => c.DeviceTypeId); // FK_RMS_Device_RMS_DeviceType
            HasMany(t => t.RmsMessages).WithMany(t => t.RmsDevices).Map(m => 
            {
                m.ToTable("RMS_DeviceMessage");
                m.MapLeftKey("DeviceID");
                m.MapRightKey("MessageID");
            });
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_DeviceType
    internal partial class RmsDeviceTypeConfiguration : EntityTypeConfiguration<RmsDeviceType>
    {
        public RmsDeviceTypeConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_DeviceType");
            HasKey(x => x.DeviceTypeId);

            Property(x => x.DeviceTypeId).HasColumnName("DeviceTypeID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.DeviceType).HasColumnName("DeviceType").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Message
    internal partial class RmsMessageConfiguration : EntityTypeConfiguration<RmsMessage>
    {
        public RmsMessageConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_Message");
            HasKey(x => x.MessageId);

            Property(x => x.MessageId).HasColumnName("MessageID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.MessageGroupId).HasColumnName("MessageGroupID").IsOptional();
            Property(x => x.Message).HasColumnName("Message").IsOptional().HasMaxLength(50);
            Property(x => x.SeverityLevelId).HasColumnName("SeverityLevelID").IsOptional();
            Property(x => x.ReadOnly).HasColumnName("ReadOnly").IsOptional();
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.ActiveStatus).HasColumnName("ActiveStatus").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasOptional(a => a.RmsMessageGroup).WithMany(b => b.RmsMessages).HasForeignKey(c => c.MessageGroupId); // FK_RMS_Message_RMS_MessageGroup
            HasOptional(a => a.RmsSeverityLevel).WithMany(b => b.RmsMessages).HasForeignKey(c => c.SeverityLevelId); // FK_RMS_Message_RMS_SeverityLevel
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_MessageGroup
    internal partial class RmsMessageGroupConfiguration : EntityTypeConfiguration<RmsMessageGroup>
    {
        public RmsMessageGroupConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_MessageGroup");
            HasKey(x => x.MessageGroupId);

            Property(x => x.MessageGroupId).HasColumnName("MessageGroupID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.MessageGroupName).HasColumnName("MessageGroupName").IsOptional().HasMaxLength(100);
            Property(x => x.MessageType).HasColumnName("MessageType").IsOptional().HasMaxLength(20);
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_MonitoringProfile
    internal partial class RmsMonitoringProfileConfiguration : EntityTypeConfiguration<RmsMonitoringProfile>
    {
        public RmsMonitoringProfileConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_MonitoringProfile");
            HasKey(x => x.MonitoringProfileId);

            Property(x => x.MonitoringProfileId).HasColumnName("MonitoringProfileID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ProfileName).HasColumnName("ProfileName").IsOptional().HasMaxLength(100);
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_MonitoringProfileDevice
    internal partial class RmsMonitoringProfileDeviceConfiguration : EntityTypeConfiguration<RmsMonitoringProfileDevice>
    {
        public RmsMonitoringProfileDeviceConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_MonitoringProfileDevice");
            HasKey(x => x.MonitoringProfileDeviceId);

            Property(x => x.MonitoringProfileDeviceId).HasColumnName("MonitoringProfileDeviceID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.MonitoringProfileId).HasColumnName("MonitoringProfileID").IsOptional();
            Property(x => x.DeviceId).HasColumnName("DeviceID").IsOptional();
            Property(x => x.HighThreshold).HasColumnName("HighThreshold").IsOptional().HasPrecision(10,2);
            Property(x => x.LowThreshold).HasColumnName("LowThreshold").IsOptional().HasPrecision(10,2);
            Property(x => x.ComPort).HasColumnName("COMPort").IsOptional().HasMaxLength(5);
            Property(x => x.BooleanValue).HasColumnName("BooleanValue").IsOptional();
            Property(x => x.StringValue).HasColumnName("StringValue").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasOptional(a => a.RmsMonitoringProfile).WithMany(b => b.RmsMonitoringProfileDevices).HasForeignKey(c => c.MonitoringProfileId); // FK_RMS_MonitoringProfileDevice_RMS_MonitoringProfile
            HasOptional(a => a.RmsDevice).WithMany(b => b.RmsMonitoringProfileDevices).HasForeignKey(c => c.DeviceId); // FK_RMS_MonitoringProfileDevice_RMS_Device
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Report_MonitoringRaw
    internal partial class RmsReportMonitoringRawConfiguration : EntityTypeConfiguration<RmsReportMonitoringRaw>
    {
        public RmsReportMonitoringRawConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_Report_MonitoringRaw");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.ClientCode).HasColumnName("ClientCode").IsOptional().HasMaxLength(50);
            Property(x => x.ClientIpAddress).HasColumnName("ClientIPAddress").IsOptional().HasMaxLength(50);
            Property(x => x.DeviceCode).HasColumnName("DeviceCode").IsOptional().HasMaxLength(20);
            Property(x => x.MessageGroupCode).HasColumnName("MessageGroupCode").IsOptional().HasMaxLength(10);
            Property(x => x.Message).HasColumnName("Message").IsOptional().HasMaxLength(50);
            Property(x => x.MessageDateTime).HasColumnName("MessageDateTime").IsOptional();
            Property(x => x.MessageRemark).HasColumnName("MessageRemark").IsOptional().HasMaxLength(500);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_Report_SummaryMonitoring
    internal partial class RmsReportSummaryMonitoringConfiguration : EntityTypeConfiguration<RmsReportSummaryMonitoring>
    {
        public RmsReportSummaryMonitoringConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_Report_SummaryMonitoring");
            HasKey(x => x.Id);

            Property(x => x.Id).HasColumnName("ID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.None);
            Property(x => x.RawId).HasColumnName("RawID").IsOptional();
            Property(x => x.ClientId).HasColumnName("ClientID").IsOptional();
            Property(x => x.ClientCode).HasColumnName("ClientCode").IsOptional().HasMaxLength(50);
            Property(x => x.ClientIpAddress).HasColumnName("ClientIPAddress").IsOptional().HasMaxLength(50);
            Property(x => x.LocationId).HasColumnName("LocationID").IsOptional();
            Property(x => x.DeviceId).HasColumnName("DeviceID").IsOptional();
            Property(x => x.DeviceCode).HasColumnName("DeviceCode").IsOptional().HasMaxLength(20);
            Property(x => x.MessageGroupId).HasColumnName("MessageGroupID").IsOptional();
            Property(x => x.MessageGroupCode).HasColumnName("MessageGroupCode").IsOptional().HasMaxLength(10);
            Property(x => x.MessageGroupName).HasColumnName("MessageGroupName").IsOptional().HasMaxLength(100);
            Property(x => x.MessageId).HasColumnName("MessageID").IsOptional();
            Property(x => x.Message).HasColumnName("Message").IsOptional().HasMaxLength(50);
            Property(x => x.Status).HasColumnName("Status").IsOptional();
            Property(x => x.MessageDateTime).HasColumnName("MessageDateTime").IsOptional();
            Property(x => x.MessageRemark).HasColumnName("MessageRemark").IsOptional().HasMaxLength(500);
            Property(x => x.EventDateTime).HasColumnName("EventDateTime").IsOptional();
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(50);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(50);
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // RMS_SeverityLevel
    internal partial class RmsSeverityLevelConfiguration : EntityTypeConfiguration<RmsSeverityLevel>
    {
        public RmsSeverityLevelConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".RMS_SeverityLevel");
            HasKey(x => x.SeverityLevelId);

            Property(x => x.SeverityLevelId).HasColumnName("SeverityLevelID").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.LevelCode).HasColumnName("LevelCode").IsOptional().HasMaxLength(10);
            Property(x => x.LevelName).HasColumnName("LevelName").IsOptional().HasMaxLength(50);
            Property(x => x.OrderList).HasColumnName("OrderList").IsOptional();
            Property(x => x.ActiveList).HasColumnName("ActiveList").IsOptional();
            Property(x => x.DefaultActionProfileId).HasColumnName("DefaultActionProfileID").IsOptional();
            Property(x => x.ColorCode).HasColumnName("ColorCode").IsOptional().HasMaxLength(50);
            Property(x => x.CreatedDate).HasColumnName("CreatedDate").IsOptional();
            Property(x => x.CreatedBy).HasColumnName("CreatedBy").IsOptional().HasMaxLength(100);
            Property(x => x.UpdatedDate).HasColumnName("UpdatedDate").IsOptional();
            Property(x => x.UpdatedBy).HasColumnName("UpdatedBy").IsOptional().HasMaxLength(100);

            // Foreign keys
            HasOptional(a => a.RmsActionProfile).WithMany(b => b.RmsSeverityLevels).HasForeignKey(c => c.DefaultActionProfileId); // FK_RMS_SeverityLevel_RMS_ActionProfile
            HasOptional(a => a.RmsColorLabel).WithMany(b => b.RmsSeverityLevels).HasForeignKey(c => c.ColorCode); // FK_RMS_SeverityLevel_RMS_ColorLabel
            InitializePartial();
        }
        partial void InitializePartial();
    }

    // sysdiagrams
    internal partial class SysdiagramConfiguration : EntityTypeConfiguration<Sysdiagram>
    {
        public SysdiagramConfiguration(string schema = "dbo")
        {
            ToTable(schema + ".sysdiagrams");
            HasKey(x => x.DiagramId);

            Property(x => x.Name).HasColumnName("name").IsRequired().HasMaxLength(128);
            Property(x => x.PrincipalId).HasColumnName("principal_id").IsRequired();
            Property(x => x.DiagramId).HasColumnName("diagram_id").IsRequired().HasDatabaseGeneratedOption(DatabaseGeneratedOption.Identity);
            Property(x => x.Version).HasColumnName("version").IsOptional();
            Property(x => x.Definition).HasColumnName("definition").IsOptional();
            InitializePartial();
        }
        partial void InitializePartial();
    }

}
