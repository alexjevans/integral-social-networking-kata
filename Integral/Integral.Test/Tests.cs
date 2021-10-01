using System;
using Xunit;
using Integral.Time;

namespace Integral.Test
{
    public class Tests
    {
        const string alicePostText = "I love the weather today.";
        const string aliceTimelinePost = alicePostText + " (0 minutes ago)";
        const string bobPost1 = "Darn! We lost!";
        const string bobTimelinePost1 = bobPost1 + " (2 minutes ago)";
        const string bobPost2 = "Good game though.";
        const string bobTimelinePost2 = bobPost2 + " (1 minute ago)";

        private DateTime now;
        private User alice, bob;
        private DateTime bobPost1Time, bobPost2Time;

        public Tests()
        {
            now = DateTime.UtcNow;
            alice = new User();
            bob = new User();

            bobPost1Time = now.Subtract(TimeSpan.FromMinutes(2));
            bobPost2Time = now.Subtract(TimeSpan.FromMinutes(1));
        }

        [Fact]
        public void Publishing()
        {
            alice.Publish(alicePostText, now);

            Assert.Equal(aliceTimelinePost, alice.GetTimeline(now));
        }

        [Fact]
        public void Timeline()
        {
            var bobTimeline = bobTimelinePost1 + Environment.NewLine + bobTimelinePost2;

            bob.Publish(bobPost1, bobPost1Time);
            bob.Publish(bobPost2, bobPost2Time);

            Assert.Equal(bobTimeline, bob.GetTimeline(now));
        }

        [Fact]
        public void Follow()
        {

        }

        [Fact]
        public void GetTimeSinceSingleMinute()
        {
            const string expectedMinutesSince = "1 minute ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromMinutes(1));

            var time = new TimeResolver();

            Assert.Equal(expectedMinutesSince, TimeResolver.GetMinutesSinceStart(start, now));
        }

        [Fact]
        public void GetTimeSinceMultipleMinutes()
        {
            const string expectedMinutesSince = "2 minutes ago";
            var now = DateTime.UtcNow;
            var start = now.Subtract(TimeSpan.FromMinutes(2));

            var time = new TimeResolver();

            Assert.Equal(expectedMinutesSince, TimeResolver.GetMinutesSinceStart(start, now));
        }
    }
}
