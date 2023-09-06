using AutoMapper;
using Twitter.Application.Dto.Tweet;
using Twitter.Infrastructure.Entities;

namespace Twitter.Application.Profiles;

public class MappingProfiles : Profile
{
    public MappingProfiles()
    {
        // Source => Destination
        CreateMap<CreateTweetDto, Tweet>()
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.Audience, opt => opt.MapFrom(src => src.Audience));

        CreateMap<Tweet, ReadTweetDto>()
            .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.Content, opt => opt.MapFrom(src => src.Content))
            .ForMember(dest => dest.CreatedAt, opt => opt.MapFrom(src => src.CreatedAt))
            .ForMember(dest => dest.LastUpdateAt, opt => opt.MapFrom(src => src.LastUpdateAt))
            .ForMember(dest => dest.Audience, opt => opt.MapFrom(src => src.Audience))
            .ForMember(dest => dest.SubTweets, opt => opt.MapFrom(src => src.SubTweets))
            .ReverseMap();

    }
}
