using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class NotificationMemoryRepository : INotificationRepository
    {
        Dictionary<int, NotificationData> notifications = new Dictionary<int, NotificationData>();

        public bool CreateNotification(NotificationData notificationData)
        {
            if (notificationData.Id == 0)
            {
                notificationData.Id = notifications.Count() + 1;

                notifications.Add(notificationData.Id, notificationData);                
            }

            return notifications.ContainsKey(notificationData.Id);
        }

        public bool DeleteNotification(NotificationData notificationData)
        {
            if (notifications.ContainsKey(notificationData.Id))
            {
                notifications.Remove(notificationData.Id);                
            }

            return !notifications.ContainsKey(notificationData.Id);
        }

        public List<NotificationData> RetrieveNotifications(string userName)
        {
            List<NotificationData> notificationsUser = new List<NotificationData>();

            foreach (NotificationData notification in notifications.Values)
            {
                if (notification.Receiver.UserName == userName)
                {
                    notificationsUser.Add(notification);
                }
            }

            return notificationsUser;
        }

        public bool UpdateNotification(NotificationData notificationData)
        {
            if (notifications.ContainsKey(notificationData.Id))
            {
                notifications[notificationData.Id] = notificationData;                
            }

            return true;
        }
    }
}
