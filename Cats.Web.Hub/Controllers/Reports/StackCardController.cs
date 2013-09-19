using System.Web.Mvc;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers.Reports
{
     [Authorize]
    public partial class StackCardController : BaseController
    {
        //
        // GET: /StackCard/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
