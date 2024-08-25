using Forum.Application.MainTopics;
using Forum.Domain.user;

namespace Forum.Application.MainUsers
{
    public class UserPageResponseModel
    {
        public User? User {  get; set; }
        public UserResponseModel? UserResponse { get; set; }
        public List<TopicResponseModel>? Topics { get; set; }

    }
}
