using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Cats.Models;

namespace Cats.Services.Common
{
    

   public interface ILedgerService
   {
       List<LedgerService.AvailableShippingCodes> GetFreeSICodes(int hubId);
       List<LedgerService.AvailableProjectCodes> GetFreePCCodes(int hubId);
       decimal GetFreeSICodesAmount(int hubId, int siCode);
       decimal GetFreePCCodes(int hubId, int pcCode);
       decimal GetAvailableAmount(int siCode);
   }
}
