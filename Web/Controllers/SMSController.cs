using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Cats.Models;
using Cats.Models.Hubs;
using Cats.Services.Hub;


namespace Cats.Controllers
{
    public class SMSController : ApiController
    {
        private readonly ISMSService _smsService;

        public SMSController(ISMSService smsService)
        {
            _smsService = smsService;
        }

        public HttpResponseMessage Post(SMSRequest request)
        {
            var smsevents = new SMSEvents();

            //var headerparam = request.GetQueryNameValuePairs();

            //if(request.Content.Headers.Contains("action"))
            //{
            //    return null;
            //}

            //var action = headerparam.SingleOrDefault(t => t.Key == "action");

            if (request.action == "test")
            {

            }

            else if (request.action == "outgoing")
            {
                var msgs = _smsService.FindBy(s => s.Status == 1);
                var messages = new List<SmsOutgoingMessage>();

                //var outmsgs = new List<out>
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
                    messages.Add(m);
                }

                var sendevents = new List<SmsEventSend>();
               
                //var messageOne = new SmsOutgoingMessage()
                //{
                //    id = "9y7c9cya5711b",
                //    message = "\"Hello Yareda! You are selected to be man of the day! CATS\"",
                //    to = "0911663223",
                //    priority = null,
                //    type = null
                //};

                //var messageTwo = new SmsOutgoingMessage()
                //{
                //    id = "9c7c9cya5711b",
                //    message = "\"Hello Fish! You are selected to be man of the day again! CATS\"",
                //    to = "0911306248",
                //    priority = null,
                //    type = null
                //};

                //messages.Add(messageOne);
                //messages.Add(messageTwo);

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