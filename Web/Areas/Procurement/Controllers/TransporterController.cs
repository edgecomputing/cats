using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.Procurement;
namespace Cats.Areas.Procurement.Controllers
{
    public class TransporterController : Controller
    {
        private readonly ITransporterService transportService;
        public TransporterController(ITransporterService transportServiceParam)
        {
            this.transportService = transportServiceParam;
        }
       
        //
        // GET: /Procurement/Transporter/

        public ActionResult Index()
        {

            return View(transportService.GetAllTransporter());
            //return View(GetAllTransporter());
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
        // GET: /Procurement/Default1/Create

        public ActionResult Create()
        {
            return View();
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

                   return View(transporter);
               }

               //
               // GET: /Procurement/Default1/Edit/5

               public ActionResult Edit(int id = 0)
                {
                    Transporter transporter = transportService.FindById(id);
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
                           
                             transportService.EditTransporter(transporter);
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
                     /*  */
        protected override void Dispose(bool disposing)
        {
          //  db.Dispose();
            base.Dispose(disposing);
        }
    }
}