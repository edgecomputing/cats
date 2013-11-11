using System;
using System.Collections;
using System.Linq;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;


namespace Cats.Areas.Hub.Controllers
{   
    [Authorize]
    public class CommodityController : BaseController
    {
        private readonly ICommodityTypeService _commodityTypeService;
        private readonly ICommodityService _commodityService;

        //
        // GET: /Commodity/
        public CommodityController(ICommodityTypeService commodityTypeService,
            ICommodityService commodityService, 
            IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _commodityTypeService = commodityTypeService;
            _commodityService = commodityService;
        }

        //public CommodityController(ICommodityRepository commodityRepository,
        //    ICommodityTypeRepository commodityTypeRepository)
        //{
        //    this.commodityRepository = commodityRepository;
        //    this.commodityTypeRepository = commodityTypeRepository;
        //}

        public ActionResult Index()
        {
            ViewBag.CommodityTypes = _commodityTypeService.GetAllCommodityType();

            var parents = _commodityService.GetAllParents();
            ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name", 1); 
    
            var firstOrDefault =
                _commodityService.GetAllParents() == null ? null : _commodityService.GetAllParents().FirstOrDefault();
            
            ViewBag.SelectedCommodityID = firstOrDefault != null ? firstOrDefault.CommodityID : 1;
          
            ViewBag.Parents = parents;

            var commReturn = _commodityService.GetAllCommodity() == null ? Enumerable.Empty<Commodity>() : _commodityService.GetAllCommodity().ToList();

            return View(commReturn);
        }
        
        public ActionResult CommodityListPartial()
        {
            // Default to food commodities
            int commodityTypeId = 1;
            if(Request["type"] != null)
            {
                commodityTypeId = Convert.ToInt32(Request["type"]);    
            }
            
            ViewBag.ShowParentCommodity = false;
            var parents = _commodityService.GetAllParents().Where(o=>o.CommodityTypeID == commodityTypeId).OrderBy(o => o.Name);
            var firstOrDefault = parents.FirstOrDefault();

            ViewBag.SelectedCommodityID = firstOrDefault != null ? firstOrDefault.CommodityID : 1;
            
            ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name");
            return PartialView("_CommodityPartial",parents);
        }

        public ActionResult SubCommodityListPartial(int? id)
        {
            if (id == null)
            {
                id = 1;
            }
            ViewBag.ShowParentCommodity = true;
            ViewBag.SelectedCommodityID = id;

            return PartialView("_CommodityPartial",
                                   _commodityService.GetAllSubCommoditiesByParantId(id.Value).OrderBy(o => o.Name));
        }

        public ActionResult GetParentList()
        {
            var parents = from listItem in _commodityService.GetAllParents()
                        select new Commodity
                        {
                            CommodityID = listItem.CommodityID,
                            Name = listItem.Name
                        };
            return Json(new SelectList(parents.OrderBy(o => o.Name), "CommodityID", "Name"), JsonRequestBehavior.AllowGet);
        }

        /**
         * <param>
         * //if param is null (i.e. item.ParentID == null ) it's a parent element /else it's a child
         * </param>
         */

        public ActionResult Update(int? param)
        {
            ViewBag.index = param != null ? 1 : 0;

            var parents = _commodityService.GetAllParents();
            if(param != null)
            {
                //for rendering the subCommodityList accordingly 
                ViewBag.SelectedCommodityID = param; 
                ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name", param);
            }
            else //it's a child commodity
            {
                var firstOrDefault =
                    _commodityService.GetAllParents() == null ? null : _commodityService.GetAllParents().FirstOrDefault();

                ViewBag.SelectedCommodityID = firstOrDefault != null ? firstOrDefault.CommodityID : 1;

                ViewBag.ParentID = new SelectList(parents, "CommodityID", "Name");
            }
            
            ViewBag.Parents = parents;
            return PartialView(_commodityService.GetAllCommodity().OrderBy(o => o.Name));
        }
        //
        // GET: /Commodity/Details/5

        public ViewResult Details(int id)
        {
            Commodity commodity = _commodityService.FindById(id);
            return View("Details",commodity);
        }
        /**
         * <type> indicates the type of commodity to be (parent/child)</type>
         * <Parent> An Optional Nullable value param for preseting the sub Commoditites to be created</Praent>
         */
        //
        // GET: /Commodity/Create

        public ActionResult Create(int type, int? parent)
        {
            //TODO  validation check @ the post server side if the user mischively sent a non-null parent 
            //this check should also be done for the editing part
            
            if (0 == type)
            {
                ViewBag.ParentID = new SelectList(_commodityService.GetAllParents(), "CommodityID", "Name", parent);

                //drop down boxes don't remove thses cos i used them to set a hidden value
                
                var firstOrDefault = _commodityService.GetAllParents().FirstOrDefault(p => p.CommodityID == parent);
                
                if (firstOrDefault != null){
                    ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name",
                                                             firstOrDefault.CommodityTypeID);
                }
                else
                {    
                   //TODO null value validation can be set here later 
                    ViewBag.CommodityTypeID = new SelectList(_commodityTypeService.GetAllCommodityType(), "CommodityTypeID", "Name");
                }

                //disabled text boxes (elias worte this part i think)
                ViewBag.isParent = true;
                
                if (parent != null)
                {
                    var commodity = _commodityService.FindById(parent.Value);
                    if (commodity.CommodityType != null){
                        ViewBag.CommodityType = commodity.CommodityType.Name;
                        ViewBag.ParentCommodity = commodity.Name;
                        ViewBag.SelectedCommodityID = commodity.CommodityID;
                    }
                    else
                    {
                        ViewBag.CommodityType = "";
                        ViewBag.ParentCommodity = "";
                    }
                }
          
            }

            else
            {
                ViewBag.CommodityTypeID = new SelectList(_commodityService.GetAllCommodity(), "CommodityTypeID", "Name");
                ViewBag.isParent = false;
            }
                
            var partialCommodity = new Commodity();
            
            var partialParent = _commodityService.GetAllParents().FirstOrDefault(p => p.CommodityID == parent);
           
            if (partialParent != null)
                {
                    partialCommodity.CommodityTypeID = partialParent.CommodityTypeID;
                }
            
            //TODO either ways it will be null, but we can make this assignment to null/and move it in side else after testing
            //to be sure for preventing hierarchy(tree)
            partialCommodity.ParentID = parent;
            
            return PartialView(partialCommodity);
        }

        //
        // POST: /Commodity/Create

        [HttpPost]
        public ActionResult Create(Commodity commodity)
        {
            if(!_commodityService.IsCodeValid(commodity.CommodityID,commodity.CommodityCode))
            {
                ModelState.AddModelError("CommodityCode",@"Commodity Code should be unique.");
            }
            if (!_commodityService.IsNameValid(commodity.CommodityID, commodity.Name))
            {
                ModelState.AddModelError("Name", @"Commodity Name should be unique.");
            }

            if (ModelState.IsValid)
            {
                _commodityService.AddCommodity(commodity);
                return Json(new { success = true }); 
            }

            Create(commodity.ParentID != null ? 0 : 1, commodity.ParentID);
            return PartialView(commodity);
        }

        //
        // GET: /Commodity/Edit/5

        //TODO  validation check @ the post server side if the user mischively sent a non-null parent 
        //this check should also be done for the creating part

        public ActionResult Edit(int id)
        {
            var commodity = _commodityService.FindById(id);
            var commodities = _commodityService.GetAllCommodity();
            
            //this node is already a parent(i.e. if we can find at least one record with this id as a parent) 
            if (commodity != null && ((_commodityService.GetAllSubCommoditiesByParantId(id).Count() != 0) || commodity.ParentID == null))
            {
                ViewBag.ParentID = new SelectList(commodities.DefaultIfEmpty().Where(c => c.CommodityID == -1), "CommodityID", "Name", commodity.ParentID);
                ViewBag.CommodityTypeID = new SelectList(_commodityService.GetAllCommodity(), "CommodityTypeID", "Name", commodity.CommodityTypeID);
                ViewBag.ShowParentCommodity = false;
                ViewBag.isParent = false;
            }
            //they must be parents with no parents (i.e. parents with value ParentID = null ) 
            // and 
            //they should not be Parents to them selves (i.e. self referencing is not allowed)
            else
            {
                if (commodity != null)
                {
                    ViewBag.ParentID =
                        new SelectList(commodities.Where(c => c.ParentID == null && c.CommodityID != id),
                                       "CommodityID", "Name", commodity.ParentID);
                    ViewBag.CommodityTypeID =
                        new SelectList(_commodityService.GetAllCommodity(), "CommodityTypeID", "Name", commodity.CommodityTypeID);

                    ViewBag.CommodityType = commodity.CommodityType.Name;
                    ViewBag.ParentCommodity = commodity.Commodity2.Name;
                }
                ViewBag.ShowParentCommodity = true;
                ViewBag.isParent = true;
            }
            //ViewBag.CommodityTypeID = new SelectList(db.CommodityTypes, "CommodityTypeID", "Name", commodity.CommodityTypeID);
            return PartialView(commodity);
        }


        public ActionResult ParentCommodities(int commodityTypeID)
        {
            var commodities = from v in _commodityTypeService.FindById(commodityTypeID).Commodities
                              where v.ParentID == null
                              select v;
            commodities = commodities.OrderBy(o => o.Name);
            var cereal = _commodityService.GetCommodityByName("Cereal");
            return Json(new SelectList(commodities, "CommodityID", "Name", cereal.CommodityID));
        }
        //
        // POST: /Commodity/Edit/5

        [HttpPost]
        public ActionResult Edit(Commodity commodity)
        {
            // TODO: move this to a shared helper function.
            if (!_commodityService.IsCodeValid(commodity.CommodityID, commodity.CommodityCode))
            {
                ModelState.AddModelError("CommodityCode", @"Commodity Code should be unique.");
            }
            if (!_commodityService.IsNameValid(commodity.CommodityID, commodity.Name))
            {
                ModelState.AddModelError("Name", @"Commodity Name should be unique.");
            }

            if (ModelState.IsValid)
            {
                _commodityService.EditCommodity(commodity);
                return Json(new { success = true });
            }
            Edit(commodity.CommodityID);
            return PartialView(commodity);
        }

        //
        // GET: /Commodity/Delete/5

        public ActionResult Delete(int id)
        {
            var delCommodity = _commodityService.FindById(id);
            return View("Delete",delCommodity);
        }

        //
        // POST: /Commodity/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            var delCommodity = _commodityService.FindById(id);
            var countOfChildren = _commodityService.GetAllSubCommodities().Count(p => p.ParentID == id); 
            
            if (delCommodity != null &&
                (countOfChildren == 0) &&
                delCommodity.ReceiveDetails.Count == 0 &&
                delCommodity.DispatchDetails.Count == 0 &&
                delCommodity.DispatchAllocations.Count == 0  &&
                delCommodity.GiftCertificateDetails.Count == 0)
            {
                _commodityService.DeleteById(id);
                return RedirectToAction("Index");
            }

            ViewBag.ERROR_MSG = "This Commodity is being referenced, so it can't be deleted";
            ViewBag.ERROR = true;
            return View("Delete", delCommodity); //this.Delete(id);
        }
        public ActionResult CommodityParentListByType(int? commodityTypeID, int? editModeVal)
        {
            if (commodityTypeID != null)
            {
                var comms =
                    _commodityService.GetAllParents().Where(p => p.CommodityTypeID == commodityTypeID).ToList();
                return Json(new SelectList(comms, "CommodityID", "Name", editModeVal), JsonRequestBehavior.AllowGet);
            }
            return Json(new EmptyResult());
        }

        public ActionResult CommodityListByType(int? commodityTypeID, int? editModeVal, string siNumber, int? commoditySourceID)
        {
            var optGroupedList = new ArrayList();
            if (commodityTypeID == null)
            {
                return Json(new EmptyResult());
            }
            var parents = _commodityService.GetAllParents().Where(p => p.CommodityTypeID == commodityTypeID).ToList();

            foreach (var parent in parents) 
            {
                var subCommodities = parent.Commodity1;
                optGroupedList.Add(
                    new {Value = parent.CommodityID, Text = parent.Name, unselectable = false, id = parent.ParentID});

                if (subCommodities == null) continue;
                foreach (var subCommodity in subCommodities)
                {
                    optGroupedList.Add(
                        new
                            {
                                Value = subCommodity.CommodityID,
                                Text = subCommodity.Name,
                                unselectable = true,
                                id = subCommodity.ParentID
                            });
                }
            }
            return Json(optGroupedList, JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsCodeValid(int? commodityID, string commodityCode)
        {
            return Json(_commodityService.IsCodeValid(commodityID, commodityCode),JsonRequestBehavior.AllowGet);
        }

        public JsonResult IsNameValid(int?commodityID, string name)
        {
            return Json(_commodityService.IsNameValid(commodityID, name), JsonRequestBehavior.AllowGet);
        }
    }
}