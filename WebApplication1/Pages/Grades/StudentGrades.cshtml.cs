using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.Grades
{
    //[Authorize("IsStudent")]
    public class StudentGradesModel : PageModel
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly ICoursesRepository _coursesRepository;

        public Student Student { get; set; }
        public IList<CourseGrade> Grades { get; set; }
        public IList<Course> Courses { get; set; }

        public StudentGradesModel(IGradesRepository gradesRepository,
            IStudentsRepository studentsRepository,
            ICoursesRepository coursesRepository)
        {
            this._gradesRepository = gradesRepository;
            this._studentsRepository = studentsRepository;
            this._coursesRepository = coursesRepository;
        }

        public async Task OnGetAsync()
        {
            var entityPersonId = User.FindFirst(c => c.Type == "EntityPersonId")?.Value;
            Courses = await _coursesRepository.GetAllAsync();
            Grades = await _gradesRepository.getStudentGrades(Guid.Parse(entityPersonId));
            Student = await _studentsRepository.GetAsync(Guid.Parse(entityPersonId));
        }
    }
}
