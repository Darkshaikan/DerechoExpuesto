using System.ComponentModel.DataAnnotations;

namespace DerechoExpuesto.Models
{
    public class Servicio
    {
        public int Id { get; set; }


        [Required(
            ErrorMessage = "El título es obligatorio."
        )]
        [StringLength(
            100,
            ErrorMessage = "El título no puede superar los 100 caracteres."
        )]
        [Display(
            Name = "Título"
        )]
        public string Titulo { get; set; }
            = string.Empty;


        [Required(
            ErrorMessage = "La descripción es obligatoria."
        )]
        [Display(
            Name = "Descripción"
        )]
        public string Descripcion { get; set; }
            = string.Empty;
    }
}