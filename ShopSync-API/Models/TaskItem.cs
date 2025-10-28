namespace ShopSync_API.Models;

public class TaskItem
{
    public int Id { get; set; }
    public string? Name { get; set; }
    public string? Description { get; set; }
    public DateTime? CreatedAt { get; set; } = DateTime.Now;
}
