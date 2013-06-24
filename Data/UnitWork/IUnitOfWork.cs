using System;
using Cats.Models;
using Cats.Data.Repository;

namespace Cats.Data.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        // TODO: Add properties to be implemented by UnitOfWork class for each repository
        IGenericRepository<ReliefRequistion> ReliefRequistionRepository { get; }
        IGenericRepository<ReliefRequisitionDetail> ReliefRequisitionDetailRepository { get; }
        IGenericRepository<RequisitionDetailLine> RequisitionDetailLineRepository { get; }
        IGenericRepository<AllocationDetailLine> AllocationDetailLineRepository { get; }



        void Save();

    }
}
