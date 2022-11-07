using System;

namespace sgc_backend.DTOs
{
    public class RespuestaAutenticacion
    {
        public string Token { get; set; }
        public DateTime Expiracion { get; set; }
        public string Names { get; set; }
    }
    public class DatosPut
    {
        public string Names { get; set; }
        public string NewNames { get; set; }
    }
}
