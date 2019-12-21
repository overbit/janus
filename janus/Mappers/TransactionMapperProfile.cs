using System;
using AutoMapper;
using overapp.janus.Models.Domain;
using overapp.janus.Models.Dtos.Request;
using overapp.janus.Models.Dtos.Response;

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
                .ForMember(dto => dto.BillingDetails, opt => opt.MapFrom(t => t.BillingDetails));
        }
    }

    public class MaskCardNumberResolver : IValueResolver<CardDto, Card, string>
    {
        public string Resolve(CardDto source, Card destination, string member, ResolutionContext context)
        {
            var mask = new String('*', 12);
            var lastDigits = source.Number.Substring(12);
            return $"{mask}{lastDigits}";
        }
    }
}
