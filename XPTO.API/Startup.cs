using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using XPTO.Repository;
using XPTO.Repository.Interfaces;
using XPTO.Repository.Repositorys;

namespace XPTO.API
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
             var migrationsAssembly = typeof(Startup).GetTypeInfo().Assembly.GetName().Name;

            //configuração de conexão com o banco
            services.AddDbContext<XPTOContext>(options =>
            {
                //options.UseLazyLoadingProxies().UseMySql(Configuration.GetConnectionString(Configuration["Database:DefaultConnection"]), x =>
                options.UseSqlServer(Configuration.GetConnectionString(Configuration["Database:DefaultConnection"]), x =>
                {
                    //x.ServerVersion(new Version(5, 7, 21), ServerType.MySql);
                    x.MigrationsHistoryTable("EfMigrations");
                    x.MigrationsAssembly(migrationsAssembly);
                    
                });
            });

            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);
            
            //Informar a aplicação que a mesma trabalha com AutoMapper
            /*
                    *Domain*    *API*  
                Ex: Evento <--> EventoDto
                DTO = Data transfer object
            */
            services.AddAutoMapper();

            //sempre que precisar do IProAgilRepository, impletamenta o ProAgilRepository
            services.AddScoped<IProductRepository, ProductRepository>();

            //Configuração de permisão - CORS
            services.AddCors();

            //Adicionando swagger
            services.AddSwaggerGen(c => {
                c.SwaggerDoc("v1", new Microsoft.OpenApi.Models.OpenApiInfo {Title = "XPTO", Version = "v1"});
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            InitializeDatabase(app);

            //app.UseAuthentication(); //informa que a api precisa ser autenticada

            //app.UseHttpsRedirection();
            app.UseCors(x => x.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());
            app.UseMvc();

            app.UseSwagger();

            app.UseSwaggerUI(c => {
                c.RoutePrefix = "swagger";
                c.SwaggerEndpoint("/swagger/v1/swagger.json", "v1");
            });

        }
        private void InitializeDatabase(IApplicationBuilder app)
        {
            using (var serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
            {
                var audieDbContext = serviceScope.ServiceProvider.GetRequiredService<XPTOContext>();
                audieDbContext.Database.Migrate();
            }
        }
    }
}
