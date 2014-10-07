using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Services.Common;

namespace Cats.Areas.Report.Controllers
{
    public class SMSController : Controller
    {
        //
        // GET: /Report/SMS/
        private readonly IMessagingService _messagingService;

        public SMSController(IMessagingService messagingServiceParam)
        {
            _messagingService = messagingServiceParam;
        }

        public ActionResult Index()
        {
            ViewBag.address = "251911663223";
            ViewBag.message = "Test From CATS";
            return View();
        }
        public ActionResult SendSMS(string address, string message)
        {
            ViewBag.address = address;
            ViewBag.message = message;
            ViewBag.result = "success";

            try
            {
                _messagingService.SendMessage(message, address);
            }
            catch (Exception e)
            {
                ViewBag.result = "Error";
            }
            return View();
        }
    }
}
