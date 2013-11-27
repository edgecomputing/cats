using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Data.Entity.ModelConfiguration.Conventions;
using Cats.Models.Hubs;
using Cats.Models.Hubs.Mapping;


namespace Cats.Data.Hub
{
    public partial class HubContext : DbContext
    {
        static HubContext()
        {
            Database.SetInitializer<HubContext>(null);
        }

        public HubContext()
            : base("Name=CatsContext")
        {
        }

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Adjustment> Adjustments { get; set; }
        public DbSet<AdjustmentReason> AdjustmentReasons { get; set; }
        public DbSet<AdminUnit> AdminUnits { get; set; }
        public DbSet<AdminUnitType> AdminUnitTypes { get; set; }
        public DbSet<Audit> Audits { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityGrade> CommodityGrades { get; set; }
        public DbSet<CommoditySource> CommoditySources { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Detail> Details { get; set; }
        public DbSet<Dispatch> Dispatches { get; set; }
        public DbSet<DispatchAllocation> DispatchAllocations { get; set; }
        public DbSet<DispatchDetail> DispatchDetails { get; set; }
        public DbSet<Donor> Donors { get; set; }
        public DbSet<ErrorLog> ErrorLogs { get; set; }
        public DbSet<FDP> FDPs { get; set; }
        public DbSet<ForgetPasswordRequest> ForgetPasswordRequests { get; set; }
        public DbSet<GiftCertificate> GiftCertificates { get; set; }
        public DbSet<GiftCertificateDetail> GiftCertificateDetails { get; set; }
        public DbSet<Models.Hubs.Hub> Hubs { get; set; }
        public DbSet<HubOwner> HubOwners { get; set; }
        public DbSet<HubSetting> HubSettings { get; set; }
        public DbSet<HubSettingValue> HubSettingValues { get; set; }
        public DbSet<InternalMovement> InternalMovements { get; set; }
        public DbSet<Ledger> Ledgers { get; set; }
        public DbSet<LedgerType> LedgerTypes { get; set; }
        public DbSet<LetterTemplate> LetterTemplates { get; set; }
        public DbSet<Master> Masters { get; set; }
        public DbSet<OtherDispatchAllocation> OtherDispatchAllocations { get; set; }
        public DbSet<Partition> Partitions { get; set; }
        public DbSet<Period> Periods { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<ProjectCode> ProjectCodes { get; set; }
        public DbSet<ReceiptAllocation> ReceiptAllocations { get; set; }
        public DbSet<Receive> Receives { get; set; }
        public DbSet<ReceiveDetail> ReceiveDetails { get; set; }
        public DbSet<ReleaseNote> ReleaseNotes { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<SessionAttempt> SessionAttempts { get; set; }
        public DbSet<SessionHistory> SessionHistories { get; set; }
        public DbSet<Setting> Settings { get; set; }
        public DbSet<ShippingInstruction> ShippingInstructions { get; set; }
        public DbSet<SMS> SMS { get; set; }
        public DbSet<StackEvent> StackEvents { get; set; }
        public DbSet<StackEventType> StackEventTypes { get; set; }
        public DbSet<Store> Stores { get; set; }
        //public DbSet<sysdiagram> sysdiagrams { get; set; }
        public DbSet<Transaction> Transactions { get; set; }
        public DbSet<TransactionGroup> TransactionGroups { get; set; }
        public DbSet<Translation> Translations { get; set; }
        public DbSet<Transporter> Transporters { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<UserHub> UserHubs { get; set; }
        public DbSet<UserProfile> UserProfiles { get; set; }
        public DbSet<UserRole> UserRoles { get; set; }
       // public DbSet<RPT_Distribution_Result> RPT_Distribution_Results { get; set; }
        public DbSet<VWCommodityReceived> VwCommodityReceiveds { get; set; }
        public DbSet<VWCarryOver> VWCarryOvers { get; set; }
        public DbSet<VWTransferredStock> VWTransferredStocks { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
            modelBuilder.Configurations.Add(new AccountMap());
            modelBuilder.Configurations.Add(new AdjustmentMap());
            modelBuilder.Configurations.Add(new AdjustmentReasonMap());
            modelBuilder.Configurations.Add(new AdminUnitMap());
            modelBuilder.Configurations.Add(new AdminUnitTypeMap());
            modelBuilder.Configurations.Add(new AuditMap());
            modelBuilder.Configurations.Add(new CommodityMap());
            modelBuilder.Configurations.Add(new CommodityGradeMap());
            modelBuilder.Configurations.Add(new CommoditySourceMap());
            modelBuilder.Configurations.Add(new CommodityTypeMap());
            modelBuilder.Configurations.Add(new ContactMap());
            modelBuilder.Configurations.Add(new DetailMap());
            modelBuilder.Configurations.Add(new DispatchMap());
            modelBuilder.Configurations.Add(new DispatchAllocationMap());
            modelBuilder.Configurations.Add(new DispatchDetailMap());
            modelBuilder.Configurations.Add(new DonorMap());
            modelBuilder.Configurations.Add(new ErrorLogMap());
            modelBuilder.Configurations.Add(new FDPMap());
            modelBuilder.Configurations.Add(new ForgetPasswordRequestMap());
            modelBuilder.Configurations.Add(new GiftCertificateMap());
            modelBuilder.Configurations.Add(new GiftCertificateDetailMap());
            modelBuilder.Configurations.Add(new HubMap());
            modelBuilder.Configurations.Add(new HubOwnerMap());
            modelBuilder.Configurations.Add(new HubSettingMap());
            modelBuilder.Configurations.Add(new HubSettingValueMap());
            modelBuilder.Configurations.Add(new InternalMovementMap());
            modelBuilder.Configurations.Add(new LedgerMap());
            modelBuilder.Configurations.Add(new LedgerTypeMap());
            modelBuilder.Configurations.Add(new LetterTemplateMap());
            modelBuilder.Configurations.Add(new MasterMap());
            modelBuilder.Configurations.Add(new OtherDispatchAllocationMap());
            modelBuilder.Configurations.Add(new PartitionMap());
            modelBuilder.Configurations.Add(new PeriodMap());
            modelBuilder.Configurations.Add(new ProgramMap());
            modelBuilder.Configurations.Add(new ProjectCodeMap());
            modelBuilder.Configurations.Add(new ReceiptAllocationMap());
            modelBuilder.Configurations.Add(new ReceiveMap());
            modelBuilder.Configurations.Add(new ReceiveDetailMap());
            modelBuilder.Configurations.Add(new ReleaseNoteMap());
            modelBuilder.Configurations.Add(new RoleMap());
            modelBuilder.Configurations.Add(new SessionAttemptMap());
            modelBuilder.Configurations.Add(new SessionHistoryMap());
            modelBuilder.Configurations.Add(new SettingMap());
            modelBuilder.Configurations.Add(new ShippingInstructionMap());
            modelBuilder.Configurations.Add(new SMSMap());
            modelBuilder.Configurations.Add(new StackEventMap());
            modelBuilder.Configurations.Add(new StackEventTypeMap());
            modelBuilder.Configurations.Add(new StoreMap());
            modelBuilder.Configurations.Add(new sysdiagramMap());
            modelBuilder.Configurations.Add(new TransactionMap());
            modelBuilder.Configurations.Add(new TransactionGroupMap());
            modelBuilder.Configurations.Add(new TranslationMap());
            modelBuilder.Configurations.Add(new TransporterMap());
            modelBuilder.Configurations.Add(new UnitMap());
            modelBuilder.Configurations.Add(new UserHubMap());
            modelBuilder.Configurations.Add(new UserProfileMap());
            modelBuilder.Configurations.Add(new UserRoleMap());
            modelBuilder.Configurations.Add(new VWCommodityReceivedMap());
            modelBuilder.Configurations.Add(new VWCarryOverMap());
            modelBuilder.Configurations.Add(new VWTransferredStockMap());

           

       /*     modelBuilder.Entity<UserProfile>()
           .HasMany(n => n.UserHubs)
           .WithRequired() // <- no param because not exposed end of relation,
                // nc => nc.News would throw an exception
                // because nc.News is in the base class
           .Map(a => a.MapKey("UserProfileID"));*/
        }
    }
}
