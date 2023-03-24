using AutoMapper;
using CMS_API.Entities;

namespace CMS_API.Models
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

/*            CreateMap<Movie, MoviewDTO>()
                .ForMember(second => second.Description,
                map => map.MapFrom(
                    first => first.Assignemnt.Description
                    ));*/

            CreateMap<UserLogin, User>();

        }
    }
}
