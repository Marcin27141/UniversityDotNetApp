using ApiDtoLibrary.GraphQL.Students;
using ApiDtoLibrary.Students;
using AutoMapper;
using GraphQL;
using System;
using System.Collections.Generic;
using System.Linq;
using WebApplication1.Services.People;

namespace WebApplication1.GraphQLServices.QueryGenerators
{
    public class StudentGraphQLQueryGenerator : IStudentGraphQLQueryGenerator
    {
        private readonly IMapper _mapper;

        public StudentGraphQLQueryGenerator(IMapper mapper)
        {
            this._mapper = mapper;
        }

        public GraphQLRequest GetQueryForAdd(Student entity, IEnumerable<Guid> coursesIds)
        {
            var postStudent = _mapper.Map<PostStudent>(entity);
            postStudent.CoursesIds = coursesIds.ToList();
            return new GraphQLRequest
            {
                Query = @"
                mutation AddStudentMutation($input: AddStudentInput) {
                  addStudent (input: $input)
                  {
                    getStudent {
                        entityPersonId
                        applicationUserId
                        firstName
                        lastName
                        pesel
                        motherland
                        birthday
                        index
                        beginningOfStudying
                        courses {
                            entityCourseId
                            courseCode
                            name
                        }
                    }
                  }
                }",
                Variables = new
                {
                    input = new AddStudentInput(postStudent)
                }
            };
        }

        public GraphQLRequest GetQueryForGetAll()
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetAllStudents {
                    students {
                        Id
                        UserId
                        FirstName
                        LastName
                        PESEL
                        Birthday
                        Motherland
                        PersonStatus
                        Index
                        BeginningOfStudying
                    }
                }",
            };
        }

        public GraphQLRequest GetQueryForGetById(Guid id)
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetStudentById($id: String) {
                  studentById(id: $id) {
                    Id
                    UserId
                    FirstName
                    LastName
                    PESEL
                    Motherland
                    Birthday
                    Index
                    BeginningOfStudying
                    Courses {
                        Id
                        CourseCode
                        Name
                    }
                  }
                }",
                Variables = new
                {
                    id = id.ToString()
                }
            };
        }

        public GraphQLRequest GetQueryForGetByUser(string userId)
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetStudentByUser($userIdVar: UUID) {
                    students(where: {applicationUserId: {eq: $userIdVar}}) {
                    Id
                    UserId
                    FirstName
                    LastName
                    PESEL
                    Birthday
                    Motherland
                    PersonStatus
                    Index
                    BeginningOfStudying
                  }
                }",
                Variables = new
                {
                    userIdVar = userId
                }
            };
        }

        public GraphQLRequest GetQueryForIndexIsOccupied(string index)
        {
            return new GraphQLRequest
            {
                Query = @"
                query GetStudentById($indexVar: String) {
                  students(where: {index: {eq: $indexVar}}) {
                    Id
                    UserId
                    FirstName
                    LastName
                    PESEL
                    Birthday
                    Motherland
                    PersonStatus
                    Index
                    BeginningOfStudying
                  }
                }",
                Variables = new
                {
                    indexVar = index
                }
            };
        }

        public GraphQLRequest GetQueryForRemoveStudentCourse(Guid studentId, Guid courseId)
        {
            return new GraphQLRequest
            {
                Query = @"
                mutation RemoveStudentCourseMutation($input: RemoveStudentCourseInput) {
                  removeStudentCourse (input: $input)
                  {
                    wasSuccessful
                  }
                }",
                Variables = new
                {
                    input = new RemoveStudentCourseInput(studentId.ToString(), courseId.ToString())
                }
            };
        }

        public GraphQLRequest GetQueryForUpdate(Student entity, IEnumerable<Guid> coursesIds)
        {
            var putStudent = _mapper.Map<PutStudent>(entity);
            putStudent.CoursesIds = coursesIds.ToList();
            return new GraphQLRequest
            {
                Query = @"
                mutation UpdateStudentMutation($input: UpdateStudentInput) {
                  updateStudent (input: $input)
                  {
                    getStudent {
                      entityPersonId
                    }
                  }
                }",
                Variables = new
                {
                    input = new UpdateStudentInput(putStudent)
                }
            };
        }
    }

    public interface IStudentGraphQLQueryGenerator
    {
        GraphQLRequest GetQueryForIndexIsOccupied(string index);
        GraphQLRequest GetQueryForGetById(Guid id);
        GraphQLRequest GetQueryForGetAll();
        GraphQLRequest GetQueryForGetByUser(string userId);
        GraphQLRequest GetQueryForAdd(Student entity, IEnumerable<Guid> coursesIds);
        GraphQLRequest GetQueryForUpdate(Student entity, IEnumerable<Guid> coursesIds);
        GraphQLRequest GetQueryForRemoveStudentCourse(Guid studentId, Guid courseId);
    }
}
