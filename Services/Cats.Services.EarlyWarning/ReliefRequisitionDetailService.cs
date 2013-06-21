using System;
using System.Collections.Generic;
using System.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{
    
    public class ReliefRequisitionDetailService : IRequisitionDetailService
    {

        private readonly IUnitOfWork _unitOfWork;

        public ReliefRequisitionDetailService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }



        public bool AddRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail)
        {
            if (reliefRequisitionDetail == null) return false;
            _unitOfWork.ReliefRequisitionDetailRepository.Add(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateRequisitionDetail(ReliefRequisitionDetail reliefRequisitionDetail)
        {
            if (reliefRequisitionDetail == null) return false;
            _unitOfWork.ReliefRequisitionDetailRepository.Edit(reliefRequisitionDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteRequisitionDetail(ReliefRequisitionDetail reliefRequistionDetail)
        {
            if (reliefRequistionDetail == null) return false;
            _unitOfWork.ReliefRequisitionDetailRepository.Delete(reliefRequistionDetail);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteRequisitionDetail(int id)
        {
            var reliefRequistionDetail = _unitOfWork.ReliefRequisitionDetailRepository.FindById(id);
            if (reliefRequistionDetail == null) return false;
            _unitOfWork.ReliefRequisitionDetailRepository.Delete(reliefRequistionDetail);
            _unitOfWork.Save();
            return true;
        } 

        public List<ReliefRequisitionDetail> GetAllReliefRequistion()
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.GetAll();
        }

        public ReliefRequisitionDetail GetReliefRequisitionDetail(int reliefRequisitionDetailId)
        {
            return _unitOfWork.ReliefRequisitionDetailRepository.FindById(reliefRequisitionDetailId);
        }
    }
}
