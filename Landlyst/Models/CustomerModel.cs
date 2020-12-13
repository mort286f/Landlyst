using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landlyst.Models
{
    //Model for a customer 
    public class CustomerModel
    {
        private string firstName;

        public string FirstName
        {
            get { return firstName; }
            set { firstName = value; }
        }

        private string lastName;

        public string LastName
        {
            get { return lastName; }
            set { lastName = value; }
        }

        private string phoneNumber;

        public string PhoneNumber
        {
            get { return phoneNumber; }
            set { phoneNumber = value; }
        }
        private string address;

        public string Address
        {
            get { return address; }
            set { address = value; }
        }
        private string email;

        public string Email
        {
            get { return email; }
            set { email = value; }
        }
        private string zipCode;

        public string ZipCode
        {
            get { return zipCode; }
            set { zipCode = value; }
        }

        private ReservationModel reservation;

        public ReservationModel Reservation
        {
            get { return reservation; }
            set { reservation = value; }
        }
        
    }
}