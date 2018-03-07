using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

using DiscussionBoardAPI.Models;

namespace DiscussionBoardAPI.Controllers
{
    [RoutePrefix("forum")]
    public class DiscussionController : ApiController
    {
        // a collection of posts maintained in memory
        static private List<Forum> forumPosts = new List<Forum>();
        

        public DiscussionController()
        {

        }

        [Route("all")]
        [HttpGet]
        public IHttpActionResult GetAllPosts()
        {
            return Ok(forumPosts.OrderByDescending(p=>p.ID).ToList());
        }

        [Route("post/{id:int}")] 
        [HttpGet]
        public IHttpActionResult GetPostByID(int id)
        {
            var post = forumPosts.Where(p => p.ID == id);
            if(post != null)
            {
                return Ok(post);
            }
            else
            {
                return NotFound();
            }
        }

        [Route("all")]
        [HttpPost]
        public IHttpActionResult AddPost(Post post)
        {
            Forum forumPost = new Forum();
            if (ModelState.IsValid)
            {
                var duplicateError = forumPosts.SingleOrDefault(p => p.ID == forumPost.ID);
                if(duplicateError == null)
                {
                    forumPost.ID = forumPosts.Count() + 1;
                    forumPost.Timestamp = DateTime.Now;
                    forumPost.UserPost = post;
                    forumPosts.Add(forumPost);
                    return Ok(forumPost);
                }
                else
                {
                    return BadRequest("ID already exists");
                }
            }
            else
            {
                return BadRequest(ModelState);
            }
        }

        [Route("all/{number:min(1)}")]
        public IHttpActionResult GetNumberPosts(int number)
        {
            var posts = forumPosts.OrderByDescending(p => p.ID).Take(number);
            return Ok(posts.ToList());
        }




    }
}
