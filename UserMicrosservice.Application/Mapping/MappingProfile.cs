using AutoMapper;
using UserMicrosservice.Domain;

namespace UserMicrosservice.Application;


public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<CreateUserDto, User>()
            .ForMember(dest => dest.UserId, opt => opt.Ignore())
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

        CreateMap<UpdateUserDto, User>()
            .ForMember(dest => dest.CreatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.UpdatedAt, opt => opt.Ignore())
            .ForMember(dest => dest.LastModifiedBy, opt => opt.Ignore());

        CreateMap<UserDto, User>().ReverseMap();
    }
}
