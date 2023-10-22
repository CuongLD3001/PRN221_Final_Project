using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemLibrary.Views;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace CinemaSystemLibrary
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        private ServiceProvider serviceProvider;

        public App()
        {
            // Config for Dependency Injection (DI)
            ServiceCollection services = new ServiceCollection();
            ConfigureServices(services);
            serviceProvider = services.BuildServiceProvider();
        }

        private void ConfigureServices(ServiceCollection services)
        {
            services.AddSingleton<IBookingManagement, BookingVM>();
            services.AddSingleton<IFilmManagement, FilmVM>();
            services.AddSingleton<IRoomManagement, RoomVM>();
            services.AddSingleton<IShowManagement, ShowVM>();
            services.AddSingleton<Booking>();
            services.AddSingleton<Film>();
            services.AddSingleton<Room>();
            services.AddSingleton<Show>();
        }

        private void Application_Startup(object sender, StartupEventArgs e)
        {

        }

        public void OnStartUp(object sender, StartupEventArgs e)
        {
            var builder = new ConfigurationBuilder()
                        .SetBasePath(Directory.GetCurrentDirectory())
                        .AddJsonFile("appsettings.json", optional: true, reloadOnChange: true);
            IConfigurationRoot configuration = builder.Build();
            var filmManagement = serviceProvider.GetService<IFilmManagement>();

            // Khởi tạo FilmManagementForm với tham số IFilmManagement
            var startUpWindow = new FilmManagementForm(filmManagement);
            startUpWindow.Show();
        }
    }
}
