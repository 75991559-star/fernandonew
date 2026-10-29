using Actividad10MVC.Services;
using Microsoft.AspNetCore.Mvc;

namespace Actividad10MVC.Controllers;

public class PrincipalController(RegistroStore store) : Controller
{
    public IActionResult Index()
    {
        ViewBag.TotalEmpleados = store.Empleados.Count;
        ViewBag.TotalProductos = store.Productos.Count;
        ViewBag.TotalClientes = store.Clientes.Count;
        // First se usa solo cuando hay productos; FirstOrDefault evita error en listas vacías.
        ViewBag.PrimerProducto = store.Productos.Count > 0 ? store.Productos.First().DesPro : null;
        ViewBag.PrimerEmpleado = store.Empleados.FirstOrDefault();
        return View();
    }

    public IActionResult Error() => View();
}
