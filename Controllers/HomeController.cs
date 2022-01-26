using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_app.Models;

namespace Web_app.Controllers;

[Controller]
public class Shit : Controller
{
    public IActionResult Index()
    { 

        return View();
    }

    public IActionResult Privacy()
    {
        ViewBag.C = "dasd";
        return View("Privacy", new ListModel { Id = 123, Name = "Mike" });

    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
