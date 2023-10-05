using ApiDtoLibrary.Courses;
using ApiDtoLibrary.GraphQL.Courses;
using ApiDtoLibrary.GraphQL.Professors;
using ApiDtoLibrary.Professors;
using AutoMapper;
using GraphQL;
using System;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.GraphQLServices.QueryGenerators
{
    public class CourseGraphQLQueryGenerator : ICourseGraphQLQueryGenerator
    {
        private readonly IMapper _mapper;

        public CourseGraphQLQueryGenerator(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public GraphQLRequest GetQueryForAdd(Course entity)
        {
            return new GraphQLRequest
            {
                Query = @"
                mutation AddCourseMutation($input: AddCourseInput) {
                  addCourse(input: $input){
                    getCourse {
                      entityCourseId
                      name
                      ects
                      professor {
	                    entityPersonId
	                    firstName
	                    lastName
                        idCode
                      }
                      isFinishedWithExam
                    }
                  }
                }",
                Variables = new
                {
                    input = new AddCourseInput(_mapper.Map<PostCourse>(entity))
                }
            };
        }

        public GraphQLRequest GetQueryForGetAll()
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetAllCourses {
                    courses {
                        Id
                        CourseCode
                        Name
                        ProfessorId
                        ECTS
                        IsFinishedWithExam
                    }
                }",
            };
        }

        public GraphQLRequest GetQueryForGetById(Guid id)
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetCourseById($id: String) {
                  courseById(id: $id) {
                    Id
                    CourseCode
                    Name
                    ECTS
                    IsFinishedWithExam
                    ProfessorId
                    Professor {
                        Id 
                        FirstName
                        LastName   
                        ProfessorId
                        Subject
                    }
                    EnrolledStudents {
                        Id 
                        FirstName
                        LastName   
                        Index
                    }
                  }
                }",
                Variables = new
                {
                    id = id.ToString()
                }
            };
        }

        public GraphQLRequest GetQueryForCodeIsOccupied(string courseCode)
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetCourseByCode($courseCodeVar: String) {
                  courses(where: {courseCode: {eq: $courseCodeVar}}) {
                    Id
                    CourseCode
                    Name
                    ECTS
                    IsFinishedWithExam
                  }
                }",
                Variables = new
                {
                    courseCodeVar = courseCode
                }
            };
        }

        public GraphQLRequest GetQueryForUpdate(Course entity)
        {
            return new GraphQLRequest
            {
                Query = @"
                mutation UpdateCourseMutation($input: UpdateCourseInput) {
                  updateCourse (input: $input)
                  {
                    getCourse {
                      entityCourseId
                    }
                  }
                }",
                Variables = new
                {
                    input = new UpdateCourseInput(_mapper.Map<PutCourse>(entity))
                }
            };
        }

        public GraphQLRequest GetQueryForDeleteById(Guid courseId)
        {
            return new GraphQLRequest
            {
                Query = @"
                mutation DeleteCourseById ($id: String) {
                  deleteCourse(input: {
                    id: $id
                  })
                  {
                    wasSuccessful
                  }
                }",
                Variables = new
                {
                    id = courseId.ToString()
                }
            };
        }
    }

    public interface ICourseGraphQLQueryGenerator
    {
        GraphQLRequest GetQueryForCodeIsOccupied(string courseCode);
        GraphQLRequest GetQueryForGetById(Guid id);
        GraphQLRequest GetQueryForGetAll();
        GraphQLRequest GetQueryForAdd(Course entity);
        GraphQLRequest GetQueryForUpdate(Course entity);
        GraphQLRequest GetQueryForDeleteById(Guid courseId);
    }
}
