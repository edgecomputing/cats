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
        IEnumerable<Beneficiaries> BarNoOfBeneficiaries();
        int Year();
    }
}
