namespace Forum.Application.MainUsers
{
    public class UserAdminResponseModel
    {
        public int Id { get; set; }
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; } 
        public bool IsBanned { get; set; } 

    }
}
