using System.Configuration;
using System.Data;
using System.Windows;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using LuxuryCarRental.Data;
using LuxuryCarRental.Repositories.Interfaces;
using LuxuryCarRental.Repositories.Implementations;
using LuxuryCarRental.ViewModels;




namespace LuxuryCarRental
{
    public partial class App : Application
    {
        private ServiceProvider? _provider;

        protected override void OnStartup(StartupEventArgs e)
        {
            var services = new ServiceCollection();
            services.AddDbContext<AppDbContext>(opts =>
                opts.UseSqlite("Data Source=LuxuryRental.db"));
            services.AddScoped<IUnitOfWork, EfUnitOfWork>();
            services.AddTransient<MainViewModel>();
            services.AddTransient<MainWindow>(sp =>
            {
                var vm = sp.GetRequiredService<MainViewModel>();
                return new MainWindow { DataContext = vm };
            });

            var provider = services.BuildServiceProvider();

            // <— HERE: create a scope to run migrations
            using (var scope = provider.CreateScope())
            {
                var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
                db.Database.Migrate();     // applies all migrations
            }

            // now the file in bin\Debug\net7.0-windows has your schema
            var mainWindow = provider.GetRequiredService<MainWindow>();
            mainWindow.Show();
        }
    }
}