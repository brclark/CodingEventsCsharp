using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Services;

public interface IEventService
{
    public List<Event> FindAll();
    public List<Event> FindAllByCreator(User creator);
    public Event FindById(int id);
    public Event FindByIdAndCreator(int id, User creator);
    public Event SaveViewModel(AddEventViewModel viewModel, User creator);
    public EventDetailViewModel GetDetailViewModel(int id, User creator);
    public void RemoveById(int id);
}
