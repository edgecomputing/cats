using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Administration;
using Cats.Services.Security;
using Cats.Web.Adminstration.Models.ViewModels;
using Cats.Web.Adminstration.ViewModelBinder;
using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Web.Adminstration.Controllers
{
    public class AuditLogController : Controller
    {
        //
        // GET: /AuditLog/

        private readonly IAuditService _auditLogService;
      

        public AuditLogController(IAuditService auditLogService)
        {
            _auditLogService = auditLogService;
          
        }

        public ActionResult Index()
        {
            return View();
        }
         public ActionResult AuditLog_Read([DataSourceRequest]DataSourceRequest request)
        {
            var auditLoges = _auditLogService.GetAllAudit();
          
            var auditLogsViewModel = AuditLogViewModelBinder.BindListAuditLogViewModel(auditLoges);
            return Json(auditLogsViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);
        }

        

        [AcceptVerbs(HttpVerbs.Post)]
        public ActionResult AuditLog_Destroy([DataSourceRequest] DataSourceRequest request,
                                                  AuditLogViewModel auditLogViewModel)
        {
            if (auditLogViewModel != null)
            {
                Guid id;
                Guid.TryParse(auditLogViewModel.AuditID, out id);
                _auditLogService.DeleteById(id);
            }

            return Json(ModelState.ToDataSourceResult());
        }
        
  


  


    }
}
