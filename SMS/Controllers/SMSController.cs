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

            if (request.action == "test")
            {

            }

            else if (request.action == "outgoing")
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
                            message = msg.Text,
                            to = msg.MobileNumber,
                            priority = null,
                            type = null
                        };

                        var messageOne = new SmsOutgoingMessage()
                        {
                            id = "9y7c9cya5711b",
                            message = "\"Hello Yareda! You are selected to be man of the day! CATS\"",
                            to = "0911663223",
                            priority = null,
                            type = null
                        };

                        messages.Add(messageOne);
                    }
                }
               

                var messageTwo = new SmsOutgoingMessage()
                {
                    id = "9c7c9cya5711b",
                    message = "\"Hello Fish! You are selected to be man of the day again! CATS\"",
                    to = "0911306248",
                    priority = null,
                    type = null
                };

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

            return Request.CreateResponse(HttpStatusCode.OK, smsevents);
        }
    }
}