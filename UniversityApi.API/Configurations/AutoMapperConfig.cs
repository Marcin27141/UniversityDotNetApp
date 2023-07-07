﻿using ApiDtoLibrary;
using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using ApiDtoLibrary.Users;
using AutoMapper;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase.Identity;
namespace UniversityApi.API.Configurations
{
    public class AutoMapperConfig : Profile
    {
        public AutoMapperConfig()
        {
            //Models/Courses
            CreateMap<EntityCourse, FullCourse>().ReverseMap();
            CreateMap<EntityCourse, GetCourse>().ReverseMap();
            CreateMap<EntityCourse, PostCourse>().ReverseMap();
            CreateMap<EntityCourse, PutCourse>().ReverseMap();

            //Models/Professors
            CreateMap<PersonDto, EntityPerson>().ReverseMap();

            //Models/Professors
            CreateMap<EntityProfessor, FullProfessor>().ReverseMap();
            CreateMap<EntityProfessor, GetProfessor>().ReverseMap();
            CreateMap<EntityProfessor, PostProfessor>().ReverseMap();
            CreateMap<EntityProfessor, PutProfessor>().ReverseMap();

            //Models/Students
            CreateMap<EntityStudent, FullStudent>().ReverseMap();
            CreateMap<EntityStudent, GetStudent>().ReverseMap();
            CreateMap<EntityStudent, PostStudent>().ReverseMap();
            CreateMap<EntityStudent, PutStudent>().ReverseMap();

            //Models/Users
            CreateMap<ApiUserDto, ApiUser>().ReverseMap();
        }
    }
}
