using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cats.Models;
using Cats.Models.Hubs;
using Cats.Services.Hub;

namespace SMS.Controllers
{
    public class SMSController : ApiController
    {
        //private readonly  ;

        static readonly ISMSService SMSService = new SMSService();

        //public SMSController()
        //{

        //}

        //public SMSController(ISMSService smsService)
        //{
        //    //_smsService = smsService;
        //}

        public HttpResponseMessage Post(SMSRequest request)
        {
            var smsevents = new SMSEvents();

            switch (request.action)
            {
                case "test":
                    break;
                case "outgoing":
                    {
                        var msgs = SMSService.FindBy(s => s.Status == 1);
                        var messages = new List<SmsOutgoingMessage>();

                        if (msgs.Count>0)
                        {
                            foreach (var msg in msgs)
                            {
                                var m = new SmsOutgoingMessage()
                                    {
                                        id = msg.SMSID.ToString(),
                                        message = "\"" + msg.Text + "\"",
                                        to = msg.MobileNumber,
                                        priority = null,
                                        type = null
                                    };

                                messages.Add(m);
                            }
                        }

                        // messages.Add(messageTwo);
                        var sendevents = new List<SmsEventSend>();

                        var ev = new SmsEventSend()
                            {
                                @event = "send",
                                messages = messages
                            };
                        sendevents.Add(ev);
                        smsevents.events = sendevents;

                        return Request.CreateResponse(HttpStatusCode.OK, smsevents);
                    }
                case "send_status":
                    {
                        if (request.status == "sent")
                        {
                            var id = Convert.ToInt32(request.id);
                            var msg = SMSService.FindById(id);
                            msg.Status = 2;
                            SMSService.EditSMS(msg);
                        }
                        break;
                    }
            }

            return Request.CreateResponse(HttpStatusCode.OK, smsevents);
        }
    }
}