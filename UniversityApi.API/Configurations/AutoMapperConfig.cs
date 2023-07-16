﻿using ApiDtoLibrary.Authentication;
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
using UniversityApi.API.DataBase.Identity;
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
            CreateMap<EntityCourse, PostCourse>().ReverseMap();
            CreateMap<EntityCourse, PutCourse>().ReverseMap();

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

            //Models/Users
            CreateMap<ApiUserDto, ApiUser>().ReverseMap();
            CreateMap<LoginDto, ApiUser>().ReverseMap();

            //Authentication
            CreateMap<SignInResultDto, SignInResult>().ReverseMap();
        }
    }
}
