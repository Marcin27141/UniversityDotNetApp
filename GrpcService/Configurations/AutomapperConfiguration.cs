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
        private readonly GrpcDbContext _context;

        public AutomapperConfiguration(GrpcDbContext dbContext)
        {
            _context = dbContext;
        }

        public AutomapperConfiguration()
        {
            //Models/People
            CreateMap<Person, ReadPersonResponse>()
                .ForMember(dest => dest.PersonStatus, opt => opt.MapFrom(src => (GrpcPersonStatus)(int)src.PersonStatus))
                .ForMember(dest => dest.PersonId, opt => opt.MapFrom(src => src.PersonId.ToString()))
                .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => src.ApplicationUserId.ToString()))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PESEL, opt => opt.MapFrom(src => src.PESEL))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday == null ? null : src.Birthday.Value.ToUniversalTime().ToTimestamp()))
                .ForMember(dest => dest.Motherland, opt => opt.MapFrom(src => src.Motherland));

            CreateMap<CreatePersonRequest, Person>()
                .ForMember(dest => dest.PersonStatus, opt => opt.MapFrom(src => (PersonStatus)(int)src.PersonStatus))
                .ForMember(dest => dest.ApplicationUserId, opt => opt.MapFrom(src => Guid.Parse(src.ApplicationUserId)))
                .ForMember(dest => dest.FirstName, opt => opt.MapFrom(src => src.FirstName))
                .ForMember(dest => dest.LastName, opt => opt.MapFrom(src => src.LastName))
                .ForMember(dest => dest.PESEL, opt => opt.MapFrom(src => src.PESEL))
                .ForMember(dest => dest.Birthday, opt => opt.MapFrom(src => src.Birthday.ToDateTime()))
                .ForMember(dest => dest.Motherland, opt => opt.MapFrom(src => src.Motherland));

        }
    }
}
