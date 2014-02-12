using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public class HubViewModelBinder
    {
        public static HubViewModel BindHubViewModel(Hub hub)
        {
            return new HubViewModel()
            {
                HubID = hub.HubID,
                HubName = hub.Name,
                HubOwnerID = hub.HubOwnerID
            };
        }
        public static List<HubViewModel> BindListHubViewModel(List<Hub> hubs)
        {
            return hubs.Select(BindHubViewModel).ToList();
        }

        public static List<Hub> BindListHub(List<HubViewModel> hubViewModels)
        {
            return hubViewModels.Select(BindHub).ToList();
        }

        public static Hub BindHub(HubViewModel hubViewModel)
        {
            return new Hub()
            {
                HubID = hubViewModel.HubID,
                Name = hubViewModel.HubName,
                HubOwnerID = hubViewModel.HubOwnerID
            };
        }
    }
}