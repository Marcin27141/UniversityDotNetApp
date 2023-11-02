using ApiDtoLibrary.Person;
using Google.Api;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using System.Security.Claims;

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

        public static string GetLayoutForUser(this IHtmlHelper helper, ClaimsPrincipal user)
        {
            switch (user.FindFirst("Status")?.Value)
            {
                case "Admin":
                    return "/Pages/Shared/_AdminLayout.cshtml";
                case "Student":
                    return "/Pages/Shared/_StudentLayout.cshtml";
                case "Professor":
                    return "/Pages/Shared/_ProfessorLayout.cshtml";
                default:
                    return "/Pages/Shared/_Layout.cshtml";
            }
        }
    }
}
