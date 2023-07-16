using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Person;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using ApiDtoLibrary.Users;
using AutoMapper;
using System.Linq;
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
            CreateMap<Course, BaseGetCourse>().ReverseMap();
            CreateMap<Course, GetCourse>().ReverseMap();
            CreateMap<Course, PostCourse>().ReverseMap();
            CreateMap<Course, PutCourse>().ReverseMap();

            //Models/People
            CreateInOutMapping<PostPersonDto, Person>();
            CreateInOutMapping<GetPersonDto, Person>();

            //Models/Professors
            CreateInOutMapping<BaseGetProfessor, Professor>();
            CreateInOutMapping<GetProfessor, Professor>();
            CreateInOutMapping<PostProfessor, Professor>();
            CreateInOutMapping<PutProfessor, Professor>();

            //Models/Students
            CreateInOutMapping<BaseGetStudent, Student>();
            CreateInOutMapping<GetStudent, Student>();
            MapOutgoingStudents<Student, PostStudent>();
            MapOutgoingStudents<Student, PutStudent>();

            //Models/ApplicationUser
            CreateMap<ApiUserDto, ApplicationUser>().ReverseMap();
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();

            //WebAppUser
            CreateMap<WebAppUser, ApplicationUser>().ReverseMap();
        }

        private void CreateInOutMapping<S, T>()
            where S : BasePersonDto
            where T : Person
        {
            MapPersonalDataInside(CreateMap<S, T>());
            MapPersonalDataOutside(CreateMap<T, S>());
        }

        private void MapPersonalDataInside<S, T>(IMappingExpression<S, T> mapping)
            where S : BasePersonDto
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
            where T : BasePersonDto
        {
            mapping
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.PersonalData.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.PersonalData.LastName))
                .ForMember(dest => dest.PESEL, opt => opt.MapFrom(src => src.PersonalData.PESEL))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.PersonalData.Birthday))
                .ForMember(dest => dest.Motherland, opt => opt.MapFrom(src => src.PersonalData.Motherland));
        }

        private void MapOutgoingStudents<S, T>()
            where S : Student
            where T : ToApiStudent
        {
            var map = CreateMap<S, T>();
            MapPersonalDataOutside<S, T>(map);
            MapCoursesToIds<S, T>(map);
        }

        private void MapCoursesToIds<S, T>(IMappingExpression<S, T> mapping)
            where S : Student
            where T : ToApiStudent
        {
            mapping
                .ForMember(dest => dest.CoursesIds, opt => opt.MapFrom(src => src.Courses.Select(c => c.EntityCourseID.ToString())));
        }
    }
}
