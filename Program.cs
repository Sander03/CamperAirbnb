using CamperAirbnb.Database.CampingDatabase;
using CamperAirbnb.Database.UserDatabase;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;

namespace CamperAirbnb;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddCors();

        builder.Services.AddAuthentication("BasicAuthenticationScheme")
        .AddScheme<AuthenticationSchemeOptions, BasicAuthenticationHandler>("BasicAuthenticationScheme", null);

        builder.Services.AddAuthorization(options =>
        {
            options.AddPolicy("BasicAuthentication",
                new AuthorizationPolicyBuilder("BasicAuthenticationScheme").RequireAuthenticatedUser().Build());
        });

        builder.Services.AddControllers();
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        builder.Services.AddSingleton<ICampingContext, CampingContext>();
        builder.Services.AddSingleton<IUserContext, UserContext>();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        

        app.UseHttpsRedirection();
        app.UseRouting();
        app.UseCors(builder=>builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader());

        app.UseAuthentication();
        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
