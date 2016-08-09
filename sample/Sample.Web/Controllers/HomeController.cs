using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CaseInsensitiveDynamicModelBinder;

namespace Sample.Web.Controllers
{
    
    public class HomeController : Controller
    {
        private static Dictionary<string, string> _dic;

        public HomeController()
        {
            _dic = new Dictionary<string, string>()
            {
                {"A","Value of A or a"}
            };
        }


        public IActionResult Index()
        {
            return View(_dic);
        }

        [HttpPut]
        [Route("test/[controller]")]
        public IActionResult Put([Insensitive] dynamic data)
        {
            string value = data.A;

            if(value != null)
            {
                _dic["A"] = value;
            }

            return Json(_dic);
        }
    }
}
