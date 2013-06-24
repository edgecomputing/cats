


using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;
using DRMFSS.BLL.Services;

namespace Cats.Services.EarlyWarning
{
    public class ReliefRequistionService : IReliefRequistionService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReliefRequistionService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }




        public bool AddReliefRequistion(ReliefRequistion reliefRequistion)
        {
            if(reliefRequistion ==null) return false;
            _unitOfWork.ReliefRequistionRepository.Add(reliefRequistion);
            _unitOfWork.Save();
            return true;
        }

        public bool UpdateReliefRequistion(ReliefRequistion reliefRequistion)
        {
            if(reliefRequistion ==null) return false;
            _unitOfWork.ReliefRequistionRepository.Edit(reliefRequistion);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteReliefRequistion(ReliefRequistion reliefRequistion)
        {
            if(reliefRequistion ==null) return false;
            _unitOfWork.ReliefRequistionRepository.Delete(reliefRequistion);
            _unitOfWork.Save();
            return true;
        }

        public bool DeleteReliefRequistion(int id)
        {
            var reliefRequistion = _unitOfWork.ReliefRequistionRepository.FindById(id);
            if(reliefRequistion==null) return false;
            _unitOfWork.ReliefRequistionRepository.Delete(reliefRequistion);
            _unitOfWork.Save();
            return true;
        }

        public List<ReliefRequistion> GetAllReliefRequistion()
        {
            return _unitOfWork.ReliefRequistionRepository.GetAll();
        }

        public ReliefRequistion GetReliefRequistion(int reliefRequistionId)
        {
            return _unitOfWork.ReliefRequistionRepository.FindById(reliefRequistionId);
        }


        public bool DeleteById(int id)
        {
            throw new NotImplementedException();
        }

        public bool EditReliefRequistion(ReliefRequistion Entity)
        {
            throw new NotImplementedException();
        }

        public ReliefRequistion FindById(int id)
        {
            throw new NotImplementedException();
        }

        public List<ReliefRequistion> FindBy(System.Linq.Expressions.Expression<Func<ReliefRequistion, bool>> predicate)
        {
            throw new NotImplementedException();
        }
    }
}
