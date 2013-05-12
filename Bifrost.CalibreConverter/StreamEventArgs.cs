using System;
using System.Diagnostics.Contracts;

namespace Bifrost.CalibreConverter
{
    public class StreamEventArgs : EventArgs
    {
        #region members

        private readonly string _text;

        #endregion

        #region ctors

        internal StreamEventArgs(string text)
        {
            Contract.Requires(text != null);

            _text = text;
        }

        #endregion

        #region properties

        public string Text
        {
            get
            {
                Contract.Ensures(Contract.Result<string>() != null);

                return _text;
            }
        }

        #endregion
    }
}
