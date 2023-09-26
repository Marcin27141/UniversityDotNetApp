using ApiDtoLibrary.Authentication;
using ApiDtoLibrary.Courses;
using ApiDtoLibrary.GraphQL.Courses;
using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Person;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using ApiDtoLibrary.Users;
using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
namespace UniversityApi.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Models/Courses
            CreateMap<EntityCourse, BaseGetCourse>().ReverseMap();
            CreateMap<EntityCourse, GetCourse>().ReverseMap();
            CreateMap<PostCourse, EntityCourse>();
            CreateMap<PutCourse, EntityCourse>();
            CreateMap<AddCourseInput, EntityCourse>();

            //Models/People
            CreateMap<PostPersonDto, EntityPerson>().ReverseMap();
            CreateMap<GetPersonDto, EntityPerson>().ReverseMap();

            //Models/Professors
            CreateMap<EntityProfessor, BaseGetProfessor>().ReverseMap();
            CreateMap<EntityProfessor, GetProfessor>().ReverseMap();
            CreateMap<EntityProfessor, PostProfessor>().ReverseMap();
            CreateMap<EntityProfessor, PutProfessor>().ReverseMap();
            CreateMap<AddProfessorInput, EntityProfessor>()
                .ForMember(dest => dest.PersonStatus, opt => opt.MapFrom(src => PersonStatus.Professor));
            //CreateMap<EntityProfessor, AddProfessorPayload>()
            //    .ForMember(dest => dest.PersonStatus, opt => opt.MapFrom(src => PersonStatus.Professor));
            //CreateMap<EntityProfessor, UpdateProfessorPayload>()
            //    .ForMember(dest => dest.PersonStatus, opt => opt.MapFrom(src => PersonStatus.Professor));

            //Models/Students
            CreateMap<EntityStudent, BaseGetStudent>().ReverseMap();
            CreateMap<EntityStudent, GetStudent>().ReverseMap();
            CreateMap<EntityStudent, PostStudent>().ReverseMap();
            CreateMap<EntityStudent, PutStudent>().ReverseMap();

            //Authentication
            CreateMap<SignInResultDto, SignInResult>().ReverseMap();
        }
    }
}
