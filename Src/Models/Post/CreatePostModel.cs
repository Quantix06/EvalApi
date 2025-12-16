using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Models.Post;

public class CreatePostModel
{
  [Required] public required string Title { get; set; }
  [Required] public required string Body { get; set; }
  [Required, Range(1, int.MaxValue)] public int UserId { get; set; }
}
