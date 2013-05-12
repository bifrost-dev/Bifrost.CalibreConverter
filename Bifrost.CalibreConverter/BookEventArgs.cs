using System;
using System.Diagnostics.Contracts;

namespace Bifrost.CalibreConverter
{
    public class BookEventArgs : EventArgs
    {
        #region members

        private readonly BookDescriptor _book;

        #endregion

        #region ctors

        internal BookEventArgs(BookDescriptor book)
        {
            Contract.Requires(book != null);

            _book = book;
        }

        #endregion

        #region properties

        public BookDescriptor Book
        {
            get { return _book; }
        }
    }

        #endregion
}
