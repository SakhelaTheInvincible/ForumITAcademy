using Forum.Application.MainComments;
using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Infrastructure.Comments;
using Forum.Infrastructure.Topics;
using Forum.Infrastructure.Users;

namespace ForumWebApp.Infrastructure.Extensions
{
    public static class ServiceExtensions
    {
        public static void AddServices(this IServiceCollection services) {
            services.AddScoped<IUserService, UserServices>();
            services.AddScoped<IUserRepository, UserRepository>();

            services.AddScoped<ITopicService, TopicService>();
            services.AddScoped<ITopicRepository, TopicRepository>();

            services.AddScoped<ICommentService, CommentService>();
            services.AddScoped<ICommentRepository, CommmentRepository>();
        }
    }
}
