using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace FMB.Services.Comments.Models.Entities;
public class Comment
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }
    public string Body { get; set; } = string.Empty;
    [Required]
    public int UserId { get; set; }
    public Comment? ParentComment { get; set; }
    public int Rate { get; set; }
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public DateTime Timestamp { get; set; }
    public List<CommentMark>? Marks { get; set; }
}
