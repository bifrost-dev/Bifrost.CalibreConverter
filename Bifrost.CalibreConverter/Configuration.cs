using System.IO;
using System.Xml;

namespace Bifrost.CalibreConverter
{
    public class Configuration
    {
        #region members

        private const string DefaultConverterPath = @"c:\Program Files\Calibre2\";
        private const bool DefaultOutputInSourceFolder = true;
        private const string DefaultOutputFormat = "epub";
        private const bool DefaultAutostartConversion = true;

        private string _converterPath;
        private bool _outputInSourceFolder;
        private string _outputFormat;
        private bool _autostartConversion;

        #endregion

        #region ctors

        internal Configuration()
        {
        }

        #endregion

        #region properties

        public string ConverterPath
        {
            get { return _converterPath; }
            set { _converterPath = value; }
        }

        public bool OutputInSourceFolder
        {
            get { return _outputInSourceFolder; }
            set { _outputInSourceFolder = value; }
        }

        public string OutputFormat
        {
            get { return _outputFormat; }
            set { _outputFormat = value; }
        }

        public bool AutostartConversion
        {
            get { return _autostartConversion; }
            set { _autostartConversion = value; }
        }

        private static string Filename
        {
            get { return Path.Combine(FolderProvider.Profile, Constants.ConfigurationFile); }
        }

        #endregion

        #region methods

        public void Load()
        {
            XmlDocument doc;
            string filename = Filename;
            if (!File.Exists(filename)) {
                doc = new XmlDocument();
                XmlElement rootElement = doc.CreateElement("FormatConverterConfiguration");
                doc.AppendChild(rootElement);

                CreateElement(rootElement, "ConverterPath", DefaultConverterPath);
                CreateElement(rootElement, "OutputInSourceFolder", DefaultOutputInSourceFolder.ToString());
                CreateElement(rootElement, "OutputFormat", DefaultOutputFormat);
                CreateElement(rootElement, "AutostartConversion", DefaultAutostartConversion.ToString());

                doc.Save(filename);
            }

            doc = new XmlDocument();
            doc.Load(Filename);
            XmlNode parent = doc.DocumentElement;
            _converterPath = ReadString(parent, "ConverterPath", DefaultConverterPath);
            _outputInSourceFolder = ReadBool(parent, "OutputInSourceFolder", DefaultOutputInSourceFolder);
            OutputFormat = ReadString(parent, "OutputFormat", DefaultOutputFormat);
            AutostartConversion = ReadBool(parent, "AutostartConversion", DefaultAutostartConversion);
        }

        private static string ReadString(XmlNode parent, string elementName, string defaultValue)
        {
            string value;
            XmlElement element = parent[elementName];
            if (element != null) {
                value = element.InnerText;
            } else {
                value = defaultValue;

                CreateElement(parent, elementName, defaultValue);
            }

            return value;
        }

        private static bool ReadBool(XmlNode parent, string elementName, bool defaultValue)
        {
            bool value;

            XmlElement element = parent[elementName];
            if (element != null) {
                value = bool.Parse(element.InnerText);
            } else {
                value = defaultValue;

                CreateElement(parent, elementName, defaultValue.ToString());
            }

            return value;
        }

        private static void CreateElement(XmlNode parent, string elementName, string value)
        {
            XmlElement newElement = parent.OwnerDocument.CreateElement(elementName);
            newElement.InnerText = value;
            parent.AppendChild(newElement);
        }

        #endregion
    }
}
