using Amazon;
using Amazon.SimpleEmail;
using Amazon.SimpleEmail.Model;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.Extensions.Logging;

namespace IdentityServer.Services
{
    public class EmailSender : IEmailSender
    {
        
        private readonly ILogger<EmailSender> _logger;
        public EmailSender(IOptions<EmailAuthOptions> optionsAccessor, ILogger<EmailSender> logger)
        {
            Options = optionsAccessor.Value;
            _logger = logger;
        }

        public EmailAuthOptions Options { get; } //set only via Secret Manager

        public Task SendEmailAsync(string email, string subject, string message)
        {
            _logger.LogDebug("Send email async");
            return Execute(email, subject, message);
        }

        public Task Execute( string email, string subject, string message)
        {
            
            _logger.LogDebug("Send email execute");
            Task response = null;

            using (var client = new AmazonSimpleEmailServiceClient(Options.AWSAccessKeyId, Options.AWSSecretAccessKey, RegionEndpoint.USEast1))
            {
                var replyToAddresses = new List<string>();
                replyToAddresses.Add(Options.EmailReplyto);

                var destinationAddresses = new List<string>();
                destinationAddresses.Add(email);


                SendEmailRequest sendRequest = new SendEmailRequest
                {
                    Source = Options.EmailFrom,
                    ReplyToAddresses = replyToAddresses,
                    Destination = new Destination
                    {
                        ToAddresses = destinationAddresses
                    },
                    Message = new Message
                    {
                        Subject = new Content(subject),
                        Body = new Body
                        {
                            Html = new Content
                            {
                                Charset = "UTF-8",
                                Data = message
                            },
                            Text = new Content
                            {
                                Charset = "UTF-8",
                                Data = message
                            }
                        }
                    }
                };

                try
                {
                    _logger.LogInformation("Sending email using Amazon SES...");
                    response = client.SendEmailAsync(sendRequest);
                    _logger.LogInformation("The email was sent successfully.");
                }
                catch (Exception ex)
                {
                    _logger.LogWarning("The email was not sent.");
                    _logger.LogError("Error message: " + ex.Message);
                }
            }

            return response;
        }
    }
}
