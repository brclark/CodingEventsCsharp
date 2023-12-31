using System;
using System.ComponentModel.DataAnnotations.Schema;
namespace CodingEvents.Models
{
	public class EventCategory
	{
        public int Id { get; set; }

        public string Name { get; set; }

        public List<Event> events { get; set; }

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

