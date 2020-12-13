using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landlyst.Models
{
    //Model for a reservation
    public class ReservationModel
    {
        private int phoneNumber;

        public int PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }

        private int visitorAmount;

        public int VisitorAmount
        {
            get { return visitorAmount; }
            set { visitorAmount = value; }
        }
        private int roomNumber;

        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
        private DateTime checkinDate;

        public DateTime CheckinDate
        {
            get { return checkinDate; }
            set { checkinDate = value; }
        }
        private DateTime checkoutDate;

        public DateTime CheckoutDate
        {
            get { return checkoutDate; }
            set { checkoutDate = value; }
        }
    }
}