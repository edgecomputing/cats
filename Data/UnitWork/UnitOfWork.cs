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
        private IGenericRepository<ReliefRequistion> _reliefRequistionRepository;
        private IGenericRepository<ReliefRequisitionDetail> _reliefRequisitionDetailRepository; 

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
            get { return this._reliefRequistionRepository ?? (this._reliefRequistionRepository = new GenericRepository<ReliefRequistion>(_context)); }
        }
       
       /// <summary>
       /// ReliefRequisitionDetailRepository
       /// </summary>
        public IGenericRepository<ReliefRequisitionDetail> ReliefRequisitionDetailRepository
       {
           get
           {
               return this._reliefRequisitionDetailRepository ??
                      (this._reliefRequisitionDetailRepository = new GenericRepository<ReliefRequisitionDetail>(_context));
           }
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
