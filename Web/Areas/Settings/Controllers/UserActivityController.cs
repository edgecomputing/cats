using System;
using System.Linq;
using System.Web.Mvc;
using Cats.Areas.Settings.Models;
using Cats.Services.Administration;

using Kendo.Mvc.Extensions;
using Kendo.Mvc.UI;

namespace Cats.Areas.Settings.Controllers
{
    [Authorize]
    public class UserActivityController : Controller
    {

        #region Field

        private readonly IUserActivityService _userActivityService;
        private readonly IUserProfileService _userProfileService;
        
        #endregion

        #region Ctor

        public UserActivityController(IUserActivityService userActivityService, IUserProfileService userProfileService)
        {
            _userActivityService = userActivityService;
            _userProfileService = userProfileService;
        }
        #endregion

        #region ActionMethod

        public ActionResult Index()
        {

            return View();
        }
        #endregion

        #region Method


        public ActionResult UserActivity_Read([DataSourceRequest]DataSourceRequest request)
        {
            
            var userActivities = _userActivityService.GetAllUserActivity();

            var oUserActivityViewModel = from userActivity in userActivities
                                             select new UserActivityViewModel()
                                             {
                                                    Action = CheckActionName(userActivity.Action),
                                                    AuditID = userActivity.AuditID.ToString(),
                                                    ColumnName = userActivity.ColumnName,
                                                    HubID = userActivity.HubID,
                                                    LogDate = userActivity.DateTime.ToString(),
                                                    UserName = _userProfileService.FindBy(o => o.UserProfileID == userActivity.LoginID).Any() ?  _userProfileService.FindBy(o => o.UserProfileID == userActivity.LoginID).FirstOrDefault().UserName:string.Empty,
                                                    NewValue = userActivity.NewValue,
                                                    OldValue = userActivity.OldValue,
                                                    TableName = userActivity.TableName,
                                            };

            return Json(oUserActivityViewModel.ToDataSourceResult(request), JsonRequestBehavior.AllowGet);

        }

        private string CheckActionName(string actionAbberviation)
        {
            string actionName = string.Empty;

            switch (actionAbberviation)
            {
                case "A":
                    actionName = "Added";
                    break;
                case "D":
                      actionName = "Deleted";
                    break;
                case "M":
                    actionName = "Modified";
                    break;

            }
            return actionName;

        }

        #endregion

    }
}
