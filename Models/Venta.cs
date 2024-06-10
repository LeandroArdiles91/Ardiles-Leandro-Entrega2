using System.Reflection;

namespace Christian_Grimberg_58425_Desafio_2;

public class Venta
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

    private string? comentarios;
    public string? Comentarios
    {
        get
        {
            if (string.IsNullOrEmpty(comentarios)) comentarios = "sin Comentarios";
            return comentarios;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                comentarios = value;
                _isEmpty = false;
            }
            else comentarios = string.Empty;
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

    public Venta()
    {
        if(
            string.IsNullOrEmpty(comentarios)
            && idUsuario == 0
        )
        {
            _isEmpty = true;
        }
        else _isEmpty = false;
    }

    public Venta(int _id, string _comentarios, int _idUsuario)
    {
        Id = _id;
        Comentarios = _comentarios;
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