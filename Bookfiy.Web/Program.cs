using Bookfiy.Web.Data;
using Bookify.CoreLayer;
using Bookify.RepositoryLayer.AppData;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Bookfiy.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var webApplicationBuilder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            var connectionString = webApplicationBuilder.Configuration.GetConnectionString("DefaultConnection") ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");
            webApplicationBuilder.Services.AddDbContext<ApplicationDbContext>(options =>
                options.UseSqlServer(connectionString));
            webApplicationBuilder.Services.AddDatabaseDeveloperPageExceptionFilter();

            webApplicationBuilder.Services.AddDefaultIdentity<IdentityUser>(options => options.SignIn.RequireConfirmedAccount = true)
                .AddEntityFrameworkStores<ApplicationDbContext>();
            webApplicationBuilder.Services.AddControllersWithViews();

            webApplicationBuilder.Services.AddDbContext<StoreDbContext>(options => 
            options.UseSqlServer(webApplicationBuilder.Configuration.GetConnectionString("StoreConnection")));

            webApplicationBuilder.Services.AddScoped<IUnitOfWork, UnitOfWork>();    

            var app = webApplicationBuilder.Build();

           var Scope =   app.Services.CreateScope();

           var Services =   Scope.ServiceProvider;  

         using  var dbcontext = Services.GetRequiredService<ApplicationDbContext>();
			using var Storedbcontext = Services.GetRequiredService<StoreDbContext>();

			var loggerFactory =   Services.GetRequiredService<ILoggerFactory>();

            try
            {
                dbcontext.Database.Migrate();
                Storedbcontext.Database.Migrate();
            }
            catch (Exception ex)
            {

              var logger  = loggerFactory.CreateLogger<Program>();

                logger.LogError(string.Empty, ex.Message);
            }

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
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

            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}");
            app.MapRazorPages();

            app.Run();
        }
    }
}
