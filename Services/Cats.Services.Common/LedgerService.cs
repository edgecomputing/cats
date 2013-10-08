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
                                 SICodeId = a.Select(t => t.ShippingInstructionID)
                             });
            var freeSILists=new AvailableShippingCodes();
            foreach (var listOfSICode in listOfSICodes)
            {
                freeSILists._amount = listOfSICode.availableAmount;
                freeSILists._siCode = listOfSICode.SICodeId.Single();
            }

            return freeSILists;
        }
    }
}
