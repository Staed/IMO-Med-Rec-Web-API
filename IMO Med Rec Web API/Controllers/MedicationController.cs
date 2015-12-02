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
    public class MedicationController : ApiController
    {
        List<Medication> medications = new List<Medication>(
            new Medication[]
            {
                new Medication("Immune Globulin Intravenous Injection",0, 50.2,"Your doctor has ordered IGIV. The drug may be given alone or added to an intravenous"
                    +"fluid that will drip through a needle or catheter placed in your vein for 2 to 4 hours, once a day for 2 to 7 days."
                    +"You will receive another single dose every 10 to 21 days or every 3 to 4 weeks, depending on your condition", 
                    "backache, headache, joint or muscle pain,general feeling of discomfort,leg cramps"),

                new Medication("Famciclovir",1, 20.57, "Famciclovir is used to treat herpes zoster (shingles; a rash that can occur in people who have had chickenpox in the past)"
                    +". It is also used to treat repeat outbreaks of herpes virus cold sores or fever blisters in people with a normal immune system",
                    "headache, nausea, vomiting, diarrhea or loose stools"),

                new Medication("Iloperidone",2, 15.4,"Iloperidone is used to treat the symptoms of schizophrenia (a mental illness that causes disturbed or unusual thinking,"
                    +"loss of interest in life, and strong or inappropriate emotions). Iloperidone is in a class of medications called atypical antipsychotics."
                    +"It works by changing the activity of certain natural substances in the brain.",
                    "weight gain, nausea, diarrhea,stomach pain, dry mouth")
            }
        );
        // GET api/<controller>

        [HttpGet]
        public IEnumerable<Medication> Get()
        {
            return medications;
        }


        [HttpGet]
        public IHttpActionResult GetSpecificMedication(int id)
        {
            var medication = medications.FirstOrDefault((a) => a.id == id);
            if (medication == null)
            {
                return NotFound();
            }

            return Ok(medication);
        }

        [HttpPost]
        public IHttpActionResult PostMedications()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;

            String name = jsonContent.Substring(jsonContent.IndexOf("medication=") + 11, jsonContent.IndexOf("price="));
            double price = System.Convert.ToDouble(jsonContent.Substring(jsonContent.IndexOf("price=")+6, jsonContent.IndexOf("description=")));
            String description = jsonContent.Substring(jsonContent.IndexOf("description=") + 12, jsonContent.IndexOf("sideEffect="));
            String sideEffect = jsonContent.Substring(jsonContent.IndexOf("sideEffect=") + 11);
            int lastID = medications[medications.Count - 1].id;
            lastID++;

            System.Diagnostics.Debug.Print(medications.Count.ToString());
            medications.Add(new Medication(name,lastID,price,description,sideEffect));
            System.Diagnostics.Debug.Print(medications.Count.ToString());
            return Ok("200");
        }

        [HttpPut]
        public IHttpActionResult PutMedication(int id)
        {
            if (id < 0 || id >= medications.Count)
            {
                return BadRequest();
            }

            HttpContent requestContent = Request.Content;

            string jsonContent = requestContent.ReadAsStringAsync().Result;
            String name = jsonContent.Substring(jsonContent.IndexOf("medication=") + 11, jsonContent.IndexOf("price="));
            double price = System.Convert.ToDouble(jsonContent.Substring(jsonContent.IndexOf("price=") + 6, jsonContent.IndexOf("description=")));
            String description = jsonContent.Substring(jsonContent.IndexOf("description=") + 12, jsonContent.IndexOf("sideEffect="));
            String sideEffect = jsonContent.Substring(jsonContent.IndexOf("sideEffect=") + 11);

            System.Diagnostics.Debug.Print(name);
            medications[id].name = name;
            medications[id].price = price;
            medications[id].description = description;
            medications[id].sideEffect = sideEffect; 

            System.Diagnostics.Debug.Print(medications[id].name);
            return Ok("200");
        }

        [HttpDelete]
        public IHttpActionResult DeleteAllergy(int id)
        {
            if (id < 0 || id >= medications.Count)
            {
                return BadRequest();
            }

            medications.Remove(medications[id]);
            return Ok("200");
        }
    }
}
