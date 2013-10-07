using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;

using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers
{
    [Authorize]
    public class CommodityGradeController : BaseController
    {
        private readonly ICommodityGradeService _commodityGradeService;

        public CommodityGradeController(ICommodityGradeService commodityGradeService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _commodityGradeService = commodityGradeService;
        }

        //
        // GET: /CommodityGrade/

        public ViewResult Index()
        {
            return View("Index", _commodityGradeService.GetAllCommodityGrade().ToList());
        }

        public ActionResult Update()
        {
            return PartialView(_commodityGradeService.GetAllCommodityGrade().ToList());
        }

        //
        // GET: /CommodityGrade/Details/5

        public ViewResult Details(int id)
        {
            CommodityGrade commoditygrade = _commodityGradeService.FindById(id);
            return View(commoditygrade);
        }

        //
        // GET: /CommodityGrade/Create

        public ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /CommodityGrade/Create

        [HttpPost]
        public ActionResult Create(CommodityGrade commoditygrade)
        {
            if (ModelState.IsValid)
            {
                _commodityGradeService.AddCommodityGrade(commoditygrade);
                return Json(new { success = true }); 
            }

            return PartialView(commoditygrade);
        }

        //
        // GET: /CommodityGrade/Edit/5

        public ActionResult Edit(int id)
        {
            var commoditygrade = _commodityGradeService.FindById(id);
            return PartialView(commoditygrade);
        }

        //
        // POST: /CommodityGrade/Edit/5

        [HttpPost]
        public ActionResult Edit(CommodityGrade commoditygrade)
        {
            if (ModelState.IsValid)
            {
                _commodityGradeService.EditCommodityGrade(commoditygrade);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            return PartialView(commoditygrade);
        }

        //
        // GET: /CommodityGrade/Delete/5

        public ActionResult Delete(int id)
        {
            CommodityGrade commoditygrade = _commodityGradeService.FindById(id);
            return View(commoditygrade);
        }

        //
        // POST: /CommodityGrade/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            CommodityGrade delCommodityGrade = _commodityGradeService.FindById(id);
            if (delCommodityGrade != null )
            {
                _commodityGradeService.DeleteById(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity Grade is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return this.Delete(id);
        }

    }
}