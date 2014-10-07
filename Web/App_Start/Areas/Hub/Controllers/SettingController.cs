using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Areas.Hub.Controllers
{
     [Authorize]
    public class SettingController : BaseController
    {
        // CTSContext context = new CTSContext();
         private readonly ISettingService _settingService;

         public SettingController(ISettingService settingService, IUserProfileService userProfileService)
             : base(userProfileService)
         {
             this._settingService = settingService;
         }

        Setting syssetting = new Setting();
        //
        // GET: /Setting/


        public ActionResult SysSettings()
        {
            SMTPInfo smtp_info = new SMTPInfo();

            // Retrieve the perviously saved Profile
            if (Session["SMTPInfo"] != null)
                smtp_info = Session["SMTPInfo"] as SMTPInfo;
            else
            {
                //CTSContext context = new CTSContext();
                Setting setting = new Setting();

                setting = syssetting.GetSetting("SMTPServer");
                if (setting != null)
                    smtp_info.SMTPServer = setting.Value;

                setting = syssetting.GetSetting("SMTPPort");
                if (setting != null)
                    smtp_info.Port = int.Parse(setting.Value);

                setting = syssetting.GetSetting("SMTPName");
                if (setting != null)
                    smtp_info.Name = setting.Value;

                Session["SMTPInfo"] = smtp_info;
            }
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            return View(smtp_info);
        }

        public ActionResult SysSettingsUpdate()
        {
            SMTPInfo smtp_info = new SMTPInfo();

            // Retrieve the perviously saved Profile
            if (Session["SMTPInfo"] != null)
                smtp_info = Session["SMTPInfo"] as SMTPInfo;            
            
            //CTSContext context = new CTSContext();
            Setting setting = new Setting();

            setting = syssetting.GetSetting("SMTPServer");
            if (setting != null)
                smtp_info.SMTPServer = setting.Value;

            setting = syssetting.GetSetting("SMTPPort");
            if (setting != null)
                smtp_info.Port = int.Parse(setting.Value);

            setting = syssetting.GetSetting("SMTPName");
            if (setting != null)
                smtp_info.Name = setting.Value;

            Session["SMTPInfo"] = smtp_info;
                
            
            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            //return  PartialView(smtp_info);
            return Json(smtp_info);
        }

        public ActionResult EditConfigSMTP()
        {
            SMTPInfo smtp_info = new SMTPInfo();

            // Retrieve the perviously saved Profile
            if (Session["SMTPInfo"] != null)
                smtp_info = Session["SMTPInfo"] as SMTPInfo;
            else
                smtp_info.Port = 25;

            this.Response.Cache.SetExpires(DateTime.UtcNow.AddMinutes(-1));
            this.Response.Cache.SetCacheability(HttpCacheability.NoCache);
            this.Response.Cache.SetNoStore();

            return PartialView(smtp_info);
        }

        [HttpPost]
        public ActionResult EditConfigSMTP(SMTPInfo smtp_info)
        {
            // If the ModelState is invalid then return
            // a PartialView passing in the Profile object
            // with the ModelState errors
            if (!ModelState.IsValid)
                return PartialView("SysSettings", smtp_info);

            // Store the Profile object and return
            // a Json result indicating the Profile 
            // has been saved
            Session["SMTPInfo"] = smtp_info;

            //context.Connection.Open();

            Setting setting = new Setting();

            if (_settingService.FindBy(m => m.Key == "SMTPServer").FirstOrDefault() != null)
            {

                setting.Key = "SMTPServer";
                setting.Value = smtp_info.SMTPServer;
                setting.Type = "String";
                setting.Option = "";
                setting.Category = "SMTP Config";

                syssetting.EditSetting(setting);

                setting.Key = "SMTPPort";
                setting.Value = smtp_info.Port.ToString();
                setting.Type = "Int";

                syssetting.EditSetting(setting);

                setting.Key = "SMTPName";
                setting.Value = smtp_info.Name;
                setting.Type = "String";

                syssetting.EditSetting(setting);
            }
            else
            {
                setting.Key = "SMTPServer";
                setting.Value = smtp_info.SMTPServer;
                setting.Type = "String";
                setting.Option = "";
                setting.Category = "SMTP Config";

                syssetting.AddSetting(setting);

                setting.Key = "SMTPPort";
                setting.Value = smtp_info.Port.ToString();
                setting.Type = "Int";

                syssetting.AddSetting(setting);

                setting.Key = "SMTPName";
                setting.Value = smtp_info.Name;
                setting.Type = "String";

                syssetting.AddSetting(setting);
            }
            //if (context.MailServerSettings.Count() == 0)
            //{
            //    MailServerSetting m = new MailServerSetting();
            //    m.Name = smtp_info.Name;
            //    m.Port = smtp_info.Port;
            //    m.SMTPServer = smtp_info.SMTPServer;
            //    context.MailServerSettings.AddObject(m);
            //}
            //else
            //{
            //    MailServerSetting m = context.MailServerSettings.First();
            //    m.Name = smtp_info.Name;
            //    m.Port = smtp_info.Port;
            //    m.SMTPServer = smtp_info.SMTPServer;               

            //}            

            //context.SaveChanges();

            return Json(new { success = true });
        }
    }
}
