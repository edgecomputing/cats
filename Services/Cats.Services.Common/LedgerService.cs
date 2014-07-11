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
       

        /// <returns>available amount,shipping Instruction Id, and Shipping Instruction Code</returns>
        public List<AvailableShippingCodes> GetFreeSICodes(int hubId)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.LedgerID == 20 );//Goods On Hand - unCommited

            var listOfSICodes =
                listOfTrans.GroupBy(t => t.ShippingInstructionID).Select(
                    a => new
                             {

                                 availableAmount = a.Sum(t => t.QuantityInMT),
                                 SICodeId = a.Select(t => t.ShippingInstructionID),
                                 SICode =a.Select(t=>t.ShippingInstruction.Value)
                             }).Where(s => s.availableAmount > 0); 
           
            var siCodeList = new List<AvailableShippingCodes>();
            foreach (var listOfSICode in listOfSICodes)
            {
                 var freeSILists=new AvailableShippingCodes();
                freeSILists.amount = listOfSICode.availableAmount;
                freeSILists.siCodeId = listOfSICode.SICodeId.FirstOrDefault();
                freeSILists.SIcode = listOfSICode.SICode.FirstOrDefault();
                siCodeList.Add(freeSILists);
            }

            return siCodeList;
        }

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

        /// <summary>
        /// Gets the balance of an SI number.
        /// </summary>
        ///  /// <param name="siCode">The si id.</param>
        /// <param name="hubId">The hub id.</param>
      

        /// <returns>available amount,shipping Instruction Id, and Shipping Instruction Code</returns>
        public decimal GetFreeSICodesAmount(int hubId,int siCode)
        {
            var goodsOnHand = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ShippingInstructionID == siCode && t.LedgerID == Ledger.Constants.GOODS_ON_HAND);//Goods On Hand - unCommited
            var commitedToFDP = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ShippingInstructionID == siCode && t.LedgerID == Ledger.Constants.COMMITED_TO_FDP);
            return goodsOnHand.Sum(s => s.QuantityInMT) - commitedToFDP.Sum(c => c.QuantityInMT);

        }

        #endregion


        #region "Project Code"

        public List<AvailableProjectCodes> GetFreePCCodes(int hubId)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.LedgerID == 20);//Goods On Hand - unCommited

            var listOfSICodes =
                listOfTrans.GroupBy(t => t.ProjectCodeID).Select(
                    a => new
                    {
                        availableAmount = a.Sum(t => t.QuantityInMT),
                        PCCodeId = a.Select(t => t.ProjectCodeID),
                        PCCode = a.Select(t => t.ProjectCode.Value),
                        HubID = a.Select(t => t.HubID)
                    }).Where(s=>s.availableAmount> 0);

            var pcCodeList = new List<AvailableProjectCodes>();
            foreach (var listOfSICode in listOfSICodes)
            {
                var freePCLists = new AvailableProjectCodes();
                freePCLists.amount = listOfSICode.availableAmount;
                freePCLists.pcCodeId = listOfSICode.PCCodeId.FirstOrDefault();
                freePCLists.PCcode = listOfSICode.PCCode.FirstOrDefault();
                freePCLists.HubId = (int)listOfSICode.HubID.FirstOrDefault();
                pcCodeList.Add(freePCLists);
            }

            return pcCodeList;
        }


        public List<AvailableProjectCodes> GetFreePCCodesByCommodity(int hubId, int commodityId)
        {
            String HubFilter = (hubId > 0) ? string.Format(" And HubID = {0}", hubId) : "";

            String query = String.Format(@"SELECT SOH.QuantityInMT - ABS(ISNULL(Commited.QuantityInMT, 0)) as amount, SOH.ProjectCodeID pcCodeId, ProjectCode.Value PCcode, SOH.HubID as HubId, Hub.Name HubName 
                                                        from (SELECT SUM(QuantityInMT) QuantityInMT , ProjectCodeID, HubID from [Transaction] 
					                                        WHERE LedgerID = {0} and CommodityID = {2} {3}
					                                        GROUP BY HubID, ProjectCodeID) AS SOH
	                                            LEFT JOIN (SELECT SUM(QuantityInMT) QuantityInMT, ProjectCodeID, HubID from [Transaction]
					                                        WHERE LedgerID = {1} and CommodityID = {2} {3}
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


        public decimal GetFreePCCodes(int hubId, int pcCode)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ProjectCodeID == pcCode && t.LedgerID == Ledger.Constants.GOODS_ON_HAND);//Goods On Hand - unCommited

            return listOfTrans.Sum(s => s.QuantityInMT);

        }

        #endregion

        public decimal GetAvailableAmount(int siCode)
       {
           var qtyAssignedAmount =
               _unitOfWork.ProjectCodeAllocationRepository.Get(p => p.SINumberID == siCode).Sum(p => p.Amount_FromSI);
           return (decimal) qtyAssignedAmount;
       }
    }
}
