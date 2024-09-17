using System.ComponentModel.DataAnnotations;

namespace InformationSystems.Server.DTO.Account
{
    public class LoginDTO
    {
        [Required]
        public string? Username { get; set; }
        [Required]
        public string? Password { get; set; }
    }
}
