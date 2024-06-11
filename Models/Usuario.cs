using System.Reflection;

namespace Leandro_Ardiles_Desafio_2;   

public class Usuario
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

    private string? nombre;
    public string? Nombre
    {
        get
        {
            if (string.IsNullOrEmpty(nombre)) nombre = "sin Nombre";
            return nombre;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                nombre = value;
                _isEmpty = false;
            }
            else nombre = string.Empty;
        }
    }

    private string? apellido;
    public string? Apellido
    {
        get
        {
            if (string.IsNullOrEmpty(apellido)) apellido = "sin Apellido";
            return apellido;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                apellido = value;
                _isEmpty = false;
            }
            else apellido = string.Empty;
        }
    }

    private string? nombreUsuario;
    public string? NombreUsuario
    {
        get
        {
            if (string.IsNullOrEmpty(nombreUsuario)) nombreUsuario = "sin NombreUsuario";
            return nombreUsuario;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                nombreUsuario = value;
                _isEmpty = false;
            }
            else nombreUsuario = string.Empty;
        }
    }

    private string? contraseña;
    public string? Contraseña
    {
        get
        {
            if (string.IsNullOrEmpty(contraseña)) contraseña = "sin Contraseña";
            return contraseña;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                contraseña = value;
                _isEmpty = false;
            }
            else contraseña = string.Empty;
        }
    }

    private string? mail;
    public string? Mail
    {
        get
        {
            if (string.IsNullOrEmpty(mail)) mail = "sin Mail";
            return mail;
        }
        set
        {
            if (!string.IsNullOrEmpty(value))
            {
                mail = value;
                _isEmpty = false;
            }
            else mail = string.Empty;
        }
    }

    public Usuario()
    {
        if(
            string.IsNullOrEmpty(nombre)
            && string.IsNullOrEmpty(apellido)
            && string.IsNullOrEmpty(nombreUsuario)
            && string.IsNullOrEmpty(contraseña)
            && string.IsNullOrEmpty(mail)
        )
        {
            _isEmpty = true;
        }
        else _isEmpty = false;
    }

    public Usuario(int _id, string _nombre, string _apellido, string _nombreUsuario, string _contraseña, string _mail)
    {
        Id = _id;
        Nombre = _nombre;
        Apellido = _apellido;
        NombreUsuario = _nombreUsuario;
        Contraseña = _contraseña;
        Mail = _mail;
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