using System.Data.SqlClient;

namespace Christian_Grimberg_58425_Desafio_2;

internal static class GestorMenu
{
    internal static bool MenuPrincipal(SqlConnection connection)
    {
        bool selection = true;

        string mainMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
            "==========MENU PRINCIPAL==========",
            "-------Seleccione un numero-------",
            "0 - Salir del programa",
            "1 - Menu de usuarios",
            "2 - Menu de productos",
            "3 - Menu de productos vendidos",
            "4 - Menu de ventas",
            "----------------------------------"
        );

        Console.Clear();
        Console.WriteLine(mainMenu);

        switch (Input("Seleccione una opcion"))
        {
            case "0":
                Console.WriteLine("Salida del programa");
                selection = false;
                break;
            case "1":
                MenuUsuarios(connection);
                break;
            case "2":
                MenuProductos(connection);
                break;
            case "3":
                MenuProductosVendidos(connection);
                break;
            case "4":
                MenuVentas(connection);
                break;
            default:
                Console.WriteLine("Opción no encontrada");
                Console.WriteLine("Presione una tecla para continuar...");
                Console.ReadKey();
                break;
        }

        return selection;
    }

    private static void MenuUsuarios(SqlConnection connection)
    {
        string usersMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
                "===========MENU USUARIOS==========",
                "-------Seleccione un numero-------",
                "1 - Obtener usuario por ID",
                "2 - Listar usuarios",
                "3 - Crear usuario",
                "4 - Modificar usuario",
                "5 - Eliminar usuario",
                "----------------------------------"
            );
        string userUpdateMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
            "===========MODIFICAR USUARIO==========",
            "-------Seleccione un numero-------",
            "1 - Modificar el nombre",
            "2 - Modificar el apellido",
            "3 - Modificar el nombre de usuario",
            "4 - Modificar la contraseña",
            "5 - Modificar el email",
            "--------------------------------------"
        );

        Console.Clear();
        Console.WriteLine(usersMenu);

        switch (Input("Seleccione una opcion"))
        {
            case "1":
                Console.Clear();
                string userId = Input("Ingrese el ID del usuario a buscar");
                Usuario user = UsuarioData.ObtenerUsuario(connection, Convert.ToInt32(userId));

                if (!user.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL USUARIO==========\n");
                    Console.WriteLine(user);
                    Console.WriteLine("======================================");
                }
                break;
            case "2":
                if (UsuarioData.ListarUsuarios(connection).Capacity > 0)
                {
                    Console.Clear();
                    Console.WriteLine("===========LISTADO USUARIOS==========\n");
                    foreach (var item in UsuarioData.ListarUsuarios(connection))
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("=====================================");
                }
                else
                {
                    Console.WriteLine("No hay usuarios en el sistema");
                }
                break;
            case "3":
                Usuario newUser = new Usuario();
                Console.Clear();
                try
                {
                    Console.WriteLine("===========NUEVO USUARIO==========\n");
                    Console.Write("Ingrese el nombre: ");
                    newUser.Nombre = Console.ReadLine();
                    Console.Write("Ingrese el apellido: ");
                    newUser.Apellido = Console.ReadLine();
                    Console.Write("Ingrese el nombre de inicio de sesion: ");
                    newUser.NombreUsuario = Console.ReadLine();
                    Console.Write("Ingrese la contraseña: ");
                    newUser.Contraseña = Console.ReadLine();
                    Console.Write("Ingrese el email: ");
                    newUser.Mail = Console.ReadLine();
                    Console.WriteLine("==================================");

                    if (!newUser.IsEmpty && UsuarioData.CrearUsuario(connection, newUser))
                    {
                        Console.WriteLine("Usuario creado con éxito");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[APPLICATION ERROR]: {ex.Message}");
                }
                break;
            case "4":
                Console.Clear();
                string agreeToUpdate;
                string userIdToUpdate = Input("Ingrese el ID a modificar");
                string fieldOption;
                Usuario userToUpdate = UsuarioData.ObtenerUsuario(connection, Convert.ToInt32(userIdToUpdate));

                if (!userToUpdate.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL USUARIO==========\n");
                    Console.WriteLine(userToUpdate);
                    Console.WriteLine("======================================");

                    agreeToUpdate = Input("Desea modificar al usuario? S(Si) - N(No)");
                    if (!string.IsNullOrEmpty(agreeToUpdate) && agreeToUpdate.ToUpper()[0] == 'S')
                    {
                        Console.Clear();
                        Console.WriteLine(userUpdateMenu);
                        fieldOption = Input("Seleccione una opcion");

                        switch (fieldOption)
                        {
                            case "1":
                                Console.Write("Ingrese el nuevo nombre: ");
                                userToUpdate.Nombre = Console.ReadLine();

                                if (!userToUpdate.IsEmpty && UsuarioData.ModificarUsuario(connection, userToUpdate))
                                {
                                    Console.WriteLine("El nombre se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el nombre");
                                }
                                break;
                            case "2":
                                Console.Write("Ingrese el nuevo apellido: ");
                                userToUpdate.Apellido = Console.ReadLine();

                                if (!userToUpdate.IsEmpty && UsuarioData.ModificarUsuario(connection, userToUpdate))
                                {
                                    Console.WriteLine("El apellido se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el apellido");
                                }
                                break;
                            case "3":
                                Console.Write("Ingrese el nuevo nombre de usuario: ");
                                userToUpdate.NombreUsuario = Console.ReadLine();

                                if (!userToUpdate.IsEmpty && UsuarioData.ModificarUsuario(connection, userToUpdate))
                                {
                                    Console.WriteLine("El nombre del usuario se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el nombre del usuario");
                                }
                                break;
                            case "4":
                                Console.Write("Ingrese la nueva contraseña: ");
                                userToUpdate.Contraseña = Console.ReadLine();

                                if (!userToUpdate.IsEmpty && UsuarioData.ModificarUsuario(connection, userToUpdate))
                                {
                                    Console.WriteLine("La contraseña se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar la contraseña");
                                }
                                break;
                            case "5":
                                Console.Write("Ingrese el nuevo email: ");
                                userToUpdate.Mail = Console.ReadLine();

                                if (!userToUpdate.IsEmpty && UsuarioData.ModificarUsuario(connection, userToUpdate))
                                {
                                    Console.WriteLine("El email se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el email");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            case "5":
                string agreeToDelete;

                Console.Clear();
                string userIdToDelete = Input("Ingrese el ID del usuario y toda su actividad a eliminar");
                Usuario userToDelete = UsuarioData.ObtenerUsuario(connection, Convert.ToInt32(userIdToDelete));

                if (!userToDelete.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL USUARIO==========\n");
                    Console.WriteLine(userToDelete);
                    Console.WriteLine("======================================");
                    agreeToDelete = Input("Desea eliminar al usuario y toda su actividad? S(Si) - N(No)");

                    if (
                        !string.IsNullOrEmpty(agreeToDelete)
                        && agreeToDelete.ToUpper()[0] == 'S'
                        && UsuarioData.EliminarUsuario(connection, userToDelete)
                    )
                    {
                        Console.WriteLine("Se eliminó el usuario y toda su actividad");
                    }
                }
                break;
            default:
                Console.WriteLine("Opción no encontrada");
                break;
        }

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void MenuProductos(SqlConnection connection)
    {
        string productsMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
                "===========MENU PRODUCTOS==========",
                "-------Seleccione un numero-------",
                "1 - Obtener producto por ID",
                "2 - Listar productos",
                "3 - Crear productos",
                "4 - Modificar productos",
                "5 - Eliminar productos",
                "-----------------------------------"
            );
        string productUpdateMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
            "===========MODIFICAR PRODUCTO==========",
            "-------Seleccione un numero-------",
            "1 - Modificar la descripcion",
            "2 - Modificar el costo",
            "3 - Modificar el precio de venta",
            "4 - Modificar el stock",
            "5 - Modificar el ID del usuario",
            "---------------------------------------"
        );

        Console.Clear();
        Console.WriteLine(productsMenu);

        switch (Input("Seleccione una opcion"))
        {
            case "1":
                Console.Clear();
                string productId = Input("Ingrese el ID del producto a buscar");
                Producto product = ProductoData.ObtenerProducto(connection, Convert.ToInt32(productId));

                if (!product.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL PRODUCTO==========\n");
                    Console.WriteLine(product);
                    Console.WriteLine("=======================================");
                }
                break;
            case "2":
                if (ProductoData.ListarProductos(connection).Capacity > 0)
                {
                    Console.Clear();
                    Console.WriteLine("===========LISTADO PRODUCTOS==========\n");
                    foreach (var item in ProductoData.ListarProductos(connection))
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("======================================");
                }
                else
                {
                    Console.WriteLine("No hay productos en el sistema");
                }
                break;
            case "3":
                Producto newProduct = new Producto();
                Console.Clear();
                try
                {
                    Console.WriteLine("===========NUEVO PRODUCTO==========\n");
                    Console.Write("Ingrese la descripcion: ");
                    newProduct.Descripcion = Console.ReadLine();
                    Console.Write("Ingrese el costo: ");
                    newProduct.Costo = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Ingrese el precio de venta: ");
                    newProduct.PrecioVenta = Convert.ToDecimal(Console.ReadLine());
                    Console.Write("Ingrese el stock: ");
                    newProduct.Stock = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese el ID del usuario: ");
                    newProduct.IdUsuario = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("===================================");

                    if (!newProduct.IsEmpty && ProductoData.CrearProducto(connection, newProduct))
                    {
                        Console.WriteLine("Producto creado con éxito");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[APPLICATION ERROR]: {ex.Message}");
                }
                break;
            case "4":
                Console.Clear();
                string agreeToUpdate;
                string productIdToUpdate = Input("Ingrese el ID a modificar");
                string fieldOption;
                Producto productToUpdate = ProductoData.ObtenerProducto(connection, Convert.ToInt32(productIdToUpdate));

                if (!productToUpdate.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL PRODUCTO==========\n");
                    Console.WriteLine(productToUpdate);
                    Console.WriteLine("=======================================");

                    agreeToUpdate = Input("Desea modificar al producto? S(Si) - N(No)");
                    if (!string.IsNullOrEmpty(agreeToUpdate) && agreeToUpdate.ToUpper()[0] == 'S')
                    {
                        Console.Clear();
                        Console.WriteLine(productUpdateMenu);
                        fieldOption = Input("Seleccione una opcion");

                        switch (fieldOption)
                        {
                            case "1":
                                Console.Write("Ingrese la nueva descripcion: ");
                                productToUpdate.Descripcion = Console.ReadLine();

                                if (!productToUpdate.IsEmpty && ProductoData.ModificarProducto(connection, productToUpdate))
                                {
                                    Console.WriteLine("La descipción se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar la descipción");
                                }
                                break;
                            case "2":
                                Console.Write("Ingrese el nuevo costo: ");
                                productToUpdate.Costo = Convert.ToDecimal(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoData.ModificarProducto(connection, productToUpdate))
                                {
                                    Console.WriteLine("El costo se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el costo");
                                }
                                break;
                            case "3":
                                Console.Write("Ingrese el nuevo precio de venta: ");
                                productToUpdate.PrecioVenta = Convert.ToDecimal(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoData.ModificarProducto(connection, productToUpdate))
                                {
                                    Console.WriteLine("El precio de venta se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el precio de venta");
                                }
                                break;
                            case "4":
                                Console.Write("Ingrese el nuevo stock: ");
                                productToUpdate.Stock = Convert.ToInt32(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoData.ModificarProducto(connection, productToUpdate))
                                {
                                    Console.WriteLine("El stock se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el stock");
                                }
                                break;
                            case "5":
                                Console.Write("Ingrese el nuevo ID de usuario: ");
                                productToUpdate.IdUsuario = Convert.ToInt32(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoData.ModificarProducto(connection, productToUpdate))
                                {
                                    Console.WriteLine("El ID de usuario se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el ID de usuario");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            case "5":
                string agreeToDelete;

                Console.Clear();
                string productIdToDelete = Input("Ingrese el ID del producto y toda su actividad a eliminar");
                Producto productToDelete = ProductoData.ObtenerProducto(connection, Convert.ToInt32(productIdToDelete));

                if (!productToDelete.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL PRODUCTO==========\n");
                    Console.WriteLine(productToDelete);
                    Console.WriteLine("=======================================");
                    agreeToDelete = Input("Desea eliminar al producto y toda su actividad? S(Si) - N(No)");

                    if (
                        !string.IsNullOrEmpty(agreeToDelete)
                        && agreeToDelete.ToUpper()[0] == 'S'
                        && ProductoData.EliminarProducto(connection, productToDelete)
                    )
                    {
                        Console.WriteLine("Se eliminó el producto y toda su actividad");
                    }
                }
                break;
            default:
                Console.WriteLine("Opción no encontrada");
                break;
        }

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void MenuProductosVendidos(SqlConnection connection)
    {
        string productsMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
                "===========MENU PRODUCTOS VENDIDOS==========",
                "-------Seleccione un numero-------",
                "1 - Obtener productos vendidos por ID",
                "2 - Listar productos vendidos",
                "3 - Ingresar un producto vendido",
                "4 - Modificar un producto vendido",
                "5 - Eliminar un producto vendido",
                "--------------------------------------------"
            );
        string productUpdateMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}",
            "===========MODIFICAR PRODUCTOS VENDIDOS==========",
            "-------Seleccione un numero-------",
            "1 - Modificar el stock",
            "2 - Modificar el ID de producto",
            "3 - Modificar el ID de venta",
            "-------------------------------------------------"
        );

        Console.Clear();
        Console.WriteLine(productsMenu);

        switch (Input("Seleccione una opcion"))
        {
            case "1":
                Console.Clear();
                string productId = Input("Ingrese el ID del producto vendido a buscar");
                ProductoVendido product = ProductoVendidoData.ObtenerProductoVendido(connection, Convert.ToInt32(productId));

                if (!product.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL PRODUCTO VENDIDO==========\n");
                    Console.WriteLine(product);
                    Console.WriteLine("=================================================");
                }
                break;
            case "2":
                if (ProductoVendidoData.ListarProductosVendidos(connection).Capacity > 0)
                {
                    Console.Clear();
                    Console.WriteLine("===========LISTADO DE PRODUCTOS VENDIDOS==========\n");
                    foreach (var item in ProductoVendidoData.ListarProductosVendidos(connection))
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("==================================================");
                }
                else
                {
                    Console.WriteLine("No hay productos vendidos en el sistema");
                }
                break;
            case "3":
                ProductoVendido newProduct = new ProductoVendido();
                Console.Clear();
                try
                {
                    Console.WriteLine("===========INGRESAR PRODUCTO VENDIDO==========\n");
                    Console.Write("Ingrese el stock: ");
                    newProduct.Stock = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese el ID de producto: ");
                    newProduct.IdProducto = Convert.ToInt32(Console.ReadLine());
                    Console.Write("Ingrese el ID de venta: ");
                    newProduct.IdVenta = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("==============================================");

                    if (!newProduct.IsEmpty && ProductoVendidoData.CrearProductoVendido(connection, newProduct))
                    {
                        Console.WriteLine("Producto vendido ingresado con éxito");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[APPLICATION ERROR]: {ex.Message}");
                }
                break;
            case "4":
                Console.Clear();
                string agreeToUpdate;
                string productIdToUpdate = Input("Ingrese el ID a modificar");
                string fieldOption;
                ProductoVendido productToUpdate = ProductoVendidoData.ObtenerProductoVendido(connection, Convert.ToInt32(productIdToUpdate));

                if (!productToUpdate.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL PRODUCTO VENDIDO==========\n");
                    Console.WriteLine(productToUpdate);
                    Console.WriteLine("===============================================");

                    agreeToUpdate = Input("Desea modificar al producto vendido? S(Si) - N(No)");
                    if (!string.IsNullOrEmpty(agreeToUpdate) && agreeToUpdate.ToUpper()[0] == 'S')
                    {
                        Console.Clear();
                        Console.WriteLine(productUpdateMenu);
                        fieldOption = Input("Seleccione una opcion");

                        switch (fieldOption)
                        {
                            case "1":
                                Console.Write("Ingrese el nuevo stock: ");
                                productToUpdate.Stock = Convert.ToInt32(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoVendidoData.ModificarProductoVendido(connection, productToUpdate))
                                {
                                    Console.WriteLine("El stock se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el stock");
                                }
                                break;
                            case "2":
                                Console.Write("Ingrese el nuevo ID de producto: ");
                                productToUpdate.IdProducto = Convert.ToInt32(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoVendidoData.ModificarProductoVendido(connection, productToUpdate))
                                {
                                    Console.WriteLine("El ID de producto se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el ID de producto");
                                }
                                break;
                            case "3":
                                Console.Write("Ingrese el nuevo ID de venta: ");
                                productToUpdate.IdVenta = Convert.ToInt32(Console.ReadLine());

                                if (!productToUpdate.IsEmpty && ProductoVendidoData.ModificarProductoVendido(connection, productToUpdate))
                                {
                                    Console.WriteLine("El ID de venta se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el ID de venta");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            case "5":
                string agreeToDelete;

                Console.Clear();
                string productIdToDelete = Input("Ingrese el ID del producto vendido y toda su actividad a eliminar");
                ProductoVendido productToDelete = ProductoVendidoData.ObtenerProductoVendido(connection, Convert.ToInt32(productIdToDelete));

                if (!productToDelete.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DEL PRODUCTO VENDIDO==========\n");
                    Console.WriteLine(productToDelete);
                    Console.WriteLine("===============================================");
                    agreeToDelete = Input("Desea eliminar al producto vendido y toda su actividad? S(Si) - N(No)");

                    if (
                        !string.IsNullOrEmpty(agreeToDelete)
                        && agreeToDelete.ToUpper()[0] == 'S'
                        && ProductoVendidoData.EliminarProductoVendido(connection, productToDelete)
                    )
                    {
                        Console.WriteLine("Se eliminó el producto vendido y toda su actividad");
                    }
                }
                break;
            default:
                Console.WriteLine("Opción no encontrada");
                break;
        }

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }

    private static void MenuVentas(SqlConnection connection)
    {
        string ventasMenu = string.Format("{0}\n{1}\n{2}\n{3}\n{4}\n{5}\n{6}\n{7}",
                "===========MENU VENTAS==========",
                "-------Seleccione un numero-------",
                "1 - Obtener ventas por ID",
                "2 - Listar ventas",
                "3 - Ingresar una venta",
                "4 - Modificar una venta",
                "5 - Eliminar una venta",
                "--------------------------------"
            );
        string ventasUpdateMenu = string.Format("{0}\n{1}\n{2}\n{3}",
            "===========MODIFICAR VENTAS==========",
            "-------Seleccione un numero-------",
            "1 - Modificar el comentario",
            "2 - Modificar el ID de usuario",
            "-------------------------------------"
        );

        Console.Clear();
        Console.WriteLine(ventasMenu);

        switch (Input("Seleccione una opcion"))
        {
            case "1":
                Console.Clear();
                string ventaId = Input("Ingrese el ID de la venta a buscar");
                Venta venta = VentaData.ObtenerVenta(connection, Convert.ToInt32(ventaId));

                if (!venta.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DE LA VENTA==========\n");
                    Console.WriteLine(venta);
                    Console.WriteLine("======================================");
                }
                break;
            case "2":
                if (VentaData.ListarVentas(connection).Capacity > 0)
                {
                    Console.Clear();
                    Console.WriteLine("===========LISTADO DE VENTAS==========\n");
                    foreach (var item in VentaData.ListarVentas(connection))
                    {
                        Console.WriteLine(item);
                    }
                    Console.WriteLine("==================================================");
                }
                else
                {
                    Console.WriteLine("No hay ventas en el sistema");
                }
                break;
            case "3":
                Venta newVenta = new Venta();
                Console.Clear();
                try
                {
                    Console.WriteLine("===========INGRESAR UNA VENTA==========\n");
                    Console.Write("Ingrese el comentario: ");
                    newVenta.Comentarios = Console.ReadLine();
                    Console.Write("Ingrese el ID de usuario: ");
                    newVenta.IdUsuario = Convert.ToInt32(Console.ReadLine());
                    Console.WriteLine("=======================================");

                    if (!newVenta.IsEmpty && VentaData.CrearVenta(connection, newVenta))
                    {
                        Console.WriteLine("Venta ingresada con éxito");
                    }
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"[APPLICATION ERROR]: {ex.Message}");
                }
                break;
            case "4":
                Console.Clear();
                string agreeToUpdate;
                string ventaIdToUpdate = Input("Ingrese el ID a modificar");
                string fieldOption;
                Venta ventaToUpdate = VentaData.ObtenerVenta(connection, Convert.ToInt32(ventaIdToUpdate));

                if (!ventaToUpdate.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DE LA VENTA==========\n");
                    Console.WriteLine(ventaToUpdate);
                    Console.WriteLine("======================================");

                    agreeToUpdate = Input("Desea modificar la venta? S(Si) - N(No)");
                    if (!string.IsNullOrEmpty(agreeToUpdate) && agreeToUpdate.ToUpper()[0] == 'S')
                    {
                        Console.Clear();
                        Console.WriteLine(ventasUpdateMenu);
                        fieldOption = Input("Seleccione una opcion");

                        switch (fieldOption)
                        {
                            case "1":
                                Console.Write("Ingrese el nuevo comentario: ");
                                ventaToUpdate.Comentarios = Console.ReadLine();

                                if (!ventaToUpdate.IsEmpty && VentaData.ModificarVenta(connection, ventaToUpdate))
                                {
                                    Console.WriteLine("El comentario se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el comentario");
                                }
                                break;
                            case "2":
                                Console.Write("Ingrese el nuevo ID de usuario: ");
                                ventaToUpdate.IdUsuario = Convert.ToInt32(Console.ReadLine());

                                if (!ventaToUpdate.IsEmpty && VentaData.ModificarVenta(connection, ventaToUpdate))
                                {
                                    Console.WriteLine("El ID de usuario se modificó con éxito");
                                }
                                else
                                {
                                    Console.WriteLine("No se pudo modificar el ID de usuario");
                                }
                                break;
                            default:
                                break;
                        }
                    }
                }
                break;
            case "5":
                string agreeToDelete;

                Console.Clear();
                string ventaIdToDelete = Input("Ingrese el ID de la venta y toda su actividad a eliminar");
                Venta ventaToDelete = VentaData.ObtenerVenta(connection, Convert.ToInt32(ventaIdToDelete));

                if (!ventaToDelete.IsEmpty)
                {
                    Console.WriteLine("\n===========DATOS DE LA VENTA==========\n");
                    Console.WriteLine(ventaToDelete);
                    Console.WriteLine("======================================");
                    agreeToDelete = Input("Desea eliminar a la venta y toda su actividad? S(Si) - N(No)");

                    if (
                        !string.IsNullOrEmpty(agreeToDelete)
                        && agreeToDelete.ToUpper()[0] == 'S'
                        && VentaData.EliminarVenta(connection, ventaToDelete)
                    )
                    {
                        Console.WriteLine("Se eliminó la venta y toda su actividad");
                    }
                }
                break;
            default:
                Console.WriteLine("Opción no encontrada");
                break;
        }

        Console.WriteLine("Presione una tecla para continuar...");
        Console.ReadKey();
    }
    private static string? Input(string message)
    {
        string option = string.Empty;

        Console.Write("{0}: ", message);

        try
        {
            option = Console.ReadLine();
        }
        catch (Exception ex)
        {
            Console.WriteLine($"[APPLICATION ERROR]: {ex.Message}");
        }

        return option;
    }
}