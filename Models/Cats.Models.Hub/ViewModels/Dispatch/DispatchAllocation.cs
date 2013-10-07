using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Data.Objects.DataClasses;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub
{
    public partial class DispatchAllocation
    {
        [NotMapped]
        public Decimal AmountInUnit
        {
            set { ; }
            get { return this.Amount; }
        }
           [NotMapped]
        public decimal RemainingQuantityInQuintals
        {
            set { ; }
            get {
                    return this.Amount - DispatchedAmount;
                }
        }
           [NotMapped]
        public decimal RemainingQuantityInUnit
        {
            set { ; }
            get
            {
                return this.Amount - DispatchedAmountInUnit;
            }
        }
           [NotMapped]
        public decimal DispatchedAmount
        {
            set { ; }
            get { return GetRelatedDispatchsAmountInQuintals(); }
        }
           [NotMapped]
        public Decimal DispatchedAmountInUnit
        {
            set { ; }
            get { return GetRelatedDispatchsAmountInUnit(); }
        }

        public decimal GetRelatedDispatchsAmountInQuintals()
        {
            return this.Dispatches.SelectMany(dispatch => dispatch.DispatchDetails).Sum(detail => (detail.DispatchedQuantityInMT*10));
        }

        public decimal GetRelatedDispatchsAmountInUnit()
        {
            return this.Dispatches.SelectMany(dispatch => dispatch.DispatchDetails).Sum(detail => (detail.DispatchedQuantityInUnit));
        }
    }
}
