using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

using Web_app.Models;
using Web_app.DatabaseService;

namespace Web_app.Controllers;

[Controller]
public class Home : Controller
{
    public IActionResult Index()
    {
        ListModel listModel = new ListModel { list = new List<ListElement>() { new ListElement {Name = "Names", Age = "Ages" } } };
        try
        {
            List<MySqlServer.DatabaseTable>? tables = MySqlServer.Run();
            if (tables != null)
            {
                foreach(var i in tables) 
                {
                    listModel.list.Add(new ListElement { Name = i.Name, Age = i.Age });
                }
            }

        }
        catch (Exception ex) { Console.WriteLine($"\t\n----->{ex.Message}\n\n"); }
        finally { }
        return View("Index", listModel);

    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
