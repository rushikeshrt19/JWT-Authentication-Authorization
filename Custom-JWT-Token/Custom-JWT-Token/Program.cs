using Custom_JWT_Token.Helpers;
using Custom_JWT_Token.Repository;
using Custom_JWT_Token.Services;
using Microsoft.EntityFrameworkCore;
using System.Configuration;

namespace Custom_JWT_Token
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.
            builder.Services.AddSwaggerGen();
            builder.Services.AddControllers();
            builder.Services.Configure<AppSettings>(builder.Configuration.GetSection("AppSettings"));

            builder.Services.AddDbContext<MyDbContext>(options =>
            {
                options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnections"));
            });

            builder.Services.AddScoped<IUserService, UserService>();
            builder.Services.AddScoped<IAuthenticationService, AuthenticationService>();
            builder.Services.AddScoped<IUserRepository, UserRepository>();

            var app = builder.Build();
            app.UseMiddleware<JwtMiddleware>();

            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            // Configure the HTTP request pipeline.
            app.UseAuthentication();
            app.UseAuthorization();

            app.MapControllers();

            app.Run();
        }
    }
}
