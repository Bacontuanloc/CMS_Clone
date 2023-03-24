using AutoMapper;
using CMS_API.Entities;

namespace CMS_API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

/*            CreateMap<Movie, MoviewDTO>()
                .ForMember(second => second.GenreDescription,
                map => map.MapFrom(
                    first => first.Genre.Description
                    ));*/

            CreateMap<UserLogin, User>();

        }
    }
}
