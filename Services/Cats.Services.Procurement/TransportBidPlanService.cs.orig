using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Procurement
{
    public class TransportBidPlanService : ITransportBidPlanService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TransportBidPlanService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool AddTransportBidPlan(TransportBidPlan item)
        {
            _unitOfWork.TransportBidPlanRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool UpdateTransportBidPlan(TransportBidPlan item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidPlanRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteTransportBidPlan(TransportBidPlan item)
        {
            if (item == null) return false;
            _unitOfWork.TransportBidPlanRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.TransportBidPlanRepository.FindById(id);
            return DeleteTransportBidPlan(item);
        }
        public TransportBidPlan FindById(int id)
        {
            var item = _unitOfWork.TransportBidPlanRepository.FindById(id);
            return item;
        }
       
        public List<TransportBidPlan> GetAllTransportBidPlan()
        {
           // return sample_data;
            return _unitOfWork.TransportBidPlanRepository.GetAll();

        }
        public List<TransportBidPlan> FindBy(Expression <Func<TransportBidPlan, bool>> predicate)
        {
            return _unitOfWork.TransportBidPlanRepository.FindBy(predicate);

        }
    }
}