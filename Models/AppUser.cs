using Microsoft.AspNetCore.Identity;

namespace InformationSystems.Server.Models
{
    public class AppUser:IdentityUser
    {
        public List<Portfolio> Portfolios { get; set; } = new List<Portfolio>();
    }
}
