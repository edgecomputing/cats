using System.Web.Mvc;
using Cats.Services.Hub;
using Cats.Web.Hub;

namespace Cats.Web.Hub.Controllers.Reports
{
     [Authorize]
    public partial class StackCardController : BaseController
    {
         public StackCardController(IUserProfileService userProfileService):base(userProfileService)
         {
             
         }
        //
        // GET: /StackCard/

        public virtual ActionResult Index()
        {
            return View();
        }

    }
}
