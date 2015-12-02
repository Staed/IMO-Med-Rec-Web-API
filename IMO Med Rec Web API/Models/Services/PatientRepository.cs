using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IMO_Med_Rec_Web_API.Models;
using System.Web;

namespace IMO_Med_Rec_Web_API.Models.Services
{
   
    public class PatientRepository
    {
        private const string CacheKey = "ContactStore";
        public PatientRepository()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                if (ctx.Cache[CacheKey] == null)
                {
                    var patient = new Patient[]
                    {
                        new Patient
                        {
                                Id = 1,
                                Name = "Glenn Block",
                                Allergy= "None"
                        },
                       
                    };

                            ctx.Cache[CacheKey] = patient;
                        }
            }
        }
        public Patient[] GetAllPatients()
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                return (Patient[])ctx.Cache[CacheKey];
            }
            else
            {
                return new Patient[]
                {
                    new Patient
                    {
                        Id = 0,
                        Name = "Placeholder"
                    }
                };
            }

        }

        public Patient GetOnePatient(int id)
        {
            var ctx = HttpContext.Current;
         
            if (ctx != null)
            {
                var currentData = ((Patient[])ctx.Cache[CacheKey]).ToArray();
                for (int i=0;i < currentData.Length; i++)
                {

                    if (currentData[i].Id == id)
                        return currentData[i];
                }
                return new Patient
                    {
                        Id = 0,
                        Name = "Placeholder"
                    }
                ;
            }
            else
            {
                return new Patient
                    {
                        Id = 0,
                        Name = "Placeholder"
                    }
                ;
            }

        }

        public bool DeletePatient(int id)
        {
            var ctx = HttpContext.Current;
            bool result = false;
            if (ctx != null)
            {
                try
                {
                    var currentData = ((Patient[])ctx.Cache[CacheKey]).ToList();
                    for(int i=0; i<currentData.Count; i++)
                    {
                        if(currentData[i].Id == id)
                        {
                            currentData.Remove(currentData[i]);
                            result = true;
                        }
                    }

                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return result;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return result;
        }



        public bool SavePatient(Patient patient)
        {
            var ctx = HttpContext.Current;

            if (ctx != null)
            {
                try
                {
                    var currentData = ((Patient[])ctx.Cache[CacheKey]).ToList();
                    currentData.Add(patient);
                    ctx.Cache[CacheKey] = currentData.ToArray();

                    return true;
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    return false;
                }
            }

            return false;
        }



        public int[] GetAllergy(string name)
        {
            var ctx = HttpContext.Current;
            List<int> temp = new List<int>();
            if (ctx != null)
            {
                try
                {
                    var currentData = ((Patient[])ctx.Cache[CacheKey]).ToList();
                    for (int i = 0; i < currentData.Count; i++)
                    {
                        if (currentData[i].Allergy.ToLower().Equals(name.ToLower()))
                        {
                            temp.Add(currentData[i].Id);
                        }
                    }

                    
                    ctx.Cache[CacheKey] = currentData.ToArray();
                    
                }
                catch (Exception ex)
                {
                    Console.WriteLine(ex.ToString());
                    
                }
            }
            int[] result = temp.ToArray();
            return result;
        }
    }
}
