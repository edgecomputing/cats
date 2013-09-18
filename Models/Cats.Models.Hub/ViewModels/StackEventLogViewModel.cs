using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub.ViewModels
{
    public class StackEventLogViewModel
    {
        [DataType(DataType.Date)]
        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }

        [Display(Name="Event Type")]
        public string StackEventType { get; set; }

        [DataType(DataType.Date)]
        [Display(Name = "Follow Up Date")]
        public DateTime FollowUpDate { get; set; }
        [Display(Name = "Description")]
        public string Description { get; set; }
        [Display(Name = "Recommendation")]
        public string Recommendation { get; set; }

    }
}
