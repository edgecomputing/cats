using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
   public partial class Season
    {
       public Season()
       {
           this.Hrds=new List<HRD>();
           
       }

       public int SeasonID { get; set; }
       public string Name { get; set; }

        #region Navigation Properties

       public ICollection<HRD> Hrds { get; set; }
        #endregion

    }
}
