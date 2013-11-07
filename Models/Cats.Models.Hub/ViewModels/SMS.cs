using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using Cats.Models.Hubs.MetaModels;

namespace Cats.Models.Hubs
{
   
    partial class SMS
    {
        public static void SendSMS(int fdpId, string text)
        {
  //          CTSContext db = new CTSContext();
  //          var contacts = (from contact in db.Contacts
  //                          where contact.FDPID == fdpId
  //                          select contact).ToList();
  //          foreach (Contact contact in contacts)
  //          {
  ////              INSERT SMS (InOutInd, MobileNumber, Text, RequestDate, SendAfterDate, Status, StatusDate, Attempts, EventTag)
  ////VALUES ('O', @MobileNumber, @SMSMessage, @Today, @SendAfterDate, 'pending', @Today, 0, 'SEND_SMS')
  //              SMS sms = new Cats.Models.Hubs.SMS();
  //              sms.Attempts = 0;
  //              sms.EventTag = "SEND_SMS";
  //              sms.InOutInd = "O";
  //              sms.LastAttemptDate = null;
  //              sms.MobileNumber = contact.PhoneNo;
  //              sms.Text = text;
  //              sms.RequestDate = DateTime.Today;
  //              sms.SendAfterDate = DateTime.Today;
  //              sms.Status = "pending";
  //              sms.StatusDate = DateTime.Today;

  //              db.SMS.Add(sms);
  //              //try
  //              //{
                    
  //              //}
  //              //catch (Exception e)
  //              //{

  //              //}

  //          }

  //          db.SaveChanges();

        }
    }
}
