using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Diagnostics.Contracts;
using System.IO;

using Bifrost.SimpleLog;
using Microsoft.Win32;

namespace Bifrost.CalibreConverter
{
    public class Engine : IDisposable
    {
        #region members

        public event EventHandler<BookEventArgs> BookAdded;
        public event EventHandler<BookEventArgs> BookRemoved;

        private static readonly ILogger _logger = Logger.GetLogger(typeof(Engine));

        private readonly Configuration _configuration;
        private readonly ReadOnlyCollection<ExecutionContext> _contexts;
        private readonly BlockingCollection<BookDescriptor> _books;
        private readonly ConcurrentDictionary<BookDescriptor, BookDescriptor> _removedBooks;
        private readonly Formats _inputFormats;
        private readonly Formats _outputFormats;

        #endregion

        #region ctors

        internal Engine(Configuration configuration)
        {
            Contract.Requires(configuration != null);

            _logger.EnterFunction();

            _configuration = configuration;

            _books = new BlockingCollection<BookDescriptor>();
            _removedBooks = new ConcurrentDictionary<BookDescriptor, BookDescriptor>();

            _inputFormats = new InputFormats();
            _outputFormats = new OutputFormats();

            List<ExecutionContext> contexts = new List<ExecutionContext>();
            for (int i = 0; i < Environment.ProcessorCount; i++) {
                contexts.Add(new ExecutionContext(_configuration, _books, _removedBooks));
            }
            _contexts = contexts.AsReadOnly();

            ValidateConverterPath();

            _logger.LeaveFunction();
        }

        public void Dispose()
        {
        }

        #endregion

        #region properties

        public Formats InputFormats
        {
            get { return _inputFormats; }
        }

        public Formats OutputFormats
        {
            get { return _outputFormats; }
        }

        public IEnumerable<ExecutionContext> Contexts
        {
            get { return _contexts; }
        }

        private bool IsReady
        {
            get
            {
                string converterBinary = Path.Combine(_configuration.ConverterPath, Constants.ConverterBinaryFile);
                bool isReady = File.Exists(converterBinary);
                return isReady;
            }
        }

        #endregion

        #region methods

        public void Add(string filename, string targetFolder, string targetFormat)
        {
            Contract.Requires(File.Exists(filename));
            Contract.Requires(Directory.Exists(targetFolder));
            Contract.Requires(!string.IsNullOrWhiteSpace(targetFormat));

            if (!IsReady) {
                _logger.Error("Cannot convert file");
                return;
            }

            BookDescriptor book = new BookDescriptor(filename, targetFolder, targetFormat);
            FireBookAdded(book);
            _books.Add(book);
        }

        public void Remove(BookDescriptor book)
        {
            Contract.Requires(book != null);

            _removedBooks.TryAdd(book, book);

            FireBookRemoved(book);
        }

        internal void StartConversion()
        {
            foreach (ExecutionContext context in _contexts) {
                context.StartConversion();
            }
        }

        private void FireBookAdded(BookDescriptor book)
        {
            Contract.Requires(book != null);

            if (BookAdded != null) {
                BookAdded(this, new BookEventArgs(book));
            }
        }

        private void FireBookRemoved(BookDescriptor book)
        {
            Contract.Requires(book != null);

            if (BookRemoved != null) {
                BookRemoved(this, new BookEventArgs(book));
            }
        }

        private void ValidateConverterPath()
        {
            string filename = Path.Combine(_configuration.ConverterPath, Constants.ConverterBinaryFile);
            if (!File.Exists(filename)) {
                _logger.Warn("Calibre not found at location specified in configuration: '{0}'", _configuration.ConverterPath);
                string calibreInstallPath = (string)Registry.GetValue(Constants.CalibreRegistryKey, "InstallPath", null);
                if (calibreInstallPath != null) {
                    filename = Path.Combine(calibreInstallPath, Constants.ConverterBinaryFile);
                    if (File.Exists(filename)) {
                        _configuration.ConverterPath = calibreInstallPath;
                    } else {
                        _logger.Error("Calibre not installed. Cannot convert files.");
                    }
                }
            }
        }

        #endregion
    }
}
