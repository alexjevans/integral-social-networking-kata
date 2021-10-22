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


        [Theory]
        [InlineData("1 minute ago", 60)]
        [InlineData("2 minutes ago", 120)]
        [InlineData("15 seconds ago", 15)]
        public void GetTimeSince(string expectedValue, int secondsOffset)
        {
            var start = now.Subtract(TimeSpan.FromSeconds(secondsOffset));
            Assert.Equal(expectedValue, TimeResolver.GetFormatedTimeSinceStart(start, now));
        }
    }
}