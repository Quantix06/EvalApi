namespace EvalApi.Src.Views.Dto.Post;

public class PostDto
{
    public int id { get; set; }
    public int userId { get; set; }
    public string title { get; set; } = string.Empty;
    public string body { get; set; } = string.Empty;
}
