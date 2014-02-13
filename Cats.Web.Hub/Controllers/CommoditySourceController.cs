using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    [Authorize]
    public class CommoditySourceController : BaseController
    {
        private readonly ICommoditySourceService _commoditySourceService;

        public CommoditySourceController(ICommoditySourceService commodityGradeService,IUserProfileService userProfileService):base(userProfileService)
        {
            _commoditySourceService = commodityGradeService;
        }

        //
        // GET: /CommoditySource/

        public ViewResult Index()
        {
            return View(_commoditySourceService.GetAllCommoditySource().ToList());
        }

        public ActionResult Update()
        {
            return PartialView(_commoditySourceService.GetAllCommoditySource().ToList());
        }
        //
        // GET: /CommoditySource/Details/5

        public ViewResult Details(int id)
        {
            var commoditysource = _commoditySourceService.FindById(id);
            return View(commoditysource);
        }

        //
        // GET: /CommoditySource/Create

        public ActionResult Create()
        {
            return PartialView();
        } 

        //
        // POST: /CommoditySource/Create

        [HttpPost]
        public ActionResult Create(CommoditySource commoditysource)
        {
            if (ModelState.IsValid)
            {
                _commoditySourceService.AddCommoditySource(commoditysource);
                return Json(new { success = true }); 
            }

            return PartialView(commoditysource);
        }
        
        //
        // GET: /CommoditySource/Edit/5

        public ActionResult Edit(int id)
        {
            var commoditysource = _commoditySourceService.FindById(id);
            return PartialView(commoditysource);
        }

        //
        // POST: /CommoditySource/Edit/5

        [HttpPost]
        public ActionResult Edit(CommoditySource commoditysource)
        {
            if (ModelState.IsValid)
            {
                _commoditySourceService.EditCommoditySource(commoditysource);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            return PartialView(commoditysource);
        }

        //
        // GET: /CommoditySource/Delete/5

        public ActionResult Delete(int id)
        {
            CommoditySource commoditysource = _commoditySourceService.FindById(id);
            return View(commoditysource);
        }

        //
        // POST: /CommoditySource/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var delCommoditysource = _commoditySourceService.FindById(id);
            if (delCommoditysource != null &&
                (delCommoditysource.Receives.Count == 0))
            {

                _commoditySourceService.DeleteById(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity Source is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return Delete(id);
        }
    }
}