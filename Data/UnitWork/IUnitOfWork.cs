using System;
using Cats.Models;
using Cats.Data.Repository;

namespace Cats.Data.UnitWork
{
    public interface IUnitOfWork : IDisposable
    {
        // TODO: Add properties to be implemented by UnitOfWork class for each repository
        IGenericRepository<RegionalRequest> RegionalRequestRepository { get; }
        IGenericRepository<RegionalRequestDetail> RegionalRequestDetailRepository { get; }
        IGenericRepository<AdminUnit> AdminUnitRepository { get; }
        IGenericRepository<AdminUnitType> AdminUnitTypeRepository { get; }
        IGenericRepository<Commodity> CommodityRepository { get; }
        IGenericRepository<CommodityType> CommodityTypeRepository { get; }
        IGenericRepository<FDP> FDPRepository { get; }
        IGenericRepository<Program> ProgramRepository { get; }
        IGenericRepository<AllocationModelDetail> AllocationModelDetailRepository { get; }
        IGenericRepository<Hub> HubRepository { get; }
        IGenericRepository<ReliefRequisitionDetail> ReliefRequisitionDetailRepository { get; }
        IGenericRepository<ReliefRequisition> ReliefRequisitionRepository { get; }
        IGenericRepository<RequisitionViewModel> RequisitionViewModelRepository { get; }
        void Save();

    }
}
