using Business.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Interfaces
{
    public interface IRequestService
    {
        void RequestRide(int offerId, string userName);
        RideRequest GetRequest(int requestId);
        RideRequest ChangeStatus(int requestId, RequestStatus requestStatus);        
        List<RideRequest> ViewRequestList(string userName);
        List<RideRequest> ViewRequestList(int offerId);
    }
}
