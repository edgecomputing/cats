using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.EarlyWarning
{
    public class BusinessProcessService :IBusinessProcessService
    {
        private readonly IUnitOfWork _unitOfWork;

        public BusinessProcessService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        public bool PromotWorkflow(BusinessProcessState state)
        {
            BusinessProcess item = this.FindById(state.ParentBusinessProcessID);
            if(item!=null)
            {

                _unitOfWork.BusinessProcessStateRepository.Add(state);
                _unitOfWork.Save();
                item.CurrentStateID = state.BusinessProcessStateID;
                
                _unitOfWork.BusinessProcessRepository.Edit(item);
                _unitOfWork.Save();
               
                return true;    
            }
            return false;
        }

        public BusinessProcess CreateBusinessProcess(int templateID, int DocumentID, string DocumentType)
        {
            BusinessProcess bp = new BusinessProcess { ProcessTypeID = templateID, DocumentID = DocumentID, DocumentType = DocumentType };
            Add(bp);
            return bp;
        }
        public bool Add(BusinessProcess item)
        {
            _unitOfWork.BusinessProcessRepository.Add(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Update(BusinessProcess item)
        {
            if (item == null) return false;
            _unitOfWork.BusinessProcessRepository.Edit(item);
            _unitOfWork.Save();
            return true;
        }
        public bool Delete(BusinessProcess item)
        {
            if (item == null) return false;
            _unitOfWork.BusinessProcessRepository.Delete(item);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            var item = _unitOfWork.BusinessProcessRepository.FindById(id);
            return Delete(item);
        }
        public BusinessProcess FindById(int id)
        {
            return _unitOfWork.BusinessProcessRepository.FindById(id);
        }
        public List<BusinessProcess> GetAll()
        {
            return _unitOfWork.BusinessProcessRepository.GetAll();

        }
        public List<BusinessProcess> FindBy(Expression<Func<BusinessProcess, bool>> predicate)
        {
            return _unitOfWork.BusinessProcessRepository.FindBy(predicate);

        }
   }
 }