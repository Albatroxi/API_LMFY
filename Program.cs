
using API_LMFY.Data;
using Microsoft.EntityFrameworkCore;

namespace API_LMFY
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Add services to the container.

			/* CONEXAO COM BANCO MYSQL */
			var connectionStringMysql = builder.Configuration.GetConnectionString("ConexaoMysql");
			builder.Services.AddDbContext<APIContextoDB>(options => options.UseMySql(connectionStringMysql, ServerVersion.Parse("10.4.32-MariaDB")));

			builder.Services.AddControllers();
			// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
			builder.Services.AddEndpointsApiExplorer();
			builder.Services.AddSwaggerGen();

			builder.Services.AddMvc(options =>
			{
				options.SuppressAsyncSuffixInActionNames = false;
			});

			var app = builder.Build();

			// Configure the HTTP request pipeline.
			if (app.Environment.IsDevelopment())
			{
				app.UseSwagger();
				app.UseSwaggerUI();
			}

			app.UseHttpsRedirection();

			app.UseAuthorization();


			app.MapControllers();

			app.Run();
		}
	}
}
