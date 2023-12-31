using System;
namespace CodingEvents.Models
{
	public class EventCategory
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Event> events { get; set; }

        public int CreatorId { get; set; }
        public User Creator { get; set; }

        public EventCategory(string name)
        {
            Name = name;
        }

        public EventCategory()
        {
        }
    }
}

