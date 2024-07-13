using Application.Services;
using Core;
using Infrastructure;
using Infrastructure.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;

namespace Web
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            builder.AddDbContexts();

            builder.Services.AddAuthentication().AddGoogle(googleOptions =>
            {
                googleOptions.ClientId = builder.Configuration["GoogleAuthorization:client_id"]!;
                googleOptions.ClientSecret = builder.Configuration["GoogleAuthorization:client_secret"]!;
                googleOptions.CallbackPath = builder.Configuration["GoogleAuthorization:callback_path"];
            });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("UserPolicy", policy =>
                    policy.RequireClaim("Role", "User", "Admin"));
            });

            builder.AddIdentity();
            builder.Services.AddDatabaseDeveloperPageExceptionFilter();
            builder.Services.AddRazorPages();

            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection(nameof(AuthMessageSenderOptions)));
            builder.Services.Configure<LoginOptions>(builder.Configuration.GetSection(nameof(LoginOptions)));

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
