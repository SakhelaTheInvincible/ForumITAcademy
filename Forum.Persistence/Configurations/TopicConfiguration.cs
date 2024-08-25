using Forum.Domain.enums;
using Forum.Domain.topic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Forum.Persistence.Configurations
{
    public  class TopicConfiguration : IEntityTypeConfiguration<Topic>  
    {
        public void Configure(EntityTypeBuilder<Topic> builder) { 
            builder.HasKey(x => x.Id);
            builder.Property(x => x.Title).IsRequired().HasMaxLength(20);
            builder.Property(x => x.Content).IsRequired();
            builder.Property(x => x.CreatedAt).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.ModifiedAt).IsRequired().HasColumnType("datetime");
            builder.Property(x => x.Status).IsRequired().HasConversion(v => v.ToString(), v => (Enums.Status)Enum.Parse(typeof(Enums.Status), v));
            builder.Property(x => x.State).IsRequired().HasConversion(v => v.ToString(), v => (Enums.State)Enum.Parse(typeof(Enums.State), v));
        }

    }
}
