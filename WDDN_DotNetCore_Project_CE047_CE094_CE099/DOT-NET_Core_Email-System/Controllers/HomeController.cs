using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using DOT_NET_Core_Email_System.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.AspNetCore.Http;

namespace DOT_NET_Core_Email_System.Controllers
{
    public class HomeController : Controller
    {
        private readonly string UserEmailSession = "_UserEmail";
        public IActionResult Index()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Privacy()
        {
            HttpContext.Session.Clear();
            return View();
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
