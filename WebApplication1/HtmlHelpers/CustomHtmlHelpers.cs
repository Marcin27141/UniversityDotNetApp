using ApiDtoLibrary.Person;
using Microsoft.AspNetCore.Html;
using Microsoft.AspNetCore.Mvc.Rendering;
using System;

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
    }
}
