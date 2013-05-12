namespace Bifrost.CalibreConverter
{
    public class InputFormats : Formats
    {
        internal InputFormats()
        {
            // order by calibre prefrence:
            Add(FormatItem.Lit);
            Add(FormatItem.Mobi);
            Add(FormatItem.Epub);
            Add(FormatItem.Fb2);
            Add(FormatItem.Html);
            Add(FormatItem.Prc);
            Add(FormatItem.Rtf);
            Add(FormatItem.Pdb);
            Add(FormatItem.Txt);
            Add(FormatItem.Pdf);

            // the rest:
            Add(FormatItem.Cbz);
            Add(FormatItem.Cbr);
            Add(FormatItem.Cbc);
            Add(FormatItem.Chm);
            Add(FormatItem.Djvu);
            Add(FormatItem.Htmlz);
            Add(FormatItem.Txtz);
            Add(FormatItem.Lrf);
            Add(FormatItem.Odt);
            Add(FormatItem.Pml);
            Add(FormatItem.Rb);
            Add(FormatItem.Snb);
            Add(FormatItem.Tcr);
        }
    }
}
