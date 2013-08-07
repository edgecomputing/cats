using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class RegionalRequestDetail
    {
        public RegionalRequestDetail()
        {
            this.RequestDetailCommodities=new List<RequestDetailCommodity>();
        }
        public int RegionalRequestDetailID { get; set; }
        public int RegionalRequestID { get; set; }
        public int Fdpid { get; set; }
        public decimal Grain { get; set; }
        public decimal Pulse { get; set; }
        public decimal Oil { get; set; }
        public decimal CSB { get; set; }
        public int Beneficiaries { get; set; }
      

        #region Navigation Properties
        public virtual RegionalRequest RegionalRequest { get; set; }
        public virtual FDP Fdp { get; set; }
        public virtual ICollection<RequestDetailCommodity> RequestDetailCommodities { get; set; }
        #endregion
    }
}
