using System.Data.Entity;
using Cats.Models;
using Cats.Models.Mapping;
using System.Data.Entity.ModelConfiguration.Conventions;


namespace Cats.Data
{
    public partial class CatsContext : DbContext
    {
        static CatsContext()
        {
            Database.SetInitializer<CatsContext>(null);
        }

        public CatsContext() : base("Name=CatsContext") { }

        // TODO: Add properties to access set of Poco classes
        public DbSet<RegionalRequest> RegionalRequests { get; set; }
        public DbSet<RegionalRequestDetail> RegionalRequestDetails { get; set; }
        public DbSet<ReliefRequisition> ReliefRequisitions { get; set; }
        public DbSet<ReliefRequisitionDetail> ReliefRequisitionDetails { get; set; }
        public DbSet<AdminUnit> AdminUnits { get; set; }
        public DbSet<Commodity> Commodities { get; set; }
        public DbSet<CommodityType> CommodityTypes { get; set; }
        public DbSet<FDP> Fdps { get; set; }
        public DbSet<Program> Programs { get; set; }
        public DbSet<AdminUnitType> AdminUnitTypes { get; set; }
        public DbSet<Hub> Hubs { get; set; }
        public DbSet<DispatchAllocation> DispatchAllocations { get; set; }
        public DbSet<DispatchAllocationDetail> DispatchDetail { get; set; }
        public DbSet<Bid> Bids { get; set; } 
        public DbSet<BidDetail> BidDetails { get; set; }
        public DbSet<Status> Statuses { get; set; } 

        public DbSet<TransportBidPlan> TransportBidPlans { get; set; }
        public DbSet<TransportBidPlanDetail> TransportBidPlanDetails { get; set; }
       
        public DbSet<ProjectCodeAllocation> ProjectCodeAllocation { get; set; }

        public DbSet<TransportRequisition> TransportRequisition { get; set; }
        public DbSet<HubAllocation> HubAllocation { get; set; }
        public DbSet<ProjectCode> ProjectCode { get; set; }
        public DbSet<ShippingInstruction> ShippingInstruction { get; set; }

       
        public DbSet<BidWinner> BidWinners { get; set; }
      

        public DbSet<TransportOrder> TransportOrders { get; set; }
        public DbSet<TransportOrderDetail> TransportOrderDetails { get; set; }
        public DbSet<vwTransportOrder> vwTransportOrders { get; set; }
        
        public DbSet<TransportRequisitionDetail> TransportRequisitionDetails { get; set; }
        public DbSet<Transaction> Transaction { get; set; }
        public DbSet<ReceiptAllocation> ReceiptAllocation { get; set; } 


        public DbSet<Workflow> Workflows { get; set; }
        public DbSet<WorkflowStatus> WorkflowStatuses { get; set; }
        public DbSet<TransportBidQuotation> TransportBidQuotations { get; set; }
        public DbSet<ApplicationSetting> ApplicationSetting { get; set; }
        public DbSet<Ration> Rations { get; set; }

        public DbSet<NeedAssessmentHeader> NeedAssessmentHeader { get; set; }
        public DbSet<NeedAssessmentDetail> NeedAssessmentDetail { get; set; }

        public DbSet<HRD> HRDs { get; set; }
        public DbSet<HRDDetail> HRDDetails { get; set; }
        public DbSet<RationDetail> RationDetails { get; set; }
        public DbSet<RegionalPSNPPlan> RegionalPSNPPlans { get; set; }
        public DbSet<RegionalPSNPPlanDetail> RegionalPSNPPlanDetails { get; set; }
        public DbSet<HRDCommodityDetail> HrdCommodityDetails { get; set; }
        //public DbSet<LocalizedText> LocalizedTexts { get; set; }

        //public DbSet<Product> Products { get; set; }
        public DbSet<RequestDetailCommodity> RequestDetailCommodities { get; set; }

        public DbSet<GiftCertificate> GiftCertificate { get; set; }
        public DbSet<Unit> Units { get; set; }
        public DbSet<Season> Seasons { get; set; } 

        public DbSet<ProcessTemplate> ProcessTemplates { get; set; }
        public DbSet<StateTemplate> StateTemplates { get; set; }
        public DbSet<FlowTemplate> FlowTemplates { get; set; }

        public DbSet<Contribution> Contributions { get; set; }
        public DbSet<ContributionDetail> ContributionDetails { get; set; }
        public DbSet<Donor> Donors { get; set; } 

        //public DbSet<AccountTransaction> AccountTransactions { get; set; }
        //public DbSet<vwPSNPAnnualPlan> vwPSNPAnnualPlans { get; set; }
        public DbSet<BusinessProcess> BusinessProcesss { get; set; }
        public DbSet<BusinessProcessState> BusinessProcessStates { get; set; }

        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            modelBuilder.Configurations.Add(new BusinessProcessStateMap());
            //TODO: Add mapping information for each Poco model.
            modelBuilder.Configurations.Add(new BusinessProcessMap());
            modelBuilder.Configurations.Add(new ProcessTemplateMap());
            modelBuilder.Configurations.Add(new StateTemplateMap());
            modelBuilder.Configurations.Add(new FlowTemplateMap());

            modelBuilder.Configurations.Add(new RegionalRequestMap());
            modelBuilder.Configurations.Add(new RegionalRequestDetailMap());
            modelBuilder.Configurations.Add(new ReliefRequisitionMap());
            modelBuilder.Configurations.Add(new ReliefRequisitionDetailMap());
            modelBuilder.Configurations.Add(new AdminUnitMap());
            modelBuilder.Configurations.Add(new CommodityMap());
            modelBuilder.Configurations.Add(new CommodityTypeMap());
            modelBuilder.Configurations.Add(new FDPMap());
            modelBuilder.Configurations.Add(new ProgramMap());
            modelBuilder.Configurations.Add(new AdminUnitTypeMap());
            modelBuilder.Configurations.Add(new BidDetailMap());
            modelBuilder.Configurations.Add(new BidMap());
            modelBuilder.Configurations.Add(new StatusMap());
       
            modelBuilder.Configurations.Add(new TransporterMap());
            modelBuilder.Configurations.Add(new TransportBidPlanMap());
            modelBuilder.Configurations.Add(new TransportBidPlanDetailMap());

            modelBuilder.Configurations.Add(new ProjectCodeAllocationMap());

            modelBuilder.Configurations.Add(new HubAllocationMap());
            modelBuilder.Configurations.Add(new ProjectCodeMap());
            modelBuilder.Configurations.Add(new ShippingInstructionMap());

            modelBuilder.Configurations.Add(new BidWinnerMap());
           
            modelBuilder.Configurations.Add(new TransportOrderMap());
            modelBuilder.Configurations.Add(new TransportOrderDetailMap());
            modelBuilder.Configurations.Add(new vwTransportOrderMap());
            modelBuilder.Configurations.Add(new TransportRequisitionMap());
            modelBuilder.Configurations.Add(new TransportRequisitionDetailMap());
            modelBuilder.Configurations.Add(new TransactionMap());
            modelBuilder.Configurations.Add(new ReceiptAllocationMap());
            modelBuilder.Conventions.Remove<PluralizingTableNameConvention>();
           
             modelBuilder.Configurations.Add(new WorkflowMap());
            modelBuilder.Configurations.Add(new WorkflowStatusMap());
            modelBuilder.Configurations.Add(new TransportBidQuotationMap());
            modelBuilder.Configurations.Add(new ApplicationSettingMap());
            modelBuilder.Configurations.Add(new RationMap());

            modelBuilder.Configurations.Add(new NeedAssessmentHeaderMap());
            modelBuilder.Configurations.Add(new NeedAssessmentDetailMap());
            modelBuilder.Configurations.Add(new NeedAssessmentMap());

            modelBuilder.Configurations.Add(new HRDMap());
            modelBuilder.Configurations.Add(new HRDDetailMap());
            modelBuilder.Configurations.Add(new RationDetailMap());

            modelBuilder.Configurations.Add(new RegionalPSNPPlanMap());
            modelBuilder.Configurations.Add(new RegionalPSNPPlanDetailMap());
            

            modelBuilder.Configurations.Add(new RequestDetailCommodityMap());

            modelBuilder.Configurations.Add(new GiftCertificateMap());
            modelBuilder.Configurations.Add(new GiftCertificateDetailMap());

            modelBuilder.Configurations.Add(new UnitMap());

            modelBuilder.Configurations.Add(new SeasonMap());

            modelBuilder.Configurations.Add(new ContributionMap());
            modelBuilder.Configurations.Add(new ContributionDetailMap());
            modelBuilder.Configurations.Add(new DonorMap());

            //modelBuilder.Configurations.Add(new AccountTransactionMap());
            //modelBuilder.Configurations.Add(new vwPSNPAnnualPlanMap());


        }


        

    }
}
