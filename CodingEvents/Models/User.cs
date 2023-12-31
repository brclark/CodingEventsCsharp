

using Microsoft.AspNetCore.Identity;

namespace CodingEvents.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }

        public List<Event> Events { get; set; }

        public List<EventCategory> Categories { get; set; }

        public List<Tag> Tags { get; set; }

    }
}