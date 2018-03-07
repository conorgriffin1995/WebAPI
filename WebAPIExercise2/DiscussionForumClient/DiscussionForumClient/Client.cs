using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;

using DiscussionForumClient.Models;

namespace DiscussionForumClient
{
    class Client
    {
        static async Task GetAll()
        {
            try
            {
                using(HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50887/");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("forum/all");
                    if (response.IsSuccessStatusCode)
                    {
                        var entries = await response.Content.ReadAsAsync<IEnumerable<Forum>>();
                        foreach(var entry in entries)
                        {
                            Console.WriteLine(entry.ID + "\n" + entry.Timestamp + "\n" + entry.UserPost.Subject + "\n" + entry.UserPost.Message + "\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetPostById()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50887");

                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    Forum entry = new Forum();
                    HttpResponseMessage response = await client.GetAsync("forum/post/1");
                    if (response.IsSuccessStatusCode)
                    {
                        Console.WriteLine(response.StatusCode);
                        entry = await response.Content.ReadAsAsync<Forum>();
                        Console.WriteLine(entry.ID + "\n" + entry.Timestamp + "\n" + entry.UserPost.Subject + "\n" + entry.UserPost.Message + "\n");
                    }

                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task GetNumberPosts()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50887/");
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    HttpResponseMessage response = await client.GetAsync("forum/all/3");
                    if (response.IsSuccessStatusCode)
                    {
                        var entries = await response.Content.ReadAsAsync<IEnumerable<Forum>>();
                        foreach (var entry in entries)
                        {
                            Console.WriteLine(entry.ID + "\n" + entry.Timestamp + "\n" + entry.UserPost.Subject + "\n" + entry.UserPost.Message + "\n");
                        }
                    }
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
            }
        }

        static async Task AddPost()
        {
            try
            {
                using (HttpClient client = new HttpClient())
                {
                    client.BaseAddress = new Uri("http://localhost:50887/");                   
                    client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

                    string subject = null, message = null;
                    bool cont = false;

                    do
                    {
                        Console.WriteLine("\nEnter subject: ");
                        subject = Console.ReadLine();
                        Console.WriteLine("Enter message: ");
                        message = Console.ReadLine();
                        
                        // POST 
                        // create a new entry
                        Post post = new Post() { Subject = subject, Message = message };
                        HttpResponseMessage response = await client.PostAsJsonAsync("forum/all", post);

                        if (response.IsSuccessStatusCode)
                        {
                            var newpost = await response.Content.ReadAsAsync<Forum>();
                            Console.WriteLine("***Added Post to Forum***\n" + newpost.ID + "\n" + newpost.Timestamp + "\n" + newpost.UserPost.Subject + "\n" + newpost.UserPost.Message + "\n");
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

        static void Main()
        {
            GetAll().Wait();
            GetPostById().Wait();
            GetNumberPosts().Wait();
            AddPost().Wait();
            Console.ReadKey();
        }
    }
}
