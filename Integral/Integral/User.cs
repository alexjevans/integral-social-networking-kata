using System;
using System.Collections.Generic;
using Integral.Time;

namespace Integral
{
    public class User
    {
        public ITimeResolver time { get; private set; }

        private List<string> posts;

        public User()
        {
            posts = new List<string>();
            time = new TimeResolver();
        }

        public void Publish(string post, DateTime time = default)
        {
            posts.Add(post);
        }

        public string GetTimeline()
        {
            return string.Join(Environment.NewLine, posts);
        }
    }
}
