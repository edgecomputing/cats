using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Services.Procurement;
using Cats.Helpers;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Procurement.Models;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransporterController : Controller
    {
        private readonly ITransporterService transportService;
        private readonly IAdminUnitService _adminUnitService;

        public TransporterController(ITransporterService transportServiceParam, IAdminUnitService adminUnitService)
        {
            this.transportService = transportServiceParam;
            this._adminUnitService = adminUnitService;
        }

        public TransporterPOCO ToPOCO(Transporter tm)
        {
            TransporterPOCO transporterPoco = new TransporterPOCO();
            transporterPoco.TransporterID = tm.TransporterID;
            transporterPoco.Name = tm.Name;
            /*
            transporterPoco.Region = _adminUnitService.FindById(tm.Region).Name;
            transporterPoco.Zone = _adminUnitService.FindById(tm.Zone).Name;
            transporterPoco.Woreda = _adminUnitService.FindById(tm.Woreda).Name;
            */
            transporterPoco.Region = tm.Region.ToString();
            transporterPoco.Zone = tm.Zone.ToString();
            transporterPoco.Woreda = tm.Woreda.ToString();

            transporterPoco.SubCity = tm.SubCity;
            transporterPoco.Kebele = tm.Kebele;
            transporterPoco.HouseNo = tm.HouseNo;
            transporterPoco.TelephoneNo = tm.TelephoneNo;
            transporterPoco.MobileNo = tm.MobileNo;
            transporterPoco.Ownership = tm.Ownership;
            transporterPoco.VehicleCount = tm.VehicleCount;
            transporterPoco.LiftCapacityFrom = tm.LiftCapacityFrom;
            transporterPoco.Capital = tm.Capital;
            transporterPoco.EmployeeCountMale = tm.EmployeeCountMale;
            transporterPoco.EmployeeCountFemale = tm.EmployeeCountFemale;
            transporterPoco.OwnerName = tm.OwnerName;
            transporterPoco.OwnerMobile = tm.OwnerMobile;
            transporterPoco.ManagerName = tm.ManagerName;
            transporterPoco.ManagerMobile = tm.ManagerMobile;

            //TransporterPOCO tp= new TransporterPOCO{
            //                                        TransporterID=tm.TransporterID,
            //                                        Name=tm.Name,
            //                                        Region= _adminUnitService.FindById(tm.Region).Name,
            //                                        Zone=_adminUnitService.FindById(tm.Zone).Name,
            //                                        Woreda=_adminUnitService.FindById(tm.Woreda).Name,
            //                                        SubCity = tm.SubCity,  
            //                                        Kebele=tm.Kebele,
            //                                        HouseNo=tm.HouseNo,
            //                                        TelephoneNo=tm.TelephoneNo,
            //                                        MobileNo = tm.MobileNo,
            //                                        Ownership=tm.Ownership,VehicleCount=tm.VehicleCount,LiftCapacityFrom=tm.LiftCapacityFrom,
            //                                        Capital=tm.Capital, EmployeeCountMale=tm.EmployeeCountMale,EmployeeCountFemale=tm.EmployeeCountFemale,
            //                                        OwnerName=tm.OwnerName, OwnerMobile=tm.OwnerMobile  ,ManagerName=tm.ManagerName,
            //                                        ManagerMobile=tm.ManagerMobile
            //                                        //, ExperienceFrom=tm.ExperienceFrom  ,ExperienceTo=tm.ExperienceTo
            //                                        };
            return transporterPoco;
        }

        public IEnumerable<TransporterPOCO> toPOCOList(IEnumerable<Cats.Models.Transporter> list)
        {
            List<TransporterPOCO> listpoco = new List<TransporterPOCO>();
            foreach (Transporter i in list)
            {
                listpoco.Add(ToPOCO(i));
            }
            return listpoco.ToList();
        }
        public ActionResult Distinct_Regions()
        {
            return Json(transportService.GetAllTransporter().Select(e => e.Region).Distinct(), JsonRequestBehavior.AllowGet);
            //return Json(db.Employees.Select(e => e.Title).Distinct(), JsonRequestBehavior.AllowGet);
        }
        public ActionResult Filter_Name([DataSourceRequest] DataSourceRequest request)
        {
            //JsonRequestBehavior.AllowGet ;
            IEnumerable<TransporterPOCO> listpoco = toPOCOList(transportService.GetAllTransporter());
            return Json(listpoco.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        public ActionResult EditingInline_Read([DataSourceRequest] DataSourceRequest request)
        {
            //JsonRequestBehavior.AllowGet ;
            IEnumerable<TransporterPOCO> listpoco = toPOCOList(transportService.GetAllTransporter());
            return Json(listpoco.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }


        public ActionResult EditingInline_Update([DataSourceRequest] DataSourceRequest request, Transporter transporter)
        {
            transportService.EditTransporter(transporter);
            return Json(ModelState.ToDataSourceResult(), JsonRequestBehavior.AllowGet);
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult EditingInline_Create([DataSourceRequest] DataSourceRequest request, Transporter transporter)
        {
            if (transporter != null && ModelState.IsValid)
            {
                transportService.AddTransporter(transporter);
            }

            return Json(new[] { transporter }.ToDataSourceResult(request, ModelState));
        }


        //
        // GET: /Procurement/Transporter/

        public ActionResult Index(/*[DataSourceRequest] DataSourceRequest request*/)
        {
            //            IEnumerable<Cats.Models.Transporter> list = (IEnumerable<Cats.Models.Transporter>)transportService.GetAllTransporter().ToDataSourceResult(request);
            IEnumerable<Transporter> list = transportService.GetAllTransporter();
            List<TransporterPOCO> listpoco = new List<TransporterPOCO>();
            foreach (Transporter i in list)
            {
                listpoco.Add(ToPOCO(i));
            }
            return View(listpoco);
        }


        //
        // GET: /Procurement/Transporter/Details/5

        public ActionResult Details(int id = 0)
        {
            Transporter transporter = transportService.FindById(id);
            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        //
        // GET: /Procurement/Transporter/Create

        public ActionResult Create()
        {
            //return RedirectToAction("Edit");
            ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            ViewBag.zones = new SelectList(_adminUnitService.FindBy(z => z.AdminUnitTypeID == 3 && z.ParentID == 3), "AdminUnitID", "Name");
            ViewBag.woredas = new SelectList(_adminUnitService.FindBy(w => w.AdminUnitTypeID == 4 && w.ParentID == 19), "AdminUnitID", "Name");
            return View();
        }

        //
        // POST: /Procurement/Default1/Create

        [HttpPost]
        public ActionResult Create(Transporter transporter)
        {
            if (!ModelState.IsValid)
            {
                transportService.AddTransporter(transporter);
            }
            //return RedirectToAction("Edit");
            //return View(transporter);
            return RedirectToAction("Index");
        }

        //
        // GET: /Procurement/Default1/Edit/5

        public ActionResult Edit(int id = 0, int ispartial = 0)
        {
            Transporter transporter;
            if (id == 0)
            {
                transporter = new Transporter();
            }
            else
            {
                transporter = transportService.FindById(id);
                ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
                ViewBag.zones = new SelectList(_adminUnitService.FindBy(z => z.AdminUnitTypeID == 3 && z.ParentID == transporter.Region), "AdminUnitID", "Name");
                ViewBag.woredas = new SelectList(_adminUnitService.FindBy(w => w.AdminUnitTypeID == 4 && w.ParentID == transporter.Zone), "AdminUnitID", "Name");
            }
            if (transporter == null)
            {
                return HttpNotFound();
            }
            if (ispartial == 1)
            {
                return RedirectToAction("EditPartial/" + id);
            }
            return View(transporter);
        }

        public ActionResult EditPartial(int id = 0)
        {

            //ViewBag.Regions = new SelectList(_adminUnitService.FindBy(t => t.AdminUnitTypeID == 2), "AdminUnitID", "Name");
            Transporter transporter;
            if (id == 0)
            {
                transporter = new Transporter();
            }
            else
            {
                transporter = transportService.FindById(id);
            }
            if (transporter == null)
            {
                return HttpNotFound();
            }

            return View(transporter);
        }
        //
        // POST: /Procurement/Default1/Edit/5

        [HttpPost]
        public ActionResult Edit(Transporter transporter, string strExperienceFrom, string strExperienceTo)
        {
            DateTime ExperienceFrom;
            DateTime ExperienceTo;

            /*  try
                {
                    ExperienceFrom = DateTime.Parse(strExperienceFrom);
                    ExperienceTo = DateTime.Parse(strExperienceTo);
                    //OpeningDate = DateTime.Parse(Opening);
                }
                catch (Exception)
                {
                    getGregorianDate EthData = new getGregorianDate();
                    ExperienceFrom = EthData.ReturnGregorianDate(strExperienceFrom);
                    ExperienceTo = EthData.ReturnGregorianDate(strExperienceTo);
                }*/
            if (ModelState.IsValid)
            {
                if (transporter.TransporterID == 0)
                {
                    transportService.AddTransporter(transporter);
                }
                else
                {
                    //   transporter.ExperienceFrom = ExperienceFrom;
                    //   transporter.ExperienceTo = ExperienceTo;
                    transportService.EditTransporter(transporter);
                }
                return RedirectToAction("Index");
            }
            return View(transporter);
        }

        [HttpPost]
        public ActionResult EditPartial(Transporter transporter)
        {
            // return Json(new[] { item }.ToDataSourceResult(request, ModelState));



            if (ModelState.IsValid)
            {
                if (transporter.TransporterID == 0)
                {
                    transportService.AddTransporter(transporter);
                }
                else
                {
                    transportService.EditTransporter(transporter);
                }
                return Json("{}", JsonRequestBehavior.AllowGet);
            }
            return View(transporter);
        }
        //
        // GET: /Procurement/Default1/Delete/5

        public ActionResult Delete(int id = 0)
        {
            Transporter transporter = transportService.FindById(id);
            if (transporter == null)
            {
                return HttpNotFound();
            }
            return View(transporter);
        }

        //
        // POST: /Procurement/Default1/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(int id)
        {
            Transporter transporter = transportService.FindById(id);
            transportService.DeleteTransporter(transporter);
            return RedirectToAction("Index");
        }


        public ActionResult EditingInline_Destroy([DataSourceRequest] DataSourceRequest request, Transporter transporter)
        {
            if (transporter != null)
            {
                transportService.DeleteById(transporter.TransporterID);
                transportService.DeleteTransporter(transporter);
            }

            return Json(ModelState.ToDataSourceResult());
        }

        public JsonResult GetAdminUnits()
        {
            var r = (from region in _adminUnitService.GetRegions()
                     select new
                              {

                                  RegionID = region.AdminUnitID,
                                  RegionName = region.Name,
                                  Zones = from zone in _adminUnitService.GetZones(region.AdminUnitID)
                                          select new
                                              {
                                                  ZoneID = zone.AdminUnitID,
                                                  ZoneName = zone.Name,
                                                  Woredas = from woreda in _adminUnitService.GetWoreda(zone.AdminUnitID)
                                                            select new
                                                            {
                                                                WoredaID = woreda.AdminUnitID,
                                                                WoredaName = woreda.Name
                                                            }
                                              }
                              }
                    );
            return Json(r, JsonRequestBehavior.AllowGet);
        }
    }
}