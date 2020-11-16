namespace IdentityServer.Services
{
    public class EmailAuthOptions
    {
        public string AWSAccessKeyId { get; set; }
        public string AWSSecretAccessKey { get; set; }
        public string AWSRegionEndpoint { get; set; }
        public string EmailFrom { get; set; }
        public string EmailReplyto { get; set; }
        public string EmailFromName { get; set; }
    }
}