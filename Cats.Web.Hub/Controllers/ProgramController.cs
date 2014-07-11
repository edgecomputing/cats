using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class ProgramController : BaseController
    {

        private IProgramService _programService;

        public ProgramController(IProgramService programService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            _programService = programService;

        }

        public virtual ViewResult Index()
        {
            var programs = _programService.GetAllProgram();
            return View(programs);
        }


        public virtual ActionResult ListPartial()
        {
            var programs = _programService.GetAllProgram();
            return PartialView(programs);
        }
        //
        // GET: /Program/Details/5

        public virtual ViewResult Details(int id)
        {
            Program program = _programService.FindById(id);
            return View(program);
        }

        //
        // GET: /Program/Create

        public virtual ActionResult Create()
        {
            return PartialView();
        }

        //
        // POST: /Program/Create

        [HttpPost]
        public virtual ActionResult Create(Program program)
        {
            if (ModelState.IsValid)
            {
                _programService.AddProgram(program);
                return Json(new { success = true }); 
            }

            return PartialView(program);
        }

        //
        // GET: /Program/Edit/5

        public virtual ActionResult Edit(int id)
        {
            Program program = _programService.FindById(id);
            return PartialView(program);
        }

        //
        // POST: /Program/Edit/5

        [HttpPost]
        public virtual ActionResult Edit(Program program)
        {
            if (ModelState.IsValid)
            {
                _programService.AddProgram(program);
                return Json(new { success = true });
                //return RedirectToAction("Index");
            }
            return PartialView(program);
        }

        //
        // GET: /Program/Delete/5

        public virtual ActionResult Delete(int id)
        {
            Program program = _programService.FindById(id);
            return View(program);
        }

        //
        // POST: /Program/Delete/5

        [HttpPost, ActionName("Delete")]
        public virtual ActionResult DeleteConfirmed(int id)
        {
            _programService.DeleteById(id);
            return RedirectToAction("Index");
        }
    }
}