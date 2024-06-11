using System.Reflection;

namespace Leandro_Ardiles_Desafio_2;

public class ProductoVendido
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

    private int idProducto;
    public int IdProducto
    {
        get
        {
            if (idProducto.GetType() != typeof(int)) idProducto = 0;
            return idProducto;
        }
        set
        {
            if (value.GetType() == typeof(int))
            {
                idProducto = value;
                _isEmpty = false;
            }
            else idProducto = 0;
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

    private int idVenta;
    public int IdVenta
    {
        get
        {
            if (idVenta.GetType() != typeof(int)) idVenta = 0;
            return idVenta;
        }
        set
        {
            if (value.GetType() == typeof(int))
            {
                idVenta = value;
                _isEmpty = false;
            }
            else idVenta = 0;
        }
    }

    public ProductoVendido()
    {
        if(
            idProducto == 0
            && stock == 0
            && idVenta == 0
        )
        {
            _isEmpty = true;
        }
        else _isEmpty = false;
    }

    public ProductoVendido(int _id, int _idProducto, int _stock, int _idVenta)
    {
        Id = _id;
        IdProducto = _idProducto;
        Stock = _stock;
        idVenta = _idVenta;
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