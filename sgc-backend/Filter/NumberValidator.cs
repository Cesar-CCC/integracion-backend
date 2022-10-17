using System;
using System.ComponentModel.DataAnnotations;

namespace sgc_backend.Filter
{
    public class NumberValidator : ValidationAttribute
    {
        public override bool IsValid(object value)
        {
            try
            {
                string test = value as string;
                int _valor = int.Parse(test);
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }
    }
}
