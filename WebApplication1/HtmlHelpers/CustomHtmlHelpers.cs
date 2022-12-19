using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;
using WebApplication1.Services.PeopleOps;

namespace WebApplication1.HtmlHelpers
{
    public static class CustomHtmlHelpers
    {
        public static IHtmlContent CreateEditLinkForPerson(this IHtmlHelper helper, PersonType personType, string key)
        {
            string output = personType switch
            {
                PersonType.Student => String.Format($"<a href=\"/EditElement/EditPerson/EditStudent/{key}\">Edit</a>"),
                PersonType.Professor => String.Format($"<a href=\"/EditElement/EditPerson/EditProfessor/{key}\">Edit</a>"),
                _ => null
            };
            return new HtmlString(output);
        }

        /*public static IHtmlContent CreateDeleteLinkForPerson(this IHtmlHelper helper, PersonType personType, string key)
        {
            string output = personType switch
            {
                PersonType.Student => String.Format($"<a href=\"/ShowResults/ShowStudent/{key}\">Delete</a>"),
                PersonType.Professor => String.Format($"<a href=\"/ShowResults/ShowProfessor/{key}\">Delete</a>"),
                _ => null
            };
            return new HtmlString(output);
        }*/

        public static IHtmlContent CreateDetailsLinkForPerson(this IHtmlHelper helper, PersonType personType, string key)
        {
            string output = personType switch
            {
                PersonType.Student => String.Format($"<a href=\"/ShowResults/ShowStudent/{key}\">Details</a>"),
                PersonType.Professor => String.Format($"<a href=\"/ShowResults/ShowProfessor/{key}\">Details</a>"),
                _ => null
            };
            return new HtmlString(output);
        }
    }
}
