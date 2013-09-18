using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Common;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Settings.Controllers
{
    public class LogController : Controller
    {
        private readonly ILogReadService _logService;

        public LogController(ILogReadService logService)
        {
            _logService = logService;
        }

        public ActionResult Index()
        {
            return View();
        }

        public ActionResult Log_Read([DataSourceRequest] DataSourceRequest request)
        {
            var list = _logService.Get().OrderByDescending(m => m.Date); ;
            return Json(list.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }
        
    }
}
