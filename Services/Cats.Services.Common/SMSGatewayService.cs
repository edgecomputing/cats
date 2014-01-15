using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using Cats.Models;
using Cats.Models.ViewModels;

namespace Cats.Services.Common
{
    public class SMSGatewayService:ISMSGatewayService
    {
        private const string ACTION_INCOMING = "incoming";
        private const string ACTION_OUTGOING = "outgoing";
        private const string RQUEST_SIGNATURE = "HTTP_X_REQUEST_SIGNATURE";

        public bool SendSMS(Models.ViewModels.SmsOutgoingMessage sms)
        {
            throw new System.NotImplementedException();
        }

        public bool is_validated(string password)
        {
            throw new NotImplementedException();
        }

        public string get_action()
        {
        //    var ACTION="";

        //    if ( HttpContext.Current.Request.Form["action"]==ACTION_OUTGOING)
        //    {
        //       ACTION = ACTION_OUTGOING;
        //    }
        //    else if (HttpContext.Current.Request.Form["action"] == ACTION_INCOMING)
        //    {
        //        ACTION = ACTION_INCOMING;
        //    }
            
            //return ACTION;
            return HttpContext.Current.Request.Headers[RQUEST_SIGNATURE];
        }
    }
}