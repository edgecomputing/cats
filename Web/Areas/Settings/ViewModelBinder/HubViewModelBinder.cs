using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Areas.Settings.Models.ViewModels;

namespace Cats.Areas.Settings.ViewModelBinder
{
    public class HubViewModelBinder
    {
        public static HubViewModel BindHubViewModel(Cats.Models.Hub hub)
        {
            return new HubViewModel()
            {
                HubID = hub.HubID,
                HubName = hub.Name,
                HubOwnerID = hub.HubOwnerID
            };
        }
        public static List<HubViewModel> BindListHubViewModel(List<Cats.Models.Hub> hubs)
        {
            return hubs.Select(BindHubViewModel).ToList();
        }

        public static List<Cats.Models.Hub> BindListHub(List<HubViewModel> hubViewModels)
        {
            return hubViewModels.Select(BindHub).ToList();
        }

        public static Cats.Models.Hub BindHub(HubViewModel hubViewModel)
        {
            return new Cats.Models.Hub()
            {
                HubID = hubViewModel.HubID,
                Name = hubViewModel.HubName,
                HubOwnerID = hubViewModel.HubOwnerID
            };
        }
    }
}