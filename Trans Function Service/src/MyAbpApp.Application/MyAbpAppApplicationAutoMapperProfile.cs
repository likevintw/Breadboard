using AutoMapper;
using MyAbpApp.Compensations;
using MyAbpApp.CompensationDtos;
namespace MyAbpApp;

public class MyAbpAppApplicationAutoMapperProfile : Profile
{
    public MyAbpAppApplicationAutoMapperProfile()
    {
        // Compensation 實體到 CompensationDto 的映射
        CreateMap<Compensation, CompensationDto>()
            .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.DeviceType))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.CompensationValue, opt => opt.MapFrom(src => src.CompensationValue));

        // CreateCompensationDto 到 Compensation 實體的映射
        CreateMap<CreateCompensationDto, Compensation>()
            .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.DeviceType))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.CompensationValue, opt => opt.MapFrom(src => src.CompensationValue));

        // Compensation 實體到 GetCompensationDto 的映射
        CreateMap<Compensation, CompensationDto>()
            .ForMember(dest => dest.DeviceType, opt => opt.MapFrom(src => src.DeviceType))
            .ForMember(dest => dest.Version, opt => opt.MapFrom(src => src.Version))
            .ForMember(dest => dest.CompensationValue, opt => opt.MapFrom(src => src.CompensationValue));

        // List<Compensation> 到 GetCompensationDto 的映射
        CreateMap<List<Compensation>, GetCompensationDto>()
            .ForMember(dest => dest.CompensationList, opt => opt.MapFrom(src => src));
    }
}