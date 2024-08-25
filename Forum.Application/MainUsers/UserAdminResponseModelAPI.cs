namespace Forum.Application.MainUsers
{
    public class UserAdminResponseModelAPI
    {
        public string? UserName { get; set; }
        public string? Email { get; set; }
        public DateTime CreatedAt { get; set; }
        public bool IsBanned { get; set; }

    }
}
