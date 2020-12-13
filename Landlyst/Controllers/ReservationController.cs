using Landlyst.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Landlyst.Controllers
{
    public class ReservationController : Controller
    {
        //Returns rooms based on searched additions
        public ActionResult PartialRoomList(string additions)
        {
            return PartialView("PartialRoomList", DALManager.SearchRooms(additions));
        }

        //Gets a form-group partial view to fill in a reservation
        public ActionResult GetReservation()
        {
            return PartialView();
        }

        //Posts the given input in the reservation to the CustomerModel
        [HttpPost]
        public ActionResult GetReservation(CustomerModel customer)
        {
            DALManager.CreateReservation(customer);
            return View();
        }
    }
}