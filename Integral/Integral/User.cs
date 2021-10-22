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
        
        private readonly List<User> followedUsers;
        public string Name { get; set; }

        public User()
        {
            posts = new List<Tuple<string, DateTime>>();
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
            return GetPosts(now, true, true);
        }

        private string GetPosts(DateTime now, bool includeFollowers, bool includeName)
        {
            var allPosts = GetWallPosts();

            var filteredPosts = allPosts
                .Where(post => post.Item3 == this || includeFollowers)
                .Select(post => 
                    FormatPost(now, includeName, post)
                );

            return string.Join(Environment.NewLine, filteredPosts);
        }

        private string FormatPost(DateTime now, bool includeName, Tuple<string, DateTime, User> post)
        {
            return FormatPost(now, includeName, post.Item3.Name, post.Item1, post.Item2);
        }

        private string FormatPost(DateTime now, bool includeName, string username, string message, DateTime time)
        {
            var timelinePost = message + GetPostTimestamp(time, now);
            if (includeName)
            {
                timelinePost = username + " - " + timelinePost;
            }

            return timelinePost;
        }

        private List<Tuple<string, DateTime, User>> GetWallPosts()
        {
            var allPosts = new List<Tuple<string, DateTime, User>>(posts.Select(post =>
                new Tuple<string, DateTime, User>(post.Item1, post.Item2, this)));
            foreach (var followedUser in followedUsers)
            {
                allPosts.AddRange(followedUser.posts.Select(post =>
                    new Tuple<string, DateTime, User>(post.Item1, post.Item2, followedUser)));
            }

            return allPosts.OrderByDescending(post => post.Item2).ToList();
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