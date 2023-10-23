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
using CinemaSystemWPF.View;
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
            services.AddSingleton<IGenreManagement, GenreVM>();
            services.AddSingleton<Booking>();
            services.AddSingleton<Film>();
            services.AddSingleton<Room>();
            services.AddSingleton<BookingForm>();
            services.AddSingleton<FilmManagementForm>();
            services.AddSingleton<GenreManagementForm>();
            services.AddSingleton<RoomManagementForm>();
            services.AddSingleton<MenuForm>();
            services.AddSingleton<Show>();
        }

        private void OnStartUp(object sender, StartupEventArgs e)
        {
            // Replace "Order" with the correct view you want to show.
            //   var mainWindow = serviceProvider.GetService<GenreManagementForm>();
            // var mainWindow = serviceProvider.GetService<RoomManagementForm>();
            //    var mainWindow = serviceProvider.GetService<BookingForm>();
            // var mainWindow = serviceProvider.GetService<FilmManagementForm>();
            var mainWindow = serviceProvider.GetService<MenuForm>();
            mainWindow.Show();
        }
    }
}
