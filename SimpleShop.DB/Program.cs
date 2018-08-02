using System;
using System.Configuration;
using System.Reflection;
using DbUp;

namespace SimpleShop.DB
{
	class Program
	{
		static int Main(string[] args)
		{
			var connectionString =
				ConfigurationManager.ConnectionStrings["DefaultConnection"].ConnectionString;

			var upgrader =
				DeployChanges.To
					.SqlDatabase(connectionString)
					.WithScriptsEmbeddedInAssembly(Assembly.GetExecutingAssembly())
					.LogToConsole()
					.Build();

			var result = upgrader.PerformUpgrade();

			if (!result.Successful)
			{
				Console.ForegroundColor = ConsoleColor.Red;
				Console.WriteLine(result.Error);
				Console.ResetColor();

				return -1;
			}

			Console.ForegroundColor = ConsoleColor.Green;
			Console.WriteLine("Success!");
			Console.ResetColor();
			return 0;
		}
	}
}

