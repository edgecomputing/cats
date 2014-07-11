using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs
{
    
    /// <summary>
    /// 
    /// </summary>
   public partial class Account
    {
        /// <summary>
        /// 
        /// </summary>
        /// 
        
       public class Constants
       {
           /// <summary>
           /// 
           /// </summary>
           public const string DONOR = "Donor";
           public const string FDP = "FDP";
           public const string HUBOWNER = "HubOwner";
           public const string HUB = "Hub";
       }
       //TODO:separation of concern
    //   IUnitOfWork unitOfWork;
       /// <summary>
       /// Gets the name of the entity.
       /// </summary>
       /// <value>
       /// The name of the entity.
       /// </value>
        [NotMapped]
       public string EntityName
       {
           get
           {
               //TODO:Refactor
               //if (unitOfWork == null)
               //{
               //    unitOfWork = new UnitOfWork();
               //}
               //if (this.EntityID != 0)
               //{
               //    switch (this.EntityType)
               //    {
               //        case Constants.DONOR:
               //            var donor = unitOfWork.DonorRepository.FindById(this.EntityID);
               //            if (donor != null)
               //                return donor.Name;
               //            break;
               //        case Constants.FDP:
               //            var fdp = unitOfWork.FDPRepository.FindById(EntityID);
               //            if (fdp != null)
               //                return fdp.Name;
               //            break;
               //        case Constants.HUBOWNER:
               //            var hubOwner = unitOfWork.HubOwnerRepository.FindById(EntityID);
               //            if (hubOwner != null)
               //                return hubOwner.Name;
               //            break;
               //        case Constants.HUB:
               //            var hub = unitOfWork.HubRepository.FindById(EntityID);
               //            if (hub != null)
               //                return hub.Name;
               //            break;
               //    }
               //}
               return "Unknown";
           }
       }
    }
}
