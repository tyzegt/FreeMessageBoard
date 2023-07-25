using FMB.Services.Comments.Models.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FMB.Services.Comments.Data.Configurations;
public class CommentMarkConfiguration : IEntityTypeConfiguration<CommentMark>
{
    public void Configure(EntityTypeBuilder<CommentMark> builder)
    {
        builder.HasKey(c => c.Id);
        builder.HasIndex(c => new { c.UserId, c.CommentId }).IsUnique();

        builder.HasOne(c => c.Comment)
            .WithMany(s => s.Marks)
            .HasPrincipalKey(s => s.Id)
            .HasForeignKey(h => h.CommentId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
