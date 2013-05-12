using System.Windows.Forms;

namespace Bifrost.CalibreConverter.DragDrop
{
    public interface IDropHandler
    {
        string Format { get; }

        bool CanHandle(IDataObject data);

        bool Handle(IDataObject data, MainForm frm);

    }
}
