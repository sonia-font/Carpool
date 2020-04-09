using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IOfferService
    {
        RideOffer PostOffer(string userName, DateTime rideDate, string destiny, string origin, string comment, int seats);
        RideOffer ViewOffer(int offerId);
        List<RideOffer> ViewOfferList();
        List<RideOffer> ViewOfferList(string userName);
        List<RideOffer> ViewOfferList(string userName, OfferStatus offerStatus);        
        RideOffer UpdateOffer(int offerId, string destiny, string origin, string comment);
        RideOffer AcceptRequest(int offerId, int requestId);
        void RejectRequest(int requestId);
        void AcceptChanges(int requestId);
        void RejectChanges(int offerId, int requestId);
        void CancelOffer(int offerId);
        void CancelRequest(int offerId, int requestId);
    }
}