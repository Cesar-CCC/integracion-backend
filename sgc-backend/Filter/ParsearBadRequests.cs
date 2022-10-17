using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Microsoft.AspNetCore.Mvc.Infrastructure;
using System.Collections.Generic;

namespace sgc_backend.Filter
{
    public class ParsearBadRequests : IActionFilter
    {
        public void OnActionExecuted(ActionExecutedContext context)
        {
            var casteoResult = context.Result as IStatusCodeActionResult;
            if (casteoResult == null)
            {
                return;
            }
            var codigoEstatus = casteoResult.StatusCode;
            if (codigoEstatus == 400 || codigoEstatus == 404)
            {
                var respuesta = new List<string>();
                // ----
                dynamic resultadoActual = codigoEstatus.Equals(400) ? context.Result as BadRequestObjectResult : context.Result as NotFoundObjectResult;
                // ---
                if (resultadoActual.Value is string)
                {
                    respuesta.Add(resultadoActual.Value.ToString());
                }
                else if (resultadoActual.Value is IEnumerable<IdentityError> errores)
                {
                    foreach (var error in errores)
                    {
                        respuesta.Add(error.Description);
                    }
                }
                else
                {
                    foreach (var llave in context.ModelState.Keys)
                    {
                        foreach (var error in context.ModelState[llave].Errors)
                        {
                            respuesta.Add($"{llave}: {error.ErrorMessage}");
                        }
                    }
                }
                context.Result = new BadRequestObjectResult(respuesta);
            }
        }
        public void OnActionExecuting(ActionExecutingContext context)
        {
        }
    }
}
