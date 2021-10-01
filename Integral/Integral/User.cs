using System;

namespace Integral
{
    public class User
    {
        private string post;

        public void Publish(string post)
        {
            this.post = post;
        }

        public string GetTimeline()
        {
            return post;
        }
    }
}
