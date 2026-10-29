using System.ComponentModel.DataAnnotations;

namespace Actividad10MVC.Models;

public class Empleado
{
    [Display(Name = "CODIGO")]
    public int Codigo { get; set; }

    [Required(ErrorMessage = "Ingrese los apellidos.")]
    [StringLength(100)]
    [Display(Name = "APELLIDOS")]
    public string Apellidos { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese los nombres.")]
    [StringLength(100)]
    [Display(Name = "NOMBRES")]
    public string Nombres { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese la fecha de nacimiento.")]
    [DataType(DataType.Date)]
    [Display(Name = "FECHA DE NACIMIENTO")]
    public DateTime? FechaNacimiento { get; set; }

    [Required(ErrorMessage = "Seleccione una categoría.")]
    [RegularExpression("^(Operario|Jefe|Supervisor|Servicio)$", ErrorMessage = "Seleccione una categoría válida.")]
    [Display(Name = "CATEGORIA")]
    public string Categoria { get; set; } = string.Empty;

    [Range(0, 3, ErrorMessage = "El número de hijos debe estar entre 0 y 3.")]
    [Display(Name = "HIJOS")]
    public int? Hijos { get; set; }

    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [Display(Name = "EMAIL")]
    public string? Correo { get; set; }
}
