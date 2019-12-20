using overapp.janus.Models.Domain;

namespace overapp.janus.Mappers
{
    public static class TransactionMapper
    {
        public static Card MapToDomainCard(Models.Dtos.Request.Card dto)
        {
            return new Card
            {
                Clue = dto.Number,
                Cvv = dto.Cvv,
                ExpiryMonth = dto.ExpiryMonth,
                ExpiryYear = dto.ExpiryYear
            };
        }
        public static BillingDetails MapToDomainBillingDetails(Models.Dtos.Request.BillingDetails dto)
        {
            return new BillingDetails
            {
                FirstName = dto.FirstName,
                LastName = dto.LastName,
                AddressLine1 = dto.AddressLine1,
                AddressLine2 = dto.AddressLine2,
                City = dto.City,
                Country = dto.Country
            };
        }
    }
}
