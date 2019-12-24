using System;
using AutoMapper;
using NUnit.Framework;
using overapp.janus.Mappers;
using overapp.janus.Models.Domain;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;
using overapp.janus.Models.ExternalDtos;

namespace janusTests.Mappers
{
    public class TransactionMapperTests
    {
        private IMapper mapper;

        [SetUp]
        public void Setup()
        {
            var config = new MapperConfiguration(opts => opts.AddProfile(new TransactionMapperProfile()));

            mapper = config.CreateMapper();
        }

        [Test]
        public void CardDtoMappingTest()
        {
            // Arrange
            var cardDto = new CardDto
            {
                Number = "4111111111111101",
                Cvv = "123",
                ExpiryMonth = "12",
                ExpiryYear = "22"
            };

            // Act
            var card = mapper.Map<Card>(cardDto);

            // Assert
            Assert.AreEqual("************1101", card.Clue);
            Assert.AreEqual(cardDto.Number, card.Number);
            Assert.AreEqual(cardDto.Cvv, card.Cvv);
            Assert.AreEqual(cardDto.ExpiryMonth, card.ExpiryMonth);
            Assert.AreEqual(cardDto.ExpiryYear, card.ExpiryYear);
        }

        [Test]
        public void TransactionMappingTest()
        {
            // Arrange
            const string expectedCardClue = "************1101";
            string expectedTransactionExternalId = Guid.NewGuid().ToString("N");

            var transaction = new Transaction
            {
                Amount = 10,
                CurrencyCode = "GBP",
                CardDetails = new BaseCard { Clue = expectedCardClue },
                ExternalId = expectedTransactionExternalId
            };

            // Act
            var dto = mapper.Map<TransactionDto>(transaction);

            // Assert
            Assert.AreEqual(expectedCardClue, dto.CardClue);
            Assert.AreEqual(expectedTransactionExternalId, dto.Id);
            Assert.AreEqual(transaction.CurrencyCode, dto.CurrencyCode);
            Assert.AreEqual(transaction.Amount, dto.Amount);
        }

        [Test]
        public void BankExternalDtosMappingTest()
        {
            // Arrange
            var b = new BillingDetails
            {
                FirstName = "first",
                LastName = "last",
                AddressLine1 = "line1",
                AddressLine2 = "line2",
                City = "city",
                Country = "country"
            };

            // Act
            var mappedBilling = mapper.Map<BankPaymentBillingDetailsDto>(b);

            // Assert
            Assert.AreEqual(b.FirstName, mappedBilling.FirstName);
            Assert.AreEqual(b.LastName, mappedBilling.LastName);
            Assert.AreEqual(b.City, mappedBilling.City);
            Assert.AreEqual(b.Country, mappedBilling.Country);
            Assert.AreEqual($"{b.AddressLine1} {b.AddressLine2}", mappedBilling.AddressLine);
            Assert.AreEqual(b.Country, mappedBilling.Country);

        }
    }
}
