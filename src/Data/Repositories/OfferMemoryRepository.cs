using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class OfferMemoryRepository : IOfferRepository
    {
        Dictionary<int, OfferData> offers = new Dictionary<int, OfferData>();

        public bool CreateOffer(OfferData offerData)
        {
            if (offerData.Id == 0)
            {
                offerData.Id = offers.Count() + 1;

                offers.Add(offerData.Id, offerData);
            }

            return offers.ContainsKey(offerData.Id);
        }

        public bool DeleteOffer(OfferData offerData)
        {
            if (offers.ContainsKey(offerData.Id))
            {
                offers.Remove(offerData.Id);
            }

            return !offers.ContainsKey(offerData.Id);
        }

        public OfferData RetrieveOffer(int offerId)
        {
            OfferData offerData = null;

            if (offers.ContainsKey(offerId))
            {
                offerData = offers[offerId];
            }

            return offerData;
        }

        public List<OfferData> RetrieveOffers()
        {
            return offers.Values.ToList();
        }

        public List<OfferData> RetrieveOffers(string userName)
        {
            List<OfferData> offerUser = new List<OfferData>();

            foreach (OfferData offer in offers.Values)
            {
                if (offer.UserData.UserName == userName)
                {
                    offerUser.Add(offer);
                }
            }

            return offerUser;
        }

        public bool UpdateOffer(OfferData offerData)
        {
            if (offers.ContainsKey(offerData.Id))
            {
                offers[offerData.Id] = offerData;
            }

            return true;
        }
    }
}
