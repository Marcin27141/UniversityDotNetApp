using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Person;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using ApiDtoLibrary.Users;
using AutoMapper;
using System;
using System.Linq;
using System.Reactive.Subjects;
using WebApplication1.Database;
using WebApplication1.GraphQLServices.GraphQLDtos;
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
            MapProfessorToId(CreateMap<Course, PostCourse>());
            MapProfessorToId(CreateMap<Course, PutCourse>());
            CreateMap<GraphQLCourseDto, Course>()
                    .ForMember(dest => dest.EntityCourseID, opt => opt.MapFrom(src => src.Id))
                    .ForMember(dest => dest.Students, opt => opt.MapFrom(src => src.EnrolledStudents));

            //Models/People
            CreateInOutMapping<PostPersonDto, Person>();
            CreateInOutMapping<GetPersonDto, Person>();
            MapGraphQLPerson(CreateMap<GraphQLPersonDto, Person>());

            //Models/Professors
            CreateInOutMapping<BaseGetProfessor, Professor>();
            CreateInOutMapping<GetProfessor, Professor>();
            CreateInOutMapping<PostProfessor, Professor>();
            CreateInOutMapping<PutProfessor, Professor>();
            MapGraphQLPerson(
                CreateMap<GraphQLProfessorDto, Professor>()
                    .ForMember(dest => dest.IdCode, opt => opt.MapFrom(src => src.ProfessorId))
                );
                

            //Models/Students
            CreateInOutMapping<BaseGetStudent, Student>();
            CreateInOutMapping<GetStudent, Student>();
            MapOutgoingStudents<Student, PostStudent>();
            MapOutgoingStudents<Student, PutStudent>();
            MapGraphQLPerson(CreateMap<GraphQLStudentDto, Student>());

            //Models/ApplicationUser
            CreateMap<ApiUserDto, ApplicationUser>().ReverseMap();
            CreateMap<LoginDto, ApplicationUser>().ReverseMap();

            //WebAppUser
            CreateMap<WebAppUser, ApplicationUser>().ReverseMap();

            //Notifications
            CreateMap<GraphQLNotificationDto, Notification>();

            //Grades
            CreateMap<ReadGradeResponse, CourseGrade>()
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => Guid.Parse(src.GradeId)))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.GradeValue))
                .ForMember(dest => dest.TimeOfAdding, opt => opt.MapFrom(src => src.SubmissionDate.ToDateTime()));

            CreateMap<CourseGrade, AddGradeRequest>()
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.GradeValue));
        }

        private void MapGraphQLPerson<S, T>(IMappingExpression<S, T> mapping)
            where S : GraphQLPersonDto
            where T : Person
        {
            mapping.ForMember(dest => dest.PersonalData, opt => opt.MapFrom(src => new PersonalData
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                PESEL = src.PESEL,
                Birthday = src.Birthday,
                Motherland = src.Motherland
            }))
            .ForMember(dest => dest.PersonStatus, opt => opt.MapFrom(src => (PersonStatus)(int)src.PersonStatus))
            .ForMember(dest => dest.EntityPersonID, opt => opt.MapFrom(src => src.Id))
            .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.UserId.ToString()));
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

        private void MapProfessorToId<S, T>(IMappingExpression<S, T> mapping)
            where S : Course
            where T : BaseCourse
        {
            mapping
                .ForMember(dest => dest.ProfessorId, opt => opt.MapFrom(src => src.Professor.EntityPersonID));
        }
    }
}
