using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface INotificationRepository
    {
        bool CreateNotification(NotificationData notificationData);
        List<NotificationData> RetrieveNotifications(string userName);
        bool UpdateNotification(NotificationData notificationData);
        bool DeleteNotification(NotificationData notificationData);
    }
}
