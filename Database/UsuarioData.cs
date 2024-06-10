using System.Data.SqlClient;

namespace Christian_Grimberg_58425_Desafio_2;

internal static class UsuarioData
{
    internal static Usuario ObtenerUsuario(SqlConnection connection, int id)
    {
        Usuario user = new Usuario();
        string queryGetUserByID = $@"
        SELECT
            [Id]
            ,[Nombre]
            ,[Apellido]
            ,[NombreUsuario]
            ,[Contraseña]
            ,[Mail]
		FROM [{connection.Database}].[dbo].[Usuario]
        WHERE [Id] = '{id.ToString()}';
        ";

        try
        {
            using (SqlCommand command = new SqlCommand(queryGetUserByID, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    if(dataReader.Read())
                    {
                        user.Id = Convert.ToInt32(dataReader["Id"]);
                        user.Nombre = dataReader["Nombre"].ToString();
                        user.Apellido = dataReader["Apellido"].ToString();
                        user.NombreUsuario = dataReader["NombreUsuario"].ToString();
                        user.Contraseña = dataReader["Contraseña"].ToString();
                        user.Mail = dataReader["Mail"].ToString();
                    }
                    else
                    {
                        Console.WriteLine("No se encuentra el usuario con el ID {0}\n", id);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SQL ERROR]: {ex.Message}");
        }

        return user;
    }

    internal static List<Usuario> ListarUsuarios(SqlConnection connection)
    {
        List<Usuario> listado = new List<Usuario>();
        string queryGetUsers = $@"
		SELECT
            [Id]
            ,[Nombre]
            ,[Apellido]
            ,[NombreUsuario]
            ,[Contraseña]
            ,[Mail]
		FROM [{connection.Database}].[dbo].[Usuario];
        ";

        try
        {
            using (SqlCommand command = new SqlCommand(queryGetUsers, connection))
            {
                using (SqlDataReader dataReader = command.ExecuteReader())
                {
                    while (dataReader.Read())
                    {
                        Usuario user = new Usuario();
                        user.Id = Convert.ToInt32(dataReader["Id"]);
                        user.Nombre = dataReader["Nombre"].ToString();
                        user.Apellido = dataReader["Apellido"].ToString();
                        user.NombreUsuario = dataReader["NombreUsuario"].ToString();
                        user.Contraseña = dataReader["Contraseña"].ToString();
                        user.Mail = dataReader["Mail"].ToString();

                        listado.Add(user);
                    }
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SQL ERROR]: {ex.Message}");
        }

        return listado;
    }

    internal static bool CrearUsuario(SqlConnection connection, Usuario user)
    {
        bool created = false;
        string queryInsertUser = $@"
        INSERT INTO [{connection.Database}].[dbo].[Usuario] (
            [Nombre]
            ,[Apellido]
            ,[NombreUsuario]
            ,[Contraseña]
            ,[Mail]
        )
        VALUES (
            '{user.Nombre}'
            ,'{user.Apellido}'
            ,'{user.NombreUsuario}'
            ,'{user.Contraseña}'
            ,'{user.Mail}'
        );
        ";

        try
        {
            using (SqlCommand command = new SqlCommand(queryInsertUser, connection))
            {
                created = (command.ExecuteNonQuery() > 0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SQL ERROR]: {ex.Message}");
        }

        return created;
    }

    internal static bool ModificarUsuario(SqlConnection connection, Usuario user)
    {
        bool created = false;
        string queryUpdatetUser = $@"
        UPDATE [{connection.Database}].[dbo].[Usuario]
        SET
            [Nombre] = '{user.Nombre}'
            ,[Apellido] = '{user.Apellido}'
            ,[NombreUsuario] ='{user.NombreUsuario}'
            ,[Contraseña] = '{user.Contraseña}'
            ,[Mail] = '{user.Mail}'
        WHERE [Id] = '{user.Id}';
        ";

        try
        {
            using (SqlCommand command = new SqlCommand(queryUpdatetUser, connection))
            {
                created = (command.ExecuteNonQuery() > 0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SQL ERROR]: {ex.Message}");
        }

        return created;
    }

    internal static bool EliminarUsuario(SqlConnection connection, Usuario user)
    {
        bool created = false;
        string queryDeleteUser = $@"
        DELETE FROM [{connection.Database}].[dbo].[ProductoVendido]
        WHERE [IdVenta] IN (SELECT [Id]
                            FROM [{connection.Database}].[dbo].[Venta]
                            WHERE [IdUsuario] = '{user.Id}');

        DELETE FROM [{connection.Database}].[dbo].[ProductoVendido]
        WHERE [IdProducto] IN (SELECT [Id]
                            FROM [{connection.Database}].[dbo].[Producto]
                            WHERE [IdUsuario] = '{user.Id}');

        DELETE FROM [{connection.Database}].[dbo].[Producto]
        WHERE [IdUsuario] = '{user.Id}';

        DELETE FROM [{connection.Database}].[dbo].[Venta]
        WHERE [IdUsuario] = '{user.Id}';

        DELETE FROM [{connection.Database}].[dbo].[Usuario]
        WHERE [Id] = '{user.Id}';
        ";

        try
        {
            using (SqlCommand command = new SqlCommand(queryDeleteUser, connection))
            {
                created = (command.ExecuteNonQuery() > 0);
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SQL ERROR]: {ex.Message}");
        }

        return created;
    }
}