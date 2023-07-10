﻿using ApiDtoLibrary.Students;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System.Xml.Linq;
using UniversityApi.API.Contracts;
using UniversityApi.API.DataBase;
using UniversityApi.API.DataBase.Entities;
using UniversityApi.API.DataBase.Identity;

namespace UniversityApi.API.Repositories
{
    public class StudentsRepository : GenericRepository<EntityStudent>, IStudentsRepository
    {
        public StudentsRepository(UniversityApiDbContext dbContext, UserManager<ApiUser> userManager) : base(dbContext, userManager)
        {
        }

        public override async Task<EntityStudent> GetAsync(Guid id)
        {
            return await _context.Set<EntityStudent>()
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Professor)
                .SingleOrDefaultAsync(s => s.EntityPersonID == id);
        }

        public override async Task<List<EntityStudent>> GetAllAsync()
        {
            return await _context.Set<EntityStudent>()
                .Include(s => s.Courses)
                    .ThenInclude(c => c.Professor)
                .ToListAsync();
        }

        public async Task<Guid> UpdateWithCoursesAsync(EntityStudent updatedStudent, IEnumerable<Guid> coursesIds)
        {
            var updatedCourses = coursesIds
                .Select(id => _context.Courses.Find(id))
                .ToList();
            var studentToUpdate = _context.Set<EntityStudent>()
                .Include(s => s.Courses)
                .SingleOrDefault(s => s.EntityPersonID == updatedStudent.EntityPersonID);
            studentToUpdate.Courses = updatedCourses;
            _context.Update(studentToUpdate);
            await _context.SaveChangesAsync();
            return studentToUpdate.EntityPersonID;
        }

        public async Task DeleteStudentsCourseAsync(Guid studentId, Guid courseId)
        {
            var student = await GetAsync(studentId);
            var course = await _context.Courses.FindAsync(courseId);
            student.Courses.Remove(course);
            await _context.SaveChangesAsync();
        }
    }
}
