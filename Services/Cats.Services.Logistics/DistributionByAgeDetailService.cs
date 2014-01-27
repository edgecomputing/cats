using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.Logistics
{
   public class DistributionByAgeDetailService:IDistributionByAgeDetailService
    {
       private readonly  IUnitOfWork _unitOfWork;


       public DistributionByAgeDetailService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddDistributionByAgeDetail(DistributionByAgeDetail entity)
       {
           _unitOfWork.DistributionByAgeDetailRepository.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditDistributionByAgeDetail(DistributionByAgeDetail entity)
       {
           _unitOfWork.DistributionByAgeDetailRepository.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool DeleteDistributionByAgeDetail(DistributionByAgeDetail entity)
        {
             if(entity==null) return false;
             _unitOfWork.DistributionByAgeDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.DistributionByAgeDetailRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.DistributionByAgeDetailRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<DistributionByAgeDetail> GetAllDistributionByAgeDetail()
       {
           return _unitOfWork.DistributionByAgeDetailRepository.GetAll();
       }
       public DistributionByAgeDetail FindById(int id)
       {
           return _unitOfWork.DistributionByAgeDetailRepository.FindById(id);
       }
       public List<DistributionByAgeDetail> FindBy(Expression<Func<DistributionByAgeDetail, bool>> predicate)
       {
           return _unitOfWork.DistributionByAgeDetailRepository.FindBy(predicate);
       }
       #endregion
     
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }


       public DistributionByAgeDetail GetDistributionDetail(int requisitionID, int fdpID)
       {
           var distributionDetail = _unitOfWork.DistributionByAgeDetailRepository.FindBy(m => m.UtilizationHeader.RequisitionId == requisitionID && m.FDPID == fdpID).FirstOrDefault();
           if (distributionDetail!=null)
           {
               var distributionByAgeDetail = new DistributionByAgeDetail()
                   {
                       FemaleLessThan5Years = distributionDetail.FemaleLessThan5Years,
                       MaleLessThan5Years = distributionDetail.MaleLessThan5Years,
                       FemaleBetween5And18Years = distributionDetail.FemaleBetween5And18Years,
                       MaleBetween5And18Years = distributionDetail.MaleBetween5And18Years,
                       FemaleAbove18Years = distributionDetail.FemaleAbove18Years,
                       MaleAbove18Years = distributionDetail.MaleAbove18Years
                   };
               return distributionByAgeDetail;
           }
           return null;
       }
    }
}
