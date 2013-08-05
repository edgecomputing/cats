using System;
using System.Data.Entity;
using Cats.Models;
using Cats.Data.Repository;


namespace Cats.Data.UnitWork
{
    public class UnitOfWork : IUnitOfWork 
    {
        private readonly CatsContext _context;

        // TODO: Add private properties to for each repository
      
      
      
        private IGenericRepository<Bid> bidRepository;
        private IGenericRepository<BidDetail> bidDetailRepository; 
        private IGenericRepository<Status> statusRepository; 
        private IGenericRepository<BidWinner> bidWinnerRepository;
        private IGenericRepository<TransportOrderDetail> transportOrderDetailRepository;
        private IGenericRepository<RationDetail> rationDetailRepository;
        private IGenericRepository<HRD> hrdRepository;
        private IGenericRepository<HRDDetail> hrdDetailRepository;
        private IGenericRepository<RegionalPSNPPlan> regionalPSNPPlanRepository;
        private IGenericRepository<RegionalPSNPPlanDetail> regionalPSNPPlanDetailRepository;

        public UnitOfWork()
        {
            this._context = new CatsContext();
        }

        // TODO: Consider adding separate properties for each repositories.

        /// <summary>
        /// ReliefRequistionRepository
        /// </summary>
        /// 
        private IGenericRepository<ReceiptAllocation> receiptAllocationReository;
        public IGenericRepository<ReceiptAllocation> ReceiptAllocationReository
        {
            get { return this.receiptAllocationReository ?? (this.receiptAllocationReository = new GenericRepository<ReceiptAllocation>(_context)); }

        }
        private IGenericRepository<Transaction> transactionReository;
        public IGenericRepository<Transaction> TransactionRepository
        {
            get { return this.transactionReository ?? (this.transactionReository = new GenericRepository<Transaction>(_context)); }

        }
        private IGenericRepository<ShippingInstruction> shippingInstructionReository;
        public IGenericRepository<ShippingInstruction> ShippingInstructionRepository
        {
            get { return this.shippingInstructionReository ?? (this.shippingInstructionReository = new GenericRepository<ShippingInstruction>(_context)); }

        }
        private IGenericRepository<ProjectCode> projectCodeRepository;
        public IGenericRepository<ProjectCode> ProjectCodeRepository
        {
            get { return this.projectCodeRepository ?? (this.projectCodeRepository = new GenericRepository<ProjectCode>(_context)); }

        }

        private IGenericRepository<ProjectCodeAllocation> projectCodeAllocationRepository;
        public IGenericRepository<ProjectCodeAllocation> ProjectCodeAllocationRepository
        {
            get { return this.projectCodeAllocationRepository ?? (this.projectCodeAllocationRepository = new GenericRepository<ProjectCodeAllocation>(_context)); }

        }


        private IGenericRepository<HubAllocation> hubAllocationRepository;
        public IGenericRepository<HubAllocation> HubAllocationRepository
        {
            get { return this.hubAllocationRepository ?? (this.hubAllocationRepository = new GenericRepository<HubAllocation>(_context)); }

        }
        private IGenericRepository<DispatchAllocation> dispatchAllocationRepository;
        public IGenericRepository<DispatchAllocation> DispatchAllocationRepository
        {
            get { return this.dispatchAllocationRepository ?? (this.dispatchAllocationRepository = new GenericRepository<DispatchAllocation>(_context)); }

        }

        private IGenericRepository<RegionalRequest> regionalRequestRepository;

        public IGenericRepository<RegionalRequest> RegionalRequestRepository
        {

            get { return this.regionalRequestRepository ?? (this.regionalRequestRepository = new GenericRepository<RegionalRequest>(_context)); }

        }



        private IGenericRepository<RegionalRequestDetail> regionalRequestDetailRepository;

        public IGenericRepository<RegionalRequestDetail> RegionalRequestDetailRepository
        {

            get { return this.regionalRequestDetailRepository ?? (this.regionalRequestDetailRepository = new GenericRepository<RegionalRequestDetail>(_context)); }

        }


        public IGenericRepository<Bid> BidRepository
        {
            get { return this.bidRepository ?? (this.bidRepository = new GenericRepository<Bid>(_context)); }
        }



        public IGenericRepository<BidDetail> BidDetailRepository
        {
            get { return this.bidDetailRepository ?? (this.bidDetailRepository = new GenericRepository<BidDetail>(_context)); }
        }

        public IGenericRepository<Status> StatusRepository
        {
            get { return this.statusRepository ?? (this.statusRepository = new GenericRepository<Status>(_context)); }
        }




        private IGenericRepository<AdminUnit> adminUnitRepository;

        public IGenericRepository<AdminUnit> AdminUnitRepository
        {

            get { return this.adminUnitRepository ?? (this.adminUnitRepository = new GenericRepository<AdminUnit>(_context)); }

        }




        private IGenericRepository<AdminUnitType> adminUnitTypeRepository;

        public IGenericRepository<AdminUnitType> AdminUnitTypeRepository
        {

            get { return this.adminUnitTypeRepository ?? (this.adminUnitTypeRepository = new GenericRepository<AdminUnitType>(_context)); }

        }




        private IGenericRepository<Commodity> commodityRepository;

        public IGenericRepository<Commodity> CommodityRepository
        {

            get { return this.commodityRepository ?? (this.commodityRepository = new GenericRepository<Commodity>(_context)); }

        }




        private IGenericRepository<CommodityType> commodityTypeRepository;

        public IGenericRepository<CommodityType> CommodityTypeRepository
        {

            get { return this.commodityTypeRepository ?? (this.commodityTypeRepository = new GenericRepository<CommodityType>(_context)); }

        }




        private IGenericRepository<FDP> fdpRepository;

        public IGenericRepository<FDP> FDPRepository
        {

            get { return this.fdpRepository ?? (this.fdpRepository = new GenericRepository<FDP>(_context)); }

        }



      

        private IGenericRepository<Program> programRepository;

        public IGenericRepository<Program> ProgramRepository
        {

            get { return this.programRepository ?? (this.programRepository = new GenericRepository<Program>(_context)); }

        }





        //private IGenericRepository<Round> roundRepository;


        //    get { return this.roundRepository ?? (this.roundRepository = new GenericRepository<Round>(_context)); }

        //}

        private IGenericRepository<Hub> hubRepository;
        public IGenericRepository<Hub> HubRepository
        {
            get { return this.hubRepository ?? (this.hubRepository = new GenericRepository<Hub>(_context)); }
        }





        private IGenericRepository<ReliefRequisition> reliefRequistionRepository;

        public IGenericRepository<ReliefRequisition> ReliefRequisitionRepository
        {

            get { return this.reliefRequistionRepository ?? (this.reliefRequistionRepository = new GenericRepository<ReliefRequisition>(_context)); }

        }




        private IGenericRepository<ReliefRequisitionDetail> reliefRequisitionRepository;

        public IGenericRepository<ReliefRequisitionDetail> ReliefRequisitionDetailRepository
        {

            get { return this.reliefRequisitionRepository ?? (this.reliefRequisitionRepository = new GenericRepository<ReliefRequisitionDetail>(_context)); }

        }

        private IGenericRepository<Transporter> transporterRepository;

        public IGenericRepository<Transporter> TransporterRepository
        {

            get { return this.transporterRepository ?? (this.transporterRepository = new GenericRepository<Transporter>(_context)); }

        }


        private IGenericRepository<TransportBidPlan> transportBidPlanRepository;

        public IGenericRepository<TransportBidPlan> TransportBidPlanRepository
        {

            get { return this.transportBidPlanRepository ?? (this.transportBidPlanRepository = new GenericRepository<TransportBidPlan>(_context)); }

        }

        private IGenericRepository<TransportBidPlanDetail> transportBidPlanDetailRepository;

        public IGenericRepository<TransportBidPlanDetail> TransportBidPlanDetailRepository
        {

            get { return this.transportBidPlanDetailRepository ?? (this.transportBidPlanDetailRepository = new GenericRepository<TransportBidPlanDetail>(_context)); }

        }

   //     private IGenericRepository<RequisitionViewModel> requisitionViewModelRepository;

        //private IGenericRepository<RequisitionViewModel> requisitionViewModelRepository;


        //public IGenericRepository<RequisitionViewModel> RequisitionViewModelRepository
        //{

        //    get { return this.requisitionViewModelRepository ?? (this.requisitionViewModelRepository = new GenericRepository<RequisitionViewModel>(_context)); }

        //}

        private IGenericRepository<TransportRequisition> transportRequisitionRepository;

        public IGenericRepository<TransportRequisition> TransportRequisitionRepository
        {
            get { return this.transportRequisitionRepository ?? (this.transportRequisitionRepository = new GenericRepository<TransportRequisition>(_context)); }

        }

        private IGenericRepository<TransportBidQuotation> transportBidQuotationRepository;

        public IGenericRepository<TransportBidQuotation> TransportBidQuotationRepository
        {
            get { return this.transportBidQuotationRepository ?? (this.transportBidQuotationRepository = new GenericRepository<TransportBidQuotation>(_context)); }

        }

        public void Save()
        {            
            _context.SaveChanges();
        }

        private bool disposed = false;

        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    _context.Dispose();
                }
            }
            this.disposed = true;
        }

        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }



        private IGenericRepository<TransportOrder> transportOrderRepository;

        public IGenericRepository<TransportOrder> TransportOrderRepository
        {

            get { return this.transportOrderRepository ?? (this.transportOrderRepository = new GenericRepository<TransportOrder>(_context)); }

        }


        private IGenericRepository<UserDashboard> userDashboardRepository;

        public IGenericRepository<UserDashboard> UserDashboardRepository
        {

            get { return this.userDashboardRepository ?? (this.userDashboardRepository = new GenericRepository<UserDashboard>(_context)); }

        }

        public IGenericRepository<TransportOrderDetail> TransportOrderDetailRepository
        {

            get { return this.transportOrderDetailRepository ?? (this.transportOrderDetailRepository = new GenericRepository<TransportOrderDetail>(_context)); }

        }


        public IGenericRepository<BidWinner> BidWinnerRepository
        {

            get { return this.bidWinnerRepository ?? (this.bidWinnerRepository = new GenericRepository<BidWinner>(_context)); }
        }
        


        private IGenericRepository<vwTransportOrder> vwTransportOrderRepository;

        public IGenericRepository<vwTransportOrder> VwTransportOrderRepository
        {

            get { return this.vwTransportOrderRepository ?? (this.vwTransportOrderRepository = new GenericRepository<vwTransportOrder>(_context)); }

        }



        




        private IGenericRepository<TransportRequisitionDetail> transportRequisitionDetailRepository;

        public IGenericRepository<TransportRequisitionDetail> TransportRequisitionDetailRepository
        {

            get { return this.transportRequisitionDetailRepository ?? (this.transportRequisitionDetailRepository = new GenericRepository<TransportRequisitionDetail>(_context)); }

        }





        private IGenericRepository<WorkflowStatus> workflowStatusRepository;

        public IGenericRepository<WorkflowStatus> WorkflowStatusRepository
        {

            get { return this.workflowStatusRepository ?? (this.workflowStatusRepository = new GenericRepository<WorkflowStatus>(_context)); }

        }

        private IGenericRepository<ApplicationSetting> applicationSettingRepository;

        public IGenericRepository<ApplicationSetting> ApplicationSettingRepository
        {

            get { return this.applicationSettingRepository ?? (this.applicationSettingRepository = new GenericRepository<ApplicationSetting>(_context)); }

        }


        private IGenericRepository<Ration> rationRepository;

        public IGenericRepository<Ration> RationRepository
        {

            get { return this.rationRepository ?? (this.rationRepository = new GenericRepository<Ration>(_context)); }

        }

        public IGenericRepository<RationDetail> RationDetailRepository
        {

            get { return this.rationDetailRepository ?? (this.rationDetailRepository = new GenericRepository<RationDetail>(_context)); }

        }

        public IGenericRepository<HRD> HRDRepository    
        {

            get { return this.hrdRepository ?? (this.hrdRepository = new GenericRepository<HRD>(_context)); }

        }

        public IGenericRepository<HRDDetail> HRDDetailRepository
        {

            get { return this.hrdDetailRepository ?? (this.hrdDetailRepository = new GenericRepository<HRDDetail>(_context)); }

        }
        public IGenericRepository<RegionalPSNPPlan> RegionalPSNPPlanRepository
        {

<<<<<<< HEAD
            get { return this.regionalPSNPPlanRepository ?? (this.regionalPSNPPlanRepository = new GenericRepository<RegionalPSNPPlan>(_context)); }

        }
=======



        private IGenericRepository<RequestDetailCommodity> requestDetailCommodityRepository;

        public IGenericRepository<RequestDetailCommodity> RequestDetailCommodityRepository
        {

            get { return this.requestDetailCommodityRepository ?? (this.requestDetailCommodityRepository = new GenericRepository<RequestDetailCommodity>(_context)); }

        }



      
      
      //  IGenericRepository<ApplicationSetting> ApplicationSettingRepository
>>>>>>> CATS-28 Allocation and Ration Commodity amount calculation based on ration

        public IGenericRepository<RegionalPSNPPlanDetail> RegionalPSNPPlanDetailRepository
        {

            get { return this.regionalPSNPPlanDetailRepository ?? (this.regionalPSNPPlanDetailRepository = new GenericRepository<RegionalPSNPPlanDetail>(_context)); }

        }
      //  IGenericRepository<RegionalPSNPPlanDetail> RegionalPSNPPlanDetailRepository { get; } 

        private IGenericRepository<NeedAssessmentHeader> needAssessmentHeaderRepository;
        public IGenericRepository<NeedAssessmentHeader> NeedAssessmentHeaderRepository
        {
            get
            {
                return this.needAssessmentHeaderRepository ?? (this.needAssessmentHeaderRepository = new GenericRepository<NeedAssessmentHeader>(_context));
            }
        }

        private IGenericRepository<NeedAssessmentDetail> needAssessmentDetailRepository;
        public IGenericRepository<NeedAssessmentDetail> NeedAssessmentDetailRepository
        {
            get
            {
                return this.needAssessmentDetailRepository ?? (this.needAssessmentDetailRepository = new GenericRepository<NeedAssessmentDetail>(_context));
            }
        }




<<<<<<< HEAD
        

=======



      
>>>>>>> CATS-28 Allocation and Ration Commodity amount calculation based on ration
    }
}
