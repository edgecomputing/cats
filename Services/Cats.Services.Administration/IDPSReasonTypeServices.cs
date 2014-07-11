using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Administration
{
   public class IDPSReasonTypeServices : IIDPSReasonTypeServices
    {

        private readonly IUnitOfWork _unitOfWork;


        public IDPSReasonTypeServices(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }

       public bool AddIDPSReasonType(IDPSReasonType idpsReasonType)
       {
           _unitOfWork.IDPSReasonTypeRepository.Add(idpsReasonType);
           _unitOfWork.Save();
           return true;
       }

       public bool DeleteIDPSReasonType(IDPSReasonType idpsReasonType)
       {
           if (idpsReasonType == null) return false;
           _unitOfWork.IDPSReasonTypeRepository.Delete(idpsReasonType);
           _unitOfWork.Save();
           return true;
       }

       public bool DeleteById(int id)
       {
           var entity = _unitOfWork.IDPSReasonTypeRepository.FindById(id);
           if (entity == null) return false;
           _unitOfWork.IDPSReasonTypeRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }

       public bool EditIDPSReasonType(IDPSReasonType idpsReasonType)
       {
           _unitOfWork.IDPSReasonTypeRepository.Edit(idpsReasonType);
           _unitOfWork.Save();
           return true;
       }

       public IDPSReasonType FindById(int id)
       {
           return _unitOfWork.IDPSReasonTypeRepository.FindById(id);
       }

       public List<IDPSReasonType> GetAllIDPSReasonType()
       {
           return _unitOfWork.IDPSReasonTypeRepository.GetAll();
       }

       public List<IDPSReasonType> FindBy(Expression<Func<IDPSReasonType, bool>> predicate)
       {
           return _unitOfWork.IDPSReasonTypeRepository.FindBy(predicate);
       }
    }
}
