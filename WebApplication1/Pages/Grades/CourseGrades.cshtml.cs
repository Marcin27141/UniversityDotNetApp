using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using WebApplication1.Contracts;
using WebApplication1.Services;
using WebApplication1.Services.People;

namespace WebApplication1.Pages.Grades
{
    [Authorize("HasProfessorRights")]
    public class CourseGradesModel : PageModel
    {
        private readonly IGradesRepository _gradesRepository;
        private readonly IStudentsRepository _studentsRepository;
        private readonly ICoursesRepository _coursesRepository;

        public Course Course { get; set; }
        public IList<CourseGrade> Grades { get; set; }
        public IList<Student> Students { get; set; }

        public CourseGradesModel(IGradesRepository gradesRepository,
            IStudentsRepository studentsRepository,
            ICoursesRepository coursesRepository)
        {
            this._gradesRepository = gradesRepository;
            this._studentsRepository = studentsRepository;
            this._coursesRepository = coursesRepository;
        }

        public async Task OnGetAsync(Guid courseId)
        {
            Course = await _coursesRepository.GetAsync(courseId);
            Grades = await _gradesRepository.getCourseGrades(courseId);
            Students = await _studentsRepository.GetAllAsync();
        }
    }
}
