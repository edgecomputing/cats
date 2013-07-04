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
        private List<TransportBidPlan> sample_data;
        public TransportBidPlanService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
            generate_sample_data();
        }
        public void generate_sample_data()
        {
            sample_data = new List<TransportBidPlan>();
            sample_data.Add(new TransportBidPlan {TransportBidPlanID=1, Year=2011,YearHalf=1,RegionID=1,ProgramID=1});
            sample_data.Add(new TransportBidPlan {TransportBidPlanID=2, Year = 2011, YearHalf = 1, RegionID = 1, ProgramID = 2 });
            sample_data.Add(new TransportBidPlan { TransportBidPlanID = 3, Year = 2011, YearHalf = 1, RegionID = 2, ProgramID = 1 });
            sample_data.Add(new TransportBidPlan { TransportBidPlanID = 4, Year = 2011, YearHalf = 1, RegionID = 2, ProgramID = 2 });
            sample_data.Add(new TransportBidPlan { TransportBidPlanID = 5, Year = 2012, YearHalf = 1, RegionID = 1, ProgramID = 1 });
            sample_data.Add(new TransportBidPlan { TransportBidPlanID = 6, Year = 2012, YearHalf = 1, RegionID = 1, ProgramID = 2 });
            sample_data.Add(new TransportBidPlan { TransportBidPlanID = 7, Year = 2012, YearHalf = 1, RegionID = 2, ProgramID = 1 });
            sample_data.Add(new TransportBidPlan { TransportBidPlanID = 8, Year = 2012, YearHalf = 1, RegionID = 2, ProgramID = 2 }); 
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
            return sample_data[id-1];
//            return _unitOfWork.TransportBidPlanRepository.FindById(id);
        }
        public List<TransportBidPlan> GetAllTransportBidPlan()
        {
            return sample_data;
//            return _unitOfWork.TransportBidPlanRepository.GetAll();

        }
        public List<TransportBidPlan> FindBy(Expression <Func<TransportBidPlan, bool>> predicate)
        {
            return _unitOfWork.TransportBidPlanRepository.FindBy(predicate);

        }
    }
}