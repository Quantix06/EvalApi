using System.ComponentModel.DataAnnotations;
using EvalApi.Src.Models.Post;

namespace EvalApi.Src.Models.User;

public class UserModel
{
  [Required, Range(1, int.MaxValue)] public int Id { get; set; }
  [Required] public required string Name { get; set; }
  [Required] public required string Username { get; set; }
  [Required, EmailAddress] public required string Email { get; set; }
  public List<PostModel>? Posts { get; set; }
}
