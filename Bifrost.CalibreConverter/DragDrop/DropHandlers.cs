using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.Windows.Forms;

namespace Bifrost.CalibreConverter.DragDrop
{
    internal class DropHandlers
    {
        #region members

        private readonly Dictionary<string, IDropHandler> _handlers;

        #endregion

        #region ctors

        internal DropHandlers()
        {
            _handlers = new Dictionary<string, IDropHandler>();
        }

        #endregion

        #region methods

        public IDropHandler this[string format]
        {
            get
            {
                Contract.Requires(!string.IsNullOrEmpty(format));

                IDropHandler handler;
                _handlers.TryGetValue(format, out handler);
                return handler;
            }
        }

        public void Add(IDropHandler handler)
        {
            Contract.Requires(handler != null);

            _handlers.Add(handler.Format, handler);
        }

        public IDropHandler Select(IDataObject data)
        {
            Contract.Requires(data != null);

            IDropHandler handler = null;
            foreach (string format in data.GetFormats()) {
                if (_handlers.TryGetValue(format, out handler)) {
                    break;
                }
            }

            return handler;
        }

        #endregion
    }
}