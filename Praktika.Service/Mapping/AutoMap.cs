using AutoMapper;
using Praktika.Domain.Entities;
using Praktika.Service.UserDto;

namespace Praktika.Service.Mapping
{
    public class AutoMap : Profile
    {
        public AutoMap()
        {
            CreateMap<UserCreateDto, User>().ReverseMap();

            CreateMap<CourseCreateDto, Course>().ReverseMap();

            CreateMap<ContentCreateDto, Content>().ReverseMap();
        }
    }
}
