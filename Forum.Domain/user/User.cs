using Forum.Domain.comment;
using Forum.Domain.topic;
using Microsoft.AspNetCore.Identity;
using static Forum.Domain.enums.Enums;


namespace Forum.Domain.user
{
    public class User : IdentityUser<int>
    {
        public DateTime CreatedAt { get; set; }
        public DateTime ModifiedAt { get; set; }
        public bool IsBanned { get; set; } = false;

        // Navigation property
        public List<Topic>? Topics { get; set; }
        public List<Comment>? Comments { get; set; }

       
        
    }
}
