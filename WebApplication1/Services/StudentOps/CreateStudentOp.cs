using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using WebApplication1.DataBase;
using WebApplication1.DataBase.Entities;
using WebApplication1.Services.People;

namespace WebApplication1.Services.StudentOps
{
    public class CreateStudentOp : ICreateStudentOp
    {
        private readonly AppDbContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public CreateStudentOp(AppDbContext context, UserManager<ApplicationUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public async Task<string> AddStudentAsync(Student student, IEnumerable<string> coursesCodes)
        {
            var codeToCourseDictionary = _context.Courses.ToDictionary(c => c.CourseCode, c => c);
            var chosenCourses = coursesCodes
                .Where(codeToCourseDictionary.ContainsKey)
                .Select(c => codeToCourseDictionary[c])
                .ToList();
            var entityStudent = student.ToEntityStudent(chosenCourses);

            var studentWithSameId = _context.Students.IgnoreQueryFilters().SingleOrDefault(s => s.StudentIndex.Equals(entityStudent.StudentIndex));

            if (studentWithSameId != null && !studentWithSameId.SoftDeleted)
                throw new Exception("Student with given index is already added");

            using (var transaction = _context.Database.BeginTransaction())
            {
                if (studentWithSameId != null)
                {
                    _context.Remove(studentWithSameId);
                    _context.SaveChanges();
                }

                _context.Add(entityStudent);
                _context.SaveChanges();

                transaction.Commit();
            }

            var entityIdClaim = new Claim("EntityId", _context.Students.SingleOrDefault(s => s.StudentIndex.Equals(student.Index)).EntityStudentID.ToString());
            _userManager.AddClaimAsync(student.PersonalData.ApplicationUser, entityIdClaim);

            var indexClaim = new Claim("Index", student.Index);
            _userManager.AddClaimAsync(student.PersonalData.ApplicationUser, indexClaim);

            return entityStudent.StudentIndex;
        }
    }

    public interface ICreateStudentOp
    { 
        Task<string> AddStudentAsync(Student student, IEnumerable<string> coursesIds);
    }
}
