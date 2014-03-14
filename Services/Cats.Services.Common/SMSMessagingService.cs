using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Net;

namespace Cats.Services.Common
{
    public class SMSMessagingService:IMessagingService
    {
        public bool SendMessage(string message, string address)
        {
            string username = "bekalive@yahoo.com";
            string password = "2qaln";
            string msgsender = "0911222222";
            string url = "http://smsc.vianett.no/v3/send.ashx?src=" + msgsender + "&dst=" + address + "&msg=" + message + "&username=" + username + "&password=" + password;

//            string url = "http://smsc.vianett.no/v3/send.ashx?src=xxxxx&dst=xxxxx&msg=Hello+world&username=xxxxx&password=xxxxx";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();

            response.GetResponseStream();

            return true;

        }
    }
}
