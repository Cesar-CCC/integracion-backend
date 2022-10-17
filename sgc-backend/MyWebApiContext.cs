using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace sgc_backend
{
    public class MyWebApiContext : IdentityDbContext
    {
        public MyWebApiContext(DbContextOptions<MyWebApiContext> options) : base(options) { }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            //#region UsuarioDatos IsUnique
            //modelBuilder.Entity<Usuario>()
            //    .HasIndex(u => u.Dni)
            //    .IsUnique();
            //modelBuilder.Entity<Usuario>()
            //    .HasIndex(u => u.Celular)
            //    .IsUnique();
            //modelBuilder.Entity<Usuario>()
            //    .HasIndex(u => u.Email)
            //    .IsUnique();
            ////modelBuilder.Entity<UsuarioDatos>()
            ////    .HasIndex(u => u.IdPeriodo)
            ////    .IsUnique();
            //#endregion
            //#region Reclamos IsUnique
            //#endregion
            //#region Periodos IsUnique
            //modelBuilder.Entity<Periodo>()
            //    .HasIndex(u => u.NombrePeriodo)
            //    .IsUnique();
            ////modelBuilder.Entity<Periodo>()
            ////    .HasIndex(u => u.IdEtapaConcurso)
            ////    .IsUnique();
            //#endregion
            //#region EtapaConcurso IsUnique
            //modelBuilder.Entity<EtapaConcurso>()
            //    .HasIndex(u => u.NombreEtapa)
            //    .IsUnique();
            ////modelBuilder.Entity<EtapaConcurso>()
            ////    .HasIndex(u => u.IdPeriodo)
            ////    .IsUnique();
            //#endregion
            //#region CuentasDocente IsUnique
            //#endregion
            //#region Contrato IsUnique
            //#endregion
            //#region FechaCambio IsUnique
            //#endregion
            //modelBuilder.Entity<Contrato>()
            //.HasOne<Usuario>(e => e.Usuario)
            //.WithMany(d => d.Contratos)
            //.HasForeignKey(e => e.UsuarioFK)
            //.IsRequired(true)
            //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<CuentasDocente>()
            //.HasOne<Usuario>(e => e.Usuario)
            //.WithMany(d => d.CuentasDocentes)
            //.HasForeignKey(e => e.UsuarioFK)
            //.IsRequired(true)
            //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Reclamo>()
            //.HasOne<Usuario>(e => e.Usuario)
            //.WithMany(d => d.Reclamos)
            //.HasForeignKey(e => e.UsuarioFK)
            //.IsRequired(true)
            //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Usuario>()
            //.HasOne<Periodo>(e => e.Periodo)
            //.WithMany(d => d.Usuarios)
            //.HasForeignKey(e => e.PeriodoFK)
            //.IsRequired(true)
            //.OnDelete(DeleteBehavior.Cascade);

            //modelBuilder.Entity<Periodo>()
            //.HasOne<EtapaConcurso>(e => e.EtapaConcurso)
            //.WithMany(d => d.Periodos)
            //.HasForeignKey(e => e.EtapaConcursoFK)
            //.IsRequired(true)
            //.OnDelete(DeleteBehavior.Cascade);
            base.OnModelCreating(modelBuilder);
        }
    }
}
