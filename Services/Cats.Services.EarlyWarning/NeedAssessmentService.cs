using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;
using log4net;


namespace Cats.Services.EarlyWarning
{

    public class NeedAssessmentService : INeedAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ILog Logger;

        public NeedAssessmentService(IUnitOfWork unitOfWork, ILog logger)
        {
            this._unitOfWork = unitOfWork;
            Logger = logger;
        }

        #region Default Service Implementation
        public bool AddNeedAssessment(NeedAssessment needAssessment)
        {
            try
            {
                _unitOfWork.NeedAssessmentRepository.Add(needAssessment);
                _unitOfWork.Save();
                return true;
            }
            catch (System.Data.ConstraintException ex)
            {
                Logger.Error("",new Exception(ex.InnerException.Message.ToString(CultureInfo.InvariantCulture)));
                throw new Exception(ex.ToString());
            }

        }

        public bool EditNeedAssessment(NeedAssessment needAssessment)
        {
            _unitOfWork.NeedAssessmentRepository.Edit(needAssessment);
            _unitOfWork.Save();
            return true;

        }
        public bool DeleteNeedAssessment(NeedAssessment needAssessment)
        {
            if (needAssessment == null) return false;
            _unitOfWork.NeedAssessmentRepository.Delete(needAssessment);
            _unitOfWork.Save();
            return true;
        }
        public bool DeleteById(int id)
        {
            
            var entity = _unitOfWork.NeedAssessmentRepository.FindById(id);
            if (entity == null) return false;
            _unitOfWork.NeedAssessmentRepository.Delete(entity);
            _unitOfWork.Save();
            return true;
        }
        public List<NeedAssessment> GetAllNeedAssessment()
        {
          
            return _unitOfWork.NeedAssessmentRepository.GetAll();
        }
        public NeedAssessment FindById(int id)
        {
            return _unitOfWork.NeedAssessmentRepository.FindById(id);
        }
        public List<NeedAssessment> FindBy(Expression<Func<NeedAssessment, bool>> predicate)
        {
            return _unitOfWork.NeedAssessmentRepository.FindBy(predicate);
        }
        #endregion

     

        public List<NeedAssessmentDao> GetListOfZones()
        {
            var zonesInNeedAssessment = GetZonesFromNeedAssessment();
            var listOfZones = _unitOfWork.AdminUnitRepository.FindBy(z => z.AdminUnitTypeID == 3).ToList();
            var filteredZones = from zone in listOfZones
                                where zonesInNeedAssessment.Contains(zone.Name)
                                select zone;

            
            return filteredZones.Select(adminUnit => new NeedAssessmentDao
                                                 {
                                                     ZoneId = adminUnit.AdminUnitID, 
                                                     Zone = adminUnit.Name
                                                     

                                                     
                                                 }).ToList();
        }

        public List<NeedAssessmentWoredaDao> GetListOfWoredas(int zoneId)
        {
            List<AdminUnit> woredas = _unitOfWork.AdminUnitRepository.FindBy(z => z.ParentID == zoneId).ToList();
            return woredas.Select(adminUnit => new NeedAssessmentWoredaDao
            {
                Woreda = adminUnit.AdminUnitID,
                WoredaName = adminUnit.Name
            }).ToList();
        }
     
       
        public bool GenerateDefefaultData(NeedAssessment needAssessment)
        {
             List<AdminUnit> zones = _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == needAssessment.Region).ToList();
            NeedAssessmentDetail woreda = null;
            foreach (var adminUnit in zones)
            {
                var zone = new NeedAssessmentHeader
                               {
                                   NeedAID = needAssessment.NeedAID,
                                   Zone = adminUnit.AdminUnitID,
                                  
                               };
              

                var woredas =  _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == zone.Zone).ToList();

                foreach (var _woreda in woredas)
                {
                     woreda = new NeedAssessmentDetail
                                  {
                                      NeedAId = zone.NAHeaderId,
                                      Woreda = _woreda.AdminUnitID,
                                      ProjectedMale = 0,
                                      ProjectedFemale = 0,
                                      RegularPSNP = 0,
                                      PSNP = 0,
                                      NonPSNP = 0,
                                      Contingencybudget = 0,
                                      TotalBeneficiaries = 0,
                                      PSNPFromWoredasMale = 0,
                                      PSNPFromWoredasFemale = 0,
                                      PSNPFromWoredasDOA = 0,
                                      NonPSNPFromWoredasMale = 0,
                                      NonPSNPFromWoredasFemale = 0,
                                      NonPSNPFromWoredasDOA = 0,
                                      NeedAssessmentHeader = zone
                                  };
                    woreda.NeedAssessmentHeader.NeedAssessment = needAssessment;
                   

                    _unitOfWork.NeedAssessmentDetailRepository.Add(woreda);
                   
                    
                }
            }
              _unitOfWork.Save();
            
           
            return true;
        }

        public IOrderedEnumerable<RegionsViewModel> GetRegions()
        {
            var regions = _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == 2).ToList();

            return regions.Select(adminUnit => new RegionsViewModel
                                                   {
                                                       Name = adminUnit.Name, AdminUnitID = adminUnit.AdminUnitID
                                                   }).OrderBy(e => e.Name);
        
        }

        public IOrderedEnumerable<RegionsViewModel> GetZoness(int region)
        {
            var zones = _unitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitTypeID == 3 && t.ParentID == region).ToList();

            return zones.Select(adminUnit => new RegionsViewModel
                                                 {
                                                     Name = adminUnit.Name,
                                                     AdminUnitID = adminUnit.AdminUnitID
                                                 }).OrderBy(e => e.Name);
        }
        public bool IsNeedAssessmentUsedInHrd(int season, int year)
        {
           
            List<HRD> used = _unitOfWork.HRDRepository.Get(h => h.Season.SeasonID == season && h.Year == year).ToList();
            if (used.Count > 0)
                return true;
            else return true;
        }
        public List<string> GetRegionsFromNeedAssessment()
        {
            return _unitOfWork.NeedAssessmentRepository.GetAll().Select(r => r.AdminUnit.Name).ToList();
        }
        public List<string> GetZonesFromNeedAssessment()
        {
            return _unitOfWork.NeedAssessmentHeaderRepository.GetAll().Select(r => r.AdminUnit.Name).ToList();
        }
        public  List<string> GetSeasonFromNeedAssessment()
        {
            return _unitOfWork.NeedAssessmentRepository.GetAll().Select(r => r.Season1.Name).ToList();   
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();

        }

    }
}


