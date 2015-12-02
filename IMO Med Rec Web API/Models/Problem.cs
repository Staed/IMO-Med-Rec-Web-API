using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace IMO_Med_Rec_Web_API.Models
{
    public class Problem
    {
        public int id { get; set; }
        public string name { get; set; }
        public string description { get; set; }
        public string symptoms { get; set; }

        public Problem(int id, string name, string description, string symptoms)
        {
            this.id = id;
            this.name = name;
            this.description = description;
            this.symptoms = symptoms;
        }
    }
}
