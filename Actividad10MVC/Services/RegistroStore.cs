using Actividad10MVC.Models;

namespace Actividad10MVC.Services;

// List<T> conserva aquí los registros mientras la aplicación está ejecutándose.
public class RegistroStore
{
    public List<Empleado> Empleados { get; } = new();
    public List<Producto> Productos { get; } = new();
    public List<Cliente> Clientes { get; } = new();

    private int _siguienteEmpleado = 1;
    private int _siguienteProducto = 1;
    private int _siguienteCliente = 1;

    public int CodigoEmpleadoSiguiente => _siguienteEmpleado;
    public int CodigoProductoSiguiente => _siguienteProducto;
    public int CodigoClienteSiguiente => _siguienteCliente;

    public int NuevoCodigoEmpleado() => _siguienteEmpleado++;
    public int NuevoCodigoProducto() => _siguienteProducto++;
    public int NuevoCodigoCliente() => _siguienteCliente++;
}
