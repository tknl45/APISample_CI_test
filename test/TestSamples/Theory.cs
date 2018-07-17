using Xunit;

namespace MyFirstUnitTests.TestSamples
{
    public class Theory
    {
        [Theory]
        [InlineData(2)]
        [InlineData(4)]
        [InlineData(6)]
        public void MyFirstTheory(int value)
        {
            Assert.True(IsOdd(value));
        }

        bool IsOdd(int value)
        {
            return value % 2 == 1;
        }
    }
}