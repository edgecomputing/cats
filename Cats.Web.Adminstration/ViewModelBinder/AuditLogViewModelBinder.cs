

using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Cats.Models;
using Cats.Web.Adminstration.Models.ViewModels;

namespace Cats.Web.Adminstration.ViewModelBinder
{
    public class AuditLogViewModelBinder
    {
        public static AuditLogViewModel BindAuditLogViewModel(Audit auditLog)
        {
            return new AuditLogViewModel()
            {
                Action=auditLog.Action,
                AuditID=auditLog.AuditID,
                ColumnName=auditLog.ColumnName,
                HubID=auditLog.HubID,
                LogDate=auditLog.DateTime,
                LoginID=auditLog.LoginID,
                NewValue=auditLog.NewValue,
                OldValue = auditLog.OldValue
                ,
                TableName = auditLog.TableName,
                




            };
        }

        public static List<AuditLogViewModel> BindListAuditLogViewModel(List<Audit> commodities)
        {
            return commodities.Select(BindAuditLogViewModel).ToList();
        }
    }
}