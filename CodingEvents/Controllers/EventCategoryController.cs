using CodingEvents.Data;
using CodingEvents.Models;
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
        private EventDbContext context;

        private UserManager<User> _userManager;

        public EventCategoryController(EventDbContext dbContext,
                                    UserManager<User> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        // GET: /<controller>/
        [HttpGet]
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            List<EventCategory> categories = context.Categories
                .Where(c => c.Creator.Id == user.Id).ToList();
            return View(categories);
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
                EventCategory newCategory = new EventCategory
                {
                    Name = addEventCategoryViewModel.Name,
                    Creator = user
                };

                context.Categories.Add(newCategory);
                context.SaveChanges();

                return Redirect("/EventCategory");
            }

            return View("Create", addEventCategoryViewModel);
        }
    }
}

