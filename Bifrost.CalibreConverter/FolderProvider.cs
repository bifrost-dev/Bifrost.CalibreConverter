using System;
using System.IO;
using System.Reflection;

namespace Bifrost.CalibreConverter
{
    internal static class FolderProvider
    {
        private static string _application;
        private static string _profile;

        public static string Application
        {
            get { return _application ?? (_application = Path.GetDirectoryName(Assembly.GetEntryAssembly().Location)); }
        }

        public static string Profile
        {
            get
            {
                if (_profile == null) {
                    string filename = Path.Combine(Application, Constants.ProfilePortablePlaceholder);
                    if (File.Exists(filename)) {
                        _profile = Path.Combine(Application, Constants.PortableDataFolder);
                    } else {
                        string appDataFolder = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData);
                        _profile = Path.Combine(appDataFolder, Constants.AppDataFolder);
                    }

                    if (!Directory.Exists(_profile)) {
                        Directory.CreateDirectory(_profile);
                    }
                }
                return _profile;
            }
        }
    }
}
