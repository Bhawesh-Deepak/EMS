using EMS.Helpers;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EMS.Controllers.Utilities
{
    public class UtilitiesController : Controller
    {
        public async Task<IActionResult> Index()
        {
            return await Task.Run(() => View(ViewHelpers.GetViewName("Utilities", "UtilitityIndex")));
        }
    }
}
