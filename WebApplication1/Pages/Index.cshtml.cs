using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System;

namespace WebApplication1.Pages
{
    public class IndexModel : PageModel
    {
        private readonly ILogger<IndexModel> _logger;
        //private static bool alreadyInitialized = false;
        /*private async void Initialize(ProfessorOperations professorOperations, StudentOperations studentOperations)
        {
            //for testing
            await professorOperations.AddProfessorAsync(new Professor
            {
                PersonalData = new()
                {
                    FirstName = "Alan",
                    LastName = "Bartosz",
                    Birthday = new DateTime(1980, 3, 12),
                    PESEL = "12345678910",
                    Motherland = "Poland"
                },
                ID = "12345",
                Subject = "Mathematics",
                FirstDayAtJob = new DateTime(2010, 10, 1),
                Salary = 4000
            });

            await professorOperations.AddProfessorAsync(new Professor
            {
                PersonalData = new()
                {
                    FirstName = "Edmund",
                    LastName = "Rataj",
                    Birthday = new DateTime(1984, 4, 4),
                    PESEL = "12345678222",
                    Motherland = "USA"
                },
                ID = "54321",
                Subject = "Science",
                FirstDayAtJob = new DateTime(2008, 10, 1),
                Salary = 5000
            });

            //testing purposes

            studentOperations.AddStudent(new Student
            {
                PersonalData = new()
                {
                    FirstName = "Marcin",
                    LastName = "Marciniak",
                    Birthday = new DateTime(2000, 4, 4),
                    PESEL = "12345555555",
                    Motherland = "Poland"
                },
                Index = "326492",
                Average = 4,
                BeginningOfStudying = new DateTime(2019, 10, 1)
            },
            new List<Course>());

            studentOperations.AddStudent(new Student
            {
                PersonalData = new()
                {
                    FirstName = "Marcin",
                    LastName = "Marciniaczek",
                    Birthday = new DateTime(2001, 4, 4),
                    PESEL = "66655544433",
                    Motherland = "Poland"
                },
                Index = "326290",
                Average = 3,
                BeginningOfStudying = new DateTime(2020, 10, 1)
            },
            new List<Course>());
        }*/

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
            /*if (!alreadyInitialized)
            {
                Initialize(professorOperations, studentOperations);
                alreadyInitialized = true;
            }*/

        }

        public void OnGet()
        {

        }
    }
}
