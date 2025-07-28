using ECommerceStore.Services;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;

namespace ECommerceStore
{
    public class Startup
    {
        public IConfiguration Configuration { get; }

        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public void ConfigureServices(IServiceCollection services)
        {
            // NÃO registrar DbContext para evitar uso do banco
            // services.AddDbContext<AppDbContext>(...);

            services.AddControllersWithViews();

            // Registrar serviços mockados (sem acesso ao banco)
            services.AddScoped<CategoryService>(); // implementar mockado
            services.AddScoped<ProductService>();  // implementar mockado
            services.AddScoped<BrandService>();    // implementar mockado
            // Outros serviços podem ser comentados se dependem do banco
            // services.AddScoped<CartService>();
            // services.AddScoped<CheckoutService>();
            // services.AddScoped<OrderService>();
            // services.AddScoped<PaymentService>();
            // services.AddScoped<WishlistService>();

            services.AddDistributedMemoryCache();
            services.AddSession(options =>
            {
                options.IdleTimeout = TimeSpan.FromHours(1);
                options.Cookie.HttpOnly = true;
                options.Cookie.IsEssential = true;
            });
        }

        public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
            {
                app.UseDeveloperExceptionPage();

                // SEM migrations e seed, pois não há banco
                /*
                using (var scope = app.ApplicationServices.CreateScope())
                {
                    var dbContext = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                    dbContext.Database.Migrate();
                    SeedData.Initialize(scope.ServiceProvider);
                }
                */
            }
            else
            {
                app.UseExceptionHandler("/Home/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseSession();

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
