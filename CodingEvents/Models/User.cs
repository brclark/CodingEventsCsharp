

using Microsoft.AspNetCore.Identity;

namespace CodingEvents.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }

        public ICollection<Event> Events { get; set; }

        public ICollection<EventCategory> Categories { get; set; }

        public ICollection<Tag> Tags { get; set; }

    }
}