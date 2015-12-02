using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using IMO_Med_Rec_Web_API.Models;
using IMO_Med_Rec_Web_API.Models.Services;

namespace IMO_Med_Rec_Web_API.Controllers
{
    public class PatientController : ApiController
    {
        private PatientRepository patientRepository;

        public PatientController()
        {
            this.patientRepository = new PatientRepository();
        }



        [HttpGet]
        public Patient[] Get()
        {
            return patientRepository.GetAllPatients();
        }

        [HttpGet]
        public Patient Id_Get(int id)
        {
  
            return patientRepository.GetOnePatient(id);
        }



        public HttpResponseMessage Post(Patient patient)
        {
            this.patientRepository.SavePatient(patient);

            var response = Request.CreateResponse<Patient>(System.Net.HttpStatusCode.Created, patient);

            return response;
        }

        
        public HttpResponseMessage Delete(int id)
        {
            bool result;
            result =this.patientRepository.DeletePatient(id);

            if (result == true) 
                return Request.CreateResponse(System.Net.HttpStatusCode.OK);

            return Request.CreateResponse(System.Net.HttpStatusCode.BadRequest);
        }

        public HttpResponseMessage GetAllergy(string Allergy)
        {
            int[] result;
            result = this.patientRepository.GetAllergy(Allergy);

            var response = Request.CreateResponse<int[]>(System.Net.HttpStatusCode.OK, result);

            return response;

        }

    }



}
