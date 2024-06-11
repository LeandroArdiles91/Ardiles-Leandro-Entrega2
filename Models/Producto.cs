using System.Reflection;

namespace Leandro_Ardiles_Desafio_2;

public class Producto
{
    private bool _isEmpty;
    public bool IsEmpty
    {
        get { return _isEmpty; }
    }

    private int id;
    public int Id
    {
        get
        {
            if (id.GetType() != typeof(int)) id = 0;
            return id;
        }
        set
        {
            if (value.GetType() == typeof(int))
            {
                id = value;
                _isEmpty = false;
            }
            else id = 0;
        }
    }

    private string? descripcion;
    public string? Descripcion
    {
        get
        {
            if (string.IsNullOrEmpty(descripcion)) descripcion = "sin Descripcion";
            return descripcion;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                descripcion = value;
                _isEmpty = false;
            }
            else descripcion = string.Empty;
        }
    }

    private decimal costo;
    public decimal Costo
    {
        get
        {
            if (costo.GetType() != typeof(decimal)) costo = 0;
            return costo;
        }
        set
        {
            if (value.GetType() == typeof(decimal))
            {
                costo = value;
                _isEmpty = false;
            }
            else costo = 0;
        }
    }

    private decimal precioVenta;
    public decimal PrecioVenta
    {
        get
        {
            if (precioVenta.GetType() != typeof(decimal)) precioVenta = 0;
            return precioVenta;
        }
        set
        {
            if (value.GetType() == typeof(decimal))
            {
                precioVenta = value;
                _isEmpty = false;
            }
            else precioVenta = 0;
        }
    }

    private int stock;
    public int Stock
    {
        get
        {
            if (stock.GetType() != typeof(int)) stock = 0;
            return stock;
        }
        set
        {
            if (value.GetType() == typeof(int))
            {
                stock = value;
                _isEmpty = false;
            }
            else stock = 0;
        }
    }

    private int idUsuario;
    public int IdUsuario
    {
        get
        {
            if (idUsuario.GetType() != typeof(int)) idUsuario = 0;
            return idUsuario;
        }
        set
        {
            if (value.GetType() == typeof(int))
            {
                idUsuario = value;
                _isEmpty = false;
            }
            else idUsuario = 0;
        }
    }

    public Producto()
    {
        if(
            string.IsNullOrEmpty(descripcion)
            && costo == 0
            && precioVenta == 0
            && stock == 0
        )
        {
            _isEmpty = true;
        }
        else _isEmpty = false;
    }

    public Producto(int _id, string _descripcion, decimal _costo, decimal _precioVenta, int _stock, int _idUsuario)
    {
        Id = _id;
        Descripcion = _descripcion;
        Costo = _costo;
        PrecioVenta = _precioVenta;
        Stock = _stock;
        IdUsuario = _idUsuario;
    }

    public override string ToString()
    {
        string returnedValue = string.Empty;

        foreach (PropertyInfo propertyInfo in GetType().GetProperties())
        {
            returnedValue += string.Format("{0} de {1} es: {2}\n", propertyInfo.Name, GetType().Name, propertyInfo.GetValue(this));
        }
        return returnedValue;
    }
}