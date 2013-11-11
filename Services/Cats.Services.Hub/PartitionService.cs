

using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels;


namespace Cats.Services.Hub
{

    public class PartitionService : IPartitionService
    {
        private readonly IUnitOfWork _unitOfWork;


        public PartitionService()
        {
            this._unitOfWork = new UnitOfWork();
        }
        #region Default Service Implementation
        public bool AddPartition(Partition entity)
        {
            _unitOfWork.PartitionRepository.Add(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool EditPartition(Partition entity)
        {
            _unitOfWork.PartitionRepository.Edit(entity);
            _unitOfWork.Save();
            return true;

        }
        public bool DeletePartition(Partition entity)
        {
            if (entity == null) return false;
            _unitOfWork.PartitionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.PartitionRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.PartitionRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<Partition> GetAllPartition()
        {
            return _unitOfWork.PartitionRepository.GetAll();
        }
        public Partition FindById(int id)
        {
            return _unitOfWork.PartitionRepository.FindById(id);
        }
        public List<Partition> FindBy(Expression<Func<Partition, bool>> predicate)
        {
            return _unitOfWork.PartitionRepository.FindBy(predicate);
        }
        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

        public List<ReplicationViewModel> GetHubsSyncrtonizationDetails(int publication)
        {
            List<ReplicationViewModel> replications = new List<ReplicationViewModel>();
            return replications;
        }
    }
}


