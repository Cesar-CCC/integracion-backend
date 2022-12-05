using System.ComponentModel.DataAnnotations;

namespace sgc_backend.DTOs
{
    public class ProductoDTO
    {
        [Required]
        public string Nombre { get; set; }
        [Required]
        public string EnlaceImagen { get; set; }
        [Required]
        public float Precio { get; set; }
    }
}
