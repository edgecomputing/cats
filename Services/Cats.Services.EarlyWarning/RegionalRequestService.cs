

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels;


namespace Cats.Services.EarlyWarning
{

    public class RegionalRequestService : IRegionalRequestService
    {
        private readonly IUnitOfWork _unitOfWork;


        public RegionalRequestService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

        #region Default Service Implementation

        public bool AddReliefRequistion(RegionalRequest reliefRequistion)
        {
            _unitOfWork.RegionalRequestRepository.Add(reliefRequistion);
            _unitOfWork.Save();
            return true;

        }

        public bool EditReliefRequistion(RegionalRequest reliefRequistion)
        {
            _unitOfWork.RegionalRequestRepository.Edit(reliefRequistion);
            _unitOfWork.Save();
            return true;

        }

        public bool DeleteReliefRequistion(RegionalRequest reliefRequistion)
        {
            if (reliefRequistion == null) return false;
            _unitOfWork.RegionalRequestRepository.Delete(reliefRequistion);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteById(int id)
        {
            var entity = _unitOfWork.RegionalRequestRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.RegionalRequestRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }

        public List<RegionalRequest> GetAllReliefRequistion()
        {
            return _unitOfWork.RegionalRequestRepository.GetAll();
        }

        public RegionalRequest FindById(int id)
        {
            return _unitOfWork.RegionalRequestRepository.FindById(id);
        }

        public List<RegionalRequest> FindBy(Expression<Func<RegionalRequest, bool>> predicate)
        {
            return _unitOfWork.RegionalRequestRepository.FindBy(predicate);
        }

        public IEnumerable<RegionalRequest> Get(
            Expression<Func<RegionalRequest, bool>> filter = null,
            Func<IQueryable<RegionalRequest>, IOrderedQueryable<RegionalRequest>> orderBy = null,
            string includeProperties = "")
        {
            var requisitions=  _unitOfWork.RegionalRequestRepository.Get(filter, orderBy, includeProperties);
            //var regionalRequests=(from itm in requisitions select new RequestView
            //                                                          {
            //                                                              ProgramID=itm.ProgramId ,
            //                                                              Program=itm.Program.Name,
            //                                                              S
            //                                                          })
            return requisitions;
        }

        #endregion

        public void Dispose()
        {
            _unitOfWork.Dispose();

        }



        public List<int?> GetZonesFoodRequested(int requestId)
        {
            var regionalRequestDetails =
                _unitOfWork.RegionalRequestDetailRepository.Get(t => t.RegionalRequestID == requestId, null,
                                                                "FDP,FDP.AdminUnit");
            var zones =
                (from requestDetail in regionalRequestDetails
                 where requestDetail.Fdp.AdminUnit.ParentID != null
                 select requestDetail.Fdp.AdminUnit.ParentID).Distinct();
            return zones.ToList();

        }


        public List<RegionalRequest> GetSubmittedRequest(int region, int month,int status)
        {

           
            if (region!=0)
            {
                return month != 0
                                               ? _unitOfWork.RegionalRequestRepository.Get(
                                                   r => r.RegionID == region && r.RequistionDate.Month == month && r.Status == status,
                                                   null,
                                                   "AdminUnit,Program").ToList()
                                               : _unitOfWork.RegionalRequestRepository.Get(r => r.RegionID == region && r.Status == status, null,
                                                                             "AdminUnit,Program").ToList();
            }

           return  month != 0
                                        ? _unitOfWork.RegionalRequestRepository.Get(r => r.RequistionDate.Month == month && r.Status == status, null,
                                                                      "AdminUnit,Program").ToList()
                                        : _unitOfWork.RegionalRequestRepository.Get(r => r.Status == status, null, "AdminUnit,Program").ToList();
            }
        }
   
}

   
 
      
  