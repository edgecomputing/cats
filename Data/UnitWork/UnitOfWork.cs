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
      
      
        private IGenericRepository<AllocationModelDetail> allocatipnModelDetailRepository;
        

        public UnitOfWork()
        {
            this._context = new CatsContext();
        }

        // TODO: Consider adding separate properties for each repositories.

        /// <summary>
        /// ReliefRequistionRepository
        /// </summary>
        /// 


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



      
        
      
        public IGenericRepository<AllocationModelDetail> AllocationModelDetailRepository
        {
            get { return this.allocatipnModelDetailRepository ?? (this.allocatipnModelDetailRepository = new GenericRepository<AllocationModelDetail>(_context)); }

        }
       

        //public IGenericRepository<AllocationDetailLine> AllocationDetailLineRepository
        //{
        //    get { return this.allocationDetailLineRepository ?? (this.allocationDetailLineRepository = new GenericRepository<AllocationDetailLine>(_context)); }
        //}

      

        //public IGenericRepository<RequisitionDetailLine> RequisitionDetailLineRepository
        //{
        //    get { return this.requisitionDetailLineRepository ?? (this.requisitionDetailLineRepository = new GenericRepository<RequisitionDetailLine>(_context)); }
        //}



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

        //public IGenericRepository<Round> RoundRepository
        //{

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




        private IGenericRepository<RequisitionViewModel> requisitionViewModelRepository;

        public IGenericRepository<RequisitionViewModel> RequisitionViewModelRepository
        {

            get { return this.requisitionViewModelRepository ?? (this.requisitionViewModelRepository = new GenericRepository<RequisitionViewModel>(_context)); }

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





      
    }
}
