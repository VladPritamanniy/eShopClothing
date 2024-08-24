using Application.Interfaces;
using Application.Mapper;
using Application.Services;
using Core.Options;
using Core.Repositories.Base;
using Core.Specifications.Base;
using Infrastructure;
using Infrastructure.Identity;
using Infrastructure.Repositories.Base;
using Microsoft.AspNetCore.Identity.UI.Services;
using StackExchange.Redis;
using Web.Interfaces;
using Web.Mapper;
using Web.Services;

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
            builder.Services.Configure<RouteOptions>(options =>
            {
                options.AppendTrailingSlash = false;
                options.LowercaseUrls = true;
                //options.LowercaseQueryStrings = true;
            });

            builder.Services.AddAutoMapper(typeof(ViewModelProfile));
            builder.Services.AddAutoMapper(typeof(DtoProfile));
            builder.Services.AddTransient<IEmailSender, EmailSender>();
            builder.Services.AddScoped<IClothingService, ClothingService>();
            builder.Services.AddScoped<ITypeService, TypeService>();
            builder.Services.AddScoped<ISizeService, SizeService>();
            builder.Services.AddScoped<IFileService, FileService>();
            builder.Services.AddScoped(typeof(IRepository<>), typeof(Repository<>));
            builder.Services.AddScoped<ISpecificationEvaluator, SpecificationEvaluator>();
            builder.Services.AddScoped<IProductPageService, ProductPageService>();
            builder.Services.AddScoped<IChangeProductPricePageService, ChangeProductPricePageService>();
            builder.Services.AddScoped<IPermissionService, PermissionService>();
            builder.Services.AddScoped<IClothingPageService, ClothingPageService>();
            //builder.Services.AddSingleton<IConnectionMultiplexer>(_ => ConnectionMultiplexer.Connect(builder.Configuration.GetConnectionString("RedisConnection")));

            builder.Services.Configure<AuthMessageSenderOptions>(builder.Configuration.GetSection(nameof(AuthMessageSenderOptions)));
            builder.Services.Configure<LoginOptions>(builder.Configuration.GetSection(nameof(LoginOptions)));
            builder.Services.Configure<ImageOptions>(builder.Configuration.GetSection(nameof(ImageOptions)));

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
