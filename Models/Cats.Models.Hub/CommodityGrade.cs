using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class CommodityGrade
    {
        public CommodityGrade()
        {
            this.Transactions = new List<Transaction>();
        }
        [Key]
        public int CommodityGradeID { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int SortOrder { get; set; }
        public virtual ICollection<Transaction> Transactions { get; set; }
    }
}
