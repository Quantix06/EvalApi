using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto.User;

public class CreateUserDto
{
  [Required] public required string name { get; init; }
  [Required] public required string username { get; init; }
  [Required, EmailAddress] public required string email { get; init; }
}
