using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Hub;
using Telerik.Web.Mvc;
using Cats.Models.Hub.ViewModels;
using Cats.Models.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class ReplicationController : BaseController
    {
        public ReplicationController(IUserProfileService userProfileService)
            : base(userProfileService)
        {
            
        }
        public ActionResult Index()
        {
            return View();
        }

        [GridAction]
        public ActionResult ReplicationGrid(int publication)
        {
            //var items = new List<ReplicationViewModel>();
            //items.Add(new ReplicationViewModel { PartitionId = 1, HubName = "Adama", LastSyncTime = DateTime.Now, PartitionCreatedDate = DateTime.Now, LastUpdated = DateTime.Now, LastAction = "The snapshot for this publication has become obsolete. The snapshot agent needs to be run again before the subscription can be synchronized." });
           // var items = repository.Partition.GetHubsSyncrtonizationDetails(1);
            return View(new GridModel(new List<ReplicationViewModel>()));
        }

        public ActionResult PublicatonOne()
        {
            return PartialView();
        }

        public ActionResult PublicationTwo()
        {
            return PartialView();
        }


    }
}
