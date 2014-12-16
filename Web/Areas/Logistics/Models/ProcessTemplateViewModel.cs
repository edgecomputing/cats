using System;
using System.Web;

namespace Cats.Areas.Logistics.Models
{
    public class ProcessTemplateViewModel
    {
        public ProcessTemplateViewModel()
        {
            Date = DateTime.Today.Date;
            PerformedBy = HttpContext.Current.User.Identity.Name;
        }

        public DateTime Date { get; private set; }

        public string PerformedBy { get; private set; }

        public int StateID { get; set; }

        public int ParentBusinessProcessID { get; set; }

        public int TransporterID { get; set; }

        public string UserName { get; set; }

        public int Index { get; set; }
    }
}