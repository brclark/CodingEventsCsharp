using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace CodingEvents.Controllers
{
    [Authorize]
    public class EventsController : Controller
    {
        private EventDbContext context;
        private readonly UserManager<User> _userManager;

        public EventsController(EventDbContext dbContext, UserManager<CodingEvents.Models.User> userManager)
        {
            context = dbContext;
            _userManager = userManager;
        }

        // GET: /<controller>/
        public IActionResult Index()
        {
            var user = _userManager.GetUserAsync(User).Result;
            List<Event> events = context.Events
                .Include(e => e.Category)
                .Where(e => e.Creator.Id == user.Id)
                .ToList();

            return View(events);
        }

        public ActionResult Add()
        {
            var user =  _userManager.GetUserAsync(User).Result;
            List<EventCategory> categories = context.Categories
                .Where(c => c.Creator.Id == user.Id).ToList();
            AddEventViewModel addEventViewModel = new AddEventViewModel(categories);

            return View(addEventViewModel);
        }

        [HttpPost]
        public IActionResult Add(AddEventViewModel addEventViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = _userManager.GetUserAsync(User).Result;
                EventCategory theCategory = context.Categories
                    .Single(c => c.Id == addEventViewModel.CategoryId &&
                            c.Creator.Id == user.Id);
                Event newEvent = new Event
                {
                    Name = addEventViewModel.Name,
                    Description = addEventViewModel.Description,
                    ContactEmail = addEventViewModel.ContactEmail,
                    Category = theCategory,
                    Creator = user
                };

                context.Events.Add(newEvent);
                context.SaveChanges();

                return Redirect("/Events");
            }

            return View(addEventViewModel);
        }

        public IActionResult Delete()
        {
            ViewBag.events = context.Events.ToList();

            return View();
        }

        [HttpPost]
        public IActionResult Delete(int[] eventIds)
        {
            foreach (int eventId in eventIds)
            {
                Event theEvent = context.Events.Find(eventId);
                context.Events.Remove(theEvent);
            }

            context.SaveChanges();

            return Redirect("/Events");
        }

        public IActionResult Detail(int id)
        {
            var user = _userManager.GetUserAsync(User).Result;
            Event theEvent = context.Events
               .Include(e => e.Category)
               .Include(e => e.Tags)
               .Single(e => e.Id == id && e.Creator.Id == user.Id);

            EventDetailViewModel viewModel = new EventDetailViewModel(theEvent);
            return View(viewModel);
        }
    }
}

