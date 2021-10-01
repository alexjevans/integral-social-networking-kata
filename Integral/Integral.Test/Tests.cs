using System;
using Xunit;
using Integral.Time;

namespace Integral.Test
{
    public class Tests
    {
        [Fact]
        public void Publishing()
        {
            const string postText = "I love the weather today.";
            const string timelinePost = postText + " (0 minutes ago)";
            var now = DateTime.UtcNow;

            User alice = new User();
            alice.Publish(postText, now);

            Assert.Equal(timelinePost, alice.GetTimeline(now));
        }

        [Fact]
        public void Timeline()
        {
            const string bobPost1 = "Darn! We lost! (2 minute ago)";
            const string bobPost2 = "Good game though. (1 minute ago)";
            var bobTimeline = bobPost1 + Environment.NewLine + bobPost2;
            User bob = new User();
            bob.Publish(bobPost1);
            bob.Publish(bobPost2);
            Assert.Equal(bobTimeline, bob.GetTimeline());
        }

        [Fact]
        public void GetTimeSinceSingleMinute()
        {
            const string expectedMinutesSince = "1 minute ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromMinutes(1));

            var time = new TimeResolver();

            Assert.Equal(expectedMinutesSince, time.GetMinutesSinceStart(start, now));
        }

        [Fact]
        public void GetTimeSinceMultipleMinutes()
        {
            const string expectedMinutesSince = "2 minutes ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromMinutes(2));

            var time = new TimeResolver();

            Assert.Equal(expectedMinutesSince, time.GetMinutesSinceStart(start, now));
        }
    }
}
