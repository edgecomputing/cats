using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.ComponentModel.DataAnnotations;
namespace Cats.Models
{
    public partial class WoredasByDonor
    {
        [Key]
        public int DonorWoredaId { get; set; }
        public int DonorId { get; set; }
        public int WoredaId { get; set; }
        public string Remark { get; set; }
        public virtual AdminUnit AdminUnit { get; set; }
        public virtual Donor Donor { get; set; }
    }
}
