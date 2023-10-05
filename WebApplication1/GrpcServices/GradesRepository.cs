using AutoMapper;
using Grpc.Core;
using Grpc.Net.Client;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services;
using GrpcService.Models;

namespace WebApplication1.GrpcServices
{
    public class GradesRepository : IGradesRepository
    {
        private readonly IMapper _mapper;
        private const string SERVER_ADDRESS = "https://localhost:7024";

        public GradesRepository(IMapper mapper)
        {
            this._mapper = mapper;
        }

        private GradesServer.GradesServerClient GetGradesServerClient(GrpcChannel channel)
        {
            return new GradesServer.GradesServerClient(channel);
        }

        public Task<Guid> addGrade(CourseGrade grade)
        {
            throw new NotImplementedException();
        }

        public async Task<CourseGrade> getGrade(Guid gradeId)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetGradesServerClient(channel);

            var request = new ReadGradeRequest
            {
                GradeId = gradeId.ToString()
            };

            try
            {
                var response = await client.ReadGradeAsync(request);
                return _mapper.Map<CourseGrade>(response);
            }
            catch (RpcException)
            {
                return null;
            }
        }

        public async Task<IList<CourseGrade>> getCourseGrades(Guid courseId)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetGradesServerClient(channel);

            var request = new ReadAllGradesCourseRequest
            {
                CourseId = courseId.ToString()
            };

            try
            {
                var response = await client.ReadAllGradesCourseAsync(request);
                return _mapper.Map<IList<CourseGrade>>(response);
            }
            catch (RpcException)
            {
                return new List<CourseGrade>();
            }
        }

        public async Task<IList<CourseGrade>> getStudentGrades(Guid studentId)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetGradesServerClient(channel);

            var request = new ReadAllGradesStudentRequest
            {
                StudentId = studentId.ToString()
            };

            try
            {
                var response = await client.ReadAllGradesStudentAsync(request);
                return _mapper.Map<IList<CourseGrade>>(response);
            }
            catch (RpcException)
            {
                return new List<CourseGrade>();
            }
        }

        public async Task<Guid> updateGrade(Guid gradeId, float newGradeValue)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetGradesServerClient(channel);

            try
            {
                var request = new UpdateGradeRequest() { 
                    GradeId = gradeId.ToString(),
                    GradeValue = newGradeValue
                };
                var response = await client.UpdateGradeAsync(request);
                return Guid.Parse(response.GradeId);
            }
            catch (RpcException)
            {
                return gradeId;
            }
        }

        public async Task<bool> deleteGrade(Guid gradeId)
        {
            using var channel = GrpcChannel.ForAddress(SERVER_ADDRESS);
            var client = GetGradesServerClient(channel);

            try
            {
                var request = new DeleteGradeRequest() { GradeId = gradeId.ToString() };
                var response = await client.DeleteGradeAsync(request);
                return response.WasSuccessful;
            }
            catch (RpcException)
            {
                return false;
            }
        }
    }
}
