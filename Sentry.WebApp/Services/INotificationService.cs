using System.Collections.Generic;
using System.Threading.Tasks;

namespace Sentry.WebApp.Services
{
    public interface INotificationService //<T> //where T : class
    {
        Task SendNotificationAsync<T>(IEnumerable<Data.Models.Notifications.SendTo> sendToList, IEnumerable<Data.Models.Notifications.SendTo> ccList, string templateId, T notificationDetails);
    }
}
