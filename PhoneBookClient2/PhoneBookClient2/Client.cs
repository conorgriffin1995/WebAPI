using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System.Net.Http;
using System.Net.Http.Headers;
using PhoneBook2.Models;

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
                    client.BaseAddress = new Uri("http://localhost:50061/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // GET /api/PhoneBook
                    // get all entries in phonebook
                    HttpResponseMessage response = await client.GetAsync("phonebook/all");
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

        static async Task GetPerson()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50061/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    PhoneBook phoneBook = new PhoneBook();
                    HttpResponseMessage response = await client.GetAsync("phonebook/number/0851235566");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result
                        phoneBook = await response.Content.ReadAsAsync<PhoneBook>();
                        Console.WriteLine(phoneBook.Name + " " + phoneBook.Number + " " + phoneBook.Address);

                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }

                }
            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        static async Task AddPerson()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    string number = null, name = null, address = null;
                    bool cont = false;
                    client.BaseAddress = new Uri("http://localhost:50061/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                    do
                    {
                        Console.WriteLine("Enter Name: ");
                        name = Console.ReadLine();
                        Console.WriteLine("Enter Address: ");
                        address = Console.ReadLine();
                        Console.WriteLine("Enter Number: ");
                        number = Console.ReadLine();

                        // POST /api/PhoneBook
                        // create a new entry
                        PhoneBook phoneBook = new PhoneBook() { Number = number, Name = name, Address = address };
                        HttpResponseMessage response = await client.PostAsJsonAsync("phonebook/all", phoneBook);

                        if (response.IsSuccessStatusCode)
                        {
                            //Uri uri = response.Headers.Location;
                            var pbook = await response.Content.ReadAsAsync<PhoneBook>();
                            //Console.WriteLine("URI for new resource: " + uri.ToString());
                            Console.WriteLine("***Added Entry to phonebook***\n" + "Name: " + pbook.Name + "\n" + "Address: " + pbook.Address + "\n" + "Number: " + pbook.Number + "\n");
                        }
                        else
                        {
                            Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                        }


                        Console.WriteLine("Press y to add another contact, press any other key to exit: ");
                        string c = Console.ReadLine();
                        if (c != "y")
                        {
                            cont = true;
                            return;
                        }
                    } while (cont == false);
                    

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task UpdateEntry()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50061/");
                    Console.WriteLine("Update number..");     
                    // PUT /api/PhoneBook
                    PhoneBook phoneBook = new PhoneBook() { Number = "087451236", Name = "Lee Mack", Address = "33 Down the road" };
                    phoneBook.Number = "0851231234";

                    HttpResponseMessage response = await client.PutAsJsonAsync("phonebook/number/087451236", phoneBook);

                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("***Updated Entry to phonebook***\n" + "Name: " + phoneBook.Name + "\n" + "Address: " + phoneBook.Address + "\n" + "Number: " + phoneBook.Number + "\n");
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                        return;
                    }

                    
                }
            }catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }
        }

        // delete a stock listing
        static async Task DeleteAsync()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50061/");

                    // api/PhoneBook/Road                                                 
                    HttpResponseMessage response = await client.DeleteAsync("phonebook/number/0871256699");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine("PhoneBook Entry Deleted!!");
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
            GetPerson().Wait();
            AddPerson().Wait();
            UpdateEntry().Wait();
            DeleteAsync().Wait();

            Console.WriteLine("press any key to escape..");
            Console.ReadKey();
        }
    }
}
