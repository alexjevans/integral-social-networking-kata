using System;
using System.Collections.Generic;
using System.Text;
using Integral.Time;

namespace Integral
{
    public class User
    {
        public ITimeResolver time { get; private set; }

        private List<Tuple<string, DateTime>> posts;

        private StringBuilder strBuilder;

        public User()
        {
            posts = new List<Tuple<string, DateTime>>();
            time = new TimeResolver();
            strBuilder = new StringBuilder();
        }

        public void Publish(string post, DateTime time = default)
        {
            posts.Add(new Tuple<string, DateTime>(post, time));
        }

        public string GetTimeline()
        {
            strBuilder.Clear();
            for(int i = 0; i < posts.Count; i++)
            {
                if(i != posts.Count - 1)
                {
                    strBuilder.AppendLine(posts[i].Item1);
                }
                else
                {
                    strBuilder.Append(posts[i].Item1);
                }
            }
            return strBuilder.ToString();
        }
    }
}
