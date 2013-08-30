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
            return (from _regionalRequests in regionalRequests group _regionalRequests by _regionalRequests.AdminUnit.Name into RegionalRequests select new Request() { RegionName=  RegionalRequests.Key, RequestsCount = RegionalRequests.Count() });
        }

        public IEnumerable<Beneficiaries> BarNoOfBeneficiaries()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var noOfBeneficiaries = _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
            return (from _noOfBeneficiaries in noOfBeneficiaries group _noOfBeneficiaries by _noOfBeneficiaries.AdminUnit.Name into NoOfBeneficiaries select new Beneficiaries()  {RegionName = NoOfBeneficiaries.Key, BeneficiariesCount = NoOfBeneficiaries.Sum(t => t.RegionalRequestDetails.Sum(m => m.Beneficiaries)) }).ToList();
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

        public IEnumerable<RegionalBeneficiaries> RegionalRequestsBeneficiary()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var beneficiaryDetail = _IUnitOfWork.RegionalRequestDetailRepository.FindBy(t => t.RegionalRequest.Month >= sixMonthsBack && t.RegionalRequest.Year >= year);
            return (from _beneficiaryDetail in beneficiaryDetail join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID where _adminUnitType.Name == "Regions" group _beneficiaryDetail by _beneficiaryDetail.RegionalRequest.AdminUnit.Name into _Beneficiaries select new RegionalBeneficiaries() { RegionName = _Beneficiaries.Key, Request = _Beneficiaries.Sum(m => m.Beneficiaries), Allocation = RegionalAllocation(_Beneficiaries.Key), HRD = RegionalHRD(_Beneficiaries.Key) });
        }

        public IEnumerable<ZonalBeneficiaries> ZonalBeneficiaries(int RegionId)
        {
            int year = Year();
            var beneficiaryDetail = _IUnitOfWork.RegionalRequestDetailRepository.FindBy(t => t.RegionalRequest.Year >= year);
            var f = (from _beneficiaryDetail in beneficiaryDetail join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.AdminUnitID == RegionId group _beneficiaryDetail by _beneficiaryDetail.RegionalRequest.Month into _Beneficiaries select new ZonalBeneficiaries() { Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Beneficiaries.Key), Request = _Beneficiaries.Sum(m => m.Beneficiaries), Allocation = ZonalAllocation(RegionId, _Beneficiaries.Key), HRD = ZonalHRD(RegionId, _Beneficiaries.Key) });
            return f;
        }

        public decimal RegionalAllocation(string RegionName)
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var commodityAllocation = (from _allocation in _IUnitOfWork.ReliefRequisitionDetailRepository.FindBy(t => t.ReliefRequisition.RegionalRequest.Month >= sixMonthsBack && t.ReliefRequisition.RegionalRequest.Year >= year) join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _allocation.ReliefRequisition.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID where _adminUnitType.Name == "Regions" group _allocation by _allocation.ReliefRequisition.AdminUnit.Name into _Allocation select new { RegionName = _Allocation.Key, Amount = _Allocation.Sum(t => t.BenficiaryNo) }).Where(t => t.RegionName == RegionName).FirstOrDefault();
            return commodityAllocation == null ? 0 : commodityAllocation.Amount;
        }

        public decimal ZonalAllocation(int RegionId, int Month)
        {
            int year = Year();
            var commodityAllocation = (from _allocation in _IUnitOfWork.ReliefRequisitionDetailRepository.FindBy(t => t.ReliefRequisition.RegionalRequest.Year >= year) join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _allocation.ReliefRequisition.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.AdminUnitID == RegionId group _allocation by _allocation.ReliefRequisition.RegionalRequest.Month into _Allocation select new { Month = _Allocation.Key, Amount = _Allocation.Sum(t => t.BenficiaryNo) }).Where(t => t.Month == Month).FirstOrDefault();
            return commodityAllocation == null ? 0 : commodityAllocation.Amount;
        }

        public decimal RegionalHRD(string RegionName)
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var hrd = (from _hrd in _IUnitOfWork.HRDDetailRepository.FindBy(t => t.StartingMonth + (t.DurationOfAssistance - 1) >= sixMonthsBack && t.HRD.Year >= year) join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _hrd.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID where _adminUnitType.Name == "Regions" group _hrd by _hrd.AdminUnit.Name into _HRD select new { RegionName = _HRD.Key, Amount = _HRD.Sum(t => t.NumberOfBeneficiaries) }).Where(t => t.RegionName == RegionName).FirstOrDefault();
            return hrd == null ? 0 : hrd.Amount;

        }

        public decimal ZonalHRD(int RegionId, int Month)
        {
            int year = Year();
            var hrd = (from _hrd in _IUnitOfWork.HRDDetailRepository.FindBy(t => t.StartingMonth + (t.DurationOfAssistance - 1) >= Month && t.StartingMonth + (t.DurationOfAssistance - 1) <= Month && t.HRD.Year >= year) join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _hrd.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.ParentID == RegionId group _hrd by _hrd.StartingMonth into _HRD select new { Month = _HRD.Key, Amount = _HRD.FirstOrDefault().NumberOfBeneficiaries }).Where(t => t.Month == Month).FirstOrDefault();
            return hrd == null ? 0 : hrd.Amount;
        }
    }
}
