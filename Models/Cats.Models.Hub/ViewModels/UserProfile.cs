using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace Cats.Models.Hubs
{
    
    partial class UserProfile
    {
        //TODO:Implement in service
        public void ChangeLanguage(string lang)
        {
            //CTSContext context = new CTSContext();         

            //UserProfile profile = context.UserProfiles.Where(p=>p.UserName == this.UserName).SingleOrDefault();
            //if(profile != null)
            //{
            //    profile.LanguageCode = lang;
            //    context.SaveChanges();
            //}
        }

        public void ChangeHub(int warehouseId)
        {
            //CTSContext entities = new CTSContext();
            //var newDefault = (from w in entities.UserHubs
            //                  where w.HubID == warehouseId && w.UserProfileID == this.UserProfileID
            //                  select w).Single();
            //var prevDefaults = (from w in entities.UserHubs
            //                    where w.HubID != warehouseId && w.UserProfileID == this.UserProfileID
            //                    && w.IsDefault.Trim().Equals("1")
            //                    select w).ToList();
            //newDefault.IsDefault = "1";
            //foreach (UserHub uw in prevDefaults)
            //{
            //    uw.IsDefault = "0";
            //}
            //entities.SaveChanges();
        }


         [NotMapped]
        public List<Hub> UserAllowedHubs { get; set; }
        //public List<Hub> UserAllowedHubs
        //{
        //    //get
        //    //{
        //    //    CTSContext entities = new CTSContext();
        //    //    return (from w in entities.UserHubs
        //    //                                      where w.UserProfileID == this.UserProfileID
        //    //                                      select w.Hub).ToList();
        //    //}
        //     get { return null; }
        //}

        public static UserProfile GetUserById(int p)
        {
            //CTSContext entities = new CTSContext();
            //return (from u in entities.UserProfiles
            //        where u.UserProfileID == p && !u.LockedInInd && u.ActiveInd
            //        select u).FirstOrDefault();
            return null;
        }
        [NotMapped]
        public Hub DefaultHubObj { get; set; }
        //public Hub DefaultHub
        //{
        //    get
        //    {

        //        //CTSContext entities = new CTSContext();
        //        //var hub = (from w in entities.UserHubs
        //        //           where w.UserProfileID == this.UserProfileID && w.IsDefault.Trim().Equals("1")
        //        //           select w.Hub).FirstOrDefault();
        //        //if (hub == null)
        //        //{
        //        //    hub = (from w in entities.UserHubs
        //        //           where w.UserProfileID == this.UserProfileID
        //        //           select w.Hub).FirstOrDefault();
        //        //}
        //        //return hub;
        //        return null;
        //    }
        //}

        public string GetFullName()
        {
            return this.FirstName + " " + this.LastName;
        }
    }
}
