using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMO_Med_Rec_Web_API.Models
{
    public class Medication
    {
        public string name { get; set; }

        public int id { get; set; }

        public double price { get; set; }

        public string description { get; set; }

        public string sideEffect { get; set; }

        public Medication(string name, int id, double price, string description, string sideEffect)
        {
            this.name = name;
            this.id = id;
            this.price = price;
            this.description = description;
            this.sideEffect = sideEffect;
        }
    }
}
