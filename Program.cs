
using API_LMFY.Data;
using API_LMFY.Helper.users;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using API_LMFY.Controllers;

namespace API_LMFY
{
	public class Program
	{
		public static void Main(string[] args)
		{
            //var MyAllowSpecificOrigins = "_myAllowSpecificOrigins";

            var MyAllowSpecificOrigins = "_myAllowSpecificOrigins"; 
            var builder = WebApplication.CreateBuilder(args);

            builder.Services.AddCors(options =>
            {
                options.AddPolicy(name: MyAllowSpecificOrigins,
                                  policy =>
                                  {
                                      policy.AllowAnyOrigin().
                                      AllowAnyHeader().
                                      AllowAnyMethod();
                                  });
            });


            // Add services to the container.

            /* CONEXAO COM BANCO MYSQL */
            var connectionStringMysql = builder.Configuration.GetConnectionString("ConexaoMysql");
			builder.Services.AddDbContext<APIContextoDB>(options => options.UseMySql(connectionStringMysql, ServerVersion.Parse("10.4.32-MariaDB")));				

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();
			builder.Services.AddScoped<IEmails, Emails>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.UseSwagger();
            app.UseSwaggerUI();

            app.UseCors(MyAllowSpecificOrigins);

            app.UseHttpsRedirection();

            app.UseAuthorization();


            app.MapControllers();


            app.Run();
        }
    }
}
