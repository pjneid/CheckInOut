namespace CheckInOut.DAL.Models
{
    public class ConfigurationEmail
    {
        public int Id { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public string SmtpClient { get; set; }
        public int Port { get; set; }
        public bool EnableSsl { get; set; }
        public string CcEmails { get; set; }
        public int CompanyId { get; set; }

        public virtual Company Company { get; set; }
    }
}
