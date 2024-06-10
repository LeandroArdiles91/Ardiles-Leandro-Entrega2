[![.NET](https://github.com/ChristianGrimberg/Christian-Grimberg-58425-Desafio-2/actions/workflows/dotnet.yml/badge.svg)](https://github.com/ChristianGrimberg/Christian-Grimberg-58425-Desafio-2/actions/workflows/dotnet.yml)

# Desafío 2: Acceso a datos

Crear las clases del proyecto final

## Consigna

Se deben crear las clases de acceso a datos para el proyecto final.

## Aspectos a incluir

* Se debe crear una clase estatica para acceder a los datos asociada a cada clase agregada en el primer desafío.
* Cada Clase se conectará con la base de datos Creada en la Clase 9

### Clase: UsuarioData

#### Metodos Estaticos

* ObtenerUsuario()
* ListarUsuarios()
* CrearUsuario()
* ModificarUsuario()
* EliminarUsuario()

### Clase: ProductoData

#### Metodos Estaticos

* ObtenerProducto()
* ListarProductos()
* CrearProducto()
* ModificarProducto()
* EliminarProducto()

### Clase: ProductoVendidoData

#### Metodos Estaticos

* ObtenerProductoVendido()
* ListarProductosVendidos()
* CrearProductoVendido()
* ModificarProductoVendido()
* EliminarProductoVendido()

### Clase: VentaData

#### Metodos Estaticos

* ObtenerVenta()
* ListarVentas()
* CrearVenta()
* ModificarVenta()
* EliminarVenta()

## Ejemplo

[Link al ejemplo](https://docs.google.com/presentation/d/1-_ZxAhULru54UOC91B0A1M-8dk3MoVluZ_f4-0n16tA/edit?usp=drive_link)

## Sugerencias

* Se recomienda ya tener una solución con una aplicación de Escritorio o Consola para poder implementarlas y probarlas.
* Leer los ejemplos de la clase 9 y 10.
* Tengan en cuenta a qué tabla van a administrar en cada clase.
* En los métodos de Obtener pueden pasar como parámetro el ID de la tabla y hacer select datos from tabla where id=@id.
* Utilizar try catch en todos los casos
* Para poder probar que todo funciona se recomienda probar los métodos con formularios de prueba o bien en consola donde se sientan más cómodos.

## Formato

* Documento de texto. Su nombre debe ser "Nombre-Apellido"
* Pueden subir un Zip o Rar con las clases generadas o bien subir a un repositorio o drive y subir link en entrega.
* La interface gráfica no es obligatoria, pero sí recomendable para probar
los métodos.