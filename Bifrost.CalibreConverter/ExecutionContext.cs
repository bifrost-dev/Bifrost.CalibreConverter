using System;
using System.Collections.Concurrent;
using System.Diagnostics;
using System.Diagnostics.Contracts;
using System.IO;
using System.Threading;
using System.Threading.Tasks;

using Bifrost.SimpleLog;

namespace Bifrost.CalibreConverter
{
    public class ExecutionContext
    {
        #region members

        private static readonly ILogger _logger = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        public event EventHandler<StreamEventArgs> NewText;
        public event EventHandler<BookEventArgs> BookChanged;

        private readonly BlockingCollection<BookDescriptor> _books;
        private readonly ConcurrentDictionary<BookDescriptor, BookDescriptor> _removedBooks;
        private readonly Configuration _configuration;
        private readonly Task _task;
        private readonly IBookReceiver _receiver;

        private StreamReader _stream;

        #endregion

        #region ctors

        internal ExecutionContext(Configuration configuration, BlockingCollection<BookDescriptor> books, ConcurrentDictionary<BookDescriptor, BookDescriptor> removedBooks)
        {
            Contract.Requires(configuration != null);
            Contract.Requires(books != null);
            Contract.Requires(removedBooks != null);

            _configuration = configuration;
            _books = books;
            _removedBooks = removedBooks;
            _receiver = _configuration.AutostartConversion ? (IBookReceiver)new ImmediateReceiver(_books) : new TriggeredReceiver(_books);

            _task = Task.Factory.StartNew(TaskWorker, this);
        }

        #endregion

        #region properties

        private StreamReader Stream
        {
            get { return _stream; }
            set
            {
                _stream = value;
                Read();
            }
        }

        internal void StartConversion()
        {
            _receiver.Start();
        }

        #endregion

        #region methods

        private void Read()
        {
            try {
                while (!_stream.EndOfStream) {
                    string text = _stream.ReadLine();
                    if (text != null) {
                        FireNewText(text);
                    }
                }
            } catch (Exception e) {
                _logger.Error(e, "Error while reading from stream");
                // die in peace
            }
        }

        private void TaskWorker(object state)
        {
            bool skipped = false;
            ExecutionContext context = (ExecutionContext)state;
            while (true) {
                try {
                    if (!skipped) {
                        _logger.Info("waiting for book");
                    }

                    BookDescriptor book = _receiver.Get();

                    if (book == null) {
                        skipped = true;
                        continue;
                    }
                    BookDescriptor tmp;
                    if (_removedBooks.TryRemove(book, out tmp)) {
                        skipped = true;
                        _logger.Info("skiping the book '{0}'. (removed)", book.FileInfo.Name);
                        continue;
                    }
                    skipped = false;

                    _logger.Info("processing  book '{0}'", book.FileInfo.Name);

                    book.Status = ConversionStatus.Processing;
                    FireBookChanged(book);
                    StartConverterProcess(book, context);
                } catch (Exception e) {
                    _logger.Error(e, "Exception while preparing book conversion");
                }
            }
        }

        private void StartConverterProcess(BookDescriptor book, ExecutionContext context)
        {
            string converterBinary = Path.Combine(_configuration.ConverterPath, Constants.ConverterBinaryFile);
            string args = CreateProcessArguments(book);
            Process prc = new Process();
            prc.StartInfo = new ProcessStartInfo(converterBinary, args);
            prc.StartInfo.CreateNoWindow = true;
            prc.StartInfo.ErrorDialog = false;
            prc.StartInfo.RedirectStandardError = true;
            prc.StartInfo.RedirectStandardInput = true;
            prc.StartInfo.RedirectStandardOutput = true;
            prc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            prc.StartInfo.UseShellExecute = false; // must be 'false' if redirecting IO streams
            if (prc.Start()) {
                context.Stream = prc.StandardOutput;

                book.Status = ConversionStatus.Finished;
                FireBookChanged(book);

                _logger.Info("processing  book... finished '{0}' => '.{1}'", book.FileInfo.Name, book.TargetFormat.ToUpper());
            } else {
                _logger.Error("Failed to start process. book: '{0}'", book.FileInfo.Name);
            }
        }

        private string CreateProcessArguments(BookDescriptor book)
        {
            FileInfo fi = book.FileInfo;
            string outFilename = Path.ChangeExtension(fi.Name, book.TargetFormat);
            string outFolder;
            if (_configuration.OutputInSourceFolder) {
                outFolder = fi.DirectoryName;
            } else {
                outFolder = book.TargetFolder;
            }
            string args = string.Format(@"""{0}"" ""{1}\{2}""", fi.FullName, outFolder, outFilename);
            return args;
        }

        private void FireBookChanged(BookDescriptor book)
        {
            Contract.Requires(book != null);

            if (BookChanged != null) {
                BookChanged(this, new BookEventArgs(book));
            }
        }

        private void FireNewText(string text)
        {
            Contract.Requires(text != null);

            if (NewText != null) {
                NewText(this, new StreamEventArgs(text));
            }
        }

        #endregion

        #region inner types

        private interface IBookReceiver
        {
            BookDescriptor Get();
            void Start();
        }

        private abstract class BookReceiver : IBookReceiver
        {
            protected readonly BlockingCollection<BookDescriptor> _books;

            protected BookReceiver(BlockingCollection<BookDescriptor> books)
            {
                Contract.Requires(books != null);

                _books = books;
            }

            public abstract BookDescriptor Get();

            public virtual void Start()
            {
            }
        }

        private class ImmediateReceiver : BookReceiver
        {
            internal ImmediateReceiver(BlockingCollection<BookDescriptor> books)
                : base(books)
            {
            }

            public override BookDescriptor Get()
            {
                BookDescriptor book = _books.Take();
                return book;
            }
        }

        private class TriggeredReceiver : BookReceiver
        {
            private readonly ManualResetEventSlim _autostartLock;

            internal TriggeredReceiver(BlockingCollection<BookDescriptor> books)
                : base(books)
            {
                _autostartLock = new ManualResetEventSlim(false);
            }

            public override BookDescriptor Get()
            {
                _autostartLock.Wait();

                BookDescriptor book;
                if (!_books.TryTake(out book, 50)) {
                    _autostartLock.Reset();
                }

                return book;
            }

            public override void Start()
            {
                base.Start();

                _autostartLock.Set();
            }
        }

        #endregion
    }
}
