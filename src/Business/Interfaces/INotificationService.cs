using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface INotificationService
    {
        void SendNotification(string message, string sender, string receiver);
        List<Notification> GetNotifications(string userName);
        List<Notification> GetNotifications(string userName, NotificationStatus status);        
    }
}
