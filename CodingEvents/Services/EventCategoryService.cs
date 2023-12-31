using CodingEvents.Data;
using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Services;

public class EventCategoryService : IEventCategoryService
{
    private EventDbContext _context;

    public EventCategoryService(EventDbContext context)
    {
        _context = context;
    }

    public List<EventCategory> FindAll()
    {
        return _context.Categories.ToList();
    }

    public List<EventCategory> FindAllByCreator(User creator)
    {
        return _context.Categories.Where(c => c.Creator.Equals(creator)).ToList();
    }

    public EventCategory FindById(int id)
    {
        return _context.Categories.Find(id);
    }

    public EventCategory FindByIdAndCreator(int id, User creator)
    {
        return _context.Categories.Where(c => c.Id == id && c.Creator.Equals(creator)).FirstOrDefault();
    }

    public EventCategory SaveViewModel(AddEventCategoryViewModel viewModel, User creator)
    {
        EventCategory newCategory = new EventCategory
        {
            Name = viewModel.Name,
            Creator = creator
        };
        _context.Categories.Add(newCategory);
        _context.SaveChanges();
        return newCategory;
    }

}
