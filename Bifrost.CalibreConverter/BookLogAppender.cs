using System;
using System.Diagnostics.Contracts;

using Bifrost.SimpleLog;

namespace Bifrost.CalibreConverter
{
    /// <summary>Sends log entries written by <c>ExecutionContext</c> to assigned tab-page in <c>ApplicationLogForm</c>.</summary>
    internal class BookLogAppender
    {
        #region members

        private readonly LogTabPage _page;
        private readonly ExecutionContext _context;

        #endregion

        #region ctors

        internal BookLogAppender(LogTabPage page, ExecutionContext context)
        {
            Contract.Requires(page != null);
            Contract.Requires(context != null);

            _page = page;
            _context = context;
            _context.NewText += OnNewText;
            _context.BookChanged += OnBookChanged;
        }

        #endregion

        #region methods

        public void Close()
        {
            _context.NewText -= OnNewText;
            _context.BookChanged -= OnBookChanged;
        }

        private void OnNewText(object sender, StreamEventArgs e)
        {
            _page.Append(Level.Info, e.Text);
        }

        private void OnBookChanged(object sender, BookEventArgs e)
        {
            if (e.Book.Status == ConversionStatus.Finished) {
                _page.Append(Level.Warn, string.Format("=== BOOK FINISHED ==={0}{0}", Environment.NewLine));
            }
        }

        #endregion
    }
}
