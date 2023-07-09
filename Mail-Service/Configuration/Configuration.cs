namespace Mail_Service.Configuration
{
    public class MailSettings
    {
        public string? DisplayName { get; set; }
        public string? From { get; set; }
        public string? Password { get; set; }
        public string? Host { get; set; }
        public string? Port { get; set; }
        public bool UseSSL { get; set; }
        public bool UseStartTls { get; set; }
    }
}
