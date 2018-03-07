using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace DiscussionBoardAPI.Models
{
    public class Post
    {       
        [Required(ErrorMessage = "Invalid Subject")]
        [StringLength(25)]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Invalid Message")]
        [StringLength(100)]
        public string Message { get; set; }
    }

    public class Forum
    {
        public int ID
        {
            get; set;
        }

        public DateTime Timestamp { get; set; }

        public Post UserPost { get; set; }
    }
}