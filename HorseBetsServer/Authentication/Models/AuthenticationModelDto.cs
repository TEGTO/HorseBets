using System.ComponentModel.DataAnnotations;

namespace HorseBets.Api.Authentication.Models
{
    public class AuthenticationModelDto
    {
        public string UserId { get; set; } = null!;
    }
}
