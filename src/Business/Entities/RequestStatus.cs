using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Business.Entities
{
    public enum RequestStatus
    {
        RequestSubmitted, //inicial
        OfferUpdated, //cambios offer
        ChangesAccepted, //pasajero acepto cambios
        RequestAccepted, //driver lo acepto
        ChangesRejected, //pasajero no acepto cambios
        RequestCancelled, //pasajero se arrepintio 
        RequestRejected, //driver lo rechazo
        OfferCancelled //oferta se cae
    }
}
