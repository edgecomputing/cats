using System;
using System.Collections.Generic;
using System.Linq;
using Cats.Models;
using Cats.Services.Administration;
using System.Web.Mvc;
using Cats.Web.Adminstration.Models.ViewModels;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
     [Authorize]
    public class ProgramController : Controller
    {

        private IProgramService _programService;

        public ProgramController(IProgramService programService)
        {
            _programService = programService;
        }

        // GET: /Program/

        public ActionResult Index()
        {
            var program=_programService.GetAllProgram();
            return View(program);
        }

        public ActionResult Program_Read([DataSourceRequest] DataSourceRequest request)
        {

            var program = _programService.GetAllProgram();
            var programToDisplay = GetPrograms(program).ToList();
            return Json(programToDisplay.ToDataSourceResult(request));
        }


        private IEnumerable<ProgramViewModel> GetPrograms(IEnumerable<Program> program)
        {
            return (from programs in program
                    select new ProgramViewModel()
                    {
                        ProgramID = programs.ProgramID,
                        ProgramName = programs.Name,
                        Description = programs.Description,
                        LongName = programs.LongName

                    });
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Program_Create([DataSourceRequest] DataSourceRequest request, ProgramViewModel program)
        {
            if (program != null && ModelState.IsValid)
            {


                _programService.AddProgram(BindProgram(program));
            }

            return Json(new[] { program }.ToDataSourceResult(request, ModelState));
        }


        private Program BindProgram(ProgramViewModel model)
        {
            if (model == null) return null;
            var program = new Program()
            {
                ProgramID = model.ProgramID,
                Name = model.ProgramName,
                Description = model.Description,
                LongName = model.LongName
            };
            return program;
        }

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Program_Update([DataSourceRequest] DataSourceRequest request, ProgramViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var origin = _programService.FindById(model.ProgramID);
                origin.Name = model.ProgramName;
                origin.Description = model.Description;
                origin.LongName = model.LongName;
              
                _programService.EditProgram(origin);
            }
            return Json(new[] { model }.ToDataSourceResult(request, ModelState));
        }


        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult Program_Destroy([DataSourceRequest] DataSourceRequest request, ProgramViewModel model)
        {
            if (model != null && ModelState.IsValid)
            {
                var program = _programService.FindById(model.ProgramID);
                _programService.DeleteProgram(program);
            }
            return Json(ModelState.ToDataSourceResult());
        }
        // GET: /Program/Delete/5

        public ActionResult Delete(int id)
        {
            var program = _programService.FindById(id);
            if(program!=null)
            {
                _programService.DeleteProgram(program);
                return RedirectToAction("Index");
            }
            return View();
        }
    
    }
}
