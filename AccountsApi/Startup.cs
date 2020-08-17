using System.Net.Http;
using AccountsApi.Context;
using AccountsApi.Repositories;
using AccountsApi.Services;
using AccountsApi.Services.Interfaces;
using AccountsApi.Services.Rules;
using AutoMapper;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace AccountsApi
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
            services.AddControllers();
            var connectionString = Configuration["ConnectionStrings:AccountsDBConnectionString"];
            services.AddDbContext<AccountsContext>(x => x.UseSqlServer(connectionString));

            services.AddHttpClient();
            services.AddScoped<IAccountRepository, AccountRepository>();
            services.AddScoped<IAccountAddressService, AccountAddressService>();

            services.AddScoped<IAccountTypeEvaluator, AccountTypeEvaluator>();
            services.AddScoped<IAccountTypeRule, SilverAccountTypeRule>();
            services.AddScoped<IAccountTypeRule, BronzeAccountTypeRule>();
            services.AddScoped<IAccountTypeRule, GoldAccountTypeRule>();
            services.AddAutoMapper(typeof(Startup));
        }

        // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();
            }

            app.UseHttpsRedirection();

            app.UseRouting();

            app.UseAuthorization();

            app.UseEndpoints(endpoints =>
            {
                endpoints.MapControllers();
            });
        }
    }
}
