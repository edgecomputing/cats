
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Cats.Models;
using Cats.Web.Administration.Models.ViewModels;
using Cats.Services.Administration;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace  Cats.Web.Adminstration.Controllers
{
    public partial class HubOwnerController : Controller
    {

       
        private readonly IHubOwnerService _hubOwnerService;

        public HubOwnerController(IHubOwnerService hubOwnerServiceParam)
        {
            this._hubOwnerService = hubOwnerServiceParam;
        }


        public virtual ActionResult Index()
        {
            return View();
        }


        public ActionResult Read([DataSourceRequest] DataSourceRequest reques)
        {

            return Json(GetHubOwner().ToDataSourceResult(reques));
        }



        private IEnumerable<HubOwnerViewModel> GetHubOwner()
        {
            var result = _hubOwnerService.GetAllHubOwner();
            var viewModelList = new List<HubOwnerViewModel>();
            foreach (var hubOwner in result)
            {
                var ownerViewModel = new HubOwnerViewModel();
                ownerViewModel.Name = hubOwner.Name;
                ownerViewModel.LongName = hubOwner.LongName;
                ownerViewModel.HubOwnerID = hubOwner.HubOwnerID;
                viewModelList.Add(ownerViewModel);
            }

            return viewModelList;
        }

        private HubOwner BindHubOwner(HubOwnerViewModel hubOwnerViewModel)
        {
            if (hubOwnerViewModel == null) return null;
            var hubOwner = new HubOwner()
                               {
                                   Name = hubOwnerViewModel.Name,
                                   LongName = hubOwnerViewModel.LongName,
                                   HubOwnerID = hubOwnerViewModel.HubOwnerID,

                               };
            return hubOwner;
        }
            
            
            [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult HubOwnerCreate([DataSourceRequest] DataSourceRequest request, HubOwnerViewModel hubOwner)
        {
            if (hubOwner != null && ModelState.IsValid)
            {


                _hubOwnerService.AddHubOwner(BindHubOwner(hubOwner));
            }

            return Json(new[] { hubOwner }.ToDataSourceResult(request, ModelState));
        }



            [AcceptVerbs(HttpVerbs.Post)]
            public ActionResult HubOwnerUpdate([DataSourceRequest] DataSourceRequest request, HubOwnerViewModel hubOwner)
            {
                if (hubOwner != null && ModelState.IsValid)
                {
                    var result = _hubOwnerService.FindById(hubOwner.HubOwnerID);
                    if (result!=null)
                    {
                        result.Name = hubOwner.Name;
                        result.LongName = hubOwner.LongName;
                        _hubOwnerService.EditHubOwner(result);
                    }
                }
                return Json(new[] { hubOwner }.ToDataSourceResult(request, ModelState)); 
            }


            [AcceptVerbs(HttpVerbs.Post)]
            public ActionResult HubOwnerDestroy([DataSourceRequest] DataSourceRequest request, HubOwnerViewModel hubOwner)
            {
                if (hubOwner != null && ModelState.IsValid)
                {
                    var result = _hubOwnerService.FindById(hubOwner.HubOwnerID);
                    if (result != null)
                    {
                        _hubOwnerService.DeleteHubOwner(result);
                    }
                }
                return Json(ModelState.ToDataSourceResult());
            }


       

        //
        // GET: /HubOwners/Delete/5

        public virtual ActionResult Delete(int id)
        {
            HubOwner HubOwner = _hubOwnerService.FindById(id);
            _hubOwnerService.DeleteHubOwner(HubOwner);
            return RedirectToAction("Index");
        }

        
    }
}