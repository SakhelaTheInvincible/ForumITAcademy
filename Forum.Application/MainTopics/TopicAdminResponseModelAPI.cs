using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Forum.Application.MainTopics
{
    public class TopicAdminResponseModelAPI
    {
        public string? Title { get; set; }
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }
        public string? UserName { get; set; }
        public string? Status { get; set; }
        public string? State { get; set; }

    }
}
