using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Cats.Models.Hubs.ViewModels;
using Cats.Models.Hubs;
using Cats.Models.Hubs.ViewModels.Common;
using Cats.Web.Hub;
using Telerik.Web.Mvc;
using Cats.Services.Hub;

namespace Cats.Areas.Hub.Controllers
{
    public class StackEventController : BaseController
    {
        
        //
        // GET: /StackEvent/
       
        private readonly IStackEventService _stackEventService;
        private readonly IUserProfileService _userProfileService;
        private readonly IStoreService _storeService;
        private readonly IHubService _hubService;
        private readonly IStackEventTypeService _StackEventTypeService; 

        public StackEventController(IStackEventService stackeventService, 
                                    IUserProfileService userProfileService,
                                    IHubService hubService,
                                    IStackEventTypeService stackEventTypeService, IStoreService storeService)
            : base(userProfileService)
        {
            _stackEventService = stackeventService;
            _userProfileService = userProfileService;
            _hubService = hubService;
            _StackEventTypeService = stackEventTypeService;
            _storeService = storeService;
        }

        public ActionResult Index()
        {
            List<StackEventType> stackEventType;
            List<StoreViewModel> store;

            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            stackEventType = _StackEventTypeService.GetAllStackEventType();
            store = _hubService.GetAllStoreByUser(user);

           
            StackEventViewModel viewModel = new StackEventViewModel(stackEventType, store);
            return View(viewModel );
        }
        
        public ActionResult EventLog()
        {

            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            List<StackEventLogViewModel> viewModel = new List<StackEventLogViewModel>();
                //repository.StackEvent.GetAllStackEvents(user);
            return PartialView(viewModel);
        }

        [GridAction]
        public ActionResult EventLogGrid(int? StackId, int? StoreId)
        {
            if (StackId.HasValue && StoreId.HasValue)
            {
                UserProfile user = _userProfileService.GetUser(User.Identity.Name);
                return View(new GridModel(_stackEventService.GetAllStackEventsByStoreIdStackId(user,StackId.Value, StoreId.Value).OrderByDescending(o => o.EventDate)));
            }
            return View(new GridModel(new List<StackEventViewModel>()));
        }

        public ActionResult EditStackEvent()
        {

            List<StackEventType> stackEventType;
            List<StoreViewModel> store;

            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            stackEventType = _StackEventTypeService.GetAllStackEventType();
            store = _hubService.GetAllStoreByUser(user);

            
            StackEventViewModel viewModel = new StackEventViewModel(stackEventType,store);
            return PartialView(viewModel);
        }
        [HttpPost]
        public ActionResult EditStackEvent(StackEventViewModel viewModel)
        {
            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            _stackEventService.AddStackEvent(new StackEvent 
            { 
                StackEventID=Guid.NewGuid(),
                EventDate = viewModel.EventDate,
                StoreID = viewModel.StoreIdTwo,
                StackEventTypeID = viewModel.StackEventTypeId,
                StackNumber = viewModel.StackNumberTwo,
                FollowUpDate = viewModel.FollowUpDate,
                FollowUpPerformed = false,
                Description = viewModel.Description,
                Recommendation  = viewModel.Recommendation,
                UserProfileID = user.UserProfileID
            });
            return RedirectToAction("Index", "StackEvent");
        }

        public ActionResult GetStacksFromStore(int? StoreId)
        {
            return Json(new SelectList(_storeService.GetStacksByStoreId(StoreId), JsonRequestBehavior.AllowGet));
        }

        public ActionResult GetStacksFromStoreTwo(int? StoreIdTwo)
        {
            return Json(new SelectList(_storeService.GetStacksByStoreId(StoreIdTwo), JsonRequestBehavior.AllowGet));
        }

        [HttpPost]
        public ActionResult GetStore()
        {

            UserProfile user = _userProfileService.GetUser(User.Identity.Name);
            return new JsonResult { Data = new SelectList(_hubService.GetAllStoreByUser(user), "StoreId", "StoreName") };
        }

        [HttpPost]
        public ActionResult GetEventType()
        {
            return new JsonResult { Data = new SelectList(_StackEventTypeService.GetAllStackEventType(), "StackEventTypeID", "Name") };
        }

        [HttpPost]
        public ActionResult GetFollowUpDate(DateTime selectedDate, int StackEventTypeId)
        {
            DateTime followupDate = DateTime.Now;
            if (selectedDate != null)
            {
                followupDate = selectedDate;
                var duration = _StackEventTypeService.GetFollowUpDurationByStackEventTypeId(StackEventTypeId);

                followupDate = followupDate.AddDays(duration);
            }
            return Json(followupDate.ToShortDateString(), JsonRequestBehavior.AllowGet);
        }
    }
}
