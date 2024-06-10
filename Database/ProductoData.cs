using System.Data.SqlClient;

namespace Christian_Grimberg_58425_Desafio_2;

internal static class ProductoData
{
	internal static Producto ObtenerProducto(SqlConnection connection, int id)
	{
		Producto product = new Producto();
		string queryGetProductoByID = $@"
		SELECT
			[Id]
			,[Descripciones]
			,[Costo]
			,[PrecioVenta]
			,[Stock]
			,[IdUsuario]
		FROM [{connection.Database}].[dbo].[Producto]
		WHERE [Producto].[Id] = '{id.ToString()}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryGetProductoByID, connection))
			{
				using (SqlDataReader dataReader = command.ExecuteReader())
				{
					if (dataReader.Read())
					{
						product.Id = Convert.ToInt32(dataReader["Id"]);
						product.Descripcion = dataReader["Descripciones"].ToString();
						product.Costo = Convert.ToDecimal(dataReader["Costo"]);
						product.PrecioVenta = Convert.ToDecimal(dataReader["PrecioVenta"]);
						product.Stock = Convert.ToInt32(dataReader["Stock"]);
						product.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);
					}
					else
					{
						Console.WriteLine("No se encuentra el producto con el ID {0}\n", id);
					}
				}
			}
		}
		catch (Exception ex)
		{
			Console.WriteLine($"[SQL ERROR]: {ex.Message}");
		}

		return product;
	}

	internal static List<Producto> ListarProductos(SqlConnection connection)
	{
		List<Producto> listado = new List<Producto>();
		string queryGetProducts = $@"
		SELECT
			[Id]
			,[Descripciones]
			,[Costo]
			,[PrecioVenta]
			,[Stock]
			,[IdUsuario]
		FROM [{connection.Database}].[dbo].[Producto]
    ";

		try
		{
			using (SqlCommand command = new SqlCommand(queryGetProducts, connection))
			{
				using (SqlDataReader dataReader = command.ExecuteReader())
				{
					while (dataReader.Read())
					{
						Producto product = new Producto();
						product.Id = Convert.ToInt32(dataReader["Id"]);
						product.Descripcion = dataReader["Descripciones"].ToString();
						product.Costo = Convert.ToDecimal(dataReader["Costo"]);
						product.PrecioVenta = Convert.ToDecimal(dataReader["PrecioVenta"]);
						product.Stock = Convert.ToInt32(dataReader["Stock"]);
						product.IdUsuario = Convert.ToInt32(dataReader["IdUsuario"]);

						listado.Add(product);
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

	internal static bool CrearProducto(SqlConnection connection, Producto product)
	{
		bool created = false;
		string queryInsertProduct = $@"
		INSERT INTO [{connection.Database}].[dbo].[Producto] (
			[Descripciones]
			,[Costo]
			,[PrecioVenta]
			,[Stock]
			,[IdUsuario]
		)
		VALUES (
			'{product.Descripcion}'
			,'{product.Costo}'
			,'{product.PrecioVenta}'
			,'{product.Stock}'
			,'{product.IdUsuario}'
		);
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryInsertProduct, connection))
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

	internal static bool ModificarProducto(SqlConnection connection, Producto product)
	{
		bool created = false;
		string queryUpdateProduct = $@"
		UPDATE [{connection.Database}].[dbo].[Producto]
		SET
      [Descripciones] = '{product.Descripcion}'
			,[Costo] = '{product.Costo}'
			,[PrecioVenta] = '{product.PrecioVenta}'
			,[Stock] = '{product.Stock}'
			,[IdUsuario] = '{product.IdUsuario}'
		WHERE [Id] = '{product.Id}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryUpdateProduct, connection))
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

	internal static bool EliminarProducto(SqlConnection connection, Producto product)
	{
		bool created = false;
		string queryDeleteProduct = $@"
		DELETE FROM [{connection.Database}].[dbo].[ProductoVendido]
		WHERE [IdProducto] IN (SELECT [Id]
												FROM [{connection.Database}].[dbo].[Producto]
												WHERE [Id] = '{product.Id}');

		DELETE FROM [{connection.Database}].[dbo].[Producto]
		WHERE [Id] = '{product.Id}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryDeleteProduct, connection))
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