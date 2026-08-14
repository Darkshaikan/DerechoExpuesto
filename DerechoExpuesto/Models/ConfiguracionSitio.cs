using System.ComponentModel.DataAnnotations;

namespace DerechoExpuesto.Models
{
    public class ConfiguracionSitio
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Teléfono")]
        public string Telefono { get; set; } = string.Empty;

        [Required]
        [EmailAddress]
        [Display(Name = "Email")]
        public string Email { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Horario")]
        public string Horario { get; set; } = string.Empty;

        [Required]
        [Display(Name = "WhatsApp")]
        public string WhatsApp { get; set; } = string.Empty;

        [Required]
        [Display(Name = "Instagram")]
        public string Instagram { get; set; } = string.Empty;
    }
}