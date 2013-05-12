using System;
using System.Collections.Concurrent;
using System.Diagnostics.Contracts;
using System.Threading;
using Bifrost.SimpleLog;

namespace Bifrost.CalibreConverter
{
    /// <summary>Sends log entries written using <c>ILogger</c> to <c>ApplicationLogForm</c>.</summary>
    internal class AppLogAppender : IAppender
    {
        #region members

        public const string ID = "AppLog";

        private ConcurrentQueue<Tuple<Level, string>> _cache;
        private LogTabPage _page;
        private SemaphoreSlim _sync;

        #endregion

        #region ctors

        internal AppLogAppender()
        {
            _cache = new ConcurrentQueue<Tuple<Level, string>>();
        }

        #endregion

        #region properties

        public string Id
        {
            get { return ID; }
        }

        #endregion

        #region methods

        internal void SetConsumer(LogTabPage page)
        {
            Contract.Requires(page != null);

            if (_page == null) {
                _sync = new SemaphoreSlim(0);
                Tuple<Level, string> tuple;
                while (_cache.TryDequeue(out tuple)) {
                    page.Append(tuple.Item1, tuple.Item2);
                }
                _page = page;
                _cache = null;
                _sync.Release();
            }
        }

        public void Write(Level level, string message)
        {
            if (_sync != null) {
                _sync.Wait();
                _sync.Dispose();
                _sync = null;
                _page.Append(level, message);
            } else {
                if (_cache != null) {
                    _cache.Enqueue(new Tuple<Level, string>(level, message));
                } else {
                    if (!_page.Disposing && !_page.IsDisposed) {
                        _page.Append(level, message);
                    }
                }
            }
        }

        public void Close()
        {
        }

        #endregion
    }
}
