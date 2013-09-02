using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models
{
    public partial class LetterTemplate
    {
        public int LetterTemplateID { get; set; }
        public string Name { get; set; }
        public string Parameters { get; set; }
        public string Template { get; set; }
        public string FileName { get; set; }
    }
}
