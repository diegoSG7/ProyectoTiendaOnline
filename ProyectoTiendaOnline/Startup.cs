using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using ProyectoTiendaOnline.Contenedor;
using ProyectoTiendaOnline.Models;
using ProyectoTiendaOnline.Services;

namespace ProyectoTiendaOnline
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
            services.AddAuthentication(options =>
            {
                options.DefaultScheme = "Schema_Customer";
            })
            .AddCookie("Schema_Admin", options =>
            {
                options.LoginPath = "/admin/login";
                options.LogoutPath = "/admin/login/logout";
                options.AccessDeniedPath = "/admin/account/accessDenied";
            })
            .AddCookie("Schema_Vendor", options =>
            {
                options.LoginPath = "/vendedor/login";
                options.LogoutPath = "/vendedor/login/logout";
                options.AccessDeniedPath = "/vendedor/login/accessDenied";
            })
            .AddCookie("Schema_Customer", options =>
            {
                options.LoginPath = "/cliente/login";
                options.LogoutPath = "/cliente/login/logout";
                options.AccessDeniedPath = "/cliente/login/accessDenied";
            });

            services.AddControllersWithViews();

            services.AddHttpContextAccessor();

            services.AddDbContext<DataBaseContext>(
                    options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

            services.AddMvc()
                .AddSessionStateTempDataProvider();
            services.AddSession();

            services.AddTransient<IUsuarioContenedor, UsuarioService>();
            services.AddTransient<IProductoContenedor, ProductoService>();
            services.AddTransient<IVentaContenedor, VentaService>();
            services.AddTransient<ICategoriaContenedor, CategoriaService>();
            services.AddTransient<IFotoContenedor, FotoService>();
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }
            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.Use(async (context, next) => {
                var principal = new ClaimsPrincipal();

                var result1 = await context.AuthenticateAsync("Schema_Admin");
                if (result1?.Principal != null)
                {
                    principal.AddIdentities(result1.Principal.Identities);
                }

                var result2 = await context.AuthenticateAsync("Schema_Vendor");
                if (result2?.Principal != null)
                {
                    principal.AddIdentities(result2.Principal.Identities);
                }

                var result3 = await context.AuthenticateAsync("Schema_Customer");
                if (result3?.Principal != null)
                {
                    principal.AddIdentities(result3.Principal.Identities);
                }

                context.User = principal;
                await next();
            });

            app.UseSession();


            app.UseAuthentication();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllerRoute(
                    name: "default",
                    pattern: "{controller=Home}/{action=Index}/{id?}");
            });
        }
    }
}
