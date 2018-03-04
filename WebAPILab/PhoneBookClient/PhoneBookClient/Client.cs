// client for phonebook service
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;
using PhoneBook.Models;

namespace PhoneBookClient
{
    class Client
    {
        // async call
        static async Task DoWork()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:19601/");                        // base url for api controller

                    // add an Accept  header for JSON
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    // 1
                    // get phonebook info for all people
                    // GET ../phonebook/all
                    HttpResponseMessage response = await client.GetAsync("phonebook/all");
                    if (response.IsSuccessStatusCode)                                               // 200.299
                    {
                        // read result
                        var phonebook = await response.Content.ReadAsAsync<IEnumerable<PhonebookInformation>>();
                        foreach(var result in phonebook)
                        {
                            Console.WriteLine(result.Name + " " + result.Number + " " + result.Address);
                        }
                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    Console.WriteLine();
                    Console.WriteLine();
                    
                    // 2
                    // get phonebook info for person by name
                    // GET ../phonebook/name/Gary      
                    PhonebookInformation personInfoName = new PhonebookInformation();
                    response = await client.GetAsync("phonebook/name/Gary");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result
                        personInfoName = await response.Content.ReadAsAsync<PhonebookInformation>();
                        Console.WriteLine(personInfoName.Name + " " + personInfoName.Number + " " + personInfoName.Address);

                    }
                    else
                    {
                        Console.WriteLine(response.StatusCode + " " + response.ReasonPhrase);
                    }
                    Console.WriteLine();
                    Console.WriteLine();

                    // 3
                    // get phonebook info for person by number
                    // GET ../phonebook/number/0867744112
                    PhonebookInformation personInfoNumber = new PhonebookInformation();
                    response = await client.GetAsync("phonebook/number/0867744112");
                    if (response.IsSuccessStatusCode)
                    {
                        // read result
                        personInfoNumber = await response.Content.ReadAsAsync<PhonebookInformation>();
                        Console.WriteLine(personInfoNumber.Name + " " + personInfoNumber.Number + " " + personInfoNumber.Address);

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
        // kick off
        static void Main()
        {
            DoWork().Wait();
            Console.ReadKey();
        }
    }
}
