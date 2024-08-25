namespace Forum.Application.Exceptions
{
    public class LoginException : Exception
    {
        public LoginException() : base() { }
        public LoginException(string message) : base(message) { }

        public readonly string Code = "Wrong UserName Or Password";
    }
}
