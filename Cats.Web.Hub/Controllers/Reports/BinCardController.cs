using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Telerik.Web.Mvc;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers.Reports
{
     [Authorize]
    public class BinCardController : BaseController
    {

         private readonly ICommodityService _commodityService;
         private readonly IProjectCodeService _projectCodeService;
         private readonly IStoreService _storeService;

         public BinCardController(ICommodityService commodityService, 
             IProjectCodeService projectCodeService,
             IStoreService storeService, 
             IUserProfileService userProfileService)
             : base(userProfileService)
         {
             _commodityService = commodityService;
             _projectCodeService = projectCodeService;
             _storeService = storeService;
         }

        public ActionResult Index(int? StoreID, int? CommodityID, string ProjectID )
        {
            ViewBag.StoreID = new SelectList(GetCurrentUserProfile().DefaultHub.Stores,"StoreID","Name",StoreID);
            //var StoreI = new SelectList(GetCurrentUserProfile().DefaultHub.Stores,"StoreID","Name", StoreID);

            ViewBag.CommodityID = new SelectList(_commodityService.GetAllParents(), "CommodityID", "Name",CommodityID);
            ViewBag.ProjectID =
                new SelectList(
                    _projectCodeService.GetProjectCodesForCommodity(GetCurrentUserProfile().DefaultHub.HubID,
                                                                       (CommodityID.HasValue) ? CommodityID.Value : 1),"ProjectCodeId","ProjectName");
            //UserProfile user =UserProfile.GetUser(User.Identity.Name);
            //var projectInputReceives = db.Receives.FirstOrDefault(p => p.ProjectNumber == ProjectID);
            //var projectInputDispatches = db.Dispatches.FirstOrDefault(p => p.ProjectNumber == ProjectID);
            //string projectSelected = "";
            //if (projectInputReceives != null)
            //{
            //    projectSelected = projectInputReceives.ProjectNumber;
            //}
            //else if (projectInputDispatches != null)
            //{
            //    projectSelected = projectInputDispatches.ProjectNumber;
            //}

            ViewBag.BinCards = _storeService.GetBinCard(UserProfile.DefaultHub.HubID, StoreID, CommodityID, ProjectID).ToList();
            //ViewBag.StoreID = new SelectList(db.Stores.Where(s => s.HubID == user.DefaultWarehouse.HubID), "StoreID", "Name");
            //ViewBag.CommodityID = new SelectList(db.Commodities, "CommodityID", "Name");

            //var UniqueprojectsReceives = from r in db.Receives
            //               group r by r.ProjectNumber into b 
            //               select b;

            //var UniqueprojectsReceivesProjects = from Ur in UniqueprojectsReceives
            //                                     where Ur.Key != null 
            //                                     select new Cats.Web.Hub.Models.Project()
            //                                     {
            //                                         Name = Ur.FirstOrDefault().ProjectNumber,
            //                                         ProjectID = Ur.FirstOrDefault().ProjectNumber //Ur.FirstOrDefault().ReceiveID
            //                                     };

            //var UniqueprojectsDispatches = from d in db.Dispatches
            //                             group d by d.ProjectNumber into a
            //                             select a;


            //var UniqueprojectsDispatchesProjects = from Ud in UniqueprojectsDispatches
            //                                       where Ud.Key != null
            //                                     select new Cats.Web.Hub.Models.Project()
            //                                     {
            //                                         Name = Ud.FirstOrDefault().ProjectNumber,
            //                                         ProjectID = Ud.FirstOrDefault().ProjectNumber//Ud.FirstOrDefault().DispatchID
            //                                     };
            //List<Cats.Web.Hub.Models.Project> Uniqueprojects = UniqueprojectsReceivesProjects.Concat(UniqueprojectsDispatchesProjects).ToList();//.Distinct(new NameComparer());//,new NameComparer());

            //var perfectUniques = Uniqueprojects.Distinct(new NameComparer());

            //ViewBag.ProjectID = new SelectList(perfectUniques, "ProjectID", "Name");
            
            return View();
        }

    }
}
