using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models;
using Cats.Services.EarlyWarning;
using Cats.Models.ViewModels;

namespace Cats.Controllers
{
    public class SMSGatewayController : Controller
    {
        //
        // GET: /CatsSmsGateway/

        private readonly IFDPService _fdpService;

        public SMSGatewayController(IFDPService fdpService)
        {
            _fdpService = fdpService;
        }

        public JsonResult Index()
        {
            var messages = new List<SmsOutgoingMessage>();
            
            var messageOne = new SmsOutgoingMessage()
                {
                    Id = new Guid(),
                    Message = "Hello this is the first ever message from the original CATS",
                    Priority = 5,
                    To = "0911663223",
                    Type = "OutGoing"
                };

            messages.Add(messageOne);

            return Json(messages, JsonRequestBehavior.AllowGet);
        }

        public JsonResult Send()
        {
            var fdps = _fdpService.GetAllFDP().Take(2);
            var hh = (from fdp in fdps
                      select new
                          {
                              fdp.Name,
                              fdp.NameAM
                          }
                     );
            return Json(hh, JsonRequestBehavior.AllowGet);
        }

    }
}
