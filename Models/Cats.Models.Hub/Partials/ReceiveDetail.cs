using System.Collections.Generic;
using System.Linq;
using Cats.Models.Hubs.MetaModels;
using System.ComponentModel.DataAnnotations;


namespace Cats.Models.Hubs
{
    [MetadataType(typeof(ReceiveDetailMetaModel))]
    public partial  class ReceiveDetail
    {
        public int? CommodityGradeID
        {
            get
            {
                Transaction t = GetAssociatedTransaction();
                if (t != null)
                {
                    if (t.CommodityGradeID != null) return t.CommodityGradeID.Value;
                }
                return null;
            }

        }
        public decimal QuantityInMT
        {
            get
            {
                Transaction t = GetAssociatedTransaction();
                if (t != null)
                {
                    return t.QuantityInMT;
                }
                return decimal.Zero;
            }
        }

        public string CommodityName
        {
            get
            {
                if (this.Commodity != null)
                    return this.Commodity.Name;
                else
                    return "";
            }
        }

        public decimal QuantityInUnit
        {
            get
            {
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
                return this.TransactionGroup.Transactions.FirstOrDefault(p => p.QuantityInMT > 0 || p.QuantityInUnit> 0);
            else
                return null;
        }    
    }
}
