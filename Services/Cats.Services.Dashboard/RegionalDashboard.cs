using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Data.Micro;
using Cats.Data.Micro.Models;
using Cats.Data.UnitWork;
using Cats.Models;
using Cats.Models.ViewModels.Dashboard;
using RegionalRequest = Cats.Models.RegionalRequest;


namespace Cats.Services.Dashboard
{
    public class RegionalDashboard : IRegionalDashboard
    {
        private IUnitOfWork _unitOfWork;
        
        public RegionalDashboard(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public List<RecentRequests> GetRecentRequests(int regionID)
        {
            /*//regionID = 5;
            var requests = new RegionalRequest();
            //var result = requests.All( columns:"RequestNumber",where:"WHERE RegionID=@0",args:regionID);
            var limResult = requests.Query("SELECT TOP 5 RegionalRequestID,RequestNumber,Month,RequestDate,Status FROM EarlyWarning.RegionalRequest WHERE RegionID=@0 ORDER BY RegionalRequestID DESC", args: regionID);
            return limResult.ToList();

            //dynamic table = new RegionalRequest();
            //var re = table.Find(Categor)*/
            var r = new List<RecentRequests>();
            var currentHRD = _unitOfWork.HRDRepository.FindBy(m => m.Status == 3).FirstOrDefault();
            var requests =  _unitOfWork.RegionalRequestRepository.FindBy(t => t.RegionID == regionID && t.PlanID==currentHRD.PlanID).OrderByDescending(t=>t.RegionalRequestID).Take(5);
           
            foreach (var regionalRequest in requests)
            {
                var n = new RecentRequests
                    {
                        RegionalRequestID = regionalRequest.RegionalRequestID,
                        RequestNumber = regionalRequest.ReferenceNumber,
                        Month = regionalRequest.RegionalRequestDetails.Count(),
                        Status = regionalRequest.Status,
                        RequestDate = regionalRequest.RequistionDate,
                        Beneficiaries = regionalRequest.RegionalRequestDetails.Sum(t => t.Beneficiaries),
                        Amount = regionalRequest.RegionalRequestDetails.Sum(t=>t.RequestDetailCommodities.Sum(s=>s.Amount))
                    };

                r.Add(n);
            }

            return r;
        }

        public List<RegionalRequestAllocationChange> GetAllocationChange(int regionID)
        {
            var r = new List<RegionalRequestAllocationChange>();
            var currentHRD = _unitOfWork.HRDRepository.FindBy(m => m.Status == 3).FirstOrDefault();
            var requests = _unitOfWork.RegionalRequestRepository.FindBy(t => t.RegionID == regionID && t.PlanID == currentHRD.PlanID).OrderByDescending(t => t.RegionalRequestID).Take(5);

            foreach (var regionalRequest in requests)
            {
                var requestedAmount = regionalRequest.RegionalRequestDetails.Sum(t => t.RequestDetailCommodities.Sum(s => s.Amount));
                var request = regionalRequest;
                var requisition = _unitOfWork.ReliefRequisitionRepository.FindBy(t => t.RegionID == regionID && t.RegionalRequest.PlanID == currentHRD.PlanID && t.RegionalRequestID == request.RegionalRequestID).ToList();
                var allocatedAmount = requisition.Sum(reliefRequisition => reliefRequisition.ReliefRequisitionDetails.Sum(reliefRequisitionDetail => reliefRequisitionDetail.Amount));
                if(requestedAmount != allocatedAmount)
                {
                    var n = new RegionalRequestAllocationChange
                    {
                        RegionalRequestID = regionalRequest.RegionalRequestID,
                        RequestNumber = regionalRequest.ReferenceNumber,
                        Month = regionalRequest.RegionalRequestDetails.Count(),
                        Status = regionalRequest.Status,
                        RequestDate = regionalRequest.RequistionDate,
                        Beneficiaries = regionalRequest.RegionalRequestDetails.Sum(t => t.Beneficiaries),
                        RequestedAmount = regionalRequest.RegionalRequestDetails.Sum(t => t.RequestDetailCommodities.Sum(s => s.Amount)),
                        AllocatedAmount = allocatedAmount
                    };
                    r.Add(n);
                }
            }
            return r;
        }

        public List<RecentRequisitions> GetRecentRequisitions(int regionID)
        {
            /*var requisitions = new RecentRequisition();
            var limResult =
                requisitions.Query(
                    "SELECT TOP 5 * FROM Dashborad_Regional_Requisitions WHERE RegionID=@0 ORDER BY RequestedDate DESC",
                    args: regionID);
            return limResult.ToList();*/
            var r = new List<RecentRequisitions>();
            var currentHRD = _unitOfWork.HRDRepository.FindBy(m => m.Status == 3);
            var requisitions = _unitOfWork.ReliefRequisitionRepository.FindBy(t => t.RegionID == regionID).OrderByDescending(t => t.RequisitionID).Take(5);

            foreach (var regionalRequisition in requisitions)
            {
                var n = new Models.ViewModels.Dashboard.RecentRequisitions
                    {
                        RequisitionID = regionalRequisition.RequisitionID,
                        RequisitionNo = regionalRequisition.RequisitionNo,
                        Status = regionalRequisition.Status,
                        RequestedDate = regionalRequisition.RequestedDate,
                        BenficiaryNo = regionalRequisition.ReliefRequisitionDetails.Sum(t => t.BenficiaryNo),
                        Name = regionalRequisition.Commodity.Name,
                        Amount = regionalRequisition.ReliefRequisitionDetails.Sum(s=>s.Amount)
                    };
                r.Add(n);
            }
            return r;
        }

        public List<object> RequisitionsPercentage(int regionID)
        {
            dynamic requisitions = new RequisitionPercentage();
            var status =
                requisitions.Query("SELECT * FROM Dashboard_regional_ReqPercentage WHERE RegionID=@0",args:regionID);

            foreach (var statu in status)
            {
                //statu.
            }
            
            return status.ToList();
        }

        public List<object> GetRecentDispatches(int regionID)
        {
            var requisitions = new RecentDispatches();
            var limResult =
                requisitions.Query(
                    "SELECT TOP 5 * FROM Dashboard_Regional_Dispatches WHERE RegionID=@0 ORDER BY DispatchDate DESC",
                    args: regionID);
            return limResult.ToList();
        }

        public void Dispose()
        {
         
        }
    }
}