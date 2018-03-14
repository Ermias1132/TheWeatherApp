using System;
using System.Collections.Generic;
using System.Text;
using System.IO;
using TheWeatherApp.iOS.Helpers;
using TheWeatherApp.Helpers;
using Xamarin.Forms;

[assembly: Dependency(typeof(FileHelper))]
namespace TheWeatherApp.iOS.Helpers
{
    public class FileHelper : IFileHelper
    {
        public string GetLocalFilePath(string filename)
        {
            string docFolder = Environment.GetFolderPath(Environment.SpecialFolder.Personal);
            string libFolder = Path.Combine(docFolder, "..", "Library", "Database");
            //string libFolder = Path.Combine(docFolder, "..", "Library");

            if (!Directory.Exists(libFolder))
            {
                Directory.CreateDirectory(libFolder);
            }

            return Path.Combine(libFolder, filename);
        }
    }
}

