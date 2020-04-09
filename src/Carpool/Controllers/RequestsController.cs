using Business.Entities;
using Business.Interfaces;
using System.Collections.Generic;
using System.Web.Mvc;

namespace Carpool.Controllers
{
    public class RequestsController : Controller
    {
        IOfferService offerService;
        INotificationService notificationService;
        ILoginService loginService;
        IRequestService requestService;

        public RequestsController()
        {
            offerService = CarpoolContext.Instance.OfferService;
            notificationService = CarpoolContext.Instance.NotificationService;
            loginService = CarpoolContext.Instance.LoginService;
            requestService = CarpoolContext.Instance.RequestService;
        }

        public ActionResult List(int id)
        {
            List<RideRequest> requests = requestService.ViewRequestList(id);

            return View(requests);
        }

        public ActionResult MyList()
        {
            List<RideRequest> requests = requestService.ViewRequestList(loginService.CurrentUser.UserName);

            return View(requests);
        }

        public ActionResult RequestRide(int id)
        {
            requestService.RequestRide(id, loginService.CurrentUser.UserName);

            return RedirectToAction("MyList"); //que lleve a myrequests
        }

        public ActionResult Cancel(int offerId, int requestId)
        {
            offerService.CancelRequest(offerId, requestId);

            return RedirectToAction("MyList");
        }

        public ActionResult Accept(int offerId, int requestId)
        {
            offerService.AcceptRequest(offerId, requestId);

            return RedirectToAction("List", new { id = offerId }); 
        }

        public ActionResult Reject(int offerId, int requestId)
        {
            offerService.RejectRequest(requestId);

            return RedirectToAction("List", new { id = offerId });
        }

        public ActionResult AcceptChanges(int requestId)
        {
            offerService.AcceptChanges(requestId);

            return RedirectToAction("MyList");
        }

        public ActionResult RejectChanges(int offerId, int requestId)
        {
            offerService.RejectChanges(offerId, requestId);

            return RedirectToAction("MyList");
        }
    }
}