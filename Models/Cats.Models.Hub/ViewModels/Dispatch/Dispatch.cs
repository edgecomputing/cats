using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.Hubs
{

    /// <summary>
    /// This is a partial class to the Dispatch entity
    /// 
    /// </summary>
    //TODO: this has to be moved to an appropriate folder in the solution. 
    // This is not a view model and should not be placed in the view models folder
    partial class Dispatch
    {
        public bool Validate()
        {
            //TODO: Validate Dispatch
            return true;
        }

        /// <summary>
        /// Updates the specified inserted.
        /// </summary>
        /// <param name="inserted">The inserted.</param>
        /// <param name="updated">The updated.</param>
        /// <param name="deleted">The deleted.</param>
        public void Update(List<DispatchDetail> inserted, List<DispatchDetail> updated, 
            List<DispatchDetail> deleted)
        {
            //TODO:Refactor
            //CTSContext db = new CTSContext();
            //Dispatch orginal = db.Dispatches.Where(p => p.DispatchID == this.DispatchID).SingleOrDefault();
            //        if (orginal != null)
            //        {
            //            orginal.BidNumber = this.BidNumber;
            //            orginal.DispatchDate = this.DispatchDate;
            //            orginal.DriverName = this.DriverName;
            //            orginal.FDPID = this.FDPID;
            //            orginal.GIN = this.GIN;
            //            orginal.PeriodYear = this.PeriodYear;
            //            orginal.PeriodMonth = this.PeriodMonth;
            //            orginal.PlateNo_Prime = this.PlateNo_Prime;
            //            orginal.PlateNo_Trailer = this.PlateNo_Trailer;
            //            //orginal.ProgramID = this.ProgramID;
            //            orginal.RequisitionNo = this.RequisitionNo;
            //            orginal.Round = this.Round;
            //            //orginal.StackNumber = this.StackNumber;
            //            //orginal.StoreID = this.StoreID;
            //            orginal.TransporterID = this.TransporterID;
            //            orginal.UserProfileID = this.UserProfileID;
            //            //orginal.WarehouseID = this.WarehouseID;
            //            orginal.WeighBridgeTicketNumber = this.WeighBridgeTicketNumber;
            //            orginal.Remark = this.Remark;
            //            orginal.DispatchedByStoreMan = this.DispatchedByStoreMan;
            //            //orginal.ProjectNumber = this.ProjectNumber;
            //            //orginal.SINumber = this.SINumber;


                  
                        

            //            foreach (DispatchDetail update in updated)
            //            {
            //                DispatchDetail updatedCommodity = db.DispatchDetails.Where(p => p.DispatchDetailID == update.DispatchDetailID).SingleOrDefault();
            //                if (updatedCommodity != null)
            //                {
            //                    updatedCommodity.CommodityID = update.CommodityID;
            //                    updatedCommodity.Description = update.Description;
            //                    //updatedCommodity.DispatchedQuantityInUnit = update.DispatchedQuantityInUnit;
            //                    //updatedCommodity.DispatchedQuantityInMT = update.DispatchedQuantityInMT;
            //                    updatedCommodity.RequestedQunatityInUnit = update.RequestedQunatityInUnit;
            //                    updatedCommodity.RequestedQuantityInMT = update.RequestedQuantityInMT;
            //                    updatedCommodity.UnitID = update.UnitID;
            //                }
            //            }
            //            db.SaveChanges();
            //        }
            
        }

        /// <summary>
        /// Gets the SMS text.
        /// </summary>
        /// <returns></returns>
        public string GetSMSText()
        {
            //TODO:Refactor
            //StringBuilder builder = new StringBuilder();
            //CTSContext entities = new CTSContext();
            //Dispatch dispatch  = entities.Dispatches.Where(d => d.DispatchID == this.DispatchID).SingleOrDefault();
            //if (dispatch != null)
            //{
            //    DispatchDetail com = dispatch.DispatchDetails.FirstOrDefault();
            //    if (com != null)
            //    {
            //        builder.Append(string.Format("There is a Dispatch with an ammount of {0}(MT) - {1} to your FDP({2}) ", com.RequestedQuantityInMT, com.Commodity.Name, dispatch.FDP.Name));
            //        builder.Append(string.Format("on a car with plate no - {0}", dispatch.PlateNo_Prime));
            //    }
            //}
            
            //return builder.ToString();
            return string.Empty;
        }

        /// <summary>
        /// Gets the type.
        /// </summary>
        public int Type
        {
            get
            {
                return (FDPID.HasValue) ? 1 : 2;
            }
        }
    }
}
