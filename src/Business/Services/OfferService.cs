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
    public class OfferService : IOfferService
    {
        IOfferRepository offerRepository;
        INotificationService notificationService;
        IRequestService requestService;
        IUserRepository userRepository;

        public OfferService(IOfferRepository offerRepository, INotificationService notificationService, IRequestService requestService, IUserRepository userRepository)
        {
            this.offerRepository = offerRepository;
            this.notificationService = notificationService;
            this.requestService = requestService;
            this.userRepository = userRepository;

            GenerateTestData();
        }

        private void GenerateTestData()
        {
            PostOffer(userName: "admin", DateTime.Now.Date, destiny: "Belgrano", origin: "Saavedra", comment: "algo", seats: 4);
            PostOffer(userName: "admin", DateTime.Now.Date, destiny: "Tu casa", origin: "La otra casa", comment: "tuvieja", seats: 4);
            PostOffer(userName: "admin", DateTime.Now.Date, destiny: "Suiza", origin: "Coghlan", comment: "que bajon", seats: 4);
            PostOffer(userName: "admin", DateTime.Now.Date, destiny: "Gerli", origin: "VicenteLopez", comment: "camino de ida", seats: 4);
            PostOffer(userName: "otro", DateTime.Now.Date, destiny: "Salta", origin: "Tucuman", comment: "otro tipo", seats: 4);
        }
    

        public void CancelRequest(int offerId, int requestId)
        {
            OfferData offerData = offerRepository.RetrieveOffer(offerId);
            RideRequest rideRequest = requestService.GetRequest(requestId);

            if (offerData == null)
            {
                throw new Exception("La offer no existe");
            }
            if (rideRequest == null)
            {
                throw new Exception("El request no existe");
            }

            if (rideRequest.Status == RequestStatus.RequestAccepted || rideRequest.Status == RequestStatus.ChangesAccepted)
            {
                if (offerData.AvailableSeats == offerData.Seats)
                {
                    throw new Exception("Usted no estaba en la offer");
                }
                
                offerData.AvailableSeats = offerData.AvailableSeats + 1;                     
                
                if (offerData.Status == OfferStatus.Full.ToString() && offerData.AvailableSeats == 1)
                {
                    offerData.Status = OfferStatus.Active.ToString();
                }

                offerRepository.UpdateOffer(offerData);
            }            

            rideRequest = requestService.ChangeStatus(requestId, RequestStatus.RequestCancelled);

            notificationService.SendNotification("Request Cancelled", rideRequest.User.UserName, offerData.UserData.UserName);
        }

        public void AcceptChanges(int requestId)
        {
            RideRequest rideRequest = requestService.ChangeStatus(requestId, RequestStatus.ChangesAccepted);

            notificationService.SendNotification("Changes Accepted", rideRequest.User.UserName, rideRequest.RideOffer.User.UserName);
        }

        public void CancelOffer(int offerId)
        {
            OfferData offerData = offerRepository.RetrieveOffer(offerId);

            if (offerData == null)
            {
                throw new Exception("La offer no existe");
            }

            offerData.Status = OfferStatus.Cancelled.ToString();

            offerRepository.UpdateOffer(offerData);
            

            List<RideRequest> requests = requestService.ViewRequestList(offerId);

            foreach (RideRequest request in requests)
            {
                requestService.ChangeStatus(request.Id, RequestStatus.OfferCancelled);

                notificationService.SendNotification("Offer cancelled", offerData.UserData.UserName, request.User.UserName);
            }
        }

        public RideOffer AcceptRequest(int offerId, int requestId)
        {
            OfferData offerData = offerRepository.RetrieveOffer(offerId);

            if (offerData == null)
            {
                throw new Exception("La offer no existe");
            }

            if (offerData.AvailableSeats == 0)
            {
                throw new Exception("El ride esta full");                                    
            }  
            
            offerData.AvailableSeats = offerData.AvailableSeats - 1;

            if (offerData.AvailableSeats == 0)
            {
                offerData.Status = OfferStatus.Full.ToString();
            }

            offerRepository.UpdateOffer(offerData);
            

            RideRequest rideRequest = requestService.ChangeStatus(requestId, RequestStatus.RequestAccepted);

            notificationService.SendNotification("Request Accepted", offerData.UserData.UserName, rideRequest.User.UserName);

            return GetOffer(offerData);
        }

        public void RejectRequest(int requestId)
        {
            RideRequest rideRequest = requestService.ChangeStatus(requestId, RequestStatus.RequestRejected);

            notificationService.SendNotification("Request Rejected", rideRequest.RideOffer.User.UserName, rideRequest.User.UserName);
        }

        public RideOffer UpdateOffer(int offerId, string destiny, string origin, string comment)
        {
            if (string.IsNullOrEmpty(destiny))
            {
                throw new ArgumentNullException(nameof(destiny));
            }
            if (string.IsNullOrEmpty(origin))
            {
                throw new ArgumentNullException(nameof(origin));
            }

            OfferData offerData = offerRepository.RetrieveOffer(offerId);

            if (offerData == null)
            {
                throw new Exception("La oferta no existe");
            }

            offerData.Comment = comment;
            offerData.Destiny = destiny;
            offerData.Origin = origin;                            

            offerRepository.UpdateOffer(offerData);

            List<RideRequest> requests = requestService.ViewRequestList(offerId);                

            foreach (RideRequest request in requests)
            {
                if (request.Status == RequestStatus.RequestAccepted)
                {
                    requestService.ChangeStatus(request.Id, RequestStatus.OfferUpdated);

                    notificationService.SendNotification("Offer Updated", offerData.UserData.UserName, request.User.UserName);
                }                    
            }            

            return GetOffer(offerData);
        }        

        public RideOffer PostOffer(string userName, DateTime rideDate, string destiny, string origin, string comment, int seats) 
        {
            if (string.IsNullOrEmpty(destiny))
            {
                throw new ArgumentNullException(nameof(destiny));
            }
            if (string.IsNullOrEmpty(origin))
            {
                throw new ArgumentNullException(nameof(origin));
            }
            if (rideDate < DateTime.Now.Date)
            {
                throw new ArgumentException("El viaje no puede ser en el pasado", nameof(rideDate));
            }
            if (seats <= 0 || seats > 4) //TODO: mejorar esto
            {
                throw new ArgumentException("Cantidad de asientos invalido", nameof(seats));
            }

            OfferData data = new OfferData();

            data.AvailableSeats = seats;
            data.Comment = comment;
            data.Destiny = destiny;            
            data.OfferDate = DateTime.Now;
            data.Origin = origin;
            data.RideDate = rideDate;
            data.Seats = seats;
            data.Status = OfferStatus.Active.ToString();

            UserData userData = userRepository.RetrieveUser(userName);

            if (userData == null)
            {
                throw new Exception(string.Format("El usuario {0} no existe", userName));
            }

            data.UserData = userData;

            offerRepository.CreateOffer(data);

            return GetOffer(data);
        }

        public void RejectChanges(int offerId, int requestId)
        {
            OfferData offerData = offerRepository.RetrieveOffer(offerId);

            if (offerData == null)
            {
                throw new Exception("La offer no existe");
            }
            if (offerData.AvailableSeats == offerData.Seats)
            {
                throw new Exception("Usted no estaba en la offer");                     
            }

            offerData.AvailableSeats = offerData.AvailableSeats + 1;

            if (offerData.Status == OfferStatus.Full.ToString() && offerData.AvailableSeats == 1)
            {
                offerData.Status = OfferStatus.Active.ToString();
            }

            offerRepository.UpdateOffer(offerData);                
            

            RideRequest rideRequest = requestService.ChangeStatus(requestId, RequestStatus.ChangesRejected);

            notificationService.SendNotification("Changes Rejected", rideRequest.User.UserName, offerData.UserData.UserName);
        }

        public RideOffer ViewOffer(int offerId)
        {
            OfferData offerData = offerRepository.RetrieveOffer(offerId); 

            if (offerData == null)
            {
                throw new Exception("El offer no existe");
            }

            return GetOffer(offerData);
        }

        public List<RideOffer> ViewOfferList()
        {
            List<OfferData> data = offerRepository.RetrieveOffers();

            return GetOffers(data);
        }

        public List<RideOffer> ViewOfferList(string userName) 
        {
            List<OfferData> data = offerRepository.RetrieveOffers(userName); 

            return GetOffers(data);
        }

        public List<RideOffer> ViewOfferList(string userName, OfferStatus offerStatus)
        {
            List<OfferData> data = offerRepository.RetrieveOffers(userName);

            List<RideOffer> offers = new List<RideOffer>();
            foreach (OfferData offerData in data)
            {
                if (offerData.Status == offerStatus.ToString())
                {
                    offers.Add(GetOffer(offerData));
                }
            }

            return offers;
        }


        private List<RideOffer> GetOffers(List<OfferData> data)
        {
            List<RideOffer> offers = new List<RideOffer>();
            foreach (OfferData offerData in data)
            {
                offers.Add(GetOffer(offerData));
            }

            return offers;

        }
        private RideOffer GetOffer(OfferData data)
        {
            RideOffer rideOffer = new RideOffer();
            
            rideOffer.AvailableSeats = data.AvailableSeats;
            rideOffer.Comment = data.Comment;
            rideOffer.Destiny = data.Destiny;
            rideOffer.Id = data.Id;
            rideOffer.OfferDate = data.OfferDate;
            rideOffer.Origin = data.Origin;
            rideOffer.RideDate = data.RideDate;
            rideOffer.Seats = data.Seats;
            rideOffer.Status = (OfferStatus)Enum.Parse(typeof(OfferStatus), data.Status);

            User user = new User();

            user.Email = data.UserData.Email;
            user.FullName = data.UserData.FullName;
            user.UserName = data.UserData.UserName;

            rideOffer.User = user;

            return rideOffer;
        }
    }
}

