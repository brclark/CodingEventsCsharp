using CodingEvents.Models;
using CodingEvents.ViewModels;

namespace CodingEvents.Services;

public interface IEventCategoryService
{
    public List<EventCategory> FindAll();
    public List<EventCategory> FindAllByCreator(User creator);

    public EventCategory FindById(int id);
    public EventCategory FindByIdAndCreator(int id, User creator);
    public EventCategory SaveViewModel(AddEventCategoryViewModel viewModel,
                                    User creator);

}
