namespace Bifrost.CalibreConverter
{
    public class OutputFormats : Formats
    {
        internal OutputFormats()
        {
            // order by calibre prefrence:
            Add(FormatItem.Lit);
            Add(FormatItem.Mobi);
            Add(FormatItem.Epub);
            Add(FormatItem.Azw3);
            Add(FormatItem.Fb2);
            Add(FormatItem.Rtf);
            Add(FormatItem.Pdb);
            Add(FormatItem.Txt);
            Add(FormatItem.Pdf);

            // the rest :
            Add(FormatItem.Htmlz);
            Add(FormatItem.Txtz);
            Add(FormatItem.Oeb);
            Add(FormatItem.Lrf);
            Add(FormatItem.Pml);
            Add(FormatItem.Rb);
            Add(FormatItem.Snb);
            Add(FormatItem.Tcr);
        }
    }
}
