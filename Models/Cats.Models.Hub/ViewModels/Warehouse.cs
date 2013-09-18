using System;
using System.Linq;

namespace Cats.Models.Hub
{
    partial class Hub
    {

        //TODO: Re-implement these two methods.
        /// <summary>
        /// Adds the user to a hub
        /// </summary>
        /// <param name="warehouseID">The warehouse ID.</param>
        /// <param name="userID">The user ID.</param>
        /// 
        ///Commented out by Banty Implemented AddHubUser(int warehouseID,int userID) in UserHubService
        //public void AddUser(int warehouseID, int userID)
        //{
        //    //IUnitOfWork repository = new UnitOfWork();
        //    //UserProfile uProfile = repository.UserProfile.FindById(userID);
        //    //var associations = from v in uProfile.UserHubs
        //    //                   where v.HubID == warehouseID
        //    //                   select v;
        //    //if (associations.Count() == 0)
        //    //{
        //    //    var userHub = new UserHub
        //    //    {
        //    //        UserProfileID = uProfile.UserProfileID,
        //    //        HubID = warehouseID
        //    //    };
        //    //    repository.UserHub.Add(userHub);
        //    //}

        //}

        /// <summary>
        /// Removes the user from a given hub
        /// </summary>
        /// <param name="warehouseID">The Hub ID.</param>
        /// <param name="userID">The user ID.</param>
        //TODO:Refactore to IUserHubService.RemoveUserHub
        //public void RemoveUser(int warehouseID, int userID)
        //{
        //    IUnitOfWork repository = new UnitOfWork();
        //    UserProfile uProfile = repository.UserProfile.FindById(userID);
        //    var associations = from v in uProfile.UserHubs
        //                       where v.HubID == warehouseID
        //                       select v;
        //    if (associations.Any())
        //    {
        //        UserHub userHub = associations.FirstOrDefault();
        //        if (userHub != null) repository.UserHub.DeleteByID(userHub.UserHubID);
        //    }
        //}
    }
}
