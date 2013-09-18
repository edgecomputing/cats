using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub
{
   public  class BinCardReport
   {
       [Key]
       public string SINumber { get; set; }
       public string DriverName { get; set; }
       public string Transporter { get; set; }
       public string TransporterAM { get; set; }
       public DateTime Date { get; set; }
       public string Projesct { get; set; }
       public decimal? Dispatched { get; set; }
       public decimal? Received { get; set; }
       public decimal? balance { get; set; }
       public string Identification { get; set; }
       public string ToFrom { get; set; }




    }
}
