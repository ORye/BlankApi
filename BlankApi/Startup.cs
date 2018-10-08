using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using BlankApi.Data;
using System;
using Swashbuckle.AspNetCore.Swagger;

namespace BlankApi
{
    public class Startup
    {
        public Startup(IConfiguration configuration, IHostingEnvironment environment)
        {
            Configuration = configuration;
            Environment = environment;
        }

        public IConfiguration Configuration { get; }

        private IHostingEnvironment Environment { get; set; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);

            Action<DbContextOptionsBuilder> dbContextOptions;
            if (Environment.IsDevelopment())
            {
                string localDbPath = "./local.db";
                string connectionString = $"Data Source={localDbPath}";
                dbContextOptions = options => options.UseSqlite(connectionString);
            }
            else
            {
                // TODO: set up other environment db contexts
                throw new NotImplementedException("Non-development DB providers are not yet set up.");
            }

            services.AddDbContext<BlankApiContext>(dbContextOptions);

            // Adding swagger docs for easy API implementation
            services.AddSwaggerGen(c =>
            {
                c.SwaggerDoc("v1", new Info { Title = "Blank API", Version = "v1" });
            });
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                using (IServiceScope serviceScope = app.ApplicationServices.GetService<IServiceScopeFactory>().CreateScope())
                {
                    BlankApiContext context = serviceScope.ServiceProvider.GetRequiredService<BlankApiContext>();
                    context.Database.EnsureCreated();
                }

                // Adding swagger UI only for development purposes
                app.UseSwaggerUI(c =>
                {
                    c.SwaggerEndpoint("/swagger/v1/swagger.json", "Blank API V1");
                });
            }
            else
            {
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseMvc();
            app.UseSwagger();
        }
    }
}
