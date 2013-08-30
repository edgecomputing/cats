using System;
using System.Collections.Generic;

using System.Linq;
using System.Linq.Expressions;
using Cats.Data.UnitWork;
using Cats.Models;

namespace Cats.Services.EarlyWarning
{

    public class NeedAssessmentService : INeedAssessmentService
    {
        private readonly IUnitOfWork _unitOfWork;


        public NeedAssessmentService(IUnitOfWork unitOfWork)
        {
            this._unitOfWork = unitOfWork;
        }
        #region Default Service Implementation
        public bool AddNeedAssessment(NeedAssessment needAssessment)
        {

            _unitOfWork.NeedAssessmentRepository.Add(needAssessment);
            _unitOfWork.Save();
            return true;

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

        public IEnumerable<NeedAssessmentHeaderViewModel> ReturnViewModel()
        {
            var needAssessment = _unitOfWork.NeedAssessmentRepository.Get(g => g.NeedAApproved == false); //featch unapproved need assessments
            return needAssessment.Select(need =>  new NeedAssessmentHeaderViewModel()
                                                                              {
                                                                                  NeedAID = need.NeedAID,
                                                                                  Region = need.Region, 
                                                                                  RegionName = need.AdminUnit.Name, 
                                                                                  Season  = need.Season1.SeasonID, 
                                                                                  SeasonName = need.Season1.Name,
                                                                                  NeedADate = (DateTime) need.NeedADate, 
                                                                                  NeedAApproved = need.NeedAApproved,
                                                                                  NeedACreaterName = need.UserProfile1.UserName,
                                                                                  NeedACreatedBy = need.NeddACreatedBy,
                                                                                  TypeOfNeedAssessment = need.TypeOfNeedAssessment1.TypeOfNeedAssessmentID
                                                                              });
        }
        public IEnumerable<NeedAssessmentHeaderViewModel> ReturnViewModelApproved()
        {
            var needAssessment = _unitOfWork.NeedAssessmentRepository.Get(g => g.NeedAApproved == true); //featch unapproved need assessments
            return needAssessment.Select(need => new NeedAssessmentHeaderViewModel()
            {
                NeedAID = need.NeedAID,
                Region = need.Region,
                RegionName = need.AdminUnit.Name,
                Season = need.Season1.SeasonID,
                SeasonName = need.Season1.Name,
                NeedADate = (DateTime)need.NeedADate,
                NeedAApproved = need.NeedAApproved,
                NeedACreaterName = need.UserProfile1.UserName,
                NeedACreatedBy = need.NeddACreatedBy,
                TypeOfNeedAssessment = need.TypeOfNeedAssessment1.TypeOfNeedAssessmentID
            });
        }

        public IEnumerable<NeedAssessmentDao> ReturnNeedAssessmentHeaderViewModel(int regionId)
        {
            List<NeedAssessmentHeader> needAssessment = _unitOfWork.NeedAssessmentHeaderRepository.Get(r => r.NeedAssessment.Region == regionId).ToList();

            return needAssessment.Select(need => need.NeedAssessment.NeedADate != null ? new NeedAssessmentDao()
                                                                                             {
                                                                                                 NeedAID = (int) need.NeedAID,
                                                                                                 NAHeaderId = need.NAHeaderId,
                                                                                                 RegionId = need.NeedAssessment.Region, 
                                                                                                 Region = need.NeedAssessment.AdminUnit.Name,
                                                                                                 Zone = need.AdminUnit.Name, 
                                                                                                
                                                                                             } : null);
        }

        public IEnumerable<NeedAssessmentWoredaDao> ReturnNeedAssessmentDetailViewModel(int region)//,string season)
        {
            var woredas =_unitOfWork.NeedAssessmentDetailRepository.FindBy(z => z.NeedAssessmentHeader.AdminUnit.ParentID == region);
            return woredas.Select(adminUnit => new NeedAssessmentWoredaDao
            {
                                                         NAId = adminUnit.NAId,
                                                         NeedAID = (int) adminUnit.NeedAId,
                                                         WoredaName =  adminUnit.AdminUnit.Name,
                                                         Woreda = adminUnit.AdminUnit.AdminUnitID,
                                                         Zone = (int) adminUnit.AdminUnit.ParentID,
                                                         ZoneName = adminUnit.NeedAssessmentHeader.AdminUnit.Name,
                                                         ProjectedMale = adminUnit.ProjectedMale,
                                                         ProjectedFemale = adminUnit.ProjectedFemale,
                                                         RegularPSNP = adminUnit.RegularPSNP,
                                                         PSNP = adminUnit.PSNP,
                                                         NonPSNP = adminUnit.NonPSNP,
                                                         Contingencybudget = adminUnit.Contingencybudget,
                                                         TotalBeneficiaries = adminUnit.TotalBeneficiaries,
                                                         PSNPFromWoredasMale = adminUnit.PSNPFromWoredasMale,
                                                         PSNPFromWoredasFemale = adminUnit.PSNPFromWoredasFemale,
                                                         PSNPFromWoredasDOA = adminUnit.PSNPFromWoredasDOA,
                                                         NonPSNPFromWoredasMale = adminUnit.NonPSNPFromWoredasMale,
                                                         NonPSNPFromWoredasFemale = adminUnit.NonPSNPFromWoredasFemale,
                                                         NonPSNPFromWoredasDOA = adminUnit.NonPSNPFromWoredasDOA
                                                     });
        }

        public IEnumerable<NeedAssessmentDetail> GetDetail(IEnumerable<NeedAssessmentViewModel> detailViewModel)
        {
            return detailViewModel.Select(viewModel => new NeedAssessmentDetail
                                                           {
                                                               NeedAId = viewModel.NeedAID,
                                                               NAId = viewModel.NAId,
                                                               Woreda = viewModel.Woreda,
                                                               NeedAssessmentHeader = { Zone = viewModel.Zone },
                                                               ProjectedMale = viewModel.ProjectedMale,
                                                               ProjectedFemale = viewModel.ProjectedFemale,
                                                               RegularPSNP = viewModel.RegularPSNP,
                                                               PSNP = viewModel.PSNP,
                                                               NonPSNP = viewModel.NonPSNP,
                                                               Contingencybudget = viewModel.Contingencybudget,
                                                               TotalBeneficiaries = viewModel.TotalBeneficiaries,
                                                               PSNPFromWoredasMale = viewModel.PSNPFromWoredasMale,
                                                               PSNPFromWoredasFemale = viewModel.PSNPFromWoredasFemale,
                                                               PSNPFromWoredasDOA = viewModel.PSNPFromWoredasDOA,
                                                               NonPSNPFromWoredasMale = viewModel.NonPSNPFromWoredasMale,
                                                               NonPSNPFromWoredasFemale = viewModel.NonPSNPFromWoredasFemale,
                                                               NonPSNPFromWoredasDOA = viewModel.NonPSNPFromWoredasDOA
                                                           }).ToList();
        }

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


