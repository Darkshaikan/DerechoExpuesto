using System.ComponentModel.DataAnnotations;

namespace DerechoExpuesto.Models
{
    public class Consulta
    {
        public int Id { get; set; }


        [Required]
        [StringLength(120)]
        public string Nombre { get; set; } = string.Empty;


        [Required]
        [EmailAddress]
        [StringLength(150)]
        public string Email { get; set; } = string.Empty;


        [StringLength(50)]
        public string? Telefono { get; set; }


        [Required]
        [StringLength(2000)]
        public string Mensaje { get; set; } = string.Empty;


        public DateTime Fecha { get; set; } =
            DateTime.UtcNow;


        public bool Respondida { get; set; } = false;
    }
}