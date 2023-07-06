using ApiDtoLibrary.Courses;
using ApiDtoLibrary.Professors;
using ApiDtoLibrary.Students;
using ApiDtoLibrary.Users;
using AutoMapper;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using WebApplication1.Services.People;

namespace WebApplication1.Configurations
{
    public class AutomapperConfiguration : Profile
    {
        public AutomapperConfiguration()
        {
            //Models/Courses
            CreateMap<Services.Course, Course>().ReverseMap();
            CreateMap<Services.Course, GetCourse>().ReverseMap();
            CreateMap<Services.Course, PostCourse>().ReverseMap();
            CreateMap<Services.Course, PutCourse>().ReverseMap();

            //Models/Professors
            CreateMap<ApiDtoLibrary.Professors.Professor, Services.People.Professor>().ForMember(dest => dest.PersonalData, opt => opt.MapFrom(src => new PersonalData
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                PESEL = src.PESEL,
                Birthday = src.Birthday,
                Motherland = src.Motherland
            })).ReverseMap();
            CreateMap<GetProfessor, Services.People.Professor>().ForMember(dest => dest.PersonalData, opt => opt.MapFrom(src => new PersonalData
            {
                FirstName = src.FirstName,
                LastName = src.LastName,
                PESEL = src.PESEL,
                Birthday = src.Birthday,
                Motherland = src.Motherland
            })).ReverseMap();
            CreateMap<Services.People.Professor, PostProfessor>().ReverseMap();
            CreateMap<Services.People.Professor, PutProfessor>().ReverseMap();

            //Models/Students
            CreateMap<Services.People.Student, ApiDtoLibrary.Students.Student>().ReverseMap();
            CreateMap<Services.People.Student, GetStudent>().ReverseMap();
            CreateMap<Services.People.Student, PostStudent>().ReverseMap();
            CreateMap<Services.People.Student, PutStudent>().ReverseMap();
        }
    }
}
