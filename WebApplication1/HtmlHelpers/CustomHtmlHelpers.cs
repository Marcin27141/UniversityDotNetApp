using ApiDtoLibrary.Person;
using Google.Api;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using System;
using System.Security.Claims;
using System.Threading.Tasks;

namespace WebApplication1.HtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent CreateEditLinkForPerson(this IHtmlHelper helper, PersonStatus personStatus, Guid id)
        {
            string output = personStatus switch
            {
                PersonStatus.Student => String.Format($"<a href=\"/EditElement/EditPerson/EditStudent/{id}\">Edit</a>"),
                PersonStatus.Professor => String.Format($"<a href=\"/EditElement/EditPerson/EditProfessor/{id}\">Edit</a>"),
                _ => null
            };
            return new HtmlString(output);
        }

        public static IHtmlContent CreateDetailsLinkForPerson(this IHtmlHelper helper, PersonStatus personStatus, Guid id)
        {

            string output = personStatus switch
            {
                PersonStatus.Student => String.Format($"<a href=\"/ShowResults/ShowStudent/{id}\">Details</a>"),
                PersonStatus.Professor => String.Format($"<a href=\"/ShowResults/ShowProfessor/{id}\">Details</a>"),
                _ => null
            };
            return new HtmlString(output);
        }

        public static IHtmlContent GetNavbarForUser(this IHtmlHelper helper, ClaimsPrincipal user)
        {
            var partialName = user.FindFirst("Status")?.Value switch
            {
                "Admin" => "/Pages/Shared/Navbar/_AdminNavbarPartial.cshtml",
                "Student" => "/Pages/Shared/Navbar/_StudentNavbarPartial.cshtml",
                "Professor" => "/Pages/Shared/Navbar/_ProfessorNavbarPartial.cshtml",
                _ => "/Pages/Shared/Navbar/_NoUserNavbarPartial.cshtml"
            };
            return helper.PartialAsync(partialName).Result;
        }
    }
}
