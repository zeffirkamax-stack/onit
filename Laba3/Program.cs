using Laba3.Data;
using Microsoft.EntityFrameworkCore;

namespace Laba3
{
	public partial class Program
	{
		public static void Main(string[] args)
		{
			var builder = WebApplication.CreateBuilder(args);

			// Строка подключения приоритетно собирается из переменных окружения (для Docker),
			// иначе берётся из конфигурации (например, appsettings.json) для локальной разработки.
			var envConnection = BuildPostgresConnectionStringFromEnv();
			var connection = string.IsNullOrWhiteSpace(envConnection)
				? builder.Configuration.GetConnectionString("DefaultConnection")
				: envConnection;

			// Add services to the container.
			builder.Services.AddControllersWithViews();
			builder.Services.AddDbContext<AppDbContext>(options => options.UseNpgsql(connection));

			var app = builder.Build();

			// Автоматически применяем миграции при старте приложения.
			using (var scope = app.Services.CreateScope())
			{
				var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();

				if (db.Database.IsRelational())
				{
					db.Database.Migrate();
				}
			}

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

			app.UseAuthorization();

			app.MapControllerRoute(
				name: "default",
				pattern: "{controller=Home}/{action=Index}/{id?}");

			app.Run();
		}

		private static string? BuildPostgresConnectionStringFromEnv()
		{
			var host = Environment.GetEnvironmentVariable("DB_HOST");
			var port = Environment.GetEnvironmentVariable("DB_PORT");
			var dbName = Environment.GetEnvironmentVariable("DB_NAME");
			var user = Environment.GetEnvironmentVariable("DB_USER");
			var password = Environment.GetEnvironmentVariable("DB_PASSWORD");

			if (string.IsNullOrWhiteSpace(host) ||
				string.IsNullOrWhiteSpace(port) ||
				string.IsNullOrWhiteSpace(dbName) ||
				string.IsNullOrWhiteSpace(user) ||
				string.IsNullOrWhiteSpace(password))
			{
				return null;
			}

			return $"Host={host};Port={port};Database={dbName};Username={user};Password={password}";
		}
	}
}
