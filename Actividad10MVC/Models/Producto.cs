using System.ComponentModel.DataAnnotations;

namespace Actividad10MVC.Models;

public class Producto
{
    [Display(Name = "IDE_PRO")]
    public int IdePro { get; set; }

    [Required(ErrorMessage = "Ingrese la descripción del producto.")]
    [StringLength(160)]
    [Display(Name = "DES_PRO")]
    public string DesPro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese la fecha de registro.")]
    [DataType(DataType.Date)]
    [Display(Name = "FRE_PRO")]
    public DateTime? FrePro { get; set; }

    [Required(ErrorMessage = "Seleccione una categoría.")]
    [RegularExpression("^(Útiles|Regalos|Fiestas)$", ErrorMessage = "Seleccione una categoría válida.")]
    [Display(Name = "CAT_PRO")]
    public string CatPro { get; set; } = string.Empty;

    [Required(ErrorMessage = "Ingrese el stock.")]
    [Range(1, 1000, ErrorMessage = "El stock debe estar entre 1 y 1000.")]
    [Display(Name = "STO_PRO")]
    public int? StoPro { get; set; }

    [Required(ErrorMessage = "Ingrese el precio.")]
    [Range(typeof(decimal), "0.01", "9999999", ErrorMessage = "El precio debe ser mayor que cero.")]
    [DataType(DataType.Currency)]
    [Display(Name = "PRE_PRO")]
    public decimal? PrePro { get; set; }
}
