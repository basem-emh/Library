using Library.BLL.Common.Helpers.Attachments;
using Library.BLL.Services.Books;
using Library.BLL.Services.Departments;
using Library.BLL.Services.Employees;
using Library.DAL.Entities.Users;
using Library.DAL.Persistence.Data;
using Library.DAL.Persistence.UnitOfWork;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Library.PL
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            #region Configure Services
            builder.Services.AddControllersWithViews();
            
            builder.Services.AddDbContext<ApplicationDbContext>(options =>
            options
            .UseLazyLoadingProxies()
            .UseSqlServer(builder.Configuration.GetConnectionString("Connection01"))
            );

            
            builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
            builder.Services.AddScoped<IDepartmentServices, DepartmentServices>();
            builder.Services.AddScoped<IEmployeeServices, EmployeeServices>();
            builder.Services.AddScoped<IBookServices, BookServices>();

            builder.Services.AddTransient<IAttachmentSettings ,  AttachmentSettings>();

            builder.Services.AddIdentity<ApplicationUser, IdentityRole>(options =>
            {                                                           
                options.Password.RequiredUniqueChars = 2;               
                options.Password.RequireDigit = true;                   
                options.Password.RequireLowercase = true;               
                options.Password.RequireUppercase = true;               
                options.Password.RequireNonAlphanumeric = true;
                options.Password.RequiredLength = 6;                                                            
                options.User.RequireUniqueEmail = true;                 
      
                options.Lockout.AllowedForNewUsers = true;              
                options.Lockout.MaxFailedAccessAttempts = 5;
                options.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromHours(2);
            }).AddEntityFrameworkStores<ApplicationDbContext>()
              .AddDefaultTokenProviders();

            builder.Services.ConfigureApplicationCookie(option =>
            {
                option.Cookie.HttpOnly = true;
                option.ExpireTimeSpan = TimeSpan.FromHours(2);
                option.SlidingExpiration = true;
                option.LoginPath = "/Account/SignIn";
                option.LogoutPath = "/Account/SignOut";
                //option.Cookie.Name = "Basem Cookies";
                option.Cookie.SecurePolicy = CookieSecurePolicy.Always;
                option.Cookie.SameSite = SameSiteMode.Strict;
            });

            var app = builder.Build();
            
            #endregion

            #region Configure Kestrel Middilewares

            // Configure the HTTP request pipeline.
            if (!app.Environment.IsDevelopment())
            {
                app.UseExceptionHandler("/Home/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();

            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllerRoute(
                name: "default",
                pattern: "{controller=Home}/{action=Index}/{id?}"); 
            #endregion

            app.Run();
        }
    }
}
