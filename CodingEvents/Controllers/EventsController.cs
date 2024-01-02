using CodingEvents.Models;
using CodingEvents.Services;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingEvents.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly IEventService _eventService;
        private readonly IEventCategoryService _eventCategoryService;

        public EventsController(UserManager<User> userManager,
                                IEventService eventService,
                                IEventCategoryService eventCategoryService)
        {
            _userManager = userManager;
            _eventService = eventService;
            _eventCategoryService = eventCategoryService;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;

            return View(_eventService.FindAllByCreator(user));
        }

        [Authorize(Roles = "Organizer")]
        public ActionResult Add()
        {
            var user =  _userManager.GetUserAsync(User).Result;
            AddEventViewModel addEventViewModel =
                new AddEventViewModel(_eventCategoryService.FindAllByCreator(user));

            return View(addEventViewModel);
        }

        [Authorize(Roles = "Organizer")]
        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                _eventService.SaveViewModel(addEventViewModel, user);

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        [Authorize(Roles = "Organizer")]
        public IActionResult Delete()
        {
            var user = _userManager.GetUserAsync(User).Result;
            ViewBag.events = _eventService.FindAllByCreator(user);

            return View();
        }

        [Authorize(Roles = "Organizer")]
        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                _eventService.RemoveById(eventId);
            }

            return Redirect("/Events");
        }

        public IActionResult Detail(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;

            return View(_eventService.GetDetailViewModel(id, user));
        }
    }
}

