// Conor Griffin
// X00111602

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;

using CA1Client.Models;

namespace CA1Client
{
    // Had issues on the laptop, couldn't get client to run properly, think its an issue with the laptop and not in code.
    // Out of disk space on the machine i think.
    class Client
    {
        static async Task GetAll()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49810/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("pressure/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var entries = await response.Content.ReadAsAsync<IEnumerable<Patient>>();
                        foreach (var entry in entries)
                        {
                            Console.WriteLine(entry.ID + "\n" + entry.Name + "\n");
                            foreach(var press in entry.Pressure)
                            {
                                Console.WriteLine(press.Systolic + "\n" + press.Diastolic + "\n");
                            }
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetById()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49810/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    BloodPressure b = new BloodPressure();
                    HttpResponseMessage response = await client.GetAsync("pressure/patient/1");
                    if (response.IsSuccessStatusCode)
                    {
                        b = await response.Content.ReadAsAsync<BloodPressure>();
                        Console.WriteLine(b);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetBloodPressureCategory()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49810/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    int systolic = 140, diastolic = 110;
                    BloodPressure b = new BloodPressure();
                    HttpResponseMessage response = await client.GetAsync("pressure/patient/140,110");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                        b = await response.Content.ReadAsAsync<BloodPressure>();
                        Console.WriteLine(b);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        // Didnt get to implement this method
        static async Task GetAverageBloodPressure()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:49810/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    int systolic = 140, diastolic = 110;
                    BloodPressure b = new BloodPressure() { Systolic = systolic, Diastolic = diastolic };
                    HttpResponseMessage response = await client.GetAsync("pressure/patient/{systolic:int},{diastolic:int}");
                    if (response.IsSuccessStatusCode)
                    {
                        b = await response.Content.ReadAsAsync<BloodPressure>();
                        Console.WriteLine(b.Category);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static void Main()
        {
            //GetAll().Wait();
            //GetById().Wait();
            GetBloodPressureCategory().Wait();
            //GetAverageBloodPressure().Wait();
            Console.ReadKey();
        }
    }
}
