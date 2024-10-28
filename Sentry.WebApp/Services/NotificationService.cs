using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using SendGrid.Helpers.Mail;
using SendGrid;
using Sentry.WebApp.Data.Models.Notifications;
using Microsoft.AspNetCore.Http;

namespace Sentry.WebApp.Services
{
    public class NotificationService : INotificationService
    {
        private readonly ILogger<NotificationService> _logger;
        private readonly Config _config;
        //private readonly HttpContext _context;

        public NotificationService(ILogger<NotificationService> logger,
            IOptions<Config> config)
            //HttpContextAccessor httpContextAccessor)
        {
            _logger = logger;
            _config = config.Value;
            //_context = httpContextAccessor.HttpContext;
        }

        private List<EmailAddress> GetDevelopersSendTo()
        {
            var sendTo = new List<EmailAddress>();
            foreach (var developer in _config.SendGrid.DeveloperSendTos)
            {
                sendTo.Add(new EmailAddress() { Email = developer.Email, Name = developer.Name });
            }
            return sendTo;
        }

        private List<EmailAddress> GetDevelopersCCs(IEnumerable<Data.Models.Notifications.SendTo> ccList)
        {
            var sendTo = new List<EmailAddress>();
            foreach (var developer in ccList)
            {
                sendTo.Add(new EmailAddress() { Email = developer.Email, Name = developer.Name });
            }
            return sendTo;
        }


        private List<EmailAddress> SetupSendTos(IEnumerable<Data.Models.Notifications.SendTo> sendToList)
        {
            var sendTo = new List<EmailAddress>();

            foreach (var email in sendToList)
            {
                sendTo.Add(new EmailAddress() { Email = email.Email, Name = email.Name });
            }

            return sendTo;
        }

        private List<EmailAddress> GetAdminSendTos()
        {
            //Add admins to BCCs
            var sendTo = new List<EmailAddress>();
            foreach (var developer in _config.SendGrid.AdminSendTos)
            {
                sendTo.Add(new EmailAddress() { Email = developer.Email, Name = developer.Name });
            }
            return sendTo;
        }


        private SendGridMessage SetupNotification(IEnumerable<Data.Models.Notifications.SendTo> sendToList, IEnumerable<Data.Models.Notifications.SendTo> ccList)
        {
            var message = new SendGridMessage();
            message.SetFrom(_config.SendGrid.From.Email, _config.SendGrid.From.Name);
            
            var bccs = GetAdminSendTos();
            if (bccs.Any())
            {
                message.AddBccs(bccs);
            }

            if (_config.SendGrid.Development)
            {
                message.AddTos(GetDevelopersSendTo());
                if (ccList != null && ccList.Any())
                {
                    message.AddCcs(GetDevelopersCCs(ccList));
                }
                return message;
            }

            message.AddTos(SetupSendTos(sendToList));
            if (ccList != null && ccList.Any())
            {
                message.AddCcs(SetupSendTos(ccList));
            }            

            return message;
        }

        /// <summary>
        /// Send a notification via email gateway (SendGrid)
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="sendToList"></param>
        /// <param name="ccList"></param>
        /// <param name="templateId"></param>
        /// <param name="notificationDetails"></param>
        /// <returns></returns>
        /// <exception cref="Exception"></exception>
        public async Task SendNotificationAsync<T>(IEnumerable<Data.Models.Notifications.SendTo> sendToList, 
            IEnumerable<Data.Models.Notifications.SendTo> ccList, string templateId, T notificationDetails)
        {
            try
            {
                var client = new SendGridClient(_config.SendGrid.ApiKey);                
                var message = SetupNotification(sendToList, ccList);
                message.SetTemplateData(notificationDetails);
                message.SetTemplateId(templateId);

                var response = await client.SendEmailAsync(message);
                if (!response.IsSuccessStatusCode)
                {
                    var body = await response.DeserializeResponseBodyAsync(response.Body);
                    //var error = body["error"];
                    throw new Exception($"SendGrid notification failed. TEMPLATE [{templateId}] Error [{response.StatusCode}] [{string.Join(Environment.NewLine, body)}]");
                }
            }
            catch (Exception ex)
            {
                throw new Exception($"Error setting up and sending notifications to [{String.Join(",", sendToList.Select(i => i.Email))}]. Error [{ex.Message}]");
            }
        }

    }
}
