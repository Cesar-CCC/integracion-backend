using System.ComponentModel.DataAnnotations;

namespace sgc_backend.Utils
{
    public class OperacionConURL
    {
        /// <summary>
        /// Colocar el siguiente enlace en este campo para la validación: https://localhost:44324/api/cuentas/(changePasswordForm | validarCuenta)  - Puede ser otro enlace(Frontend). Recordar que al enlace se le agrega: dominio/{[nombre}/.../{[nombre]}
        /// </summary>
        [Url(ErrorMessage = "No ingreso un URL valido.")]
        public string UrlApiOrFrontend { get; set; }
        [EmailAddress(ErrorMessage = "No ingresó un email valido.")]
        public string Email { get; set; }
    }
}
