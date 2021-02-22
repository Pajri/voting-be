using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace VotingBackend.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    public class Home : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
