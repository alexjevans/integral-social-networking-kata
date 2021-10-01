using System;
using System.Collections.Generic;
using System.Text;
using Integral.Time;

namespace Integral
{
    public class User
    {
        private List<Tuple<string, DateTime>> posts;
        private StringBuilder strBuilder;

        public User()
        {
            posts = new List<Tuple<string, DateTime>>();
            strBuilder = new StringBuilder();
        }

        public void Publish(string post, DateTime time = default)
        {
            posts.Add(new Tuple<string, DateTime>(post, time));
        }

        public string GetTimeline(DateTime now = default)
        {
            strBuilder.Clear();
            for(int i = 0; i < posts.Count; i++)
            {
                var timelinePost = posts[i].Item1 + " (" + TimeResolver.GetMinutesSinceStart(posts[i].Item2, now) + ")";
                if (i != posts.Count - 1)
                {
                    strBuilder.AppendLine(timelinePost);
                }
                else
                {
                    strBuilder.Append(timelinePost);
                }
            }
            return strBuilder.ToString();
        }
    }
}
