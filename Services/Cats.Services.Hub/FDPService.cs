

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.Hub;
using Cats.Models.Hub;
using Cats.Models.Hub.ViewModels.Report;


namespace Cats.Services.Hub
{

    public class FDPService : IFDPService
    {
        private readonly IUnitOfWork _unitOfWork;


        public FDPService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddFDP(FDP fdp)
        {
            _unitOfWork.FDPRepository.Add(fdp);
            _unitOfWork.Save();
            return true;

        }
        public bool EditFDP(FDP fdp)
        {
            _unitOfWork.FDPRepository.Edit(fdp);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteFDP(FDP fdp)
        {
            if (fdp == null) return false;
            _unitOfWork.FDPRepository.Delete(fdp);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.FDPRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.FDPRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<FDP> GetAllFDP()
        {
            return _unitOfWork.FDPRepository.GetAll();
        }
        public FDP FindById(int id)
        {
            return _unitOfWork.FDPRepository.FindById(id);
        }
        public List<FDP> FindBy(Expression<Func<FDP, bool>> predicate)
        {
            return _unitOfWork.FDPRepository.FindBy(predicate);
        }
        #endregion
        /// <summary>
        /// Gets the FDPs by region.
        /// </summary>
        /// <param name="regionId">The region id.</param>
        /// <returns></returns>
        public List<FDP> GetFDPsByRegion(int regionId)
        {
            return
                _unitOfWork.FDPRepository.Get(t => t.AdminUnit.AdminUnit2.ParentID == regionId,
                                              o => o.OrderBy(t => t.Name), "AdminUnit").ToList();


        }

        /// <summary>
        /// Gets the FDPs by woreda.
        /// </summary>
        /// <param name="woredaId">The woreda id.</param>
        /// <returns></returns>
        public List<FDP> GetFDPsByWoreda(int woredaId)
        {
            return _unitOfWork.FDPRepository.Get(t => t.AdminUnitID == woredaId,
                                                 o => o.OrderBy(t => t.Name), "AdminUnit").ToList();

        }

        /// <summary>
        /// Gets the FDPs by zone.
        /// </summary>
        /// <param name="zoneId">The zone id.</param>
        /// <returns></returns>
        public List<FDP> GetFDPsByZone(int zoneId)
        {
            return _unitOfWork.FDPRepository.Get(t => t.AdminUnit.ParentID == zoneId,
                                                o => o.OrderBy(t => t.Name), "AdminUnit").ToList();

        }



        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public List<AreaViewModel> GetAllFDPForReport()
        {
            throw new NotImplementedException();
        }
    }
}


