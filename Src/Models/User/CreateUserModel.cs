using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Models.User;

public class CreateUserModel
{
    [Required] public required string Name { get; set; }
    [Required] public required string Username { get; set; }
    [Required, EmailAddress] public required string Email { get; set; }
}
