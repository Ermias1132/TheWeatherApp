using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TheWeatherApp.Helpers;
using TheWeatherApp.Models;
using TheWeatherApp.Repository;
using TheWeatherApp.Services;
using TheWeatherApp.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Maps;

namespace TheWeatherApp
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            
            BindingContext = new WeatherViewModel(Navigation);
        }

        //private async void SearchButton_Clicked(object sender, EventArgs e)
        //{
            
        //}

        //private async void FavButton_Clicked(object sender, EventArgs e)
        //{
           
            
        //}
        
    }
}
