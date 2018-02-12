using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using SMSService.Models;
using System.IO;

namespace SMSServiceClient
{
    class Client
    {
        static async Task GetAllAsync()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4138/");

                    // add an Accept header for JSON
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET ../api/stock
                    // get all stock listings
                    HttpResponseMessage response = await client.GetAsync("api/SMSService");                  // async call, await suspends until result available            
                    if (response.IsSuccessStatusCode)                                                   // 200..299
                    {
                        // read result 
                        var messages = await response.Content.ReadAsAsync<IEnumerable<TextMessage>>();
                        foreach (var msg in messages)
                        {
                            Console.WriteLine("Sent By:\t" + msg.PhoneNumberSender + "\nBody   :\t" + msg.Content + "\nSent To:\t" + msg.PhoneNumberReceiver + "\n");
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
        static async Task SendMessage()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    string Body = null;
                    string Sender = null;
                    string Receiver = null;
                    client.BaseAddress = new Uri("http://localhost:4138/");

                    // add an Accept header for JSON - preference for response 
                    client.DefaultRequestHeaders.
                        Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    Console.WriteLine("Enter Message Content:");
                    Body = Console.ReadLine();
                    Console.WriteLine("Enter Your number:");
                    Sender = Console.ReadLine();
                    Console.WriteLine("Enter receiver number:");
                    Receiver = Console.ReadLine();

                    TextMessage text = new TextMessage() { PhoneNumberSender = Sender, Content = Body, PhoneNumberReceiver = Receiver };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/SMSService", text);
                    if (response.IsSuccessStatusCode)
                    {
                        Uri uri = response.Headers.Location;
                        var msg = await response.Content.ReadAsAsync<TextMessage>();
                        Console.WriteLine("URI for new resource: " + uri.ToString());
                        Console.WriteLine("Sent By:\t" + msg.PhoneNumberSender + "\nBody   :\t" + msg.Content + "\nSent To:\t" + msg.PhoneNumberReceiver + "\n");
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


        static void Main()
        {
            GetAllAsync().Wait();
            SendMessage().Wait();
            Console.ReadKey();
        }
    }
}
