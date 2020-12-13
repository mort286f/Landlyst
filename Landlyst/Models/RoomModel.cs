using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landlyst.Models
{
    //Model for each room in the database
    public class RoomModel
    {
        private int roomNumber;

        public int RoomNumber
        {
            get { return roomNumber; }
            set { roomNumber = value; }
        }
        private double price;

        public double Price
        {
            get { return price; }
            set { price = value; }
        }
        private List<AdditionModel> addition;

        public List<AdditionModel> Addition
        {
            get { return addition; }
            set { addition = value; }
        }
    }
}