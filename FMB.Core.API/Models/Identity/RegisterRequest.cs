using System.ComponentModel.DataAnnotations;

namespace FMB.Core.API.Models.Identity;

public class RegisterRequest
{
    [Required]
    [DataType(DataType.Password)]
    public string Password { get; set; } = null!;

    [Required]
    [DataType(DataType.Password)]
    public string PasswordConfirm { get; set; } = null!;

    [Required]
    public string UserName { get; set; } = null!;

}