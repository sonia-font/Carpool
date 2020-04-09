using Business.Entities;
using System.Collections.Generic;
using System.Web.Mvc;
using Business.Interfaces;

namespace Carpool.Controllers
{
    public class OffersController : Controller
    {
        IOfferService offerService;
        INotificationService notificationService;
        ILoginService loginService;
        IRequestService requestService;

        public OffersController()
        {
            offerService = CarpoolContext.Instance.OfferService;
            notificationService = CarpoolContext.Instance.NotificationService;
            loginService = CarpoolContext.Instance.LoginService;
            requestService = CarpoolContext.Instance.RequestService;
        }

        public ActionResult List()
        {
            List<RideOffer> offers = offerService.ViewOfferList();

            return View(offers);
        }

        public ActionResult MyList()
        {
            List<RideOffer> offers = offerService.ViewOfferList(loginService.CurrentUser.UserName);

            return View(offers);
        }

        public ActionResult Post()
        {
            return View();
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Post([Bind(Include = "RideDate,Destiny,Origin,Comment,Seats")] RideOffer rideOffer)
        {
            if (ModelState.IsValid)
            {
                offerService.PostOffer(loginService.CurrentUser.UserName, rideOffer.RideDate, rideOffer.Destiny, rideOffer.Origin, rideOffer.Comment, rideOffer.Seats);
                return RedirectToAction("MyList");
            }

            return View(rideOffer);
        }

        public ActionResult ViewOffer(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

            RideOffer rideOffer = offerService.ViewOffer(id);
            if (rideOffer == null)
            {
                return HttpNotFound();
            }
            return View(rideOffer);
        }

        public ActionResult ViewMyOffer(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/

            RideOffer rideOffer = offerService.ViewOffer(id);
            if (rideOffer == null)
            {
                return HttpNotFound();
            }
            return View(rideOffer);
        }

        public ActionResult Update(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            RideOffer rideOffer = offerService.ViewOffer(id);
            if (rideOffer == null)
            {
                return HttpNotFound();
            }
            return View(rideOffer);
        }
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Update([Bind(Include = "Id,Destiny,Origin,Comment")] RideOffer rideOffer)
        {
            if (ModelState.IsValid)
            {
                offerService.UpdateOffer(rideOffer.Id, rideOffer.Destiny, rideOffer.Origin, rideOffer.Comment);
                return RedirectToAction("MyList");
            }

            return View(rideOffer);
        }

        public ActionResult Cancel(int id)
        {
            /*if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }*/
            RideOffer rideOffer = offerService.ViewOffer(id);
            if (rideOffer == null)
            {
                return HttpNotFound();
            }
            return View(rideOffer);
        }
        
        [HttpPost, ActionName("Cancel")]
        [ValidateAntiForgeryToken]
        public ActionResult CancelOffer(int id)
        {
            RideOffer rideOffer = offerService.ViewOffer(id);

            offerService.CancelOffer(id);

            return RedirectToAction("MyList");
        }
    }
}