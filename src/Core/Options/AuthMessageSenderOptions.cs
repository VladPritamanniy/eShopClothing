namespace Core.Options;

public class AuthMessageSenderOptions
{
    public string? FromEmail { get; set; }
    public string? FromEmailPassword { get; set; }
    public string? FromDisplayName { get; set; }
    public string? SmtpHost { get; set; }
    public int SmtpPort { get; set; }
    public bool UseDefaultCredentials { get; set; }
}
