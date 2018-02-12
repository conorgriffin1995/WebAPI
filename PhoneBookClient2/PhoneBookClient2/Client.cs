using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using PhoneBookClient2.Models;

namespace PhoneBookClient2
{
    class Client
    {
        static async Task GetAllAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:4622/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET /api/PhoneBook
                    // get all entries in phonebook
                    HttpResponseMessage response = await client.GetAsync("api/PhoneBook");
                    if (response.IsSuccessStatusCode)
                    {
                        var entries = await response.Content.ReadAsAsync<IEnumerable<PhoneBook>>();
                        foreach(var entry in entries)
                        {
                            Console.WriteLine(entry.Name + "\n" + entry.Number + "\n" + entry.Address + "\n");
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

        static async Task AddAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string name = null, address = null;
                     
                    client.BaseAddress = new Uri("http://localhost:4622/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Console.WriteLine("Enter Name: ");
                    name = Console.ReadLine();
                    Console.WriteLine("Enter Address: ");
                    address = Console.ReadLine();
                    Console.WriteLine("Enter Number: ");
                    int number = Convert.ToInt32(Console.ReadLine());
                 
                    // POST /api/PhoneBook
                    // create a new entry
                    PhoneBook phoneBook = new PhoneBook() { Name = name, Address = address, Number = number };
                    HttpResponseMessage response = await client.PostAsJsonAsync("api/PhoneBook", phoneBook);

                    if (response.IsSuccessStatusCode)
                    {
                        Uri uri = response.Headers.Location;
                        var pbook = await response.Content.ReadAsAsync<PhoneBook>();
                        Console.WriteLine("URI for new resource: " + uri.ToString());
                        Console.WriteLine("***Added Entry to phonebook***\n" + "Name: " + pbook.Name + "\n" + "Address: " + pbook.Address + "\n" + "Number: " + pbook.Number + "\n");
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
            AddAsync().Wait();

            Console.WriteLine("press any key to escape..");
            Console.ReadKey();
        }
    }
}
