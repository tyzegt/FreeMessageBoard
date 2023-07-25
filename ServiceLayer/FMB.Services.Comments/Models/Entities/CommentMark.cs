using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FMB.Services.Comments.Models.Entities;
public class CommentMark
{
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    [Required]
    public int Id { get; set; }
    [Required]
    public int CommentId { get; set; }
    [Required]
    public int UserId { get; set; }
    public string Mark { get; set; } = string.Empty;
    public Comment Comment { get; set; } = null!;
}
