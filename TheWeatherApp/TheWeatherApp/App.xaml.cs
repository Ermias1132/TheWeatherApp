using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using TheWeatherApp.Helpers;
using TheWeatherApp.Repository;
using Xamarin.Forms;

namespace TheWeatherApp
{
    public partial class App : Application
    {
        static FavoritesRepository _database;

        public App()
        {
            InitializeComponent();

            MainPage = new NavigationPage( new MainPage());
        }

        public static FavoritesRepository Database => _database ?? (_database =
                                                          new FavoritesRepository(DependencyService.Get<IFileHelper>()
                                                              .GetLocalFilePath("Favorites.db3")));

        protected override void OnStart()
        {
            // Handle when your app starts
        }

        protected override void OnSleep()
        {
            // Handle when your app sleeps
        }

        protected override void OnResume()
        {
            // Handle when your app resumes
        }
    }
}
