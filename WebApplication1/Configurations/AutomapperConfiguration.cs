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
            MapPersonalData(CreateMap<PostPersonDto, Person>());

            //Models/Professors
            MapPersonalData(CreateMap<FullProfessor, Professor>());
            MapPersonalData(CreateMap<GetProfessor, Professor>());
            MapPersonalData(CreateMap<PostProfessor, Professor>());
            MapPersonalData(CreateMap<PutProfessor, Professor>());

            //Models/Students
            MapPersonalData(CreateMap<FullStudent, Student>());
            MapPersonalData(CreateMap<GetStudent, Student>());
            MapPersonalData(CreateMap<PostStudent, Student>());
            MapPersonalData(CreateMap<PutStudent, Student>());

            //Models/ApplicationUser
            CreateMap<ApiUserDto, ApplicationUser>().ReverseMap();
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();

            //WebAppUser
            CreateMap<WebAppUser, ApplicationUser>().ReverseMap();
        }

        private void MapPersonalData<S, T>(IMappingExpression<S, T> mapping)
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
            })).ReverseMap();
        }
    }
}
