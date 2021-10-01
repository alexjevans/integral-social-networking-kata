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
    }
}
