using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using System.Collections;
using Cats.Models;


namespace Cats.Services.EarlyWarning
{
    public class DashboardService : IDashboardService
    {
        public DashboardService()
        {
            this._IUnitOfWork = new UnitOfWork();
        }

        private readonly IUnitOfWork _IUnitOfWork;

        public List<RegionalRequest> RegionalRequestsByRegionID(int RegionId)
        {
            return _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.RegionID == RegionId);
        }

        public List<RegionalRequest> RegionalRequests(int RegionId)
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            return _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year && t.RegionID == RegionId);
        }

        public List<RegionalRequest> Requests()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            return _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
        }

        public IEnumerable<Request> PieRegionalRequests()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var regionalRequests = _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
            return (from _regionalRequests in regionalRequests group _regionalRequests by _regionalRequests.AdminUnit.Name into RegionalRequests select new Request() { RegionName = RegionalRequests.Key, RequestsCount = RegionalRequests.Count() });
        }

        public IEnumerable<Beneficiaries> BarNoOfBeneficiaries()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var noOfBeneficiaries = _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
            return (from _noOfBeneficiaries in noOfBeneficiaries group _noOfBeneficiaries by _noOfBeneficiaries.AdminUnit.Name into NoOfBeneficiaries select new Beneficiaries() { RegionName = NoOfBeneficiaries.Key, BeneficiariesCount = NoOfBeneficiaries.Sum(t => t.RegionalRequestDetails.Sum(m => m.Beneficiaries)) }).ToList();
        }

        public int SixMonthsBack()
        {
            return DateTime.Now.AddMonths(-1 * 6).Month;
        }

        public int Year()
        {
            int _Year = DateTime.Now.Year;
            if (DateTime.Now.Month - 6 < 1)
                return _Year--;
            return _Year;
        }
    }
}