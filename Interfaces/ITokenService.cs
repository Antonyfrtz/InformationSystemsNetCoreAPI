using InformationSystems.Server.Models;

namespace InformationSystems.Server.Interfaces
{
    public interface ITokenService
    {
        string CreateToken(AppUser user);
    }
}
