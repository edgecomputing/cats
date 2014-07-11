using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Cats.Models.Hubs
{
    public partial class LetterTemplate
    {
        [Key]
        public int LetterTemplateID { get; set; }
        public string Name { get; set; }
        public string Parameters { get; set; }
        public string Template { get; set; }
    }
}
