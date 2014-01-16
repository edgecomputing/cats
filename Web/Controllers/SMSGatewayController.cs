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
            var events = new List<SmsEventSend>();
            
            var messageOne = new SmsOutgoingMessage()
                {
                    id = "4f7c9cea5e11b",
                    message = "Hello this is the first ever message from the original CATS",
                    to = "0911663223",
                };

            messages.Add(messageOne);

            var ev = new SmsEventSend()
                {
                    @event = "send",
                    messages = messages
                };

            
            events.Add(ev);

            //var s =(events= (
            //    from evt in events
            //                 select new SmsEventSend()
            //                     {
            //                         @event = ev.@event,
            //                         messages = ev.messages;
            //                     }
            //      ) 
            //);

            //eventsw.Add(ev);

            return Json(null, JsonRequestBehavior.AllowGet);
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
