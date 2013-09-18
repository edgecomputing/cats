using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hub
{
    public partial class ReleaseNote
    {
        [Key]
        public int ReleaseNoteID { get; set; }
        public string ReleaseName { get; set; }
        public Nullable<int> BuildNumber { get; set; }
        public System.DateTime Date { get; set; }
        public string Notes { get; set; }
        public string Details { get; set; }
        public int DBuildQuality { get; set; }
        public string Comments { get; set; }
    }
}
