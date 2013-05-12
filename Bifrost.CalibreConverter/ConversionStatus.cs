using System.Diagnostics;

namespace Bifrost.CalibreConverter
{
    [DebuggerDisplay("{DebuggerDisplay,nq}")]
    public sealed class ConversionStatus
    {
        #region members

        public static readonly ConversionStatus New = new ConversionStatus("New");
        public static readonly ConversionStatus Processing = new ConversionStatus("Processing");
        public static readonly ConversionStatus Finished = new ConversionStatus("Finished");

        private static int idSeed;

        private readonly int _id;
        private readonly string _text;

        #endregion

        #region ctors

        private ConversionStatus(string text)
        {
            _id = ++idSeed;
            _text = text;
        }

        #endregion

        #region properties

        public int Id
        {
            get { return _id; }
        }

        public string Text
        {
            get { return _text; }
        }

        private string DebuggerDisplay
        {
            get { return string.Format("{0}: {1}", GetType().Name, _text); }
        }

        #endregion

        #region methods

        public override string ToString()
        {
            return string.Format("{0}: {1}", GetType().Name, _text);
        }

        #endregion
    }
}
