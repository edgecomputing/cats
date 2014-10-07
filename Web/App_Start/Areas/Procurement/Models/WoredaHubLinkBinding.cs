using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;

namespace Cats.Areas.Procurement.Models
{
    public class WoredaHubLinkBinding
    {
        public static List<WoredaHubLinkViewModel> WoredaHubLinkListViewModelBinder(List<WoredaHubLink> woredaHubLinks)
        {
            return woredaHubLinks.Select(woredaHubLink => new WoredaHubLinkViewModel
            {
                WoredaHubLinkID = woredaHubLink.WoredaHubLinkID,
                WoredaID = woredaHubLink.WoredaID,
                Woreda = woredaHubLink.AdminUnit.Name,
                HubID = woredaHubLink.HubID,
                Hub = woredaHubLink.Hub.Name
            }).ToList();
        }

        public static WoredaHubLink WoredaHubLinkListBinder(WoredaHubLinkViewModel model)
        {
            if (model == null) return null;
            var woredaHubLink = new WoredaHubLink()
            {
                WoredaHubLinkID = model.WoredaHubLinkID,
                WoredaID = model.WoredaID,
                HubID = model.HubID
            };
            return woredaHubLink;
        }
    }
}