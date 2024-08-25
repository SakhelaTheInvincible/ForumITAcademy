namespace Forum.Application.Exceptions
{
    public class EmailAlreadyExistsException : Exception
    {
        public EmailAlreadyExistsException() : base() { }
        public EmailAlreadyExistsException(string message) : base(message) { }

        public readonly string Code = "Email Must Be Unique";
    }
}
