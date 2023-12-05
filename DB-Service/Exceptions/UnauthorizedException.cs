namespace DB_Service.Exceptions
{
    public class UnauthorizedException : Exception
    {
        public int StatusCode { get; }
        public UnauthorizedException(string message) : base(message)
        {
            StatusCode = 401;
        }
    }
}
