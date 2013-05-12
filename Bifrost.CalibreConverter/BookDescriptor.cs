using System.Diagnostics.Contracts;
using System.IO;

namespace Bifrost.CalibreConverter
{
    public class BookDescriptor
    {
        #region members

        private readonly FileInfo _fi;
        private readonly string _targetFolder;
        private readonly string _targetFormat;

        private ConversionStatus _status;

        #endregion

        #region ctors

        internal BookDescriptor(string filename, string targetFolder, string targetFormat)
        {
            Contract.Requires(File.Exists(filename));
            Contract.Requires(Directory.Exists(targetFolder));
            Contract.Requires(!string.IsNullOrWhiteSpace(targetFormat));

            _fi = new FileInfo(filename);
            _targetFolder = targetFolder;
            _targetFormat = targetFormat;

            _status = ConversionStatus.New;
        }

        #endregion

        #region properties

        public FileInfo FileInfo
        {
            get { return _fi; }
        }

        public ConversionStatus Status
        {
            get { return _status; }

            // TODO: change this so the property doesn't have setter
            internal set { _status = value; }
        }

        public string TargetFolder
        {
            get { return _targetFolder; }
        }

        public string TargetFormat
        {
            get { return _targetFormat; }
        }

        #endregion

        #region methods


        #endregion
    }
}
