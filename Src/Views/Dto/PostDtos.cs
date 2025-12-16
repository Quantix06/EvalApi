using System.ComponentModel.DataAnnotations;

namespace EvalApi.Src.Views.Dto;

public class PostDto
{
    public int id { get; set; }
    public int userId { get; set; }
    public string title { get; set; } = string.Empty;
    public string body { get; set; } = string.Empty;
}

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

public class UpdatePostDto
{
    [Required]
    [Range(1, int.MaxValue)]
    public int id { get; set; }

    [Required]
    [Range(1, int.MaxValue)]
    public int userId { get; set; }

    [Required]
    public string title { get; set; } = string.Empty;

    [Required]
    public string body { get; set; } = string.Empty;
}
