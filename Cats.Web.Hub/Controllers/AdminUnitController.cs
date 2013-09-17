using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Web.Hub.Infrastructure;
using Telerik.Web.Mvc;
using Cats.Models.Hub;
using Telerik.Web.Mvc.UI;

namespace Cats.Web.Hub.Controllers
{

    public class AdminUnitController : BaseController
    {
        private readonly IAdminUnitService _adminUnitService;
        private readonly IUserProfileService _userProfileService;
        private readonly IDispatchAllocationService _dispatchAllocationService;
        
        public AdminUnitController(IAdminUnitService adminUnitService, IUserProfileService userProfileService,
            IDispatchAllocationService dispatchAllocationService)
        {
            _adminUnitService = adminUnitService;
            _userProfileService = userProfileService;
            _dispatchAllocationService = dispatchAllocationService;
        }

        [Authorize]
        public ActionResult Index()
        {
            var types = _adminUnitService.GetAdminUnitTypes();
            return View("Index", types);
        }

        [Authorize]
        public ActionResult AdminUnits(int? id)
        {
            if (id == null)
            {
                return new EmptyResult();
            }
            ViewBag.Regions = _adminUnitService.GetRegions();
            var type = _adminUnitService.GetAdminUnitType(id.Value);
            ViewBag.Title = type.Name + "s";
            ViewBag.SelectedTypeId = id;
            var list = type.AdminUnits.OrderBy(a => a.Name);
            //.Select(s => new Models.AdminUnitItem()
            //{ Id = s.AdminUnitID, Name = s.Name});
            switch (id)
            {
                case 3:
                    list = type.AdminUnits.OrderBy(a => a.AdminUnit2.Name).ThenBy(a => a.Name);
                    break;
                case 4:
                    list = type.AdminUnits.OrderBy(a => a.AdminUnit2.AdminUnit2.Name).ThenBy(a => a.AdminUnit2.Name).ThenBy(a => a.Name);
                    break;
            }
            var viewName = "Lists/AdminUnits." + id + "";
            return PartialView(viewName, list);
        }

        //
        // GET: /AdminUnit/Create
        [Authorize]
        public ActionResult Create(int typeid)
        {
            var model = new AdminUnitModel();
            switch (typeid)
            {
                case 2:
                    model.SelectedAdminUnitTypeId = Infrastructure.Configuration.RegionTypeId;
                    return PartialView("CreateRegion", model);
                case 3:
                    model.SelectedAdminUnitTypeId = Infrastructure.Configuration.ZoneTypeId;
                    return PartialView("CreateZone", model);
                case 4:
                    model.SelectedAdminUnitTypeId = Infrastructure.Configuration.WoredaTypeId;
                    return View("CreateWoreda", model);
                default:
                    model.SelectedAdminUnitTypeId = Infrastructure.Configuration.RegionTypeId;
                    return PartialView("CreateRegion", model);
            }

        }

        [Authorize]
        public ActionResult CreateRegion()
        {
            var model = new AdminUnitModel { SelectedAdminUnitTypeId = Infrastructure.Configuration.RegionTypeId };
            return PartialView("CreateRegion", model);
        }

        [Authorize]
        public ActionResult CreateZone(int? regionId)
        {
            var model = new AdminUnitModel();
            if (regionId.HasValue)
            {
                var region = _adminUnitService.FindById(regionId.Value);
                model.SelectedRegionId = region.AdminUnitID;
                model.SelectedRegionName = region.Name;
            }
            model.SelectedAdminUnitTypeId = Infrastructure.Configuration.ZoneTypeId;
            return PartialView("CreateZone", model);
        }

        public ActionResult CreateWoreda(int? zoneId)
        {
            var model = new AdminUnitModel { SelectedAdminUnitTypeId = Infrastructure.Configuration.WoredaTypeId };

            if (zoneId.HasValue)
            {
                var zone = _adminUnitService.FindById(zoneId.Value);
                model.SelectedZoneName = zone.Name;
                model.SelectedZoneId = zone.AdminUnitID;
                model.SelectedRegionId = zone.AdminUnit2.AdminUnitID;
                model.SelectedRegionName = zone.AdminUnit2.Name;
            }
            return PartialView("CreateWoreda", model);
        }

        //
        // POST: /AdminUnit/Create

        [HttpPost]
        public ActionResult Create(AdminUnitModel unit)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var aunit = new AdminUnit {AdminUnitTypeID = unit.SelectedAdminUnitTypeId};
                    if (aunit.AdminUnitTypeID == Infrastructure.Configuration.ZoneTypeId)
                    {
                        aunit.ParentID = unit.SelectedRegionId;
                    }
                    else if (aunit.AdminUnitTypeID == Infrastructure.Configuration.WoredaTypeId)
                    {
                        aunit.ParentID = unit.SelectedZoneId;
                    }
                    aunit.Name = unit.UnitName;
                    aunit.NameAM = unit.UnitNameAM;

                    _adminUnitService.AddAdminUnit(aunit);
                    return Json(new {success = true});
                }
                catch (Exception)
                {
                    return View();
                }
            }
            return View("Create");
        }

        //
        // GET: /AdminUnit/Edit/5

        public ActionResult Edit(int id)
        {
            var unit = _adminUnitService.FindById(id);
            return PartialView("Edit", unit);
        }

        //
        // POST: /AdminUnit/Edit/5

        [HttpPost]
        public ActionResult Edit(int id, AdminUnit unit)
        {

            if (ModelState.IsValid)
            {
                _adminUnitService.EditAdminUnit(unit);
                return Json(new {success = true});
            }
            return PartialView("Edit", unit);
        }

        //
        // GET: /AdminUnit/Delete/5

        public ActionResult Delete(int id)
        {
            var unit = _adminUnitService.FindById(id);
            return View("Delete", unit);
        }

        //
        // POST: /AdminUnit/Delete/5

        [HttpPost]
        public ActionResult Delete(int id, AdminUnit unit)
        {
            try
            {
                _adminUnitService.DeleteAdminUnit(unit);
                return RedirectToAction("Index");
            }
            catch
            {
                return View("Delete");
            }
        }

        public ActionResult GetRegions()
        {
            var units = from item in _adminUnitService.GetRegions()
                        select new AdminUnitItem
                                   {
                                       Id = item.AdminUnitID,
                                       Name = item.Name
                                   };
            return Json(new SelectList(units.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChildren(int? unitId)
        {
            if (!unitId.HasValue)
            {
                return Json(new SelectList(new List<AdminUnitItem>(), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            var units = from item in _adminUnitService.GetChildren(unitId.Value)
                        select new AdminUnitItem
                            {
                                Id = item.AdminUnitID,
                                Name = item.Name
                            };
            return Json(new SelectList(units.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetChildrenReport(int? unitId)
        {
            if (unitId.HasValue)
            {
                var units = (from item in _adminUnitService.GetChildren(unitId.Value)
                             select new AdminUnitItem
                                        {
                                            Id = item.AdminUnitID,
                                            Name = item.Name
                                        }).ToList();
                return Json(new SelectList(units.OrderBy(o => o.Name), "Id", "Name"), JsonRequestBehavior.AllowGet);
            }
            return Json(new SelectList(new List<AdminUnitItem>(), "Id", "Name"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetZones(int? selectedRegionId)
        {
            if (selectedRegionId == null)
            {
                //
                if (Request["SelectedRegionId2"] != null)
                {
                    selectedRegionId = Convert.ToInt32(Request["SelectedRegionId2"]);
                }
                else if (Request["RegionID"] != null)
                {
                    selectedRegionId = Convert.ToInt32(Request["RegionID"]);
                }
            }
            return GetChildren(selectedRegionId);
        }


        public ActionResult GetWoredas(int? selectedZoneId)
        {
            if (selectedZoneId == null)
            {
                if (Request["SelectedZoneId2"] != null)
                {
                    selectedZoneId = Convert.ToInt32(Request["SelectedZoneId2"]);
                }
                else if (Request["ZoneID"] != null)
                {
                    selectedZoneId = Convert.ToInt32(Request["ZoneID"]);
                }
            }
            return GetChildren(selectedZoneId);
        }

        [GridAction]
        public ActionResult GetAdminUnitsByParent(int? parentId)
        {
            if (parentId.HasValue)
            {
                var units = from item in _adminUnitService.GetChildren(parentId.Value)
                            orderby item.Name
                            select new
                            {
                                item.AdminUnitID, item.Name,
                                AdminUnit2 = new
                                    {
                                        item.AdminUnit2.Name,
                                        AdminUnit2 = new
                                            {
                                                item.AdminUnit2.AdminUnit2.Name
                                            }
                                    },
                            };
                return View(new GridModel(units));
            }
            var woredas = from item in _adminUnitService.GetAllWoredas()
                          orderby item.Name
                          select new
                            {
                                item.AdminUnitID, item.Name,
                                AdminUnit2 = new
                                    {
                                        item.AdminUnit2.Name,
                                        AdminUnit2 = new
                                            {
                                                item.AdminUnit2.AdminUnit2.Name
                                            }
                                    },
                            };
            return View(new GridModel(woredas));

        }

        [GridAction]
        public ActionResult GetWoredasByParent(int? regionId, int? zoneId)
        {
            List<AdminUnit> units = null;
            if (zoneId.HasValue)
            {
                units = _adminUnitService.GetChildren(zoneId.Value);

            }
            else if (regionId.HasValue)
            {
                units = _adminUnitService.GetWoredasByRegion(regionId.Value);
            }

            if (units != null)
            {

                var woredas = from item in units
                              select new
                                {
                                    item.AdminUnitID, item.Name,
                                    AdminUnit2 = new
                                        {
                                            item.AdminUnit2.Name,
                                            AdminUnit2 = new
                                                {
                                                    item.AdminUnit2.AdminUnit2.Name
                                                }
                                        },
                                };
                return View(new GridModel(woredas));
            }
            return View(new GridModel(new List<object>()));

        }

        [GridAction]
        public ActionResult GetZonesByParent(int? regionId)
        {
            var units = new List<AdminUnit>();

            if (regionId.HasValue)
            {
                units = _adminUnitService.GetChildren(regionId.Value);
            }

            var woredas = from item in units
                          select new
                            {
                                item.AdminUnitID, item.Name,
                                AdminUnit2 = new
                                {
                                    item.AdminUnit2.Name,
                                },
                            };

            return View(new GridModel(woredas));
        }

        public ActionResult GetTreeElts(TreeViewItem node, bool? closedToo)
        {
            var user = _userProfileService.GetUser(User.Identity.Name);

            var parentId = !string.IsNullOrEmpty(node.Value) ? (int?) Convert.ToInt32(node.Value) : null;

            if (parentId == null)
            {
                // TODO: Here implement a null argument exception
                return null;
            }
            var thelist = _adminUnitService.GetTreeElts(parentId.Value, user.DefaultHub.HubID);
            IEnumerable nodes = from item in thelist
                                // where item.ParentID == parentId || (parentId == null && item.ParentID == null)
                                group item by new {item.Value, item.Name, item.LoadOnDemand}
                                into itm
                                select new TreeViewItemModel
                                    {
                                        Text = itm.Key.Name + "( " + itm.Sum(l => l.Count) + " )",
                                        //item.Name g.Sum(b => b.QuantityInMT)
                                        Value = itm.Key.Value.ToString(CultureInfo.InvariantCulture),
                                        LoadOnDemand = true,
                                        //itm.Key.LoadOnDemand,
                                        Enabled = true
                                    };

            return new JsonResult {Data = nodes};
        }

        private int CountAllocationsUnder(int adminUnitId)
        {

            var user = _userProfileService.GetUser(User.Identity.Name);
            var unclosed = (from dAll in _dispatchAllocationService.GetAllDispatchAllocation()
                            where dAll.ShippingInstructionID.HasValue && dAll.ProjectCodeID.HasValue
                                  && user.DefaultHub.HubID == dAll.HubID && dAll.IsClosed == false
                            select dAll);

            var adminUnit = _adminUnitService.FindById(adminUnitId);

            switch (adminUnit.AdminUnitType.AdminUnitTypeID)
            {
                case 2:
                    return
                        unclosed.Count(p => p.FDP.AdminUnit.AdminUnit2.AdminUnit2.AdminUnitID == adminUnit.AdminUnitID);
                case 3:
                    return
                        unclosed.Count(p => p.FDP.AdminUnit.AdminUnit2.AdminUnitID == adminUnit.AdminUnitID);
                case 4:
                    return
                        unclosed.Count(p => p.FDP.AdminUnit.AdminUnitID == adminUnit.AdminUnitID);
                default:
                    return 0;
            }
        }

        public ActionResult GetZonesReport(int? areaId)
        {
            return Json(areaId.HasValue ? new SelectList(_adminUnitService.GetZonesForReport(areaId.Value), "AreaId", "AreaName") : 
                new SelectList(Enumerable.Empty<SelectListItem>(), "AreaId", "AreaName"), JsonRequestBehavior.AllowGet);
        }

        public ActionResult GetWoredasReport(int? zoneId)
        {
            return Json(zoneId.HasValue ? new SelectList(_adminUnitService.GetWoredasForReport(zoneId.Value), "AreaId", "AreaName") : 
                new SelectList(Enumerable.Empty<SelectListItem>(), "AreaId", "AreaName"), JsonRequestBehavior.AllowGet);
        }
    }
}
