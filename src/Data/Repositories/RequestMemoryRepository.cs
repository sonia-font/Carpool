using Data.Entities;
using Data.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Repositories
{
    public class RequestMemoryRepository : IRequestRepository
    {
        Dictionary<int, RequestData> requests = new Dictionary<int, RequestData>();

        public bool CreateRequest(RequestData requestData)
        {
            if (requestData.Id == 0)
            {
                requestData.Id = requests.Count() + 1;

                requests.Add(requestData.Id, requestData);
            }

            return requests.ContainsKey(requestData.Id);
        }

        public bool DeleteRequest(RequestData requestData)
        {
            if (requests.ContainsKey(requestData.Id))
            {
                requests.Remove(requestData.Id);
            }

            return !requests.ContainsKey(requestData.Id);
        }

        public RequestData RetrieveRequest(int requestId)
        {
            RequestData requestData = null;

            if (requests.ContainsKey(requestId))
            {
                requestData = requests[requestId];
            }

            return requestData;
        }

        public List<RequestData> RetrieveRequests(string userName)
        {
            List<RequestData> requestsUser = new List<RequestData>();

            foreach (RequestData request in requests.Values)
            {
                if (request.UserData.UserName == userName)
                {
                    requestsUser.Add(request);
                }
            }

            return requestsUser;
        }

        public List<RequestData> RetrieveRequests(int offerId)
        {
            List<RequestData> requestsOffer = new List<RequestData>();

            foreach (RequestData request in requests.Values)
            {
                if (request.OfferData.Id == offerId)
                {
                    requestsOffer.Add(request);
                }
            }

            return requestsOffer;
        }

        public bool UpdateRequest(RequestData requestData)
        {
            if (requests.ContainsKey(requestData.Id))
            {
                requests[requestData.Id] = requestData;
            }

            return true;
        }
    }
}
