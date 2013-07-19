using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Procurement;
using System.Web.Mvc;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;
using Cats.Procurement.Models;

namespace Cats.Areas.Procurement.Controllers
{
    public class TransporterController : Controller
    {
        private readonly ITransporterService transportService;
        public TransporterController(ITransporterService transportServiceParam)
        {
            this.transportService = transportServiceParam;
        }

        public TransporterPOCO ToPOCO(Cats.Models.Transporter tm)
        {
            TransporterPOCO tp= new TransporterPOCO{TransporterID=tm.TransporterID,Name=tm.Name,Region=tm.Region,Zone=tm.Zone
                                                    ,SubCity=tm.SubCity    
                                                   ,Woreda=tm.Woreda,Kebele=tm.Kebele,HouseNo=tm.HouseNo,TelephoneNo=tm.TelephoneNo
                                                    ,MobileNo=tm.MobileNo,Ownership=tm.Ownership,VehicleCount=tm.VehicleCount,LiftCapacityFrom=tm.LiftCapacityFrom
                                                    ,Capital=tm.Capital, EmployeeCountMale=tm.EmployeeCountMale,EmployeeCountFemale=tm.EmployeeCountFemale
                                                    ,OwnerName=tm.OwnerName, OwnerMobile=tm.OwnerMobile  ,ManagerName=tm.ManagerName
                                                    ,ManagerMobile=tm.ManagerMobile
                                                    //, ExperienceFrom=tm.ExperienceFrom  ,ExperienceTo=tm.ExperienceTo
                                                    };
            
       
            return tp;
            
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

        public ActionResult Index([DataSourceRequest] DataSourceRequest request)
        {
//            IEnumerable<Cats.Models.Transporter> list = (IEnumerable<Cats.Models.Transporter>)transportService.GetAllTransporter().ToDataSourceResult(request);
            IEnumerable<Cats.Models.Transporter> list = (IEnumerable<Cats.Models.Transporter>)transportService.GetAllTransporter();
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
            return RedirectToAction("Edit");
        }
       
               //
               // POST: /Procurement/Default1/Create

               [HttpPost]
               public ActionResult Create(Transporter transporter)
               {
                   if (ModelState.IsValid)
                   {
                       transportService.AddTransporter(transporter);
                       return RedirectToAction("Index");
                   }
                   return RedirectToAction("Edit");
                   //return View(transporter);
               }

               //
               // GET: /Procurement/Default1/Edit/5

               public ActionResult Edit(int id = 0)
                {
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
                     public ActionResult Edit(Transporter transporter)
                     {
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
                             return RedirectToAction("Index");
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

                     /*  */
        protected override void Dispose(bool disposing)
        {
          //  db.Dispose();
            base.Dispose(disposing);
        }
    }
}