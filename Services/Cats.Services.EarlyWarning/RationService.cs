

using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Services.EarlyWarning;


namespace Cats.Services.EarlyWarning
{

    public class RationService:IRationService
   {
       private readonly  IUnitOfWork _unitOfWork;
      

       public RationService(IUnitOfWork unitOfWork)
       {
           this._unitOfWork = unitOfWork;
       }

        public void SetDefault(int rationId)
        {
            var CurrentRation = FindById(rationId);
            if (CurrentRation!=null)
            {
                UnsetDefault();
                CurrentRation.IsDefaultRation = true;
                SetSettingValue("DefaultRation", rationId + "");
                _unitOfWork.Save();
            }
        }
        public void SetSettingValue(string name, string value)
        {
            List<ApplicationSetting> ret = _unitOfWork.ApplicationSettingRepository.FindBy(t => t.SettingName == name);
            if (ret.Count == 1)
            {
                ret[0].SettingValue = value;
                _unitOfWork.ApplicationSettingRepository.Edit(ret[0]);
              //  UpdateApplicationSetting(ret[0]);
                return;
            }
            ApplicationSetting apset = new ApplicationSetting { SettingName = name, SettingValue = value };
            _unitOfWork.ApplicationSettingRepository.Add(apset);
            //AddApplicationSetting(apset);

        }
        public void UnsetDefault()
        {
            var rations = FindBy(r => r.IsDefaultRation == true).ToList();
            if (rations!=null)
            {
                foreach (var ration in rations)
                {
                    ration.IsDefaultRation = false;
                   
                }
            }
        }
       #region Default Service Implementation
       public bool AddRation(Ration ration)
       {
           _unitOfWork.RationRepository.Add(ration);
           _unitOfWork.Save();
           return true;
           
       }
       public bool EditRation(Ration ration)
       {
           _unitOfWork.RationRepository.Edit(ration);
           _unitOfWork.Save();
           return true;

       }
         public bool DeleteRation(Ration ration)
        {
             if(ration==null) return false;
           _unitOfWork.RationRepository.Delete(ration);
           _unitOfWork.Save();
           return true;
        }
       public  bool DeleteById(int id)
       {
           var entity = _unitOfWork.RationRepository.FindById(id);
           if(entity==null) return false;
           _unitOfWork.RationRepository.Delete(entity);
           _unitOfWork.Save();
           return true;
       }
       public List<Ration> GetAllRation()
       {
           return _unitOfWork.RationRepository.GetAll();
       } 
       public Ration FindById(int id)
       {
           return _unitOfWork.RationRepository.FindById(id);
       }
       public List<Ration> FindBy(Expression<Func<Ration, bool>> predicate)
       {
           return _unitOfWork.RationRepository.FindBy(predicate);
       }
       
       public IEnumerable<Ration> Get(
           Expression<Func<Ration, bool>> filter = null,
           Func<IQueryable<Ration>, IOrderedQueryable<Ration>> orderBy = null,
           string includeProperties = "")
        {
           return _unitOfWork.RationRepository.Get(filter, orderBy, includeProperties);
        }
       #endregion
       
       public void Dispose()
       {
           _unitOfWork.Dispose();
           
       }
       
   }
   }
   
 
      