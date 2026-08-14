using Microsoft.AspNetCore.Mvc.ModelBinding.Validation;

namespace DerechoExpuesto.Models
{
    public class HomeViewModel
    {
        // Los servicios solamente se utilizan
        // para mostrar las tarjetas en la página.
        // No forman parte del formulario.
        [ValidateNever]
        public List<Servicio> Servicios { get; set; }
            = new();


        // Este sí es el modelo que queremos
        // validar cuando alguien envía
        // el formulario de contacto.
        public ContactoViewModel Contacto { get; set; }
            = new();


        // La configuración se utiliza para mostrar
        // teléfono, email, horario, WhatsApp, etc.
        // No forma parte del formulario de contacto.
        [ValidateNever]
        public ConfiguracionSitio Configuracion { get; set; }
            = new();
    }
}