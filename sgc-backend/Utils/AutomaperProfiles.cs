using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sgc_backend.DTOs;
using sgc_backend.models;
using System.Collections.Generic;

namespace sgc_backend.Controllers
{
    public class AutomaperProfiles : Profile
    {
        public AutomaperProfiles()
        {
            CreateMap<ProductoDTO, Producto>().ReverseMap();
        }
    }
}
