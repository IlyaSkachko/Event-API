namespace Events.Application.Exceptions
{
    public class AlreadyExistException : Exception
    {
        public AlreadyExistException() : base("Already exist") { }
        public AlreadyExistException(string message) : base(message) { }
    }
}
