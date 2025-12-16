using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto;

public class UserDto
{
  [Required, Range(1, int.MaxValue)] public required int id { get; init; }
  [Required] public required string name { get; init; }
  [Required] public required string username { get; init; }
  [Required, EmailAddress] public required string email { get; init; }
}

public class CreateUserDto
{
  [Required] public required string name { get; init; }
  [Required] public required string username { get; init; }
  [Required, EmailAddress] public required string email { get; init; }
}
