using System.Data.SqlClient;

namespace Christian_Grimberg_58425_Desafio_2;

internal static class GestorBaseDatos
{
    internal static SqlConnection Inicializacion(string server, string database, string user, string password)
    {
        string connectionString = $"Server={server}; User={user}; Password={password};";

        SqlConnection connection = new SqlConnection(connectionString);
        connection.Open();

        try
        {
            SqlCommand newDatabaseCommand = new SqlCommand(DeployDatabase(database), connection);

            using (SqlDataReader newDatabaseReader = newDatabaseCommand.ExecuteReader())
            {
                while (newDatabaseReader.Read())
                {
                    Console.WriteLine(string.Format("[SQL INFO]: {0}", newDatabaseReader[0]));
                }
            }

            SqlCommand newTablesCommand = new SqlCommand(DeployDatabaseStructure(database), connection);

            using (SqlDataReader newTablesReader = newTablesCommand.ExecuteReader())
            {
                while (newTablesReader.Read())
                {
                    Console.WriteLine(string.Format("[SQL INFO]: {0}", newTablesReader[0]));
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[SQL ERROR]: {ex.Message}");
        }

        connection.Close();

        return connection;
    }

    private static string DeployDatabase(string baseDeDatos)
    {
        return $@"
        IF EXISTS(SELECT [name] FROM [sys].[databases] WHERE [name] = '{baseDeDatos}')
        BEGIN
            ALTER DATABASE [{baseDeDatos}] SET single_user with rollback immediate;
            DROP DATABASE [{baseDeDatos}];
        END

        CREATE DATABASE [{baseDeDatos}];
        SELECT 'Conectado a la nueva base de datos [{baseDeDatos}]';
        ";
    }

    private static string DeployDatabaseStructure(string baseDeDatos)
    {
        return $@"
        CREATE TABLE [{baseDeDatos}].[dbo].[Producto](
            [Id] [bigint] IDENTITY(1,1) NOT NULL,
            [Descripciones] [varchar](max) NOT NULL,
            [Costo] [money] NULL,
            [PrecioVenta] [money] NOT NULL,
            [Stock] [int] NOT NULL,
            [IdUsuario] [bigint] NOT NULL,
        CONSTRAINT [PK_Producto] PRIMARY KEY CLUSTERED 
        (
            [Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]
        
        CREATE TABLE [{baseDeDatos}].[dbo].[ProductoVendido](
            [Id] [bigint] IDENTITY(1,1) NOT NULL,
            [Stock] [int] NOT NULL,
            [IdProducto] [bigint] NOT NULL,
            [IdVenta] [bigint] NOT NULL,
        CONSTRAINT [PK_ProductoVendido] PRIMARY KEY CLUSTERED 
        (
            [Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY]
        
        CREATE TABLE [{baseDeDatos}].[dbo].[Usuario](
            [Id] [bigint] IDENTITY(1,1) NOT NULL,
            [Nombre] [varchar](max) NOT NULL,
            [Apellido] [varchar](max) NOT NULL,
            [NombreUsuario] [varchar](max) NOT NULL,
            [Contraseña] [varchar](max) NOT NULL,
            [Mail] [varchar](max) NOT NULL,
        CONSTRAINT [PK_Usuario] PRIMARY KEY CLUSTERED 
        (
            [Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

        CREATE TABLE [{baseDeDatos}].[dbo].[Venta](
            [Id] [bigint] IDENTITY(1,1) NOT NULL,
            [Comentarios] [varchar](max) NULL,
            [IdUsuario] [bigint] NOT NULL,
        CONSTRAINT [PK_Venta] PRIMARY KEY CLUSTERED 
        (
            [Id] ASC
        )WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON) ON [PRIMARY]
        ) ON [PRIMARY] TEXTIMAGE_ON [PRIMARY]

        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[Producto] ON
        INSERT [{baseDeDatos}].[dbo].[Producto] ([Id], [Descripciones], [Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES (1, N'Remera manga larga', 500.0000, 1400.0000, 23, 1)
        INSERT [{baseDeDatos}].[dbo].[Producto] ([Id], [Descripciones], [Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES (2, N'Pantalon Jean', 800.0000, 4000.0000, 12, 1)
        INSERT [{baseDeDatos}].[dbo].[Producto] ([Id], [Descripciones], [Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES (3, N'Bermuda', 600.0000, 3000.0000, 10, 1)
        INSERT [{baseDeDatos}].[dbo].[Producto] ([Id], [Descripciones], [Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES (4, N'Musculosa', 300.0000, 1100.0000, 20, 1)
        INSERT [{baseDeDatos}].[dbo].[Producto] ([Id], [Descripciones], [Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES (5, N'Campera', 1500.0000, 8000.0000, 5, 1)
        INSERT [{baseDeDatos}].[dbo].[Producto] ([Id], [Descripciones], [Costo], [PrecioVenta], [Stock], [IdUsuario]) VALUES (6, N'Buzo', 1000.0000, 3000.0000, 14, 1)
        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[Producto] OFF

        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[ProductoVendido] ON
        INSERT [{baseDeDatos}].[dbo].[ProductoVendido] ([Id], [Stock], [IdProducto], [IdVenta]) VALUES (1, 2, 1, 1)
        INSERT [{baseDeDatos}].[dbo].[ProductoVendido] ([Id], [Stock], [IdProducto], [IdVenta]) VALUES (2, 1, 2, 1)
        INSERT [{baseDeDatos}].[dbo].[ProductoVendido] ([Id], [Stock], [IdProducto], [IdVenta]) VALUES (3, 1, 5, 1)
        INSERT [{baseDeDatos}].[dbo].[ProductoVendido] ([Id], [Stock], [IdProducto], [IdVenta]) VALUES (4, 1, 3, 1)
        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[ProductoVendido] OFF

        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[Usuario] ON
        INSERT [{baseDeDatos}].[dbo].[Usuario] ([Id], [Nombre], [Apellido], [NombreUsuario], [Contraseña], [Mail]) VALUES (1, N'Tobias', N'Casazza', N'tcasazza', N'SoyTobiasCasazza', N'tobiascasazza@gmail.com')
        INSERT [{baseDeDatos}].[dbo].[Usuario] ([Id], [Nombre], [Apellido], [NombreUsuario], [Contraseña], [Mail]) VALUES (2, N'Ernesto', N'Perez', N'eperez', N'SoyErnestoPerez', N'ernestoperez@gmail.com')
        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[Usuario] OFF

        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[Venta] ON
        INSERT [{baseDeDatos}].[dbo].[Venta] ([Id], [Comentarios], [IdUsuario]) VALUES (1, N'', 1)
        INSERT [{baseDeDatos}].[dbo].[Venta] ([Id], [Comentarios], [IdUsuario]) VALUES (2, N'', 1)
        INSERT [{baseDeDatos}].[dbo].[Venta] ([Id], [Comentarios], [IdUsuario]) VALUES (3, N'', 1)
        INSERT [{baseDeDatos}].[dbo].[Venta] ([Id], [Comentarios], [IdUsuario]) VALUES (4, N'', 1)
        INSERT [{baseDeDatos}].[dbo].[Venta] ([Id], [Comentarios], [IdUsuario]) VALUES (5, N'', 1)
        INSERT [{baseDeDatos}].[dbo].[Venta] ([Id], [Comentarios], [IdUsuario]) VALUES (6, N'', 1)
        SET IDENTITY_INSERT [{baseDeDatos}].[dbo].[Venta] OFF

        ALTER TABLE [{baseDeDatos}].[dbo].[Producto]  WITH CHECK ADD  CONSTRAINT [FK_Producto_Usuario] FOREIGN KEY([IdUsuario])
        REFERENCES [{baseDeDatos}].[dbo].[Usuario] ([Id])
        
        ALTER TABLE [{baseDeDatos}].[dbo].[Producto] CHECK CONSTRAINT [FK_Producto_Usuario]
        
        ALTER TABLE [{baseDeDatos}].[dbo].[ProductoVendido]  WITH CHECK ADD  CONSTRAINT [FK_ProductoVendido_Producto] FOREIGN KEY([IdProducto])
        REFERENCES [{baseDeDatos}].[dbo].[Producto] ([Id])
        
        ALTER TABLE [{baseDeDatos}].[dbo].[ProductoVendido] CHECK CONSTRAINT [FK_ProductoVendido_Producto]
        
        ALTER TABLE [{baseDeDatos}].[dbo].[ProductoVendido]  WITH CHECK ADD  CONSTRAINT [FK_ProductoVendido_Venta] FOREIGN KEY([IdVenta])
        REFERENCES [{baseDeDatos}].[dbo].[Venta] ([Id])
        
        ALTER TABLE [{baseDeDatos}].[dbo].[ProductoVendido] CHECK CONSTRAINT [FK_ProductoVendido_Venta]
        
        ALTER TABLE [{baseDeDatos}].[dbo].[Venta]  WITH CHECK ADD  CONSTRAINT [FK_Venta_Usuario] FOREIGN KEY([IdUsuario])
        REFERENCES [{baseDeDatos}].[dbo].[Usuario] ([Id])
        
        ALTER TABLE [{baseDeDatos}].[dbo].[Venta] CHECK CONSTRAINT [FK_Venta_Usuario]

        SELECT 'Tablas y datos creados con exito en base de datos [{baseDeDatos}]';
        ";
    }
}