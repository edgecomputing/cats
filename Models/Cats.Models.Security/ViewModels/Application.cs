using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Cats.Models.Security.ViewModels
{
    public class Application
    {
        public string ApplicationName { get; set; }
        public List<Role> Roles { get; set; }
    }
}
