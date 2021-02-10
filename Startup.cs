using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.OpenApi.Models;
using WebApiTest.Models;
using Microsoft.EntityFrameworkCore;

namespace WebApiTest
{
    public class Startup
    {
        //Konfigurera Services och göra dem tillgängliga via Dependency Injection
        public void ConfigureServices(IServiceCollection services)
        {
            // services.AddDbContext<AdviceDbContext>(opt => opt.UseInMemoryDatabase("AdviceDatabase"));
            services.AddDbContext<AdviceDbContext>(opt => opt.UseSqlite("Data Source=minDatabas.db"));

            services.AddControllers(); 
            
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new OpenApiInfo { Title = "WebApiDemo", Version = "v1" });
            });
        }

        //Konfigurera Middleware för hantering av http requests och responses
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
                app.UseSwagger();
                app.UseSwaggerUI(c => c.SwaggerEndpoint("/swagger/v1/swagger.json", "WebApiDemo v1"));
            }

            app.UseDefaultFiles();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseEndpoints( endpoints => endpoints.MapControllers());
        }
    }
}
