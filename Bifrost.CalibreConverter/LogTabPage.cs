using System;
using System.Drawing;
using System.Windows.Forms;

using Bifrost.SimpleLog;

namespace Bifrost.CalibreConverter
{
    public partial class LogTabPage : TabPage
    {
        #region members

        private readonly RichTextBox _richTextLogEntries;

        #endregion

        #region ctors

        public LogTabPage()
        {
            InitializeComponent();

            _richTextLogEntries = new RichTextBox();
            _richTextLogEntries.Dock = DockStyle.Fill;
            _richTextLogEntries.Font = new Font("DejaVu Sans Mono", 8f, FontStyle.Regular);
            _richTextLogEntries.ReadOnly = true;
            _richTextLogEntries.WordWrap = false;
            _richTextLogEntries.Parent = this;
        }

        #endregion

        #region methods

        public void Append(Level level, string message)
        {
            if (_richTextLogEntries.InvokeRequired) {
                _richTextLogEntries.Invoke(new MethodInvoker(() => Append(level, message)), level, message);
            } else {
                if (_richTextLogEntries.Disposing && _richTextLogEntries.IsDisposed) {
                    return;
                }
                try {
                    int start = _richTextLogEntries.TextLength;
                    _richTextLogEntries.AppendText(message);
                    _richTextLogEntries.AppendText(Environment.NewLine);

                    _richTextLogEntries.Select(start, _richTextLogEntries.TextLength - start);
                    _richTextLogEntries.SelectionColor = GetLogEntryColor(level);
                } finally {
                    _richTextLogEntries.SelectionLength = 0;
                }
            }
        }

        private static Color GetLogEntryColor(Level level)
        {
            Color clr;
            switch (level) {
            case Level.Enter:
            case Level.Leave:
                clr = Color.DimGray;
                break;
            case Level.Trace:
                clr = Color.DimGray;
                break;
            case Level.Debug:
                clr = Color.Black;
                break;
            case Level.Info:
                clr = Color.DarkOrchid;
                break;
            case Level.Warn:
                clr = Color.MediumVioletRed;
                break;
            case Level.Error:
                clr = Color.Red;
                break;
            case Level.Fatal:
                clr = Color.DarkGreen;
                break;
            default:
                clr = Color.DarkOrange;
                break;
            }

            return clr;
        }

        #endregion
    }
}
