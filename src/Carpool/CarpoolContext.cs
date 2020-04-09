using Business.Interfaces;
using Business.Services;
using Data.Interfaces;
using Data.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Carpool
{
    public class CarpoolContext
    {
        private static CarpoolContext instance;

        private CarpoolContext() 
        {
            INotificationRepository notificationDB = new NotificationMemoryRepository();
            IOfferRepository offerDB = new OfferMemoryRepository();
            IRequestRepository requestDB = new RequestMemoryRepository();
            IUserRepository userDB = new UserMemoryRepository();

            LoginService = new LoginService(userDB);
            NotificationService = new NotificationService(notificationDB, userDB);
            RequestService = new RequestService(requestDB, userDB, offerDB, NotificationService);
            OfferService = new OfferService(offerDB, NotificationService, RequestService, userDB);
        }

        public ILoginService LoginService { get; private set; }
        public INotificationService NotificationService { get; private set; }
        public IRequestService RequestService { get; private set; }
        public IOfferService OfferService { get; private set; }

        public static CarpoolContext Instance //prop readonly
        {
            get
            {
                if (instance == null)
                {
                    instance = new CarpoolContext();
                }
                return instance;
            }
        }


        /*public static CarpoolContext GetInstance()
        {
            if (instance == null)
            {
                instance = new CarpoolContext();                
            }            
            return instance;
        }*/   

        
    }
}