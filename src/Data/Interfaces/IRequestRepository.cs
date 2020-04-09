using Data.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Data.Interfaces
{
    public interface IRequestRepository
    {
        bool CreateRequest(RequestData requestData);
        RequestData RetrieveRequest(int requestId);
        List<RequestData> RetrieveRequests(string userName);
        List<RequestData> RetrieveRequests(int offerId);
        bool UpdateRequest(RequestData requestData);
        bool DeleteRequest(RequestData requestData); //!HACE FALTA O SOLO CON CAMBIO DE ESTATUS SE CANCELA?
    }
}
