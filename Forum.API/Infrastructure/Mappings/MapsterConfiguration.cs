using Forum.Application.MainComments;
using Forum.Application.MainTopics;
using Forum.Application.MainUsers;
using Forum.Domain.comment;
using Forum.Domain.topic;
using Forum.Domain.user;
using Mapster;

namespace Forum.API.Infrastructure.Mappings
{
    public static class MapsterConfiguration
    {
        public static void RegisterMaps(this IServiceCollection services) {
            TypeAdapterConfig<CommentCreateModel, Comment>
                 .NewConfig()
                 .Map(dest => dest.CreatedAt, src => DateTime.Now)
                 .Map(dest => dest.ModifiedAt, src => DateTime.Now)
                 .TwoWays();

            TypeAdapterConfig<Comment, CommentResponseModel>
                 .NewConfig()
                 .Map(dest => dest.UserName, src => src.Author!.UserName)
                 .TwoWays();

            TypeAdapterConfig<List<Comment>, List<CommentResponseModel>>
                 .NewConfig()
                 .TwoWays();

            TypeAdapterConfig<CommentUpdateModel, Comment>
                 .NewConfig()
                 .Map(dest => dest.ModifiedAt, src => DateTime.Now)
                 .TwoWays();




            TypeAdapterConfig<TopicCreateModel, Topic>
                 .NewConfig()
                 .Map(dest => dest.CreatedAt, src => DateTime.Now)
                 .Map(dest => dest.ModifiedAt, src => DateTime.Now)
                 .TwoWays();

            TypeAdapterConfig<TopicUpdateModel, Topic>
                 .NewConfig()
                 .Map(dest => dest.ModifiedAt, src => DateTime.Now)
                 .TwoWays();


            TypeAdapterConfig<Topic, TopicResponseModel>
                 .NewConfig()
                 .Map(dest => dest.Status, src => src.Status.ToString())
                 .Map(dest => dest.UserName, src => src.Author!.UserName)
                 .Map(dest => dest.CommentCount, src => src.Comments!.Count)
                 .TwoWays();


            TypeAdapterConfig<Topic, TopicAdminResponseModel>
                 .NewConfig()
                 .Map(dest => dest.State, src => src.State.ToString())
                 .Map(dest => dest.Status, src => src.Status.ToString())
                 .Map(dest => dest.UserName, src => src.Author!.UserName)
                 .Map(dest => dest.CommentCount, src => src.Comments!.Count)
                 .TwoWays();


            TypeAdapterConfig<List<Topic>, List<TopicResponseModel>>
                 .NewConfig()
                 .TwoWays();

            TypeAdapterConfig<List<Topic>, List<TopicAdminResponseModel>>
                 .NewConfig()
                 .TwoWays();





            TypeAdapterConfig<UserCreateModel, User>
                 .NewConfig()
                 .Map(dest => dest.NormalizedEmail, src => src.Email!.ToUpper())
                 .Map(dest => dest.NormalizedUserName, src => src.UserName!.ToUpper())
                 .Map(dest => dest.CreatedAt, src => DateTime.Now)
                 .Map(dest => dest.ModifiedAt, src => DateTime.Now)
                 .TwoWays();

            TypeAdapterConfig<UserUpdateModel, User>
                 .NewConfig()
                 .Map(dest => dest.NormalizedEmail, src => src.Email!.ToUpper())
                 .Map(dest => dest.NormalizedUserName, src => src.UserName!.ToUpper())
                 .Map(dest => dest.ModifiedAt, src => DateTime.Now);


            TypeAdapterConfig<List<User>, List<UserResponseModel>>
                 .NewConfig()
                 .TwoWays();



        }
    }
}
