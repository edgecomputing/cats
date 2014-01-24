using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;


namespace Cats.Services.Logistics
{

    public class DistributionByAgeService:IDistributionByAgeService
   {
       private readonly  IUnitOfWork _unitOfWork;


       public DistributionByAgeService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }
       #region Default Service Implementation
       public bool AddDistributionByAge(DistributionByAge entity)
       {
           _unitOfWork.DistributionByAgeRepositroy.Add(entity);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditDistributionByAge(DistributionByAge entity)
       {
           _unitOfWork.DistributionByAgeRepositroy.Edit(entity);
           _unitOfWork.Save();
           return true;

       }
       public bool DeleteDistributionByAge(DistributionByAge entity)
        {
             if(entity==null) return false;
             _unitOfWork.DistributionByAgeRepositroy.Delete(entity);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.DistributionByAgeRepositroy.FindById(id);
           if(entity==null) return false;
           _unitOfWork.DistributionByAgeRepositroy.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<DistributionByAge> GetAllDistributionByAge()
       {
           return _unitOfWork.DistributionByAgeRepositroy.GetAll();
       }
       public DistributionByAge FindById(int id)
       {
           return _unitOfWork.DistributionByAgeRepositroy.FindById(id);
       }
       public List<DistributionByAge> FindBy(Expression<Func<DistributionByAge, bool>> predicate)
       {
           return _unitOfWork.DistributionByAgeRepositroy.FindBy(predicate);
       }
       #endregion
       //public List<RegionalRequest> FilterByRegion(int regionID,int round,int planID,int programID,int month)
       //{
       //    var regionalRequest =_unitOfWork.RegionalRequestRepository.FindBy(
       //                                              m => m.RegionID == regionID && m.Round == round && m.ProgramId == programID
       //                                              && m.PlanID == planID && m.Month == month);
       //    if (regionalRequest!=null)
       //    {
       //        var  
       //    }

       //}


       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
  
