using System.Data.SqlClient;

namespace Leandro_Ardiles_Desafio_2;

class Program
{
    static void Main(string[] args)
    {
        string server = ".";
        string database = "Desafio2";
        string user = "sa";
        string password = "P@ssw0rd";

        SqlConnection initializedConnection = GestorBaseDatos.Inicializacion(server, database, user, password);
        initializedConnection.Open();
        initializedConnection.ChangeDatabase(database);

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();

        while (GestorMenu.MenuPrincipal(initializedConnection)) { }

        initializedConnection.Close();
    }
}