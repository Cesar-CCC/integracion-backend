using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using sgc_backend.Controllers;
using sgc_backend.DTOs;
using System;
using System.Reflection;
using System.Threading.Tasks;
using System.Linq;

namespace sgc_backend.Utils
{
    public class OperacionesAdicionales : ControllerBase
    {
        private readonly MyWebApiContext context;
        private readonly SignInManager<IdentityUser> signInManager;

        public OperacionesAdicionales(MyWebApiContext context, SignInManager<IdentityUser> signInManager)
        {
            this.context = context;
            this.signInManager = signInManager;
        }
    }
}
