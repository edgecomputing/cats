using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class TransportBidPlanDetailService : ITransportBidPlanDetailService
    {
        private readonly IUnitOfWork _unitOfWork;

        public TransportBidPlanDetailService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddTransportBidPlanDetail(TransportBidPlanDetail item)
        {
            _unitOfWork.TransportBidPlanDetailRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateTransportBidPlanDetail(TransportBidPlanDetail item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidPlanDetailRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteTransportBidPlanDetail(TransportBidPlanDetail item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidPlanDetailRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.TransportBidPlanDetailRepository.FindById(id);
            return DeleteTransportBidPlanDetail(item);
        }
        public TransportBidPlanDetail FindById(int id)
        {
            return _unitOfWork.TransportBidPlanDetailRepository.FindById(id);
        }
        public List<TransportBidPlanDetail> GetAllTransportBidPlanDetail()
        {
            return _unitOfWork.TransportBidPlanDetailRepository.GetAll();

        }
        public List<TransportBidPlanDetail> FindBy(Expression<Func<TransportBidPlanDetail, bool>> predicate)
        {
            return _unitOfWork.TransportBidPlanDetailRepository.FindBy(predicate);

        }
        public double GetRegionPlanTotal(int bidplanid, int regionId, int programId)
        {
            List<TransportBidPlanDetail> bidDetails = this.GetAllTransportBidPlanDetail();
            decimal r=
            (from planDetail in bidDetails
             where planDetail.ProgramID == programId && planDetail.BidPlanID==bidplanid && planDetail.Destination.AdminUnit2.AdminUnit2.AdminUnitID==regionId
                 select planDetail.Quantity).Sum();

            return (double)r;
        }
    }
}