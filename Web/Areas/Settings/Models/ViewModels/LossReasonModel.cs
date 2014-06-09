using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Areas.Settings.Models.ViewModels
{
    public class LossReasonModel
    {
        public int LossReasonId { get; set; }
        public string LossReasonEg { get; set; }
        public string LossReasonAm { get; set; }
        public string LossReasonCodeEg { get; set; }
        public string LossReasonCodeAm { get; set; }
        public string Description { get; set; }
    }
}