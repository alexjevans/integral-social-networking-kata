using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Integral.Time;

namespace Integral
{
    public class User
    {
        public List<Tuple<string, DateTime>>
            posts
        {
            get;
            private set;
        } //TODO - Move into another class for persistant storage. Create mock class for tests, uses dependency injection to inject mock here.

        private StringBuilder strBuilder;
        private readonly List<User> followedUsers;
        public string Name { get; set; }

        public User()
        {
            posts = new List<Tuple<string, DateTime>>();
            strBuilder = new StringBuilder();
            followedUsers = new List<User>();
            Name = string.Empty;
        }

        public void Publish(string post, DateTime time = default)
        {
            posts.Add(new Tuple<string, DateTime>(post, time));
        }

        public void Follow(User user)
        {
            followedUsers.Add(user);
        }

        public string GetTimeline(DateTime now = default)
        {
            return GetPosts(now, false, false);
        }

        public string GetWall(DateTime now = default)
        {
            return "Charlie - " + GetPosts(now, true, true);
        }

        private string GetPosts(DateTime now, bool includeFollowers, bool includeName)
        {
            strBuilder.Clear();

            var allPosts = new List<Tuple<string, DateTime, User>>(posts.Select(post =>
                new Tuple<string, DateTime, User>(post.Item1, post.Item2, this)));
            foreach (var followedUser in followedUsers)
            {
                allPosts.AddRange(followedUser.posts.Select(post =>
                    new Tuple<string, DateTime, User>(post.Item1, post.Item2, followedUser)));
            }

            allPosts = allPosts.OrderByDescending(post => post.Item2).ToList();

            for (int i = 0; i < allPosts.Count; i++)
            {
                var (message, time, user) = allPosts[i];
                if (user != this && !includeFollowers)
                {
                    continue;
                }

                var username = user.Name;
                var timelinePost = message + GetPostTimestamp(time, now);
                if (i != allPosts.Count - 1)
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
            if (timestamp == string.Empty)
            {
                return string.Empty;
            }

            return " (" + timestamp + ")";
        }
    }
}