using System;
using System.ComponentModel.DataAnnotations;
namespace sgc_backend.Filter
{
    public class EstablecerNumMaxEstados : ValidationAttribute
    {
        public byte NumeroMax_Estado { get; }
        public EstablecerNumMaxEstados(byte nMax_estado) => NumeroMax_Estado = nMax_estado;
        public override bool IsValid(object value)
        {
            try
            {
                int n = Convert.ToInt32(value);
                if (n < NumeroMax_Estado) return true;
                else return false;
            }
            catch (Exception) { return false; }
        }
        public override string FormatErrorMessage(string name)
        {
            return "El índice ingresado exedió el valor aceptado en el estado.";
        }
    }
}
