namespace DB_Service.Exceptions
{
    public class ConflictException : Exception
    {
        public int StatusCode { get; }

        public ConflictException(string message) : base(message) 
        {
            StatusCode = 409;
        }
    }
}
