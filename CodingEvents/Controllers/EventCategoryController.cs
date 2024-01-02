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
    public class EventCategoryController : Controller
    {
        private UserManager<User> _userManager;

        private IEventCategoryService _categoryService;

        public EventCategoryController(UserManager<User> userManager,
                                    IEventCategoryService categoryService)
        {
            _userManager = userManager;
            _categoryService = categoryService;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            return View(_categoryService.FindAllByCreator(user));
        }

        [HttpGet]
        public IActionResult Create()
        {
            AddEventCategoryViewModel addEventCategoryViewModel = new AddEventCategoryViewModel();
            return View(addEventCategoryViewModel);
        }

        [HttpPost]
        public IActionResult ProcessCreateEventCategoryForm(AddEventCategoryViewModel addEventCategoryViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                _categoryService.SaveViewModel(addEventCategoryViewModel, user);

                return Redirect("/EventCategory");
            }

            return View("Create", addEventCategoryViewModel);
        }
    }
}

