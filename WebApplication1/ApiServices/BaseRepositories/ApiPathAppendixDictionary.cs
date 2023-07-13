using System.Collections.Generic;
using WebApplication1.ApiServices.BaseRepositories;

namespace WebApplication1.ApiServices.BaseRepositories
{
    public static class ApiPathAppendixDictionary
    {
        private static readonly Dictionary<ApiGenericTypes, string> dictionary;

        static ApiPathAppendixDictionary()
        {
            dictionary = new Dictionary<ApiGenericTypes, string>
            {
                { ApiGenericTypes.Course, "/Courses" },
                { ApiGenericTypes.Professor, "/Professors" },
                { ApiGenericTypes.Student, "/Students" }
            };
        }

        public static string GetValue(ApiGenericTypes key)
        {
            return dictionary[key];
        }
    }
}
