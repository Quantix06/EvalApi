using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto.Post;

public class CreatePostDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int userId { get; set; }

    [Required]
    public string title { get; set; } = string.Empty;

    [Required]
    public string body { get; set; } = string.Empty;
}
