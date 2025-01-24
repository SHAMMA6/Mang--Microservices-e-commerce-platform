using Microsoft.AspNetCore.Identity;

namespace Mang.Services.AuthAPI.Models
{
    public class ApplicationUser : IdentityUser
    {
        public string Name { get; set; }
    }
}
