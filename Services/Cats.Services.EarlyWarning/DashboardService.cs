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

        public IEnumerable<RegionalMonthlyRequest> RMRequests()
        {

            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var requests =  _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
            return (from r in requests 
                    select new RegionalMonthlyRequest 
                    {
                      RequestID = r.RegionalRequestID,
                      RegionId = r.RegionID,
                      RegionName = r.AdminUnit.Name,
                      ReferenceNumber = r.ReferenceNumber,
                      Year = r.Year,
                      Month = r.MonthName,
                      daysAgo = Daysdifference(r.RequistionDate)});
        }

        public IEnumerable<Request> PieRegionalRequests()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var regionalRequests = _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
            return (from _regionalRequests in regionalRequests group _regionalRequests by _regionalRequests.AdminUnit.Name 
                        into RegionalRequests select new Request() { RegionName = RegionalRequests.Key, RequestsCount = RegionalRequests.Count() });
        }

        public IEnumerable<ReliefRequisition> RequisitionBasedOnStatus()
        {
            var requisitions = _IUnitOfWork.ReliefRequisitionRepository.GetAll();
            return requisitions;
        }

        public IEnumerable<Beneficiaries> BarNoOfBeneficiaries()
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();
            var noOfBeneficiaries = _IUnitOfWork.RegionalRequestRepository.FindBy(t => t.Month >= sixMonthsBack && t.Year >= year);
            return (from _noOfBeneficiaries in noOfBeneficiaries group _noOfBeneficiaries by _noOfBeneficiaries.AdminUnit.Name 
                        into NoOfBeneficiaries select new Beneficiaries() { RegionName = NoOfBeneficiaries.Key,
                        BeneficiariesCount = NoOfBeneficiaries.Sum(t => t.RegionalRequestDetails.Sum(m => m.Beneficiaries)) }).ToList();
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

            //var g= (from _beneficiaryDetail in beneficiaryDetail join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.AdminUnitType.Name == "Region" group _beneficiaryDetail by _beneficiaryDetail.RegionalRequest.AdminUnit.Name into _Beneficiaries select new RegionalBeneficiaries() { RegionName = _Beneficiaries.Key, Request = _Beneficiaries.Sum(m => m.Beneficiaries), Allocation = RegionalAllocation(_Beneficiaries.Key), HRD = RegionalHRD(_Beneficiaries.Key) });

            //return g;

            return (from _beneficiaryDetail in beneficiaryDetail join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() 
                    on _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.AdminUnitType.Name == "Region" 
                    group _beneficiaryDetail by _beneficiaryDetail.RegionalRequest.AdminUnit.Name into _Beneficiaries 
                    select new RegionalBeneficiaries() { RegionName = _Beneficiaries.Key, Request = _Beneficiaries.Sum(m => m.Beneficiaries), 
                        Allocation = RegionalAllocation(_Beneficiaries.Key), HRD = RegionalHRD(_Beneficiaries.Key) });

        }

        public IEnumerable<ZonalBeneficiaries> ZonalBeneficiaries(int RegionId)
        {
            int year = Year();
            var beneficiaryDetail = _IUnitOfWork.RegionalRequestDetailRepository.FindBy(t => t.RegionalRequest.Year >= year);

            //var f = (from _beneficiaryDetail in beneficiaryDetail join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.AdminUnitID == RegionId && _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID == RegionId group _beneficiaryDetail by new { _beneficiaryDetail.RegionalRequest.Month, _beneficiaryDetail.Fdp.AdminUnit.AdminUnitID } into _Beneficiaries select new ZonalBeneficiaries() { Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Beneficiaries.Key.Month), Zone = _IUnitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitID == _Beneficiaries.Key.AdminUnitID).FirstOrDefault().Name, Request = _Beneficiaries.Sum(m => m.Beneficiaries), Allocation = ZonalAllocation(RegionId, _Beneficiaries.Key.Month, _Beneficiaries.Key.AdminUnitID), HRD = ZonalHRD(RegionId, _Beneficiaries.Key.Month, _Beneficiaries.Key.AdminUnitID) });

            return (from _beneficiaryDetail in beneficiaryDetail join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() 
                     on _beneficiaryDetail.RegionalRequest.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID where _adminUnit.AdminUnitID == RegionId 
                     group _beneficiaryDetail by new { _beneficiaryDetail.RegionalRequest.Month, _beneficiaryDetail.Fdp.AdminUnit.AdminUnitID } 
                     into _Beneficiaries select new ZonalBeneficiaries() { Month = System.Globalization.CultureInfo.CurrentCulture.DateTimeFormat.GetMonthName(_Beneficiaries.Key.Month), 
                         Zone = _IUnitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitID == _Beneficiaries.Key.AdminUnitID).FirstOrDefault().Name, 
                         Request = _Beneficiaries.Sum(m => m.Beneficiaries), Allocation = ZonalAllocation(RegionId, _Beneficiaries.Key.Month,
                         _Beneficiaries.Key.AdminUnitID), HRD = ZonalHRD(RegionId, _Beneficiaries.Key.Month, _Beneficiaries.Key.AdminUnitID) });

        }

        public decimal RegionalAllocation(string RegionName)
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();

            //var commodityAllocation = (from _allocation in _IUnitOfWork.ReliefRequisitionDetailRepository.FindBy(t => t.ReliefRequisition.RegionalRequest.Month >= sixMonthsBack && t.ReliefRequisition.RegionalRequest.Year >= year) join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _allocation.ReliefRequisition.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID where _adminUnitType.Name == "Region" group _allocation by _allocation.ReliefRequisition.AdminUnit.Name into _Allocation select new { RegionName = _Allocation.Key, Amount = _Allocation.Sum(t => t.BenficiaryNo) }).Where(t => t.RegionName == RegionName).FirstOrDefault();

            var commodityAllocation = (from _allocation in _IUnitOfWork.ReliefRequisitionDetailRepository.FindBy(t => t.ReliefRequisition.RegionalRequest.Month >= sixMonthsBack && t.ReliefRequisition.RegionalRequest.Year >= year)
                                       join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _allocation.ReliefRequisition.AdminUnit.AdminUnitID 
                                       equals _adminUnit.AdminUnitID join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID 
                                       where _adminUnitType.Name == "Region" group _allocation by _allocation.ReliefRequisition.AdminUnit.Name into _Allocation 
                                       select new { RegionName = _Allocation.Key, Amount = _Allocation.Sum(t => t.BenficiaryNo) }).Where(t => t.RegionName == RegionName).FirstOrDefault();

            return commodityAllocation == null ? 0 : commodityAllocation.Amount;
        }

        public decimal ZonalAllocation(int RegionId, int Month, int ZoneId)
        {
            int year = Year();
            var commodityAllocation = (from _allocation in _IUnitOfWork.ReliefRequisitionDetailRepository.FindBy(t => t.ReliefRequisition.RegionalRequest.Year >= year) 
                                       join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _allocation.ReliefRequisition.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID 
                                       where _adminUnit.AdminUnitID == RegionId && _allocation.FDP.AdminUnit.ParentID == ZoneId group _allocation 
                                       by new { _allocation.ReliefRequisition.RegionalRequest.Month, _allocation.FDP.AdminUnitID} into _Allocation 
                                       select new { Month = _Allocation.Key.Month, Zone=_IUnitOfWork.AdminUnitRepository.FindBy(t=>t.AdminUnitID == _Allocation.Key.AdminUnitID).FirstOrDefault().Name, 
                                       Amount = _Allocation.Sum(t => t.BenficiaryNo) }).Where(t => t.Month == Month).FirstOrDefault();
            return commodityAllocation == null ? 0 : commodityAllocation.Amount;
        }

        public decimal RegionalHRD(string RegionName)
        {
            int year = Year();
            int sixMonthsBack = SixMonthsBack();

            //var hrd = (from _hrd in _IUnitOfWork.HRDDetailRepository.FindBy(t => t.StartingMonth + (t.DurationOfAssistance - 1) >= sixMonthsBack && t.HRD.Year >= year) join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _hrd.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID where _adminUnitType.Name == "Region" group _hrd by _hrd.AdminUnit.Name into _HRD select new { RegionName = _HRD.Key, Amount = _HRD.Sum(t => t.NumberOfBeneficiaries) }).Where(t => t.RegionName == RegionName).FirstOrDefault();

            var hrd = (from _hrd in _IUnitOfWork.HRDDetailRepository.FindBy(t => t.StartingMonth + (t.DurationOfAssistance - 1) >= sixMonthsBack && t.HRD.Year >= year) 
                       join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _hrd.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID 
                       join _adminUnitType in _IUnitOfWork.AdminUnitTypeRepository.GetAll() on _adminUnit.AdminUnitTypeID equals _adminUnitType.AdminUnitTypeID 
                       where _adminUnitType.Name == "Region" group _hrd by _hrd.AdminUnit.Name into _HRD 
                       select new { RegionName = _HRD.Key, Amount = _HRD.Sum(t => t.NumberOfBeneficiaries) }).Where(t => t.RegionName == RegionName).FirstOrDefault();

            return hrd == null ? 0 : hrd.Amount;

        }

        public decimal ZonalHRD(int RegionId, int Month, int ZoneId)
        {
            int year = Year();
            var hrd = (from _hrd in _IUnitOfWork.HRDDetailRepository.FindBy(t => t.StartingMonth + (t.DurationOfAssistance - 1) >= Month && t.StartingMonth + (t.DurationOfAssistance - 1) <= Month && t.HRD.Year >= year)
                       join _adminUnit in _IUnitOfWork.AdminUnitRepository.GetAll() on _hrd.AdminUnit.AdminUnitID equals _adminUnit.AdminUnitID 
                       where _adminUnit.AdminUnitID == RegionId group _hrd by new { _hrd.StartingMonth, _adminUnit.FDPs.FirstOrDefault().AdminUnitID } 
                       into _HRD select new { Month = _HRD.Key.StartingMonth, Zone = _IUnitOfWork.AdminUnitRepository.FindBy(t => t.AdminUnitID == _HRD.Key.AdminUnitID).FirstOrDefault().Name,
                      Amount = _HRD.FirstOrDefault().NumberOfBeneficiaries }).Where(t => t.Month == Month).FirstOrDefault();
            return hrd == null ? 0 : hrd.Amount;
        }

        public int getRegionId(string regionName)
        {
            return _IUnitOfWork.AdminUnitRepository.FindBy(t => t.Name == regionName).FirstOrDefault().AdminUnitID;
        }


        public IEnumerable<ZonalBeneficiaries> ZonalMonthlyBeneficiaries(string RegionName, string ZoneName)
        {
            var zonalMonthlyBeneficiaries = ZonalBeneficiaries(getRegionId(RegionName));
            return (from _beneficiaries in zonalMonthlyBeneficiaries where _beneficiaries.Zone == ZoneName select _beneficiaries);
        }
        public int Daysdifference(DateTime Past) {
            return (int)(DateTime.Now - Past).TotalDays;

        }
    }
}
