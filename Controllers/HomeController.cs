﻿using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;
using Web_app.Models;

using MySql.Data.MySqlClient;

namespace Web_app.Controllers;

class MySqlServer
{

    string connectionString { get; } = "server=eu-cdbr-west-02.cleardb.net;port=3306;database=heroku_0bd8a63ec43f5a6;uid=b774c1c17b13b5;pwd=71218385;";
    public MySqlConnection connection { private set; get; }
    public record DatabaseTable(string Name, string Age);

    private MySqlServer()
    {
        connection = new MySqlConnection(connectionString);
        connection.Open();
    }

    private List<DatabaseTable> GetData()
    {
        MySqlDataReader? sqlDataReader = null;
        List<DatabaseTable> result = new List<DatabaseTable>();

        try
        {
            MySqlCommand sqlCommand = new MySqlCommand("select * from users", this.connection);
            sqlDataReader = sqlCommand.ExecuteReader();

            while (sqlDataReader.Read())
            {
                result.Add(new DatabaseTable($"{sqlDataReader["Name"]}", $"{sqlDataReader["Age"]}"));
            }
        }
        finally
        {
            sqlDataReader?.Close();
        }

        return result;
    }

    public static List<DatabaseTable>? Run()
    {
        MySqlServer? server = null;
        List<DatabaseTable>? result = null;

        try
        {
            server = new MySqlServer();
            result = server.GetData();
        }

        finally
        {
            server?.connection.Close();
        }
        return result;
    }

}

[Controller]
public class Shit : Controller
{
    public IActionResult Index()
    {
        /*ListModel listModel = new ListModel();
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
        finally { }

        ViewBag.C = "dasd";
        return View("Index", listModel);*/
        return View();

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
