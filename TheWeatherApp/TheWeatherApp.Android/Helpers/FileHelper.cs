using Android.OS;
using TheWeatherApp.Droid.Helpers;
using Xamarin.Forms;
using System.IO;
using System;
using TheWeatherApp.Helpers;

[assembly: Dependency(typeof(FileHelper))]
namespace TheWeatherApp.Droid.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string path = System.Environment.GetFolderPath(System.Environment.SpecialFolder.Personal);
            return Path.Combine(path, filename);
        }
    }
}