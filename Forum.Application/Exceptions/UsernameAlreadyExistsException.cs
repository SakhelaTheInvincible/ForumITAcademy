namespace Forum.Application.Exceptions
{
    public class UserNameAlreadyExistsException : Exception
    {
        public UserNameAlreadyExistsException() : base() { }
        public UserNameAlreadyExistsException(string message) : base(message) { }

        public readonly string Code = "UserName Must Be Unique";
    }
}
