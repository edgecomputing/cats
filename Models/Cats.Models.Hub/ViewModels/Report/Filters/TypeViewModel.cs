using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cats.Models.Hub.ViewModels.Report
{
    /// <summary>
    /// Dispatch type like FDP dispatch | Transfer to other hub
    /// </summary>
    public class TypeViewModel
    {
        public int TypeId { get; set; }
        public string TypeName { get; set; }
    }
}
