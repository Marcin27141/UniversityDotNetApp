using ApiDtoLibrary.Person;
using AutoMapper;
using Google.Protobuf.WellKnownTypes;
using GrpcService.Database;
using GrpcService.Models;
using GrpcService.Protos;
using System;

namespace GrpcService.Configurations
{
    public class AutomapperConfiguration : Profile
    {
        private readonly GrpcDbContext? _context;

        public AutomapperConfiguration(GrpcDbContext dbContext)
        {
            _context = dbContext;
        }

        public AutomapperConfiguration()
        {
            //Models/People
            CreateMap<Grade, ReadGradeResponse>()
                .ForMember(dest => dest.GradeId, opt => opt.MapFrom(src => src.GradeId.ToString()))
                .ForMember(dest => dest.StudentId, opt => opt.MapFrom(src => src.GradedStudentId))
                .ForMember(dest => dest.CourseId, opt => opt.MapFrom(src => src.GradedCourseId))
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.GradeValue))
                .ForMember(dest => dest.SubmissionDate, opt => opt.MapFrom(src => src.DateOfGradeSubmision.ToUniversalTime().ToTimestamp()));

            CreateMap<AddGradeRequest, Grade>()
                .ForMember(dest => dest.GradedStudentId, opt => opt.MapFrom(src => src.StudentId))
                .ForMember(dest => dest.GradedCourseId, opt => opt.MapFrom(src => src.CourseId))
                .ForMember(dest => dest.GradeValue, opt => opt.MapFrom(src => src.GradeValue))
                .ForMember(dest => dest.DateOfGradeSubmision, opt => opt.MapFrom(src => DateTime.Now));

        }
    }
}
