using NUnit.Framework;

namespace overapp.janus.Attributes.Tests
{
    [TestFixture]
    public class StringIsoCurrencyCodeAttributeTests
    {
        [TestCase("GBP")]
        [TestCase("usd")]
        public void IsValidTest(string currencyCode)
        {
            // Arrange
            var attr = new StringIsoCurrencyCodeAttribute();

            // Act & Assert
            Assert.IsTrue(attr.IsValid(currencyCode));
        }

        [TestCase("ITT")]
        [TestCase("mom")]
        public void IsNotValidTest(string currencyCode)
        {
            // Arrange
            var attr = new StringIsoCurrencyCodeAttribute();

            // Act & Assert
            Assert.IsFalse(attr.IsValid(currencyCode));
        }
    }
}