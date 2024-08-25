using Forum.Domain.comment;
using Forum.Domain.topic;
using Forum.Domain.user;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Forum.Persistence.Identity
{
    public class ForumManagementIdentityContext : IdentityDbContext<User, IdentityRole<int>, int>
    {
        public ForumManagementIdentityContext(DbContextOptions<ForumManagementIdentityContext> options) : base(options)
        {

        }

        public DbSet<Comment> Comment { get; set; }
        public DbSet<Topic> Topic { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ForumManagementIdentityContext).Assembly);
        }

    }
}


