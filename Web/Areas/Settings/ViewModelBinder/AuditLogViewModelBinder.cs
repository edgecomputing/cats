

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Areas.Settings.Models.ViewModels;

namespace Cats.Areas.Settings.ViewModelBinder
{
    public class AuditLogViewModelBinder
    {
        public static AuditLogViewModel BindAuditLogViewModel(Audit auditLog)
        {
            return new AuditLogViewModel()
            {
                Action=auditLog.Action,
                AuditID=auditLog.AuditID.ToString(),
                ColumnName=auditLog.ColumnName,
                HubID=auditLog.HubID,
                LogDate=auditLog.DateTime,
                LoginID=auditLog.LoginID,
                NewValue=auditLog.NewValue,
                OldValue = auditLog.OldValue
                ,
                TableName = auditLog.TableName,
                //UserName = users.Find(t=>t.UserProfileID==auditLog.LoginID).UserName




            };
        }

        public static List<AuditLogViewModel> BindListAuditLogViewModel(List<Audit> audits)
        {
            return audits.Select(audit => BindAuditLogViewModel(audit)).ToList();
        }
    }
}