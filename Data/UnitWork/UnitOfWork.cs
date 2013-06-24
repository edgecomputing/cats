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
        private IGenericRepository<ReliefRequistion> reliefRequistionRepository;
        private IGenericRepository<AllocationDetailLine> allocationDetailLineRepository;
        private IGenericRepository<ReliefRequisitionDetail> reliefRequisitionDetailRepository;
        private IGenericRepository<RequisitionDetailLine> requisitionDetailLineRepository;

        public UnitOfWork()
        {
            this._context = new CatsContext();
        }

        // TODO: Consider adding separate properties for each repositories.

        /// <summary>
        /// ReliefRequistionRepository
        /// </summary>
        public IGenericRepository<ReliefRequistion> ReliefRequistionRepository
        {
            get { return this.reliefRequistionRepository ?? (this.reliefRequistionRepository = new GenericRepository<ReliefRequistion>(_context)); }
        }

        public IGenericRepository<AllocationDetailLine> AllocationDetailLineRepository
        {
            get { return this.allocationDetailLineRepository ?? (this.allocationDetailLineRepository = new GenericRepository<AllocationDetailLine>(_context)); }
        }

        public IGenericRepository<ReliefRequisitionDetail> ReliefRequisitionDetailRepository
        {
            get { return this.reliefRequisitionDetailRepository ?? (this.reliefRequisitionDetailRepository = new GenericRepository<ReliefRequisitionDetail>(_context)); }
        }

        public IGenericRepository<RequisitionDetailLine> RequisitionDetailLineRepository
        {
            get { return this.requisitionDetailLineRepository ?? (this.requisitionDetailLineRepository = new GenericRepository<RequisitionDetailLine>(_context)); }
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
