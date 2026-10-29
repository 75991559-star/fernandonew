using System.ComponentModel.DataAnnotations;

namespace Actividad10MVC.Models;

public class Cliente
{
    [Display(Name = "Código de Cliente")]
    public int IdeCli { get; set; }

    [Required(ErrorMessage = "Ingrese el nombre del cliente.")]
    [StringLength(120)]
    [Display(Name = "Nombre del Cliente")]
    public string NomCli { get; set; } = string.Empty;

    [Required(ErrorMessage = "Seleccione el distrito.")]
    [Display(Name = "Distrito")]
    public string IdeDis { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese el número telefónico.")]
    [RegularExpression(@"^[0-9+()\- ]{7,20}$", ErrorMessage = "Ingrese un número telefónico válido.")]
    [Display(Name = "Número Telefónico")]
    public string TelCli { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese el correo electrónico.")]
    [EmailAddress(ErrorMessage = "Ingrese un correo electrónico válido.")]
    [StringLength(160)]
    [Display(Name = "Correo Electrónico")]
    public string CorCli { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese la fecha de registro.")]
    [DataType(DataType.Date)]
    [Display(Name = "Fecha de Registro")]
    public DateTime? FreCli { get; set; }
}
