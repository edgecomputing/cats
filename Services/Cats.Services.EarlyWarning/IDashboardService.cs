using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Data.UnitWork;
using Cats.Models;
using System.Collections;

namespace Cats.Services.EarlyWarning
{
    public interface IDashboardService
    {
        List<RegionalRequest> RegionalRequestsByRegionID(int RegionId);
        List<RegionalRequest> RegionalRequests(int RegionId);
        IEnumerable<Request> PieRegionalRequests();
        IEnumerable<ReliefRequisition> RequisitionBasedOnStatus();
        IEnumerable<Beneficiaries> BarNoOfBeneficiaries();
        List<RegionalRequest> Requests();
        IEnumerable<RegionalBeneficiaries> RegionalRequestsBeneficiary();
        IEnumerable<ZonalBeneficiaries> ZonalBeneficiaries(int RegionId);
        int getRegionId(string regionName);

        IEnumerable<ZonalBeneficiaries> ZonalMonthlyBeneficiaries(string RegionName, string ZoneName);

        IEnumerable<RegionalMonthlyRequest> RMRequests();

    }
}
