using System.ComponentModel.DataAnnotations;

namespace FMB.Core.API.Models.Identity;

public class RegistrationRequest
{
    [Required]
    public string Email { get; set; } = null!;

    [Required]
    public string Username { get; set; } = null!;

    [Required]
    public string Password { get; set; } = null!;
}