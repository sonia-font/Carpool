using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IOfferRepository
    {
        bool CreateOffer(OfferData offerData);
        OfferData RetrieveOffer(int offerId);
        List<OfferData> RetrieveOffers();
        List<OfferData> RetrieveOffers(string userName);        
        bool UpdateOffer(OfferData offerData);
        bool DeleteOffer(OfferData offerData);        
    }
}
