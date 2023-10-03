using AutoMapper;
using Grpc.Core;
using GrpcService.Database;
using GrpcService.Models;
using GrpcService.Protos;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;
using static GrpcService.Services.GrpcModelHelper;

namespace GrpcService.Services
{
    public class GradesService : GradesServer.GradesServerBase
    {
        private readonly GrpcDbContext _dbContext;
        private readonly IMapper _mapper;

        public GradesService(GrpcDbContext dbContext, IMapper mapper)
        {
            this._dbContext = dbContext;
            this._mapper = mapper;
        }

        public override async Task<AddGradeResponse> AddGrade(AddGradeRequest request, ServerCallContext context)
        {
            var nullChecklist = new List<string>() { request.StudentId, request.CourseId, request.GradeValue.ToString() };
            VerifyStringsNullabilityRequirements(nullChecklist);
            var guidChecklist = new List<string>() { request.StudentId, request.CourseId };
            VerifyGuidsValidity(guidChecklist);
            VerifyGradeValidity((float)request.GradeValue);

            var grade = _mapper.Map<Grade>(request);
            await _dbContext.AddAsync(grade);
            await _dbContext.SaveChangesAsync();

            return await Task.FromResult(new AddGradeResponse
            {
                GradeId = grade.GradeId.ToString()
            });
        }

        public override async Task<ReadGradeResponse> ReadGrade(ReadGradeRequest request, ServerCallContext context)
        {
            var gradeIdString = request.GradeId;
            VerifyGuidsValidity(new List<string>() { gradeIdString });

            var grade = await _dbContext.Grades.FindAsync(Guid.Parse(gradeIdString));

            if (grade != null)
                return await Task.FromResult(_mapper.Map<ReadGradeResponse>(grade));
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No grade with id {gradeIdString} was found"));
        }

        public override async Task<ReadAllGradesCourseResponse> ReadAllGradesCourse(ReadAllGradesCourseRequest request, ServerCallContext context)
        {
            var courseIdString = request.CourseId;
            VerifyGuidsValidity(new List<string>() { courseIdString });

            var response = new ReadAllGradesCourseResponse();

            var courseWithGrades = await _dbContext
                .Courses
                .Include(c => c.Grades)
                .FirstOrDefaultAsync(c => c.CourseID == Guid.Parse(courseIdString))
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"No course with id {courseIdString} was found"));
            
            courseWithGrades.Grades.ToList().ForEach(grade => response.Grades.Add(_mapper.Map<ReadGradeResponse>(grade)));
            return await Task.FromResult(response);
        }

        public override async Task<ReadAllGradesStudentResponse> ReadAllGradesStudent(ReadAllGradesStudentRequest request, ServerCallContext context)
        {
            var studentIdString = request.StudentId;
            VerifyGuidsValidity(new List<string>() { studentIdString });

            var response = new ReadAllGradesStudentResponse();

            var studentWithGrades = await _dbContext
                .Set<Student>()
                .Include(s => s.Grades)
                .FirstOrDefaultAsync(s => s.PersonId == Guid.Parse(studentIdString))
                ?? throw new RpcException(new Status(StatusCode.NotFound, $"No student with id {studentIdString} was found"));

            studentWithGrades.Grades.ToList().ForEach(grade => response.Grades.Add(_mapper.Map<ReadGradeResponse>(grade)));
            return await Task.FromResult(response);
        }

        public override async Task<UpdateGradeResponse> UpdateGrade(UpdateGradeRequest request, ServerCallContext context)
        {
            var nullChecklist = new List<String>() { request.GradeId };
            VerifyStringsNullabilityRequirements(nullChecklist);
            VerifyGradeValidity((float)request.GradeValue);

            var grade = await _dbContext.Grades.FindAsync(Guid.Parse(request.GradeId));

            if (grade != null)
            {
                grade.GradeValue = (decimal)request.GradeValue;
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new UpdateGradeResponse()
                {
                    GradeId = request.GradeId
                });
            }
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No grade with id {request.GradeId} was found"));
        }

        public override async Task<DeleteGradeResponse> DeleteGrade(DeleteGradeRequest request, ServerCallContext context)
        {
            var gradeIdString = request.GradeId;
            VerifyGuidsValidity(new List<String>() { gradeIdString });

            var grade = await _dbContext.Grades.FindAsync(Guid.Parse(gradeIdString));

            if (grade != null)
            {
                _dbContext.Remove(grade);
                await _dbContext.SaveChangesAsync();
                return await Task.FromResult(new DeleteGradeResponse()
                {
                    WasSuccessful = true
                });
            }
            else
                throw new RpcException(new Status(StatusCode.NotFound, $"No grade with id {gradeIdString} was found"));
        }
    }
}
