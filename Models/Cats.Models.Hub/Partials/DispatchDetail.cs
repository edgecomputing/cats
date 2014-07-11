using System.ComponentModel.DataAnnotations;
using System.Linq;
using Cats.Models.Hubs.MetaModels;

namespace Cats.Models.Hubs
{
    /// <summary>
    /// 
    /// </summary>
   [MetadataType(typeof(DispatchDetailMetaModel))]
    public partial class DispatchDetail
    {
        /// <summary>
        /// Gets the dispatched quantity in MT.
        /// </summary>
        public decimal DispatchedQuantityInMT
        {
            get
            {
                // checks if this is on edit mode,if it is edit mode, it returns the associated transaction.
                Transaction t = GetAssociatedTransaction();
                if (t != null)
                {
                    return t.QuantityInMT;
                }
                return decimal.Zero;
            }
        }
        /// <summary>
        /// Gets the dispatched quantity in unit.
        /// </summary>
        public decimal DispatchedQuantityInUnit 
        {
            get
            {
                
                    // checks if this is on edit mode,if it is edit mode, it returns the associated transaction.
                    Transaction t = GetAssociatedTransaction();
                    if (t != null)
                    {
                        return t.QuantityInUnit;
                    }
                
                    return decimal.Zero;
                
            }
        }
        /// <summary>
        /// Gets the associated transaction.
        /// </summary>
        /// <returns></returns>
        private Transaction GetAssociatedTransaction()
        {
            if (this.TransactionGroup != null)
                return this.TransactionGroup.Transactions.FirstOrDefault(p=>p.QuantityInMT > 0 || p.QuantityInUnit > 0);
            else
                return null;
        }
    }
}
