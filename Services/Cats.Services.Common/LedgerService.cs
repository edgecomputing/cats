using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;
using Cats.Data.UnitWork;

namespace Cats.Services.Common
{

   public class LedgerService:ILedgerService
    {

#region "SI Struct"
       public struct AvailableShippingCodes
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
       public struct AvailableProjectCodes
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
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.LedgerID == 2 );//Goods On Hand - unCommited

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
            List<Transaction> listOfTrans=new List<Transaction>();
            if (hubId > 0)
            {
                listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.CommodityID == commodityId && t.LedgerID == 2);//Goods On Hand - unCommited
            }
            else
            {
                listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.CommodityID == commodityId && t.LedgerID == 2);//Goods On Hand - unCommited
            }
            var listOfSICodes =
                listOfTrans.GroupBy(t => t.ShippingInstructionID).Select(
                    a => new
                    {
                        availableAmount = a.Sum(t => t.QuantityInMT),
                        SICodeId = a.Select(t => t.ShippingInstructionID),
                        SICode = a.Select(t => t.ShippingInstruction.Value),
                        HubID = a.Select(t => t.HubID),

                    }).Where(s => s.availableAmount > 0);

            var siCodeList = new List<AvailableShippingCodes>();
            foreach (var listOfSICode in listOfSICodes)
            {
                var freeSILists = new AvailableShippingCodes();
                freeSILists.amount = listOfSICode.availableAmount;
                freeSILists.siCodeId = listOfSICode.SICodeId.FirstOrDefault();
                freeSILists.SIcode = listOfSICode.SICode.FirstOrDefault();
                freeSILists.HubId = (int)listOfSICode.HubID.FirstOrDefault();
                siCodeList.Add(freeSILists);
            }

            return siCodeList;
        }

        /// <summary>
        /// Gets the balance of an SI number.
        /// </summary>
        ///  /// <param name="siCode">The si id.</param>
        /// <param name="hubId">The hub id.</param>
      

        /// <returns>available amount,shipping Instruction Id, and Shipping Instruction Code</returns>
        public decimal GetFreeSICodesAmount(int hubId,int siCode)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ShippingInstructionID == siCode && t.LedgerID == 2);//Goods On Hand - unCommited
            return listOfTrans.Sum(s => s.QuantityInMT);

        }

        #endregion


        #region "Project Code"

        public List<AvailableProjectCodes> GetFreePCCodes(int hubId)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.LedgerID == 2);//Goods On Hand - unCommited

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
            List<Transaction> listOfTrans = new List<Transaction>();
            if (hubId > 0)
            {
                _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.CommodityID == commodityId && t.LedgerID == 2);//Goods On Hand - unCommited
            }
            else
            {
                _unitOfWork.TransactionRepository.FindBy(t => t.CommodityID == commodityId && t.LedgerID == 2);//Goods On Hand - unCommited
            }
            var listOfSICodes =
                listOfTrans.GroupBy(t => t.ProjectCodeID).Select(
                    a => new
                    {
                        availableAmount = a.Sum(t => t.QuantityInMT),
                        PCCodeId = a.Select(t => t.ProjectCodeID),
                        PCCode = a.Select(t => t.ProjectCode.Value)
                    }).Where(s => s.availableAmount > 0);

            var pcCodeList = new List<AvailableProjectCodes>();
            foreach (var listOfSICode in listOfSICodes)
            {
                var freePCLists = new AvailableProjectCodes();
                freePCLists.amount = listOfSICode.availableAmount;
                freePCLists.pcCodeId = listOfSICode.PCCodeId.FirstOrDefault();
                freePCLists.PCcode = listOfSICode.PCCode.FirstOrDefault();
                pcCodeList.Add(freePCLists);
            }

            return pcCodeList;
        }


        public decimal GetFreePCCodes(int hubId, int pcCode)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ProjectCodeID == pcCode && t.LedgerID == 3);//Goods On Hand - unCommited

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
