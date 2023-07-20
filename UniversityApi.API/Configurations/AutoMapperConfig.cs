using ApiDtoLibrary.Authentication;
using ApiDtoLibrary.Courses;
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
        private readonly UniversityApiDbContext _context;

        public AutoMapperConfig(UniversityApiDbContext dbContext)
        {
            _context = dbContext;
        }

        public AutoMapperConfig()
        {
            //Models/Courses
            CreateMap<EntityCourse, BaseGetCourse>().ReverseMap();
            CreateMap<EntityCourse, GetCourse>().ReverseMap();
            CreateMap<PostCourse, EntityCourse>();
            CreateMap<PutCourse, EntityCourse>();

            //Models/People
            CreateMap<PostPersonDto, EntityPerson>().ReverseMap();
            CreateMap<GetPersonDto, EntityPerson>().ReverseMap();

            //Models/Professors
            CreateMap<EntityProfessor, BaseGetProfessor>().ReverseMap();
            CreateMap<EntityProfessor, GetProfessor>().ReverseMap();
            CreateMap<EntityProfessor, PostProfessor>().ReverseMap();
            CreateMap<EntityProfessor, PutProfessor>().ReverseMap();

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
