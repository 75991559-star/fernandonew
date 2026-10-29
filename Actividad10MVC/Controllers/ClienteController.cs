using Actividad10MVC.Models;
using Actividad10MVC.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Actividad10MVC.Controllers;

public class ClienteController(RegistroStore store) : Controller
{
    // El enunciado pide distrito en combo; se muestran distritos de Lima como opciones de ejemplo.
    private static readonly List<string> Distritos = new() { "Cercado de Lima", "Ate", "Barranco", "Comas", "La Molina", "Miraflores", "San Isidro", "San Juan de Lurigancho", "Surco" };
    private void CargarDistritos(string? seleccionado = null) =>
        ViewBag.Distritos = Distritos.Select(d => new SelectListItem(d, d, d == seleccionado));

    public IActionResult List(string? q)
    {
        var registros = store.Clientes.AsEnumerable();
        if (!string.IsNullOrWhiteSpace(q))
            registros = registros.Where(c => c.NomCli.Contains(q, StringComparison.OrdinalIgnoreCase) || c.IdeDis.Contains(q, StringComparison.OrdinalIgnoreCase));
        ViewBag.Busqueda = q;
        return View(registros.OrderBy(c => c.IdeCli).ToList());
    }

    public IActionResult Create()
    {
        CargarDistritos();
        ViewBag.CodigoSiguiente = store.CodigoClienteSiguiente;
        return View(new Cliente { FreCli = DateTime.Today });
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Create(Cliente cliente)
    {
        if (!ModelState.IsValid)
        {
            CargarDistritos(cliente.IdeDis);
            ViewBag.CodigoSiguiente = store.CodigoClienteSiguiente;
            return View(cliente);
        }
        cliente.IdeCli = store.NuevoCodigoCliente();
        store.Clientes.Add(cliente);
        TempData["Mensaje"] = "Cliente registrado correctamente.";
        return RedirectToAction(nameof(List));
    }

    public IActionResult Details(int id)
    {
        var cliente = store.Clientes.Find(c => c.IdeCli == id);
        return cliente is null ? NotFound() : View(cliente);
    }

    public IActionResult Edit(int id)
    {
        var cliente = store.Clientes.Find(c => c.IdeCli == id);
        if (cliente is null) return NotFound();
        CargarDistritos(cliente.IdeDis);
        return View(cliente);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public IActionResult Edit(int id, Cliente cambios)
    {
        if (id != cambios.IdeCli) return BadRequest();
        if (!ModelState.IsValid)
        {
            CargarDistritos(cambios.IdeDis);
            return View(cambios);
        }
        var cliente = store.Clientes.Find(c => c.IdeCli == id);
        if (cliente is null) return NotFound();
        cliente.NomCli = cambios.NomCli;
        cliente.IdeDis = cambios.IdeDis;
        cliente.TelCli = cambios.TelCli;
        cliente.CorCli = cambios.CorCli;
        cliente.FreCli = cambios.FreCli;
        TempData["Mensaje"] = "Cliente actualizado correctamente.";
        return RedirectToAction(nameof(List));
    }

    public IActionResult Delete(int id)
    {
        var cliente = store.Clientes.Find(c => c.IdeCli == id);
        return cliente is null ? NotFound() : View(cliente);
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public IActionResult DeleteConfirmed(int id)
    {
        if (!store.Clientes.Exists(c => c.IdeCli == id)) return NotFound();
        var cliente = store.Clientes.Find(c => c.IdeCli == id);
        if (cliente is not null) store.Clientes.Remove(cliente);
        TempData["Mensaje"] = "Cliente eliminado.";
        return RedirectToAction(nameof(List));
    }
}
