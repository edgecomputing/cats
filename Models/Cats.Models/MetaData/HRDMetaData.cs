using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.MetaData
{
    public class HRDMetaData
    {
        
        public int Year { get; set; }

        [Required(ErrorMessage = "Please Select Month")]
        public int Month { get; set; }

         [Required(ErrorMessage = "Please Enter Created Date")]
        public DateTime CreatedDate { get; set; }

         [Required(ErrorMessage = "Please Enter Published Date")]
        public DateTime PublishedDate { get; set; }
        //public int StatusID { get; set; }
        [Required(ErrorMessage = "Please Select Ration")]
        public int RationID { get; set; }

       

    }
}
