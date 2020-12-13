using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Landlyst.Models
{
    //A model for the additions each room has
    public class AdditionModel
    {
        private string addition;

        public string Addition
        {
            get { return addition; }
            set { addition = value; }
        }
        private double additionPrice;

        public double AdditionPrice
        {
            get { return additionPrice; }
            set { additionPrice = value; }
        }

    }
}