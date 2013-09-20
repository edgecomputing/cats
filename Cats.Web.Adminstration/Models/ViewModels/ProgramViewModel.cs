using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Cats.Web.Adminstration.Models.ViewModels
{
    public class ProgramViewModel
    {
        public int ProgramID { get; set; }
        public string ProgramName { get; set; }
        public string Description { get; set; }
        public string LongName { get; set; }
    }
}