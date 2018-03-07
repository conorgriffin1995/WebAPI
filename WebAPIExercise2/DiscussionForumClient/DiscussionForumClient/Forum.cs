using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DiscussionForumClient.Models
{
    class Forum
    {
        public int ID
        {
            get; set;
        }

        public DateTime Timestamp { get; set; }

        public Post UserPost { get; set; }
    }

    class Post
    {
        public string Subject { get; set; }
        public string Message { get; set; }
    }

}
