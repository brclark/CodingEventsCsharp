using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;
using Microsoft.EntityFrameworkCore;

namespace CodingEvents.Services;

public class EventService : IEventService
{
    private readonly EventDbContext _context;

    public EventService(EventDbContext context)
    {
        _context = context;
    }

    public List<Event> FindAll()
    {
        return _context.Events.Include(e => e.Category).ToList();
    }

    public List<Event> FindAllByCreator(User creator)
    {
        return _context.Events
            .Where(e => e.Creator.Equals(creator))
            .Include(e => e.Category)
            .ToList();
    }

    public Event FindById(int id)
    {
        return _context.Events
            .Include(e => e.Category)
            .FirstOrDefault(e => e.Id == id);    
    }

    public Event FindByIdAndCreator(int id, User creator)
    {
        return _context.Events
            .Include(e => e.Category)
            .Where(e => e.Id == id && e.Creator.Equals(creator))
            .FirstOrDefault();
    }

    public Event SaveViewModel(AddEventViewModel viewModel, User creator)
    {
        EventCategory category = _context.Categories.Find(viewModel.CategoryId);
        Event newEvent = new Event
        {
            Name = viewModel.Name,
            Description = viewModel.Description,
            ContactEmail = viewModel.ContactEmail,
            Category = category,
            Creator = creator,
            Tags = new List<Tag>()
        };
        _context.Events.Add(newEvent);
        _context.SaveChanges();
        return newEvent;
    }

    public EventDetailViewModel GetDetailViewModel(int id, User creator)
    {
        Event eventToDisplay = _context.Events
            .Include(e => e.Category)
            .Include(e => e.Tags)
            .Where(e => e.Id == id && e.Creator.Equals(creator))
            .FirstOrDefault();
        return new EventDetailViewModel(eventToDisplay);
    }

    public void RemoveById(int id)
    {
        Event theEvent = _context.Events.Find(id);
        _context.Events.Remove(theEvent);
        _context.SaveChanges();
    }
}
