using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hub;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers
{
    public class AuditController : BaseController
    {

        private readonly IAuditService _auditService;


        public AuditController(IAuditService auditService, IUserProfileService userProfileService)
            : base(userProfileService)
        {
            this._auditService = auditService;
        }

       

        //
        // GET: /Audit/

        //public ActionResult Audits(int id, string tableName, string fieldName)
        //{
        //    List<FieldChange> changes = Audit.GetChanges(id, tableName, fieldName);
        //    return PartialView("FieldAudits", changes);
        //}
        //TODO : Add a role here to the authorization
        [Authorize]
        public ActionResult Audits(string id, string tableName, string fieldName, string foreignTable, string foreignFeildName,string foreignFeildKey)
        {
            List<FieldChange> changes = _auditService.GetChanges(tableName, fieldName, foreignTable, foreignFeildName, foreignFeildKey, id.ToString());
            return PartialView("FieldAudits", changes);
        }
    }
}
