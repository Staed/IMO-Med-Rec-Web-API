using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMO_Med_Rec_Web_API.Models
{
    public class Allergy
    {
        public string name { get; set; }
        public int id { get; set; }

        public Allergy(string name, int id)
        {
            this.name = name;
            this.id = id;
        }
    }
}