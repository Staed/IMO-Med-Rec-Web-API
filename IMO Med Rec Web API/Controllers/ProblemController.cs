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
    class ProblemController : ApiController
    {
        List<Problem> problems = new List<Problem>(new Problem[]
        {
            new Problem(1, "Ectropion", "Ectropion is a condition in which your eyelid (typically the lower lid) turns out. This leaves the inner eyelid surface exposed and prone to irriation.", "irritation, excessive tearing, excessive dryness"),
            new Problem(2, "Edema", "Edema is swelling caused by excess fluid trapped in your body's tissues.", "swelling or puffiness of tissue directly undr your skin, stretched or shiny skin, skin that retains a dimple after being pressed for several seconds, increased abdominal size"),
            new Problem(3, "Ocular rosacea", "Ocular rosacea is an inflammation that causes redness, burning and itching of the eyes.", "dry eyes, burning or stinging in the eyes, itchy eyes, grittiness or feeling of having a foreign body in the eye or eyes, blurred vision, photophobia, redness, dilated small blood vessels on the white pat of the eye that are visible when you look in a mirror, red swollen eyelids, tearing"),
            new Problem(4, "Tennis elbow", "Tennis elbow is a painful condition that occurs when tendons in your elbow are overworked, usually by repetitive motions of the wrist and arm.", "pain and weakness inhibit ability to shake hands, turn a doorknob, and/or hold a coffee cup")
        });

        [HttpGet]
        public IEnumerable<Problem> Get()
        {
            return problems;
        }

        [HttpGet]
        public IHttpActionResult GetSpecificProblem(int id)
        {
            var problem = problems.FirstOrDefault((a) => a.id == id);
            if (problem == null)
                return NotFound();
            return Ok(problem);
        }

        [HttpPost]
        public IHttpActionResult PostProblem()
        {
            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
​
            String name = jsonContent.Substring(jsonContent.IndexOf("problem=") + 8, jsonContent.IndexOf("description="));
            String description = jsonContent.Substring(jsonContent.IndexOf("description=") + 12, jsonContent.IndexOf("symptoms="));
            String symptoms = jsonContent.Substring(jsonContent.IndexOf("symptoms=") + 9);
            int lastID = problems[problems.Count - 1].id;
            lastID++;
​
            System.Diagnostics.Debug.Print(problems.Count.ToString());
            problems.Add(new Problem(lastID, name, description, symptoms));
            System.Diagnostics.Debug.Print(problems.Count.ToString());
            return Ok("200");
        }

        [HttpPut]
        public IHttpActionResult PutProblem(int id)
        {
            if (id < 0 || id >= problems.Count)
                return BadRequest();

            HttpContent requestContent = Request.Content;
            string jsonContent = requestContent.ReadAsStringAsync().Result;
            String name = jsonContent.Substring(jsonContent.IndexOf("problem=") + 8, jsonContent.IndexOf("description="));
            String description = jsonContent.Substring(jsonContent.IndexOf("description=") + 12, jsonContent.IndexOf("symptoms="));
            String symptoms = jsonContent.Substring(jsonContent.IndexOf("symptoms=") + 9);
            System.Diagnostics.Debug.Print(name);
            problems[id].name = name;
            problems[id].description = description;
            problems[id].symptoms = symptoms;

            System.Diagnostics.Debug.Print(problems[id].name);
            return Ok("200");
        }

        [HttpDelete]
        public IHttpActionResult DeleteProblem(int id)
        {
            if (id < 0 || id >= problems.Count)
                return BadRequest();
            problems.Remove(problems[id]);
            return Ok("200");
        }
    }
}
