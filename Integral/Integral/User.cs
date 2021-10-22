using System;
using System.Collections.Generic;
using System.Text;
using Integral.Time;

namespace Integral
{
    public class User
    {
        public List<Tuple<string, DateTime>> posts { get; private set; } //TODO - Move into another class for persistant storage. Create mock class for tests, uses dependency injection to inject mock here.
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

        public void Follow(User user)
        {
            //TODO
        }

        public string GetTimeline(DateTime now = default)
        {
            return GetPosts(now);
        }

        public string GetWall(DateTime now = default)
        {
            return "Charlie - " + GetPosts(now);
        }

        private string GetPosts(DateTime now)
        {
            strBuilder.Clear();
            for (int i = 0; i < posts.Count; i++)
            {
                var timelinePost = posts[i].Item1 + GetPostTimestamp(posts[i].Item2, now);
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

        private string GetPostTimestamp(DateTime postTime, DateTime now)
        {
            var timestamp = TimeResolver.GetFormatedTimeSinceStart(postTime, now);
            if(timestamp == string.Empty)
            {
                return string.Empty;
            }
            return " (" + timestamp + ")";
        }
    }
}
