using Business.Entities;
using Business.Interfaces;
using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Services
{
    public class RequestService : IRequestService
    {
        IRequestRepository requestRepository;
        IUserRepository userRepository;
        IOfferRepository offerRepository;
        INotificationService notificationService;       

        public RequestService(IRequestRepository requestRepository, IUserRepository userRepository, IOfferRepository offerRepository, INotificationService notificationService)
        {
            this.requestRepository = requestRepository;
            this.userRepository = userRepository;
            this.offerRepository = offerRepository;
            this.notificationService = notificationService;            
        }

        public void RequestRide(int offerId, string userName) 
        {
            RequestData data = new RequestData();

            data.Date = DateTime.Now;

            data.Status = RequestStatus.RequestSubmitted.ToString();

            UserData userData = userRepository.RetrieveUser(userName); 

            if (userData == null)
            {
                throw new Exception(string.Format("El usuario {0} no existe", userName));
            }

            data.UserData = userData;

            OfferData offerData = offerRepository.RetrieveOffer(offerId);

            if (offerData == null)
            {
                throw new Exception("La oferta no existe");
            }

            data.OfferData = offerData;

            requestRepository.CreateRequest(data);

            notificationService.SendNotification("New Request", userData.UserName, offerData.UserData.UserName);
        }

        public RideRequest GetRequest(int requestId)
        {
            RequestData requestData = requestRepository.RetrieveRequest(requestId);
            if (requestData == null)
            {
                throw new Exception("El request no existe");
            }

            return GetRequest(requestData);    
        }

        public RideRequest ChangeStatus(int requestId, RequestStatus requestStatus)
        {
            RequestData requestData = requestRepository.RetrieveRequest(requestId);

            if (requestData == null)
            {
                throw new Exception("El request no existe");
            }

            requestData.Status = requestStatus.ToString();

            requestRepository.UpdateRequest(requestData);

            return GetRequest(requestData);
        }

        public List<RideRequest> ViewRequestList(string userName)
        {
            List<RequestData> data = requestRepository.RetrieveRequests(userName);

            return GetRequests(data);
        }

        public List<RideRequest> ViewRequestList(int offerId)
        {
            List<RequestData> data = requestRepository.RetrieveRequests(offerId);

            return GetRequests(data);
        }


        private List<RideRequest> GetRequests(List<RequestData> data)
        {
            List<RideRequest> requests = new List<RideRequest>();
            foreach (RequestData requestData in data)
            {
                requests.Add(GetRequest(requestData));
            }

            return requests;
        }

        private RideRequest GetRequest(RequestData requestData)
        {
            RideRequest request = new RideRequest();

            request.Id = requestData.Id;
            request.Date = requestData.Date;
            request.Status = (RequestStatus)Enum.Parse(typeof(RequestStatus), requestData.Status);

            User user = new User();

            user.Email = requestData.UserData.Email;
            user.FullName = requestData.UserData.FullName;            
            user.UserName = requestData.UserData.UserName;

            request.User = user;

            RideOffer rideOffer = new RideOffer();

            rideOffer.AvailableSeats = requestData.OfferData.AvailableSeats;
            rideOffer.Comment = requestData.OfferData.Comment;
            rideOffer.OfferDate = requestData.OfferData.OfferDate;
            rideOffer.RideDate = requestData.OfferData.RideDate;
            rideOffer.Destiny = requestData.OfferData.Destiny;
            rideOffer.Id = requestData.OfferData.Id;
            rideOffer.Origin = requestData.OfferData.Origin;
            rideOffer.Seats = requestData.OfferData.Seats;
            rideOffer.Status = (OfferStatus)Enum.Parse(typeof(OfferStatus), requestData.OfferData.Status);

            User driver = new User();

            driver.Email = requestData.OfferData.UserData.Email;
            driver.FullName = requestData.OfferData.UserData.FullName;
            driver.UserName = requestData.OfferData.UserData.UserName;

            rideOffer.User = driver;

            request.RideOffer = rideOffer;

            return request;
        }
    }
}
