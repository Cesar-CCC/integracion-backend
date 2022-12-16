using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.Extensions.Logging;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using sgc_backend.Controllers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using sgc_backend.Utils;
using System.Reflection;
using System.IO;
using sgc_backend.Filter;

namespace sgc_backend
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddAutoMapper(typeof(Startup));

            services.AddControllers(options =>
            {
                options.Filters.Add(typeof(ParsearBadRequests));
            }).ConfigureApiBehaviorOptions(BehaviorBadRequests.Parsear);

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "sgc_backend", Version = "v1" });
                // using System.Reflection;
                var xmlFilename = $"{Assembly.GetExecutingAssembly().GetName().Name}.xml";
                c.IncludeXmlComments(Path.Combine(AppContext.BaseDirectory, xmlFilename));
            });

            services.AddDbContext<MyWebApiContext>(opt => opt.UseNpgsql(Configuration.GetConnectionString("MyWebApiConection")));

            services.AddScoped<MyWebApiContext>();           // Base de datos (Una instancia para cada peticion).

            services.AddTransient<OperacionesAdicionales>();    // Operaciones en varias tablas. (Una instancia para todas las peticion).

            // -------------
            services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme).AddJwtBearer(options => options.TokenValidationParameters = new TokenValidationParameters
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = true,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Configuration["llavejwt"])),
                ClockSkew = TimeSpan.Zero
            });

            // -------
            services.AddIdentity<IdentityUser, IdentityRole>().AddEntityFrameworkStores<MyWebApiContext>().AddDefaultTokenProviders();

            services.AddCors(options =>
            {
                var frontEnd = Configuration.GetValue<string>("frontendUrl");
                options.AddDefaultPolicy(builder =>
                {
                    // para que accedan de cualquier origen a nuestra API AllowAnyOrigin
                    builder.WithOrigins(frontEnd).AllowAnyMethod().AllowAnyOrigin().AllowAnyHeader();
                });
            });

            // Tiempo de vida del token.
            services.Configure<DataProtectionTokenProviderOptions>(options =>
            options.TokenLifespan = TimeSpan.FromMinutes(20));

            // ReCaptcha.
            services.AddOptions<CaptchaSettings>().BindConfiguration("Captcha");
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "sgc_backend v1"));
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseCors();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
