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
        [Display(Name = "Year")]
        public int Year { get; set; }

        [Required(ErrorMessage = "Please Select Season")]
        [Display(Name = "Season")]
        public int SeasonID { get; set; }

         [Required(ErrorMessage = "Please Enter Created Date")]
         [Display(Name = "Created Date")]
         public DateTime CreatedDate { get; set; }

         [Required(ErrorMessage = "Please Enter Published Date")]
         [Display(Name = "Published Date")]
         public DateTime PublishedDate { get; set; }
        //public int StatusID { get; set; }
         [Display(Name = "Ration")]
        [Required(ErrorMessage = "Please Select Ration")]
        public int RationID { get; set; }

       

    }
}
