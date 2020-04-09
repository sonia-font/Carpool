using Business.Entities;
using Business.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Carpool.Controllers
{
    public class NotificationsController : Controller
    {
        IOfferService offerService;
        INotificationService notificationService;
        ILoginService loginService;
        IRequestService requestService;

        public NotificationsController()
        {
            offerService = CarpoolContext.Instance.OfferService;
            notificationService = CarpoolContext.Instance.NotificationService;
            loginService = CarpoolContext.Instance.LoginService;
            requestService = CarpoolContext.Instance.RequestService;
        }

        public ActionResult MyList()
        {
            List<Notification> notifications = notificationService.GetNotifications(loginService.CurrentUser.UserName);

            return View(notifications);
        }
    }
}