using Business.Entities;
using Business.Interfaces;
using Data;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class NotificationService : INotificationService
    {
        INotificationRepository notificationRepository;
        IUserRepository userRepository;

        public NotificationService(INotificationRepository notificationRepository, IUserRepository userRepository)
        {
            this.notificationRepository = notificationRepository;
            this.userRepository = userRepository;

            SendNotification("te odio", "otro", "admin");
        }

        public List<Notification> GetNotifications(string userName) 
        {         
            List<NotificationData> data = notificationRepository.RetrieveNotifications(userName);
            List<Notification> notifications = new List<Notification>();            

            foreach (NotificationData notificationData in data)
            {
                Notification notification = new Notification();

                notification.Date = notificationData.Date;                
                notification.Message = notificationData.Message;
                notification.Status = (NotificationStatus)Enum.Parse(typeof(NotificationStatus),notificationData.Status);

                User sender = new User();

                sender.Email = notificationData.Sender.Email;
                sender.FullName = notificationData.Sender.FullName;                
                sender.UserName = notificationData.Sender.UserName;

                notification.Sender = sender;

                User receiver = new User();

                receiver.Email = notificationData.Receiver.Email;
                receiver.FullName = notificationData.Receiver.FullName;                
                receiver.UserName = notificationData.Receiver.UserName;

                notification.Receiver = receiver;

                notifications.Add(notification);                
            }

            return notifications;
        }

        public List<Notification> GetNotifications(string userName, NotificationStatus status)
        {
            List<Notification> notificationResult = new List<Notification>();

            List<Notification> notifications = GetNotifications(userName);
            foreach (Notification notification in notifications)
            {
                if (notification.Status == status)
                {
                    notificationResult.Add(notification);
                }

            }
            return notificationResult;
        }

        public void SendNotification(string message, string sender, string receiver)
        {
            if (string.IsNullOrEmpty(message))
            {
                throw new ArgumentNullException(nameof(message));
            }            
            if (string.IsNullOrEmpty(receiver))
            {
                throw new ArgumentNullException(nameof(receiver));
            }

            NotificationData data = new NotificationData();

            data.Message = message;
            data.Date = DateTime.Now;            
            data.Status = NotificationStatus.Unread.ToString();

            UserData senderData = userRepository.RetrieveUser(sender); 

            if (senderData == null)
            {
                throw new Exception("El usuario no existe");
            }

            data.Sender = senderData;

            UserData receiverData = userRepository.RetrieveUser(receiver);

            if (receiverData == null)
            {
                throw new Exception("Destinatario no encontrado");
            }

            data.Receiver = receiverData;

            notificationRepository.CreateNotification(data);
        }
    }
}