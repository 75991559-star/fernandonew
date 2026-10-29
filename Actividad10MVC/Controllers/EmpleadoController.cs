using Actividad10MVC.Models;
using Actividad10MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Actividad10MVC.Controllers;

public class EmpleadoController(RegistroStore store) : Controller
{
    private static readonly List<string> Categorias = new() { "Operario", "Jefe", "Supervisor", "Servicio" };

    private void CargarCategorias(string? seleccionada = null) =>
        ViewBag.Categorias = Categorias.Select(c => new SelectListItem(c, c, c == seleccionada));

    public IActionResult List(string? q)
    {
        var registros = store.Empleados.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(q))
            registros = registros.Where(e => $"{e.Apellidos} {e.Nombres}".Contains(q, StringComparison.OrdinalIgnoreCase));
        ViewBag.Busqueda = q;
        return View(registros.OrderBy(e => e.Codigo).ToList());
    }

    public IActionResult Create()
    {
        CargarCategorias();
        ViewBag.CodigoSiguiente = store.CodigoEmpleadoSiguiente;
        return View(new Empleado { FechaNacimiento = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Empleado empleado)
    {
        if (!ModelState.IsValid)
        {
            CargarCategorias(empleado.Categoria);
            ViewBag.CodigoSiguiente = store.CodigoEmpleadoSiguiente;
            return View(empleado);
        }
        empleado.Codigo = store.NuevoCodigoEmpleado();
        store.Empleados.Add(empleado);
        TempData["Mensaje"] = "Empleado registrado correctamente.";
        return RedirectToAction(nameof(List));
    }

    public IActionResult Details(int id)
    {
        var empleado = store.Empleados.Find(e => e.Codigo == id);
        return empleado is null ? NotFound() : View(empleado);
    }

    public IActionResult Edit(int id)
    {
        var empleado = store.Empleados.Find(e => e.Codigo == id);
        if (empleado is null) return NotFound();
        CargarCategorias(empleado.Categoria);
        return View(empleado);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Empleado cambios)
    {
        if (id != cambios.Codigo) return BadRequest();
        if (!ModelState.IsValid)
        {
            CargarCategorias(cambios.Categoria);
            return View(cambios);
        }
        var empleado = store.Empleados.Find(e => e.Codigo == id);
        if (empleado is null) return NotFound();
        empleado.Apellidos = cambios.Apellidos;
        empleado.Nombres = cambios.Nombres;
        empleado.FechaNacimiento = cambios.FechaNacimiento;
        empleado.Categoria = cambios.Categoria;
        empleado.Hijos = cambios.Hijos;
        empleado.Correo = cambios.Correo;
        TempData["Mensaje"] = "Empleado actualizado correctamente.";
        return RedirectToAction(nameof(List));
    }

    public IActionResult Delete(int id)
    {
        var empleado = store.Empleados.Find(e => e.Codigo == id);
        return empleado is null ? NotFound() : View(empleado);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!store.Empleados.Exists(e => e.Codigo == id)) return NotFound();
        var empleado = store.Empleados.Find(e => e.Codigo == id);
        if (empleado is not null) store.Empleados.Remove(empleado);
        TempData["Mensaje"] = "Empleado eliminado.";
        return RedirectToAction(nameof(List));
    }
}
