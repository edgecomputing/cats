using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub
{
    public partial class OtherDispatchAllocation
    {
        public decimal RemainingQuantityInMt { 
                get {
                return this.QuantityInMT - GetRelatedDispatchsAmountInQuintals();
                }
        }

        public Decimal RemainingQuantityInUnit { 
            get
            {
                return this.QuantityInUnit - GetRelatedDispatchsAmountInUnits();
            }
        }

        public decimal GetRelatedDispatchsAmountInQuintals()
        {
            return this.Dispatches.SelectMany(dispatch => dispatch.DispatchDetails).Sum(detail => (detail.DispatchedQuantityInMT));
        }

        public decimal GetRelatedDispatchsAmountInUnits()
        {
            return this.Dispatches.SelectMany(dispatch => dispatch.DispatchDetails).Sum(detail => (detail.DispatchedQuantityInUnit));
        }
    }
}
