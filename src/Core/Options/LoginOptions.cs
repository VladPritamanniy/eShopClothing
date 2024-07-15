namespace Core.Options
{
    public class LoginOptions
    {
        public int MaxFailedAccessAttempts { get; set; }
        public int BlockingInMinutes { get; set; }
    }
}
