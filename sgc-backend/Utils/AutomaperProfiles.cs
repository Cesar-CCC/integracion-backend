using AutoMapper;
using Microsoft.AspNetCore.Identity;
using sgc_backend.DTOs;
using System.Collections.Generic;

namespace sgc_backend.Controllers
{
    public class AutomaperProfiles : Profile
    {
        public AutomaperProfiles()
        {
            //CreateMap<IdentityUser, UsuarioGetDTO>().ReverseMap();
            //CreateMap<IdentityUser, CredsUsuarioLogin>().ReverseMap();
            //CreateMap<CredsUsuarioLogin, Usuario>().ReverseMap();
            //CreateMap<CredsUsuarioLogin, CredsUsuarioRegister>().ReverseMap();
            //CreateMap<CredsUsuarioLogin, UsuarioGetDTO>().ReverseMap();
            //CreateMap<UsuarioGetDTO, Usuario>().ReverseMap();                       // post y get
            //CreateMap<UsuarioGetDTO, UsuarioPostPutDTO>().ReverseMap();             // post y get
            //CreateMap<UsuarioPostPutDTO, Usuario>().ReverseMap();                   // put
            ////CreateMap<Usuario, Usuario>();
            //CreateMap<ContratoDTO, Contrato>().ReverseMap();
            //CreateMap<CuentasDocenteDTO, CuentasDocente>().ReverseMap();
            //CreateMap<ReclamosDTO, Reclamo>().ReverseMap();
            //CreateMap<PeriodoDTO, Periodo>().ReverseMap();
            //CreateMap<EtapaConcursoDTO, EtapaConcurso>().ReverseMap();
            //CreateMap<List<Usuario>, List<UsuarioDTO>>().ReverseMap();
        }
    }
}
