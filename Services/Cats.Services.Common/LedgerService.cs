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
       public struct AvailableShippingCodes
       {
           public decimal _amount;
           public int? _siCode;
           public string SIcode;
       }

     
       private readonly IUnitOfWork _unitOfWork;

        public LedgerService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }


       
        public AvailableShippingCodes GetFreeSICodes(int hubId)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.LedgerID == 3 );//Goods On Hand - Commited

            var listOfSICodes =
                listOfTrans.GroupBy(t => t.ShippingInstructionID).Where(l => l.Sum(c => c.QuantityInMT) > 0).Select(
                    a => new
                             {
                                 availableAmount = a.Sum(t => t.QuantityInMT),
                                 SICodeId = a.Select(t => t.ShippingInstructionID),
                                 SICode =a.Select(t=>t.ShippingInstruction.Value)
                             });
            var freeSILists=new AvailableShippingCodes();
            foreach (var listOfSICode in listOfSICodes)
            {
                freeSILists._amount = listOfSICode.availableAmount;
                freeSILists._siCode = listOfSICode.SICodeId.Single();
                freeSILists.SIcode = listOfSICode.SICode.SingleOrDefault();
            }

            return freeSILists;
        }

        public decimal GetFreeSICodes(int hubId,int siCode)
        {
            var listOfTrans = _unitOfWork.TransactionRepository.FindBy(t => t.HubID == hubId && t.ShippingInstructionID == siCode && t.LedgerID == 3);//Goods On Hand - Commited

            return listOfTrans.Sum(s => s.QuantityInMT);

        }

       public decimal GetAvailableAmount(int siCode)
       {
           var qtyAssignedAmount =
               _unitOfWork.ProjectCodeAllocationRepository.Get(p => p.SINumberID == siCode).Sum(p => p.Amount_FromSI);
           return (decimal) qtyAssignedAmount;
       }
    }
}
