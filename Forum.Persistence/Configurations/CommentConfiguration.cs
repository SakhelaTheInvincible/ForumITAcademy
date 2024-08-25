using Forum.Domain.comment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Configurations
{
    public class CommentConfiguration : IEntityTypeConfiguration<Comment>
    {

        public void Configure(EntityTypeBuilder<Comment> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Content).IsUnicode(false).IsRequired();
            builder.Property(x => x.CreatedAt).HasColumnType("datetime");
            builder.Property(x => x.ModifiedAt).HasColumnType("datetime");
        }
    }
}
