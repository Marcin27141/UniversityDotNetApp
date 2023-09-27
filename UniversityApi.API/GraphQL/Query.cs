﻿using Microsoft.CodeAnalysis.Text;
using Microsoft.EntityFrameworkCore;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;

namespace UniversityApi.API.GraphQL
{
    public class Query
    {
        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityPerson> GetPeople(UniversityApiDbContext context)
        {
            return context.People;            
        }

        public EntityPerson GetPersonById(UniversityApiDbContext context, string id)
        {
            if (Guid.TryParse(id, out Guid personId))
                return context.Set<EntityPerson>().Find(personId);
            else
                return default;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityCourse> GetCourses(UniversityApiDbContext context)
        {
            return context.Courses;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityProfessor> GetProfessors(UniversityApiDbContext context)
        {
            return context.Set<EntityProfessor>();
        }

        public EntityProfessor GetProfessorById(UniversityApiDbContext context, string id)
        {
            if (Guid.TryParse(id, out Guid personId))
                return context.Set<EntityProfessor>().Find(personId);
            else
                return default;
        }

        [UseFiltering]
        [UseSorting]
        public IQueryable<EntityStudent> GetStudents(UniversityApiDbContext context)
        {
            return context.Set<EntityStudent>();
        }
    }
}
