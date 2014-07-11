using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Serialization;
using Cats.Models;
using Cats.Services.Administration;
using Cats.Services.Common;
using Cats.Models.ViewModels;
using Newtonsoft.Json;
using System.Net;

//using Cats.Services.EarlyWarning.IFDPService;

namespace Cats.Controllers
{
    public class SMSGatewayController : Controller
    {
        //
        // GET: /CatsSmsGateway/

        //private readonly IFDPService _fdpService;
        private readonly ISMSGatewayService _smsService;
        //private readonly IContactService _contactService;

        public SMSGatewayController(ISMSGatewayService smsGatewayService)
        {
            _smsService = smsGatewayService;
        }

        public JsonResult Index2()
        {
            ////var request = (WebRequest)Request.Url.AbsoluteUri;
            ////HttpContext.Response.AddHeader("Content-Type", "application/json");
            ////Response.AddHeader("Content-Type: application/json");
            ////Request.Headers.Add("Content-Type", "application/json");
            ////HttpContext.Response.AddHeader("Content-Type","application/json");
            ////Response.AddHeader();
            ////Response.AddHeader("Content-Type", "application/json");

            //HttpContext.Response.AddHeader("Content-Type", "text/xml");
            
            //var messages = new List<SmsOutgoingMessage>();
            //var sendevents = new List<SmsEventSend>();
            //var pack = new List<List<SmsEventSend>>();-

            //var messageOne = new SmsOutgoingMessage()
            //    {
            //        id = "4f7c9cea5e11b",
            //        message = "Hello Nati Man! You are selected to be man of the day! CATS",
            //        to = "0911474539",
            //    };

            ////var contacts = _contactService.FindBy(t => t.FDP.Name == );

            //messages.Add(messageOne);

            //var ev = new SmsEventSend()
            //    {
            //        @event = "send",
            //        messages = messages
            //    };

            //sendevents.Add(ev);

            //var result = new { events = sendevents };
            ////return Json(sendevents, "EV", JsonRequestBehavior.AllowGet);
            //return Json(result, JsonRequestBehavior.AllowGet);
            ////return "HI";
            ///*dynamic collectionWrapper = new
            //{
            //      events = sendevents
            //};

            //var output = JsonConvert.SerializeObject(collectionWrapper);
            //var o = JsonConvert.SerializeObject(collectionWrapper, Newtonsoft.Json.Formatting.Indented);
            //string result = o.ToString();
            //return o;
            //*/
            ///*
            //string xml;

            //using (MemoryStream ms = new MemoryStream())
            //{
            //    var sw = new StreamWriter(ms);

            //    var settings = new XmlWriterSettings();
            //    settings.Encoding = Encoding.UTF8;
            //    settings.Indent = true;

            //    //var s = new StringBuilder();
            //    //var setting = new XmlWriterSettings { Encoding = Encoding.UTF8, Indent = true };
            //    XmlWriter writer = XmlWriter.Create(sw, settings);

            //    using (writer)
            //    {

            //        writer.WriteStartDocument();
            //        writer.WriteStartElement("response");

            //        foreach (var eve in sendevents)
            //        {
            //            writer.WriteStartElement("messages");
            //            foreach (var sms in eve.messages)
            //            {
            //                writer.WriteStartElement("sms");

            //                //id="52d7924725bf0" to="251911663223">Hello Yareda CATS wishs you a great lunch!! ENVAYASMS is working now!

            //                // writer.WriteAttributeString("id", sms.id);
            //                writer.WriteAttributeString("to", sms.to);
            //                writer.WriteRaw(sms.message);
            //                writer.WriteEndElement();
            //            }
            //            writer.WriteEndElement();
            //        }

            //        writer.WriteEndElement();
            //        writer.WriteEndDocument();
            //    }

            //    using (StreamReader sr = new StreamReader(ms))
            //    {
            //        ms.Position = 0;
            //        xml = sr.ReadToEnd();
            //        sr.Close();
            //    }
            //}

            //return xml;
            //*/
            ////string re = "<?xml version='1.0' encoding='UTF-8'?><response><messages><sms id="52d7924725bf0" to="251911663223
            ////">Hello Yareda CATS wishs you a great lunch!! ENVAYASMS is working now!</sms></messages></response>";
            return Json(new HRD(), JsonRequestBehavior.AllowGet);
        }


        public bool Send()
        {
            //var messageOne = new SmsOutgoingMessage()
            //{
            //    //id = "4f7c9cea8e17b",
            //    message = "Hello this is the first ever message from the original CATS",
            //    to = "251911474539",
            //};
            //var result = _smsService.SendSMS(messageOne);
            //return result;
            return true;
        }
    }
}