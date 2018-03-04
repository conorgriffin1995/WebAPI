using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using WeatherClient.Models;

namespace WeatherClient
{
    class Client
    {
        static async Task GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50148/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET weather/all
                    HttpResponseMessage response = await client.GetAsync("weather/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var entries = await response.Content.ReadAsAsync<IEnumerable<WeatherInformation>>();
                        foreach (var entry in entries)
                        {
                            Console.WriteLine(entry.City + "\t" + entry.Temperature + "\n" + entry.Condition + "\t" + entry.Warning + "\t" + entry.WindSpeed);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetCity()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50148/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    WeatherInformation weather = new WeatherInformation();
                    // GET weather/city/{name}
                    HttpResponseMessage response = await client.GetAsync("weather/city/Dublin");
                    if (response.IsSuccessStatusCode)
                    {
                        weather = await response.Content.ReadAsAsync<WeatherInformation>();
                        Console.WriteLine("*** Weather Information for specific city ***");
                        Console.WriteLine(weather.City + "\t" + weather.Temperature + "\n" + weather.Condition + "\t" + weather.Warning + "\t" + weather.WindSpeed);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetWarningCities()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50148/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // GET weather/city/{name}
                    HttpResponseMessage response = await client.GetAsync("weather/all/true");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("\n*** Weather Information with Warning: true ***");
                        var entries = await response.Content.ReadAsAsync<IEnumerable<WeatherInformation>>();
                        foreach (var entry in entries)
                        {
                            Console.WriteLine(entry.City + "\t" + entry.Temperature + "\n" + entry.Condition + "\t" + entry.Warning + "\t" + entry.WindSpeed);
                            Console.WriteLine();
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task UpdateCity()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50148/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    // PUT weather/city/Dublin
                    WeatherInformation weather = new WeatherInformation()
                    {
                        City = "Dublin",
                        Condition = "Storm",
                        Temperature = 5.1,
                        Warning = true,
                        WindSpeed = 35
                    };
                    HttpResponseMessage response = await client.PutAsJsonAsync("weather/city/Dublin", weather);
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("\n*** Updated Weather Information: Dublin ***\n");
                        Console.WriteLine(weather.City + "\t" + weather.Temperature + "\n" + weather.Condition + "\t" + weather.Warning + "\t" + weather.WindSpeed);
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
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
            GetAllAsync().Wait();
            GetCity().Wait();
            GetWarningCities().Wait();
            UpdateCity().Wait();
            Console.ReadKey();
        }
    }
}
