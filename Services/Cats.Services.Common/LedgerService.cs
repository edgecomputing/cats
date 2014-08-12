using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{

   public class LedgerService : ILedgerService
    {

#region "SI Struct"
       public class AvailableShippingCodes
       {
           private decimal _amount;
           private int? _siCodeId;
           private string _SIcode;
           private string _hubName;
           private int _hubId;

           public int HubId
           {
               get { return _hubId; }
               set { _hubId = value; }
           }

           public string HubName
           {
               get { return _hubName; }
               set { _hubName = value; }
           }
           public decimal amount
           {
               get { return _amount; }
               set { _amount = value; }
           }

           public int? siCodeId
           {
               get { return _siCodeId; }
               set { _siCodeId = value; }
           }
           public string SIcode
           {
               get { return _SIcode; }
               set { _SIcode = value; }
           }
       }

#endregion

#region "PC Struct"
       public class AvailableProjectCodes
       {
           private decimal _amount;
           private int? _pcCodeId;
           private string _PCcode;
           private string _hubName;
           private int _hubId;

           public int HubId
           {
               get { return _hubId; }
               set { _hubId = value; }
           }

           public string HubName
           {
               get { return _hubName; }
               set { _hubName = value; }
           }

           public decimal amount
           {
               get { return _amount; }
               set { _amount = value; }
           }

           public int? pcCodeId
           {
               get { return _pcCodeId; }
               set { _pcCodeId = value; }
           }
           public string PCcode 
           {
               get { return _PCcode; }
               set { _PCcode = value; }
           }

       }

#endregion


       private readonly IUnitOfWork _unitOfWork;

        public LedgerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        #region "SI Code"

       

        /// <summary>
        /// Gets the balance of an SI number commodity .
        /// </summary>
        /// <param name="hubId">The hub id.</param>
        /// <param name="commodityId">The commodity id.</param>
       
        /// <returns>available amount,shipping Instruction Id, and Shipping Instruction Code</returns>
        public List<AvailableShippingCodes> GetFreeSICodesByCommodity(int hubId, int commodityId)
        {
            String HubFilter = (hubId > 0) ? string.Format(" And HubID = {0}", hubId) : "";

            String query = String.Format(@"SELECT SOH.QuantityInMT - ABS(ISNULL(Commited.QuantityInMT, 0)) as amount, SOH.ShippingInstructionID siCodeId, ShippingInstruction.Value SIcode, SOH.HubID as HubId, Hub.Name HubName 
                                                        from (SELECT SUM(QuantityInMT) QuantityInMT , ShippingInstructionID, HubID from [Transaction] 
					                                        WHERE LedgerID = {0} and CommodityID = {2} {3}
					                                        GROUP BY HubID, ShippingInstructionID) AS SOH
	                                            LEFT JOIN (SELECT SUM(QuantityInMT) QuantityInMT, ShippingInstructionID, HubID from [Transaction]
					                                        WHERE LedgerID = {1} and CommodityID = {2} {3}
					                                        GROUP By HubID, ShippingInstructionID) AS Commited	
		                                            ON SOH.ShippingInstructionID = Commited.ShippingInstructionID and SOH.HubId = Commited.HubId
	                                            JOIN ShippingInstruction 
		                                            ON SOH.ShippingInstructionID = ShippingInstruction.ShippingInstructionID 
                                                JOIN Hub
                                                    ON Hub.HubID = SOH.HubID
                                                WHERE 
                                                 SOH.QuantityInMT - ISNULL(Commited.QuantityInMT, 0) > 0    
                                                ", Ledger.Constants.GOODS_ON_HAND,Ledger.Constants.COMMITED_TO_FDP, commodityId, HubFilter);

            var availableShippingCodes = _unitOfWork.Database.SqlQuery<AvailableShippingCodes>(query);
           
            return availableShippingCodes.ToList();
        }

        
        #endregion


      


        public List<AvailableProjectCodes> GetFreePCCodesByCommodity(int hubId, int commodityId)
        {
            String HubFilter = (hubId > 0) ? string.Format(" And HubID = {0}", hubId) : "";

            String query = String.Format(@"SELECT SOH.QuantityInMT - ABS(ISNULL(Commited.QuantityInMT, 0)) as amount, SOH.ProjectCodeID pcCodeId, ProjectCode.Value PCcode, SOH.HubID as HubId, Hub.Name HubName 
                                                        from (SELECT SUM(QuantityInMT) QuantityInMT , ProjectCodeID, HubID from [Transaction] 
					                                        WHERE LedgerID = {0} and CommodityID = {2} {3} and ShippingInstructionID = NULL
					                                        GROUP BY HubID, ProjectCodeID) AS SOH
	                                            LEFT JOIN (SELECT SUM(QuantityInMT) QuantityInMT, ProjectCodeID, HubID from [Transaction]
					                                        WHERE LedgerID = {1} and CommodityID = {2} {3} and ShippingInstructionID = NULL
					                                        GROUP By HubID, ProjectCodeID) AS Commited	
		                                            ON SOH.ProjectCodeID = Commited.ProjectCodeID and SOH.HubId = Commited.HubId
	                                            JOIN ProjectCode 
		                                            ON SOH.ProjectCodeID = ProjectCode.ProjectCodeID 
                                                JOIN Hub
                                                    ON Hub.HubID = SOH.HubID
                                                WHERE 
                                                 SOH.QuantityInMT - ISNULL(Commited.QuantityInMT, 0) > 0    
                                                ", Ledger.Constants.GOODS_ON_HAND, Ledger.Constants.COMMITED_TO_FDP, commodityId, HubFilter);

            var availableShippingCodes = _unitOfWork.Database.SqlQuery<AvailableProjectCodes>(query);

            return availableShippingCodes.ToList();
        }


     

    }
}
