using System.Collections.Generic;
using System.ComponentModel;
using System.Windows.Input;
using Xamarin.Forms;
using TheWeatherApp.Helpers;
using TheWeatherApp.Models;
using TheWeatherApp.Services;

namespace TheWeatherApp.ViewModels
{
    public class WeatherViewModel : INotifyPropertyChanged
    {
        public string ZipCode { get; set; }
        public bool IsRunning { get; set; }
        public bool IsVisible { get; set; }

        public event PropertyChangedEventHandler PropertyChanged;

        public ICommand WeatherCommand { get; private set; }

        public ICommand FavoriteCommand { get; private set; }
        private readonly INavigation _navigation;

        public WeatherViewModel(INavigation navigation)
        {
            _navigation = navigation;
            //ZipCode = "30303";
            WeatherCommand = new Command<string>(GetWeatherInfo);
            FavoriteCommand = new Command<string>(SaveFavorite);

            //var savedZipCodes = await App.Database.GetFavoritesAsync();

            //if (savedZipCodes != null)
            //{
            //  load zip codes from saved db
            //}
        }

        private async void SaveFavorite(string zipCode)
        {
            
            if (zipCode.IsEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a valid zip code.", "OK");
                return;
            }

            List<Favorites> zipCodes = new List<Favorites>();
            foreach (var value in zipCode.Split(','))
            {
                if (value.Trim().IsValidZipCode())
                {
                    zipCodes.Add(new Favorites { ZipCode = value.Trim() });
                }
            }

            if (zipCodes.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a valid zip code.", "OK");
                return;
            }

            IsRunning = true;
            IsVisible = true;

            OnPropertyChanged("IsRunning");
            OnPropertyChanged("IsVisible");

            var ret = await App.Database.SaveFavoriteAsync(zipCodes);

            if (ret)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Saved to favorite list.", "OK");
                //return;
            }

            IsRunning = false;
            IsVisible = false;

            OnPropertyChanged("IsRunning");
            OnPropertyChanged("IsVisible");
        }

        private async void GetWeatherInfo(string zipCode)
        {
            var service = new WeatherService();
            
            var zipCodeText = ZipCode;

            if (zipCodeText.IsEmpty())
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a valid zip code.", "OK");
                return;
            }

            List<string> zipCodes = new List<string>();
            foreach (var value in zipCodeText.Split(','))
            {
                if (value.Trim().IsValidZipCode())
                {
                    zipCodes.Add(value.Trim());
                }
            }

            if (zipCodes.Count == 0)
            {
                await Application.Current.MainPage.DisplayAlert("Alert", "Please enter a valid zip code.", "OK");
                return;
            }

            IsRunning = true;
            IsVisible = true;
            OnPropertyChanged("IsRunning");
            OnPropertyChanged("IsVisible"); 

           var weatherInfoList = await service.GetWeatherInfo(zipCodes);

            var curLocation = await Utilities.GetUserLocation();

            if (curLocation.Success)
            {
                var curLocationWeatherInfo = await service.GetWeatherInfo(curLocation.Latitude, curLocation.Longitude);
                curLocationWeatherInfo.ZipCode = "Current loc.";
                weatherInfoList.Add(curLocationWeatherInfo);
            }

            IsRunning = false;
            IsVisible = false;

            OnPropertyChanged("IsRunning");
            OnPropertyChanged("IsVisible");

            await _navigation.PushAsync(new DetailPage(weatherInfoList));
        }

        protected virtual void OnPropertyChanged(string propertyName)
        {
            var changed = PropertyChanged;
            if (changed != null)
            {
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
            }
        }
    }
}
