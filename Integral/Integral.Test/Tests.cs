using System;
using Xunit;

namespace Integral.Test
{
    public class Tests
    {
        [Fact]
        public void Publishing()
        {
            const string postText = "I love the weather today.";
            User alice = new User();
            alice.Publish(postText);
            Assert.Equal(postText, alice.GetTimeline());
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
    }
}
