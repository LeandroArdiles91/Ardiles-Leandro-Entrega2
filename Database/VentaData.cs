using System.Data.SqlClient;

namespace Christian_Grimberg_58425_Desafio_2;

internal static class VentaData
{
	internal static Venta ObtenerVenta(SqlConnection connection, int id)
	{
		Venta venta = new Venta();
		string queryGetVentaByID = $@"
		SELECT
			[Id]
			,[Comentarios]
			,[IdUsuario]
		FROM [{connection.Database}].[dbo].[Venta]
		WHERE [Id] = '{id.ToString()}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryGetVentaByID, connection))
			{
				using (SqlDataReader dataReader = command.ExecuteReader())
				{
					if (dataReader.Read())
					{
						venta.Id = Convert.ToInt32(dataReader["Id"]);
						venta.Comentarios = dataReader["Comentarios"].ToString();
						venta.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
					}
					else
					{
						Console.WriteLine("No se encuentra la venta con el ID {0}\n", id);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[SQL ERROR]: {ex.Message}");
		}

		return venta;
	}

	internal static List<Venta> ListarVentas(SqlConnection connection)
	{
		List<Venta> listado = new List<Venta>();
		string queryGetVentas = $@"
		SELECT
			[Id]
			,[Comentarios]
			,[IdUsuario]
		FROM [{connection.Database}].[dbo].[Venta]
    ";

		try
		{
			using (SqlCommand command = new SqlCommand(queryGetVentas, connection))
			{
				using (SqlDataReader dataReader = command.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Venta venta = new Venta();
						venta.Id = Convert.ToInt32(dataReader["Id"]);
						venta.Comentarios = dataReader["Comentarios"].ToString();
						venta.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

						listado.Add(venta);
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

	internal static bool CrearVenta(SqlConnection connection, Venta venta)
	{
		bool created = false;
		string queryInsertVenta = $@"
		INSERT INTO [{connection.Database}].[dbo].[Venta] (
			[Comentarios]
			,[IdUsuario]
		)
		VALUES (
			'{venta.Comentarios}'
			,'{venta.IdUsuario}'
		);
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryInsertVenta, connection))
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

	internal static bool ModificarVenta(SqlConnection connection, Venta venta)
	{
		bool created = false;
		string queryUpdateVenta = $@"
		UPDATE [{connection.Database}].[dbo].[Venta]
		SET
      [Comentarios] = '{venta.Comentarios}'
			,[IdUsuario] = '{venta.IdUsuario}'
		WHERE [Id] = '{venta.Id}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryUpdateVenta, connection))
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

	internal static bool EliminarVenta(SqlConnection connection, Venta venta)
	{
		bool created = false;
		string queryDeleteVenta = $@"
		DELETE FROM [{connection.Database}].[dbo].[ProductoVendido]
		WHERE [IdVenta] = '{venta.Id}';

		DELETE FROM [{connection.Database}].[dbo].[Venta]
		WHERE [Id] = '{venta.Id}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryDeleteVenta, connection))
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