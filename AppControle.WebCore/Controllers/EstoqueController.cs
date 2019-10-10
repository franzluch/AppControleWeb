using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace AppControle.WebCore.Controllers
{
    
    public class EstoqueController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}