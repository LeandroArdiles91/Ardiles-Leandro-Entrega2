using System.Data.SqlClient;

namespace Leandro_Ardiles_Desafio_2;

internal static class ProductoVendidoData
{
	internal static ProductoVendido ObtenerProductoVendido(SqlConnection connection, int id)
	{
		ProductoVendido product = new ProductoVendido();
		string queryGetProductoVendidoByID = $@"
		SELECT
			[Id]
      ,[Stock]
      ,[IdProducto]
      ,[IdVenta]
		FROM [{connection.Database}].[dbo].[ProductoVendido]
		WHERE [Id] = '{id.ToString()}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryGetProductoVendidoByID, connection))
			{
				using (SqlDataReader dataReader = command.ExecuteReader())
				{
					if (dataReader.Read())
					{
						product.Id = Convert.ToInt32(dataReader["Id"]);
						product.Stock = Convert.ToInt32(dataReader["Stock"]);
						product.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
						product.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);
					}
					else
					{
						Console.WriteLine("No se encuentra el producto vendido con el ID {0}\n", id);
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

	internal static List<ProductoVendido> ListarProductosVendidos(SqlConnection connection)
	{
		List<ProductoVendido> listado = new List<ProductoVendido>();
		string queryGetProductosVendidos = $@"
		SELECT
			[Id]
      ,[Stock]
      ,[IdProducto]
      ,[IdVenta]
		FROM [{connection.Database}].[dbo].[ProductoVendido]
    ";

		try
		{
			using (SqlCommand command = new SqlCommand(queryGetProductosVendidos, connection))
			{
				using (SqlDataReader dataReader = command.ExecuteReader())
				{
					while (dataReader.Read())
					{
						ProductoVendido product = new ProductoVendido();
						product.Id = Convert.ToInt32(dataReader["Id"]);
						product.Stock = Convert.ToInt32(dataReader["Stock"]);
						product.IdProducto = Convert.ToInt32(dataReader["IdProducto"]);
						product.IdVenta = Convert.ToInt32(dataReader["IdVenta"]);

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

	internal static bool CrearProductoVendido(SqlConnection connection, ProductoVendido product)
	{
		bool created = false;
		string queryInsertProductoVendido = $@"
		INSERT INTO [{connection.Database}].[dbo].[ProductoVendido] (
			[Stock]
      ,[IdProducto]
      ,[IdVenta]
		)
		VALUES (
			'{product.Stock}'
			,'{product.IdProducto}'
			,'{product.IdVenta}'
		);
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryInsertProductoVendido, connection))
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

	internal static bool ModificarProductoVendido(SqlConnection connection, ProductoVendido product)
	{
		bool created = false;
		string queryUpdateProductoVendido = $@"
		UPDATE [{connection.Database}].[dbo].[ProductoVendido]
		SET
      [Stock] = '{product.Stock}'
			,[IdProducto] = '{product.IdProducto}'
			,[IdVenta] = '{product.IdVenta}'
		WHERE [Id] = '{product.Id}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryUpdateProductoVendido, connection))
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

	internal static bool EliminarProductoVendido(SqlConnection connection, ProductoVendido product)
	{
		bool created = false;
		string queryDeleteProductoVendido = $@"
		DELETE FROM [{connection.Database}].[dbo].[ProductoVendido]
		WHERE [Id] = '{product.Id}';
		";

		try
		{
			using (SqlCommand command = new SqlCommand(queryDeleteProductoVendido, connection))
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