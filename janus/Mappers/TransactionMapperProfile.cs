using AutoMapper;
using overapp.janus.Models.Domain;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;
using overapp.janus.Models.ExternalDtos;

namespace overapp.janus.Mappers
{
    public class TransactionMapperProfile : Profile
    {
        public TransactionMapperProfile()
        {
            CreateMap<CardDto, Card>()
                .ForMember(card => card.Clue, opt => opt.MapFrom<MaskCardNumberResolver>())
                .ReverseMap();

            CreateMap<BillingDetailsDto, BillingDetails>().ReverseMap();

            CreateMap<Transaction, TransactionDto>()
                .ForMember(dto => dto.Id, opt => opt.MapFrom(t => t.ExternalId))
                .ForMember(dto => dto.CardClue, opt => opt.MapFrom(t => t.CardDetails.Clue))
                .ForMember(dto => dto.BillingDetails, opt => opt.MapFrom(t => t.BillingDetails))
                .ForMember(dto => dto.IsSuccess, opt => opt.MapFrom(t => t.Status));

            CreateMap<Card, BankPaymentCardDto>();
            CreateMap<BillingDetails, BankPaymentBillingDetailsDto>()
                .ForMember(dto => dto.AddressLine, opt => opt.MapFrom<AddressLineResolver>());
        }
    }

    public class MaskCardNumberResolver : IValueResolver<CardDto, Card, string>
    {
        public string Resolve(CardDto source, Card destination, string member, ResolutionContext context)
        {
            var mask = new string('*', 12);
            var lastDigits = source.Number.Substring(12);
            return $"{mask}{lastDigits}";
        }
    }

    public class AddressLineResolver : IValueResolver<BillingDetails, BankPaymentBillingDetailsDto, string>
    {
        public string Resolve(BillingDetails source, BankPaymentBillingDetailsDto destination, string member, ResolutionContext context)
        {
            return $"{source.AddressLine1} {source.AddressLine2}";
        }
    }
}
