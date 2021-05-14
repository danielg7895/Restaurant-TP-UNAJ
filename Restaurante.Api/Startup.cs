using AccessData;
using AccessData.Commands;
using AccessData.Queries;
using Application.Services;
using Domain.Commands;
using Domain.DTOs;
using Domain.Interfaces;
using FluentValidation;
using FluentValidation.AspNetCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using SqlKata.Compilers;
using System.Data;
using System.Data.SqlClient;

namespace Restaurante.Api
{
    public class Startup
    {
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }
        public void ConfigureServices(IServiceCollection services)
        {
            string sqlConnection = Configuration.GetConnectionString("SqlConnection");
            services.AddDbContext<RestaurantContext>(options => options.UseSqlServer(sqlConnection));

            services.AddControllers().AddFluentValidation();

            services.AddTransient<Compiler, SqlServerCompiler>();
            services.AddTransient<IDbConnection>(b =>
            {
                return new SqlConnection(sqlConnection);
            });

            // Queries
            services.AddTransient<IComandaQuery, ComandaQuery>();
            services.AddTransient<IMercaderiaQuery, MercaderiaQuery>();
            services.AddTransient<ITipoMercaderiaQuery, TipoMercaderiaQuery>();
            services.AddTransient<IComandaMercaderiaQuery, ComandaMercaderiaQuery>();

            // Repositories
            services.AddTransient<IGenericRepository, GenericRepository>();

            // Services
            services.AddTransient<IComandaService, ComandaService>();
            services.AddTransient<IMercaderiaService, MercaderiaService>();

            // Validators
            services.AddTransient<IValidator<AddMercaderiaDTO>, AddMercaderiaDTOValidator>();
            services.AddTransient<IValidator<UpdateMercaderiaDTO>, UpdateMercaderiaDTOValidator>();
            services.AddTransient<IValidator<AddComandaDTO>, AddComandaDTOValidator>();

            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "Restaurante.Api", Version = "v1" });
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                //app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "Restaurante.Api v1"));
            }

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
