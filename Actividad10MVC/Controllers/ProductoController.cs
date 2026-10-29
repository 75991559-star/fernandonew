using Actividad10MVC.Models;
using Actividad10MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Actividad10MVC.Controllers;

public class ProductoController(RegistroStore store) : Controller
{
    private static readonly List<string> Categorias = new() { "Útiles", "Regalos", "Fiestas" };
    private void CargarCategorias(string? seleccionada = null) =>
        ViewBag.Categorias = Categorias.Select(c => new SelectListItem(c, c, c == seleccionada));

    public IActionResult List(string? q)
    {
        var registros = store.Productos.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(q))
            registros = registros.Where(p => p.DesPro.Contains(q, StringComparison.OrdinalIgnoreCase));
        ViewBag.Busqueda = q;
        return View(registros.OrderBy(p => p.IdePro).ToList());
    }

    public IActionResult Create()
    {
        CargarCategorias();
        ViewBag.CodigoSiguiente = store.CodigoProductoSiguiente;
        return View(new Producto { FrePro = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Producto producto)
    {
        if (!ModelState.IsValid)
        {
            CargarCategorias(producto.CatPro);
            ViewBag.CodigoSiguiente = store.CodigoProductoSiguiente;
            return View(producto);
        }
        producto.IdePro = store.NuevoCodigoProducto();
        store.Productos.Add(producto);
        TempData["Mensaje"] = "Producto registrado correctamente.";
        return RedirectToAction(nameof(List));
    }

    public IActionResult Details(int id)
    {
        var producto = store.Productos.Find(p => p.IdePro == id);
        return producto is null ? NotFound() : View(producto);
    }

    public IActionResult Edit(int id)
    {
        var producto = store.Productos.Find(p => p.IdePro == id);
        if (producto is null) return NotFound();
        CargarCategorias(producto.CatPro);
        return View(producto);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Producto cambios)
    {
        if (id != cambios.IdePro) return BadRequest();
        if (!ModelState.IsValid)
        {
            CargarCategorias(cambios.CatPro);
            return View(cambios);
        }
        var producto = store.Productos.Find(p => p.IdePro == id);
        if (producto is null) return NotFound();
        producto.DesPro = cambios.DesPro;
        producto.FrePro = cambios.FrePro;
        producto.CatPro = cambios.CatPro;
        producto.StoPro = cambios.StoPro;
        producto.PrePro = cambios.PrePro;
        TempData["Mensaje"] = "Producto actualizado correctamente.";
        return RedirectToAction(nameof(List));
    }

    public IActionResult Delete(int id)
    {
        var producto = store.Productos.Find(p => p.IdePro == id);
        return producto is null ? NotFound() : View(producto);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!store.Productos.Exists(p => p.IdePro == id)) return NotFound();
        var producto = store.Productos.Find(p => p.IdePro == id);
        if (producto is not null) store.Productos.Remove(producto);
        TempData["Mensaje"] = "Producto eliminado.";
        return RedirectToAction(nameof(List));
    }
}
