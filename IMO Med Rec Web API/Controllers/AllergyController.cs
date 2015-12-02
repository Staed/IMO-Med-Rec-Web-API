using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IMO_Med_Rec_Web_API.Models;
using Newtonsoft.Json.Linq;
using Newtonsoft.Json;

namespace IMO_Med_Rec_Web_API.Controllers
{
    public class AllergyController : ApiController
    {
        List<Allergy> allergies = new List<Allergy>(
            new Allergy[]
            {
                new Allergy("Dairy", 1),
                new Allergy("Shellfish", 2),
                new Allergy("Gluten", 3),
                new Allergy("Nuts", 4)
            }
        );

        [HttpGet]
        public IEnumerable<Allergy> Get()
        {
            return allergies;
        }
 
        [HttpGet]
        public IHttpActionResult GetSpecificAllergy(int id)
        {
            var allergy = allergies.FirstOrDefault((a) => a.id == id);
            if (allergy == null)
            {
                return NotFound();
            }

            return Ok(allergy);
        }

        [HttpPost]
        public IHttpActionResult PostAllergy()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            
            //THERE MUST BE A BETTER WAY
            String value = jsonContent.Substring(jsonContent.IndexOf("allergy=") + 8);

            int lastID = allergies[allergies.Count - 1].id;
            lastID++;

            System.Diagnostics.Debug.Print(allergies.Count.ToString());
            allergies.Add(new Allergy(value, lastID));
            System.Diagnostics.Debug.Print(allergies.Count.ToString());
            return Ok("200");
        }

        [HttpPut]
        public IHttpActionResult PutAllergy(int id)
        {
            if (id < 0 || id >= allergies.Count)
            {
                return BadRequest();
            }

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            String value = jsonContent.Substring(jsonContent.IndexOf("allergy=") + 8);
            System.Diagnostics.Debug.Print(value);
            allergies[id].name = value;

            System.Diagnostics.Debug.Print(allergies[id].name);
            return Ok("200");
        }

        [HttpDelete]
        public IHttpActionResult DeleteAllergy(int id)
        {
            if (id < 0 || id >= allergies.Count)
            {
                return BadRequest();
            }

            allergies.Remove(allergies[id]);
            return Ok("200");
        }

    }
}
