using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using CinemaSystemLibrary.DataAccess;
using CinemaSystemLibrary.ViewModel;
using CinemaSystemLibrary.Views;
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
            services.AddSingleton<Booking>();
            services.AddSingleton<Film>();
            services.AddSingleton<Room>();
        }

        public void OnStartup(object sender, StartupEventArgs e)
        {
            // Replace "Order" with the correct view you want to show.
            var mainWindow = serviceProvider.GetService<FilmManagementForm>();
            mainWindow.Show();
        }
    }
}
