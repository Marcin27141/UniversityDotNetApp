using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Person;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using ApiDtoLibrary.Users;
using AutoMapper;
using WebApplication1.Database;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Configurations
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            //Models/Courses
            CreateMap<Course, FullCourse>().ReverseMap();
            CreateMap<Course, GetCourse>().ReverseMap();
            CreateMap<Course, PostCourse>().ReverseMap();
            CreateMap<Course, PutCourse>().ReverseMap();

            //Models/People
            CreateInOutMapping<PostPersonDto, Person>();

            //Models/Professors
            CreateInOutMapping<FullProfessor, Professor>();
            CreateInOutMapping<GetProfessor, Professor>();
            CreateInOutMapping<PostProfessor, Professor>();
            CreateInOutMapping<PutProfessor, Professor>();

            //Models/Students
            CreateInOutMapping<FullStudent, Student>();
            CreateInOutMapping<GetStudent, Student>();
            CreateInOutMapping<PostStudent, Student>();
            CreateInOutMapping<PutStudent, Student>();

            //Models/ApplicationUser
            CreateMap<ApiUserDto, ApplicationUser>().ReverseMap();
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();

            //WebAppUser
            CreateMap<WebAppUser, ApplicationUser>().ReverseMap();
        }

        private void CreateInOutMapping<S, T>()
            where S : PostPersonDto
            where T : Person
        {
            MapPersonalDataInside(CreateMap<S, T>());
            MapPersonalDataOutside(CreateMap<T, S>());
        }

        private void MapPersonalDataInside<S, T>(IMappingExpression<S, T> mapping)
            where S : PostPersonDto
            where T : Person
        {
            mapping.ForMember(dest => dest.PersonalData, opt => opt.MapFrom(src => new PersonalData
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                PESEL = src.PESEL,
                Birthday = src.Birthday,
                Motherland = src.Motherland
            }));
        }

        private void MapPersonalDataOutside<S, T>(IMappingExpression<S, T> mapping)
            where S : Person
            where T : PostPersonDto
        {
            mapping
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.PersonalData.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.PersonalData.LastName))
                .ForMember(dest => dest.PESEL, opt => opt.MapFrom(src => src.PersonalData.PESEL))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.PersonalData.Birthday))
                .ForMember(dest => dest.Motherland, opt => opt.MapFrom(src => src.PersonalData.Motherland));
        }
    }
}
