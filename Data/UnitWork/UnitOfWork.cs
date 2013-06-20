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
