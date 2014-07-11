using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.Constant;

namespace Cats.Services.Common
{
    public class CommonService:ICommonService
    {
        private readonly IUnitOfWork _unitOfWork;

        public CommonService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public IEnumerable<Models.CommodityType> GetCommodityTypes(System.Linq.Expressions.Expression<Func<Models.CommodityType, bool>> filter = null, Func<IQueryable<Models.CommodityType>, IOrderedQueryable<Models.CommodityType>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CommodityTypeRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<Models.Commodity> GetCommodities(System.Linq.Expressions.Expression<Func<Models.Commodity, bool>> filter = null, Func<IQueryable<Models.Commodity>, IOrderedQueryable<Models.Commodity>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.CommodityRepository.Get(filter, orderBy, includeProperties);
            
        }

        public IEnumerable<Models.Donor> GetDonors(System.Linq.Expressions.Expression<Func<Models.Donor, bool>> filter = null, Func<IQueryable<Models.Donor>, IOrderedQueryable<Models.Donor>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DonorRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<Models.Program> GetPrograms(System.Linq.Expressions.Expression<Func<Models.Program, bool>> filter = null, Func<IQueryable<Models.Program>, IOrderedQueryable<Models.Program>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.ProgramRepository.Get(filter, orderBy, includeProperties);
        }
        public IEnumerable<Models.Ration> GetRations(System.Linq.Expressions.Expression<Func<Models.Ration, bool>> filter = null, Func<IQueryable<Models.Ration>, IOrderedQueryable<Models.Ration>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.RationRepository.Get(filter, orderBy, includeProperties);
        }

        public IEnumerable<Models.AdminUnit> GetAminUnits(System.Linq.Expressions.Expression<Func<Models.AdminUnit, bool>> filter = null, Func<IQueryable<Models.AdminUnit>, IOrderedQueryable<Models.AdminUnit>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.AdminUnitRepository.Get(filter, orderBy, includeProperties);
        }
       


     

        public IEnumerable<Models.Detail> GetDetails(System.Linq.Expressions.Expression<Func<Models.Detail, bool>> filter = null, Func<IQueryable<Models.Detail>, IOrderedQueryable<Models.Detail>> orderBy = null, string includeProperties = "")
        {
            return _unitOfWork.DetailRepository.Get(filter, orderBy, includeProperties);
        }
       
        public string GetStatusName(Models.Constant.WORKFLOW workflow, int statusId)
        {
            var workflowStatus =
                _unitOfWork.WorkflowStatusRepository.Get(t => t.WorkflowID == (int)workflow && t.StatusID == statusId).
                    FirstOrDefault();
            return workflowStatus != null ? workflowStatus.Description :
             string.Empty;
        }
        public List<Models.WorkflowStatus> GetStatus(Models.Constant.WORKFLOW workflow)
        {
            return _unitOfWork.WorkflowStatusRepository.Get(t => t.WorkflowID == (int)workflow).ToList();
        }
        public void Dispose()
        {
            _unitOfWork.Dispose();
        }


        public IEnumerable<Models.Season> GetSeasons(System.Linq.Expressions.Expression<Func<Models.Season, bool>> filter = null, Func<IQueryable<Models.Season>, IOrderedQueryable<Models.Season>> orderBy = null, string includeProperties = "")
        {
          return  _unitOfWork.SeasonRepository.GetAll();
        }


        public List<Models.Plan> GetPlan(string programName)
        {
            if (programName=="PSNP")
            {
                return _unitOfWork.PlanRepository.FindBy(m => m.Program.Name == programName && m.Status==(int)PlanStatus.PSNPCreated);
            }
            return  _unitOfWork.PlanRepository.FindBy(m => m.Program.Name == programName && m.Status == (int)PlanStatus.HRDCreated);
        }
       public List<Plan> GetPlan(int programID)
       {
          
           if (programID == 2)
           {
               var psnpPlanID = _unitOfWork.RegionalPSNPPlanRepository.FindBy(t => t.AttachedBusinessProcess.CurrentState.BaseStateTemplate.StateNo >2).Select(m => m.PlanId).Distinct();
               return _unitOfWork.PlanRepository.FindBy(m => psnpPlanID.Contains(m.PlanID) && m.ProgramID == programID && m.Status == (int)PlanStatus.PSNPCreated);
           }
           var planId = _unitOfWork.HRDRepository.FindBy(m => m.Status ==(int)HRDStatus.Approved || m.Status ==(int)HRDStatus.Published).Select(m => m.PlanID).Distinct();
           return _unitOfWork.PlanRepository.FindBy(m => planId.Contains(m.PlanID) && m.ProgramID == programID && m.Status == (int)PlanStatus.HRDCreated);
       }

     public  List<Plan> GetPlans()
     {
         return _unitOfWork.PlanRepository.GetAll();
     }
       public List<FDP> GetFDPs(int woredaID)
       {
           return _unitOfWork.FDPRepository.FindBy(m => m.AdminUnitID == woredaID);
       }


       public List<Commodity> GetRationCommodity(int id)
       {
           var ration = _unitOfWork.RationRepository.FindById(id);
           var commodity = (from detail in ration.RationDetails
                            select new Commodity()
                                {
                                    CommodityID = detail.CommodityID,
                                    Name = detail.Commodity.Name
                                }).ToList();
           return commodity;
       }
       public List<AdminUnit> FindBy(Expression<Func<AdminUnit, bool>> predicate)
       {
           return _unitOfWork.AdminUnitRepository.FindBy(predicate);
       }
       public List<AdminUnit> GetRegions()
       {
           return _unitOfWork.AdminUnitRepository.Get(t => t.AdminUnitTypeID == 2).ToList();
       }
       public List<AdminUnit> GetZones(int regionId)
       {
           return _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == regionId).ToList();
       }
       public List<AdminUnit> GetWoreda(int zoneId)
       {
           return _unitOfWork.AdminUnitRepository.Get(t => t.ParentID == zoneId).ToList();
       }


       public List<SupportType> GetAllSupportType()
       {
           return _unitOfWork.SupportTypeRepository.GetAll();
       }


       public int GetZoneID(int woredaID)
       {
           var woreda = _unitOfWork.AdminUnitRepository.FindBy(m => m.AdminUnitID == woredaID).FirstOrDefault();
           if (woreda != null)
               return woreda.AdminUnit2.
                   AdminUnitID;
           return 0;
       }

        public int GetRegion(int zoneID)
        {
            var zone = _unitOfWork.AdminUnitRepository.FindBy(m => m.AdminUnit2.AdminUnitID == zoneID).FirstOrDefault();
            if (zone!=null)
            {
                return zone.AdminUnit2.AdminUnit2.AdminUnitID;
            }
            return 0;
        }

       public List<Plan> GetRequisitionGeneratedPlan(int programID, int zoneID)
       {
           var requisition = _unitOfWork.ReliefRequisitionRepository.FindBy(m => m.ZoneID == zoneID).Select(m => m.RegionalRequestID).Distinct();
           var request = _unitOfWork.RegionalRequestRepository.FindBy(m => requisition.Contains(m.RegionalRequestID) && m.ProgramId == programID).Select(m => m.PlanID).Distinct();
           var requestCreated = _unitOfWork.PlanRepository.FindBy(m => request.Contains(m.PlanID));
           return requestCreated;
       }
       public List<CommoditySource> GetCommoditySource()
       {
           return
               _unitOfWork.CommoditySourceRepository.FindBy(
                   m =>
                   m.CommoditySourceID == 2 || m.CommoditySourceID == 5 || m.CommoditySourceID == 8 ||
                   m.CommoditySourceID == 9); //populate commodity sources for Loan,Transfer,Repayment and Swap

       }
       public int GetShippingInstruction(string siNumber)
       {
           var sINumber =_unitOfWork.ShippingInstructionRepository.FindBy(m => m.Value == siNumber).FirstOrDefault();
           if (sINumber==null)
           {
               var shippingInstruction = new ShippingInstruction();
               shippingInstruction.Value = siNumber;
               _unitOfWork.ShippingInstructionRepository.Add(shippingInstruction);
               _unitOfWork.Save();
               return shippingInstruction.ShippingInstructionID;
           }
           return sINumber.ShippingInstructionID;
       }
        public List<Hub> GetAllHubs()
        {
            return _unitOfWork.HubRepository.GetAll();
        }
        public List<GiftCertificate> GetAllGiftCertificates()
        {
            return _unitOfWork.GiftCertificateRepository.GetAll();
        }
        public string GetCommditySourceName(int id)
        {
            var commoditySource = _unitOfWork.CommoditySourceRepository.FindById(id);
            if (commoditySource!=null)
            {
                return commoditySource.Name;
            }
            return null;
        }
        
    }
}
