using AutoMapper;
using Microsoft.AspNetCore.Routing.Constraints;

namespace Application.Mapping;

public class MappingProfile : Profile
{
    public MappingProfile()
    {
        CreateMap<UpdateTopicDto, Topic>()
            .ForMember(dest => dest.Location, opt => opt.MapFrom(source => Location.Of(
                source.Location.City,
                source.Location.Street
                )))
            .ForMember(dest => dest.Id, opt => opt.MapFrom((_, dest) => dest.Id));

        CreateMap<CreateTopicDto, Topic>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(_ => TopicId.Of(Guid.NewGuid()))) 
            .ForMember(dest => dest.Location, opt => opt.MapFrom(source => Location.Of(
                source.Location.City,
                source.Location.Street
                )));
    }
}
