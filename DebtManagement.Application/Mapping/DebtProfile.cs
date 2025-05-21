using AutoMapper;
using DebtManagement.Application.Commands;
using DebtManagement.Application.Dtos;
using DebtManagement.Domain.Entities;
using DebtManagement.Domain.ValueObjects;

namespace DebtManagement.Application.Mapping;
public class DebtProfile : Profile
{
    public DebtProfile()
    {

        // Mapeamento CreateDebtCommand -> Debt
        CreateMap<CreateDebtCommand, Debt>()
            .ForMember(dest => dest.Id, opt => opt.Ignore()) // Ignorar Id auto-generado
            .ForMember(dest => dest.Installments,
                       opt => opt.MapFrom(src => src.Installments))
            .ReverseMap();

        // Mapeamento DebtInstallment -> DebtInstallmentDto
        CreateMap<DebtInstallment, DebtInstallmentDto>()
            .ForMember(dest => dest.Value,
                       opt => opt.MapFrom(src => src.OriginalValue))
            .ReverseMap()
            .ForMember(dest => dest.OriginalValue,
                       opt => opt.MapFrom(src => src.Value));


        // Mapeamento das parcelas
        CreateMap<DebtInstallmentDto, DebtInstallment>()
        .ForMember(dest => dest.DebtId, opt => opt.Ignore()) // Ignorar no mapeamento inicial
        .ForMember(dest => dest.OriginalValue, opt => opt.MapFrom(src => src.Value))
        .ReverseMap()
        .ForMember(dest => dest.Value, opt => opt.MapFrom(src => src.OriginalValue));

        CreateMap<Debt, DebtDto>()
             .ForMember(dest => dest.Installments, opt => opt.MapFrom(src => src.Installments));
        

        CreateMap<DebtSummary, DebtSummaryDto>()
            .ForMember(dest => dest.DebtNumber, opt => opt.MapFrom(src => src.DebtNumber))
            .ForMember(dest => dest.DebtorName, opt => opt.MapFrom(src => src.DebtorName))
            .ForMember(dest => dest.InstallmentCount, opt => opt.MapFrom(src => src.InstallmentCount))
            .ForMember(dest => dest.OriginalAmount, opt => opt.MapFrom(src => src.OriginalAmount))
            .ForMember(dest => dest.DaysOverdue, opt => opt.MapFrom(src => src.DaysOverdue))
            .ForMember(dest => dest.UpdatedAmount, opt => opt.MapFrom(src => src.UpdatedAmount))
            .ForMember(dest => dest.FineAmount, opt => opt.MapFrom(src => src.FineAmount))
            .ForMember(dest => dest.InterestAmount, opt => opt.MapFrom(src => src.InterestAmount))
            .ReverseMap(); // Se precisar do mapeamento inverso
    }

}
