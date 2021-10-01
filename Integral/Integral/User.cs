using System;
using System.Collections.Generic;

namespace Integral
{
    public class User
    {
        private List<string> posts;

        public User()
        {
            posts = new List<string>();
        }

        public void Publish(string post)
        {
            posts.Add(post);
        }

        public string GetTimeline()
        {
            return string.Join("\n", posts);
        }
    }
}
