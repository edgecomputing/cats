using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;


namespace Cats.Models.Hub.ViewModels.Dispatch
{
   public class DispatchHomeViewModel
    {
       //TODO:Generated IDispatchAllocationService
       public DispatchHomeViewModel(List<DispatchAllocationViewModelDto> toFdps,List<OtherDispatchAllocation> loans
           , List<OtherDispatchAllocation> transfers,List<CommodityType> commodityTypes,List<AdminUnit> adminUnits,UserProfile user)
       {

           ToFDPs = toFdps;// dispatchAllocationService.GetCommitedAllocationsByHubDetached(user.DefaultHub.HubID, user.PreferedWeightMeasurment.ToUpperInvariant(), null, null, null);

           Loans = loans;// repository.OtherDispatchAllocation.GetAllToOtherOwnerHubs(user);

           Transfers = transfers;// repository.OtherDispatchAllocation.GetAllToCurrentOwnerHubs(user);

           AdminUnits = adminUnits;// new List<AdminUnit>() { repository.AdminUnit.FindById(1) };

           CommodityTypes = commodityTypes;// repository.CommodityType.GetAll();

           CommodityTypeID = 1; //food is the default
       }

       public int CommodityTypeID { get; set; }

       public List<OtherDispatchAllocation> Transfers { get; set; }
       public List<OtherDispatchAllocation> Loans { get; set; }
       public List<DispatchAllocationViewModelDto> ToFDPs { get; set; }
       //public List<AdminUnit> Regions { get; set; }
       public List<AdminUnit> AdminUnits { get; set; }
       public List<CommodityType> CommodityTypes { get; set; }
    }
}
