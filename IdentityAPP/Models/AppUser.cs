using Microsoft.AspNetCore.Identity;

namespace IdentityAPP.Models
{
    public class AppUser : IdentityUser
    {
        public string? FullName {get;set;}
        
    }
}