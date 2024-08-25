using Forum.Application.MainTopics;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.MainUsers
{
    public class UserPageResponseModelAPI
    {
        public UserResponseModel? User { get; set; }
        public List<TopicResponseModelAPI>? Topics { get; set; }

    }
}
