using System.ComponentModel.DataAnnotations;

namespace DerechoExpuesto.Models
{
    public class ContactoViewModel
    {
        [Required(
            ErrorMessage =
                "El nombre es obligatorio."
        )]
        [StringLength(
            120,
            ErrorMessage =
                "El nombre no puede superar los 120 caracteres."
        )]
        [Display(Name = "Nombre y apellido")]
        public string Nombre { get; set; } =
            string.Empty;


        [Required(
            ErrorMessage =
                "El email es obligatorio."
        )]
        [EmailAddress(
            ErrorMessage =
                "Ingresá un email válido."
        )]
        [StringLength(150)]
        [Display(Name = "Email")]
        public string Email { get; set; } =
            string.Empty;


        [StringLength(50)]
        [Display(Name = "Teléfono")]
        public string? Telefono { get; set; }


        [Required(
            ErrorMessage =
                "La consulta es obligatoria."
        )]
        [StringLength(
            2000,
            ErrorMessage =
                "La consulta no puede superar los 2000 caracteres."
        )]
        [Display(Name = "Consulta")]
        public string Mensaje { get; set; } =
            string.Empty;
    }
}