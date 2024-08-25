using Forum.Domain.user;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Forum.Persistence.Configurations
{
    public class UserConfiguration :IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder) { 
            builder.HasKey(x => x.Id);
            builder.HasIndex(x => x.UserName).IsUnique();
            builder.Property(x => x.UserName).IsUnicode(false).IsRequired().HasMaxLength(15);
            builder.HasIndex(x => x.Email).IsUnique();
            builder.Property(x => x.Email).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.ModifiedAt).IsRequired().HasColumnType("datetime");
            builder.HasMany(x => x.Topics).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId).OnDelete(DeleteBehavior.NoAction);
            builder.HasMany(x => x.Comments).WithOne(t => t.Author).HasForeignKey(t => t.AuthorId).OnDelete(DeleteBehavior.Cascade);

        }
    }
}
