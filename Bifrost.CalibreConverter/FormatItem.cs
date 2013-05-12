using System.Diagnostics.Contracts;

namespace Bifrost.CalibreConverter
{
    public sealed class FormatItem
    {
        #region members

        public static readonly FormatItem Cbz = new FormatItem("cbz", "Comic book zipped");
        public static readonly FormatItem Cbr = new FormatItem("cbr", "Comic book rarred");
        public static readonly FormatItem Cbc = new FormatItem("cbc", "Comic book");
        public static readonly FormatItem Chm = new FormatItem("chm", "Compiled html");
        public static readonly FormatItem Djvu = new FormatItem("djvu", "DjVu");
        public static readonly FormatItem Epub = new FormatItem("epub", "Electronic publication");
        public static readonly FormatItem Fb2 = new FormatItem("fb2", "Fiction book");
        public static readonly FormatItem Html = new FormatItem("html", "Hypertext");
        public static readonly FormatItem Htmlz = new FormatItem("htmlz", "Zipped hypertext");
        public static readonly FormatItem Lit = new FormatItem("lit", "Literature");
        public static readonly FormatItem Lrf = new FormatItem("lrf", "Broad band ebook");
        public static readonly FormatItem Mobi = new FormatItem("mobi", "Mobi pocket");
        public static readonly FormatItem Odt = new FormatItem("odt", "Open document text");
        public static readonly FormatItem Pdb = new FormatItem("pdb", "Plucker");
        public static readonly FormatItem Pdf = new FormatItem("pdf", "Portable document format");
        public static readonly FormatItem Prc = new FormatItem("prc", "Palm resource code");
        public static readonly FormatItem Pml = new FormatItem("pml", "Palm markup language");
        public static readonly FormatItem Rb = new FormatItem("rb", "Rocket ebook");
        public static readonly FormatItem Rtf = new FormatItem("rtf", "Rich text format");
        public static readonly FormatItem Snb = new FormatItem("snb", "Shanda bambook");
        public static readonly FormatItem Tcr = new FormatItem("tcr", "Text compression for reader");
        public static readonly FormatItem Txt = new FormatItem("txt", "Text file");
        public static readonly FormatItem Txtz = new FormatItem("txtz", "Zipped text file");

        public static readonly FormatItem Azw3 = new FormatItem("azw3", "Kindle format 8");
        public static readonly FormatItem Oeb = new FormatItem("oeb", "Open ebook");

        private readonly string _extension;
        private readonly string _name;

        #endregion

        #region ctors

        private FormatItem(string extension, string name)
        {
            Contract.Requires(!string.IsNullOrEmpty(extension));
            Contract.Requires(extension[0] != '.');
            Contract.Requires(!string.IsNullOrEmpty(name));

            _extension = extension;
            _name = name;
        }

        #endregion

        #region properties

        public string Name
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

                return _name;
            }
        }

        public string Extension
        {
            get
            {
                Contract.Ensures(!string.IsNullOrEmpty(Contract.Result<string>()));

                return _extension;
            }
        }

        #endregion

        #region methods

        public override string ToString()
        {
            return string.Format("{0} ({1})", _extension.ToUpper(), _name);
        }

        #endregion
    }
}
