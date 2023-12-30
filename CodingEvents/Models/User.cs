

using Microsoft.AspNetCore.Identity;

namespace CodingEvents.Models
{
    public class User : IdentityUser
    {
        [PersonalData]
        public string? Name { get; set; }

    }
}