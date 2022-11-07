using System.ComponentModel.DataAnnotations;

namespace sgc_backend.DTOs
{
    public class CredsUsuarioRegister
    {
        [Required(ErrorMessage = "Este campo es requerido.")]
        [EmailAddress(ErrorMessage = "Se esperaba un @")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Este campo es requerido.")]
        public string Names { get; set; }
    }
}
