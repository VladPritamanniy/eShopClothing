using Application.Services;
using Core.Options;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Admin
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddDbContexts();

            builder.Services.AddAuthentication();
            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("AdminPolicy", policy =>
                    policy.RequireClaim("Role", "Admin"));
            });

            builder.AddIdentity();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection(nameof(AuthMessageSenderOptions)));

            var app = builder.Build();
            await app.Services.MigrateDatabaseAsync();

            if (app.Environment.IsDevelopment())
            {
                app.UseMigrationsEndPoint();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                app.UseHsts();
            }

            app.UseHttpsRedirection();
            app.UseStaticFiles();

            app.UseRouting();
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapRazorPages();

            await app.RunAsync();
        }
    }
}
