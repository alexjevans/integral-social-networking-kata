using System;
using Xunit;
using Integral.Time;

namespace Integral.Test
{
    public class Tests
    {
        const string alicePostText = "I love the weather today.";
        const string aliceTimelinePost1 = alicePostText;
        const string aliceTimelinePost2 = alicePostText + " (5 minutes ago)";
        const string bobPost1 = "Darn! We lost!";
        const string bobTimelinePost1 = bobPost1 + " (2 minutes ago)";
        const string bobPost2 = "Good game though.";
        const string bobTimelinePost2 = bobPost2 + " (1 minute ago)";
        const string charliePost = "I'm in New York today! Anyone wants to have a coffee?";
        const string charlieTimelinePost = charliePost + " (15 seconds ago)";

        private DateTime now;
        private User alice, bob, charlie;
        private DateTime bobPost1Time, bobPost2Time, charliePostTime, alicePostTime;

        public Tests()
        {
            now = DateTime.UtcNow;
            alice = new User { Name = "Alice" };
            bob = new User { Name = "Bob" };
            charlie = new User { Name = "Charlie" };

            bobPost1Time = now.Subtract(TimeSpan.FromMinutes(2));
            bobPost2Time = now.Subtract(TimeSpan.FromMinutes(1));
            charliePostTime = now.Subtract(TimeSpan.FromSeconds(15));
            alicePostTime = now.Subtract(TimeSpan.FromMinutes(5));
        }

        [Fact]
        public void Publishing()
        {
            alice.Publish(alicePostText, now);

            Assert.Equal(aliceTimelinePost1, alice.GetTimeline(now));
        }

        [Fact]
        public void Timeline()
        {
            var bobTimeline = bobTimelinePost2 + Environment.NewLine + bobTimelinePost1;

            bob.Publish(bobPost1, bobPost1Time);
            bob.Publish(bobPost2, bobPost2Time);

            Assert.Equal(bobTimeline, bob.GetTimeline(now));
        }

        [Fact]
        public void Following()
        {
            var charlieTimeline =
                "Charlie - " + charlieTimelinePost + Environment.NewLine +
                "Bob - " + bobTimelinePost2 + Environment.NewLine +
                "Bob - " + bobTimelinePost1 + Environment.NewLine +
                "Alice - " + aliceTimelinePost2;

            alice.Publish(alicePostText, alicePostTime);
            bob.Publish(bobPost1, bobPost1Time);
            bob.Publish(bobPost2, bobPost2Time);
            charlie.Publish(charliePost, charliePostTime);
            charlie.Follow(alice);
            charlie.Follow(bob);

            Assert.Equal(charlieTimeline, charlie.GetWall(now));
        }

        [Fact]
        public void GetTimeSinceSingleMinute()
        {
            const string expectedMinutesSince = "1 minute ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromMinutes(1));

            Assert.Equal(expectedMinutesSince, TimeResolver.GetFormatedTimeSinceStart(start, now));
        }

        [Fact]
        public void GetTimeSinceMultipleMinutes()
        {
            const string expectedMinutesSince = "2 minutes ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromMinutes(2));

            Assert.Equal(expectedMinutesSince, TimeResolver.GetFormatedTimeSinceStart(start, now));
        }

        [Fact]
        public void GetTimeSinceSeconds()
        {
            const string expectedSecondsSince = "15 seconds ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromSeconds(15));
            Assert.Equal(expectedSecondsSince, TimeResolver.GetFormatedTimeSinceStart(start, now));
        }
    }
}