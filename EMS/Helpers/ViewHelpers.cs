using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Helpers
{
    public static class ViewHelpers
    {
        public static string GetViewName(string folderName, string viewName) =>
             $@"~/Views/{folderName}/{viewName}.cshtml";
    }
}
