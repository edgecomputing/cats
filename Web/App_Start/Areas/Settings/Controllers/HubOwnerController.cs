using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;
using Cats.Areas.Settings.Models;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
namespace Cats.Areas.Settings.Controllers
{
    public partial class HubOwnerController : BaseController
    {

       
        private readonly IHubOwnerService _hubOwnerService;

        public HubOwnerController(IHubOwnerService hubOwnerServiceParam,
            IUserProfileService userProfileService)
            : base(userProfileService)
        {
            this._hubOwnerService = hubOwnerServiceParam;
        }

        public IEnumerable<Cats.Areas.Settings.Models.HubOwnerViewModel> toViewModelCollection(List<Cats.Models.Hubs.HubOwner> ermodel)
        {
            return (from item in ermodel
                    select toViewModel(item));
        }
        public Cats.Areas.Settings.Models.HubOwnerViewModel toViewModel(Cats.Models.Hubs.HubOwner item)
        {
            return new HubOwnerViewModel
                {
                    Name = item.Name,
                    HubOwnerID = item.HubOwnerID,
                    LongName = item.LongName
                };
        }
        public ActionResult ReadAllJson([DataSourceRequest] DataSourceRequest request)
        {

            var list = toViewModelCollection(_hubOwnerService.GetAllHubOwner());
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult UpdateJson([DataSourceRequest] DataSourceRequest request, HubOwnerViewModel viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                var original = _hubOwnerService.FindById(viewModel.HubOwnerID);
                original.Name = viewModel.Name;
                original.LongName = viewModel.LongName;
                _hubOwnerService.EditHubOwner(original);
            }
            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }

        public ActionResult CreateJson([DataSourceRequest] DataSourceRequest request, HubOwnerViewModel viewModel)
        {
            if (viewModel != null && ModelState.IsValid)
            {
                Cats.Models.Hubs.HubOwner original = new Cats.Models.Hubs.HubOwner
                {
                    Name = viewModel.Name,
                    LongName = viewModel.LongName
                };
                _hubOwnerService.AddHubOwner(original);
            }
            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }
        public ActionResult DeleteJson([DataSourceRequest] DataSourceRequest request, HubOwnerViewModel viewModel)
        {
            var original = _hubOwnerService.FindById(viewModel.HubOwnerID);
            _hubOwnerService.DeleteHubOwner(original);

            return Json(new[] { viewModel }.ToDataSourceResult(request, ModelState));
        }
        public virtual ActionResult Index()
        {
            return View();
        }

        

        #region

        public virtual ActionResult Update()
        {
            return PartialView(_hubOwnerService.GetAllHubOwner());
        }
        
        //
        // GET: /HubOwners/Details/5

        public virtual ViewResult Details(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            return View(HubOwner);
        }

        //
        // GET: /HubOwners/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /HubOwners/Create

        [HttpPost]
        public virtual ActionResult Create(HubOwner HubOwner)
        {
            if (ModelState.IsValid)
            {
                _hubOwnerService.AddHubOwner(HubOwner);
                return Json(new { success = true }); 
            }

            return PartialView(HubOwner);
        }
        
        //
        // GET: /HubOwners/Edit/5

        public virtual ActionResult Edit(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            return PartialView(HubOwner);
        }

        //
        // POST: /HubOwners/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(HubOwner HubOwner)
        {
            if (ModelState.IsValid)
            {
                _hubOwnerService.EditHubOwner(HubOwner);
                return Json(new { success = true }); 
            }
            return PartialView(HubOwner);
        }

        //
        // GET: /HubOwners/Delete/5

        public virtual ActionResult Delete(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            return View(HubOwner);
        }

        //
        // POST: /HubOwners/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _hubOwnerService.DeleteById(id);
            return RedirectToAction("Index");
        }
#endregion
    }
}