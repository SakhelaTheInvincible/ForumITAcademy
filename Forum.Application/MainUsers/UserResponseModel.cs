using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.MainUsers
{
    public class UserResponseModel
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public int CommentsOnOtherTopicsCount { get; set; }

    }
}
