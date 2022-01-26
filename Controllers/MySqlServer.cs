using MySql.Data.MySqlClient;
namespace Web_app.DatabaseService;

class MySqlServer
{

    public MySqlConnection connection { private set; get; }
    public record DatabaseTable(string Name, string Age);

    private string? DatabaseAuth 
    {
        get 
        {
            var connUrl = Environment.GetEnvironmentVariable("CLEARDB_DATABASE_URL");
            if (connUrl != null)
            {
                connUrl = connUrl.Replace("mysql://", string.Empty);

                string userPassSide = connUrl.Split("@")[0], 
                    hostSide = connUrl.Split("@")[1], 
                    connUser = userPassSide.Split(":")[0],
                    connPass = userPassSide.Split(":")[1],
                    connHost = hostSide.Split("/")[0],
                    connDb = hostSide.Split("/")[1].Split("?")[0];

                return $"server={connHost};Uid={connUser};Pwd={connPass};Database={connDb}";
            }
            return null;
        }
    }

    private MySqlServer()
    {
        connection = new MySqlConnection(DatabaseAuth);
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

        catch (Exception ex) { Console.WriteLine($"\t\n----->{ex.Message}\n----->{ex.InnerException.Message}\n"); }

        finally
        {
            server?.connection.Close();
        }
        return result;
    }

}
