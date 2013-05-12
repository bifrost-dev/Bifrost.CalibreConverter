using System;
using System.Collections.Concurrent;
using System.Collections.Generic;
using System.Diagnostics.Contracts;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Windows.Forms;

using Bifrost.CalibreConverter.DragDrop;
using Bifrost.SimpleLog;

namespace Bifrost.CalibreConverter
{
    public partial class MainForm : Form
    {
        #region members

        private static readonly ILogger _logger = Logger.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

        private readonly Configuration _configuration;
        private readonly Engine _engine;
        private readonly ConcurrentDictionary<BookDescriptor, ListViewItem> _books;
        private readonly List<BookLogAppender> _bookLogAppenders;
        private readonly DropHandlers _dndHandlers;
        private readonly ApplicationLogForm _appLogForm;

        #endregion

        #region ctors

        public MainForm(Engine engine, Configuration configuration)
        {
            Contract.Requires(engine != null);
            Contract.Requires(configuration != null);

            _logger.EnterFunction();

            _engine = engine;
            _configuration = configuration;

            _books = new ConcurrentDictionary<BookDescriptor, ListViewItem>();

            _dndHandlers = new DropHandlers();
            _dndHandlers.Add(new FileDropHandler());

            _appLogForm = new ApplicationLogForm();
            LogTabPage appLogPage = _appLogForm.AddLogPage("Application");
            AppLogAppender appLogAppender = LogManager.Instance.Appenders[AppLogAppender.ID] as AppLogAppender;
            if (appLogAppender != null) {
                appLogAppender.SetConsumer(appLogPage);
            }

            _bookLogAppenders = new List<BookLogAppender>();
            foreach (ExecutionContext context in _engine.Contexts) {
                LogTabPage page = _appLogForm.AddLogPage("Worker");
                _bookLogAppenders.Add(new BookLogAppender(page, context));

                context.BookChanged += OnBookChanged;
            }

            _engine.BookAdded += OnBookAdded;
            _engine.BookRemoved += OnBookRemoved;

            InitializeComponent();
            InitializeComponents();

            _toolStripButtonConvert.Enabled = !_configuration.AutostartConversion;

            _logger.LeaveFunction();
        }

        #endregion

        #region properties

        private static string DefaultOutputFolder
        {
            get { return FolderProvider.Profile; }
        }

        private FormatItem TargetFormat
        {
            get
            {
                FormatItem targetFormat;

                if (InvokeRequired) {
                    targetFormat = (FormatItem)Invoke(new Func<FormatItem>(() => TargetFormat));
                } else {
                    targetFormat = (FormatItem)_toolStripComboOutFormat.SelectedItem;
                }

                return targetFormat;
            }
        }

        #endregion

        #region methods

        protected override void OnClosed(EventArgs e)
        {
            base.OnClosed(e);

            _engine.BookAdded -= OnBookAdded;
            _engine.BookRemoved -= OnBookRemoved;

            foreach (ExecutionContext context in _engine.Contexts) {
                context.BookChanged -= OnBookChanged;
            }

            foreach (BookLogAppender appender in _bookLogAppenders) {
                appender.Close();
            }
        }

        private void InitializeComponents()
        {
            int statusColWidth = 70;

            ColumnHeader hdrFilename = new ColumnHeader();
            hdrFilename.Width = _listViewInputFiles.Width - statusColWidth - SystemInformation.VerticalScrollBarWidth - 5; // 5: magic value to fit v-sbar
            hdrFilename.Text = @"Filename";
            _listViewInputFiles.Columns.Add(hdrFilename);

            ColumnHeader hdrStatus = new ColumnHeader();
            hdrStatus.Width = statusColWidth;
            hdrStatus.Text = @"Status";
            _listViewInputFiles.Columns.Add(hdrStatus);

            FormatItem selection = null;
            string outFormat = _configuration.OutputFormat;
            foreach (FormatItem item in _engine.OutputFormats) {
                _toolStripComboOutFormat.Items.Add(item);
                if (string.Equals(item.Extension, outFormat, StringComparison.InvariantCultureIgnoreCase)) {
                    selection = item;
                }
            }
            _toolStripComboOutFormat.SelectedItem = selection;

            _checkBoxOutIsInSource.Checked = _configuration.OutputInSourceFolder;
            CheckBoxOutIsInSourceCheckedChanged(_checkBoxOutIsInSource, EventArgs.Empty);

            Resources.ImageResources images = Resources.Instance.Images;
            _toolStripButtonAdd.Image = images.Add;
            _toolStripButtonRemove.Image = images.Remove;
            _toolStripButtonRemoveFinished.Image = images.RemoveFinished;
            _toolStripButtonLog.Image = images.ShowLog;
            _toolStripButtonConvert.Image = images.Convert;

            Icon = Resources.Instance.Icons.Application;
        }

        private void AddFiles(IEnumerable<string> filenames)
        {
            string targetFolder = _textBoxOutputFolder.Text;
            string targetFormat = TargetFormat.Extension;

            foreach (string filename in filenames) {
                if (!File.Exists(filename)) {
                    _logger.Warn("File doesn't exist: '{0}'", filename);
                    continue;
                }
                string extension = Path.GetExtension(filename);
                if (!_engine.InputFormats.Contains(extension)) {
                    _logger.Warn("File format not supported: '{0}'", filename);
                    continue;
                }
                _engine.Add(filename, targetFolder, targetFormat);
            }
        }

        private void RemoveSelectedItems()
        {
            _listViewInputFiles.BeginUpdate();

            foreach (ListViewItem item in _listViewInputFiles.SelectedItems) {
                _engine.Remove((BookDescriptor)item.Tag);
            }

            _listViewInputFiles.EndUpdate();
        }

        private void RemoveFinishedItems()
        {
            _listViewInputFiles.BeginUpdate();

            foreach (ListViewItem item in _listViewInputFiles.Items) {
                BookDescriptor book = (BookDescriptor)item.Tag;
                if (book.Status == ConversionStatus.Finished) {
                    _listViewInputFiles.Items.Remove(item);
                }
            }

            _listViewInputFiles.EndUpdate();
        }

        private void UpdateSubItemText(ListViewItem.ListViewSubItem subItem, string text)
        {
            Contract.Requires(subItem != null);

            if (InvokeRequired) {
                Invoke(new MethodInvoker(() => UpdateSubItemText(subItem, text)));
            } else {
                subItem.Text = text;
            }
        }

        private void ShowAddFilesDialog()
        {
            OpenFileDialog frm = new OpenFileDialog();
            frm.Multiselect = true;

            int filterIndex = 1; // 1: FilterIndex property is 1-based
            bool mobiFound = false;
            StringBuilder sb = new StringBuilder();
            StringBuilder sb2 = new StringBuilder();
            foreach (FormatItem fmt in _engine.InputFormats) {
                sb.AppendFormat("{0} (*.{1})|*.{1}|", fmt.Name, fmt.Extension);
                sb2.AppendFormat("*.{0};", fmt.Extension);
                if (fmt.Extension != FormatItem.Mobi.Extension) {
                    if (!mobiFound) {
                        ++filterIndex;
                    }
                } else {
                    mobiFound = true;
                }
            }
            sb.AppendFormat("All Calibre Formats|*.{0}|", sb2);
            sb.AppendFormat("All Files (*.*)|*.*");
            frm.Filter = sb.ToString();

            frm.AddExtension = true;
            frm.DefaultExt = Constants.DefaultInputExtension;
            frm.FilterIndex = filterIndex;

            if (frm.ShowDialog(this) == DialogResult.OK) {
                AddFiles(frm.FileNames);
            }
        }

        private void ShowLogDialog()
        {
            if (_appLogForm.InvokeRequired) {
                _appLogForm.Invoke(new MethodInvoker(ShowLogDialog));
            } else {
                if (_appLogForm.Visible) {
                    _appLogForm.BringToFront();
                } else {
                    _appLogForm.Show(this);
                }
            }
        }

        #region event-handlers

        private void OnButtonSelectOutputFolderClick(object sender, EventArgs e)
        {
            FolderBrowserDialog frm = new FolderBrowserDialog();
            frm.ShowNewFolderButton = true;
            if (frm.ShowDialog(this) == DialogResult.OK) {
                _textBoxOutputFolder.Text = frm.SelectedPath;
            }
        }

        private void CheckBoxOutIsInSourceCheckedChanged(object sender, EventArgs e)
        {
            _configuration.OutputInSourceFolder = _checkBoxOutIsInSource.Checked;
            if (_checkBoxOutIsInSource.Checked) {
                _textBoxOutputFolder.Enabled = false;
                _buttonSelectOutputFolder.Enabled = false;
            } else {
                _textBoxOutputFolder.Enabled = true;
                _buttonSelectOutputFolder.Enabled = true;
            }
            if (!Directory.Exists(_textBoxOutputFolder.Text)) {
                _textBoxOutputFolder.Text = DefaultOutputFolder;
            }
        }

        private void ListViewInputFilesDragEnter(object sender, DragEventArgs e)
        {
            IDropHandler handler = _dndHandlers.Select(e.Data);
            e.Effect = handler != null ? DragDropEffects.Copy : DragDropEffects.None;
        }

        private void ListViewInputFilesDragDrop(object sender, DragEventArgs e)
        {
            IDropHandler handler = _dndHandlers.Select(e.Data);
            if (handler != null) {
                handler.Handle(e.Data, this);
            }
        }

        private void ListViewInputFilesKeyDown(object sender, KeyEventArgs e)
        {
            bool handled = false;
            switch (e.KeyCode) {
            case Keys.Delete:
                if (e.Modifiers == Keys.None) {
                    RemoveSelectedItems();
                    handled = true;
                }
                break;
            case Keys.A:
                if (e.Modifiers == Keys.Control) {
                    _listViewInputFiles.BeginUpdate();
                    foreach (ListViewItem item in _listViewInputFiles.Items) {
                        item.Selected = true;
                    }
                    _listViewInputFiles.EndUpdate();
                    handled = true;
                }
                break;
            }

            e.Handled = handled;
        }

        private void OnBookAdded(object sender, BookEventArgs e)
        {
            Contract.Requires(sender != null);
            Contract.Requires(e != null);

            BookDescriptor book = e.Book;
            ListViewItem item = new ListViewItem(book.FileInfo.Name);
            item.Tag = book;
            ListViewItem.ListViewSubItem status = new ListViewItem.ListViewSubItem();
            status.Name = "Status";
            status.Text = book.Status.Text;
            item.SubItems.Add(status);

            _books.TryAdd(book, item);

            _listViewInputFiles.Items.Add(item);
        }

        private void OnBookRemoved(object sender, BookEventArgs e)
        {
            BookDescriptor removedBook = e.Book;
            foreach (ListViewItem item in _listViewInputFiles.Items) {
                BookDescriptor book = (BookDescriptor)item.Tag;
                if (book == removedBook) {
                    _listViewInputFiles.Items.Remove(item);
                    break;
                }
            }
        }

        private void OnBookChanged(object sender, BookEventArgs e)
        {
            Contract.Requires(sender != null);
            Contract.Requires(e != null);

            ListViewItem item;

            BookDescriptor book = e.Book;
            if (_books.TryGetValue(book, out item)) {
                ListViewItem.ListViewSubItem subItem = item.SubItems["Status"];
                UpdateSubItemText(subItem, book.Status.Text);
            }
        }

        #region tool-strip event-handlers

        private void OnToolStripButtonAddClick(object sender, EventArgs e)
        {
            ShowAddFilesDialog();
        }

        private void OnToolStripButtonRemoveClick(object sender, EventArgs e)
        {
            RemoveSelectedItems();
        }

        private void OnToolStripButtonRemoveFinishedClick(object sender, EventArgs e)
        {
            RemoveFinishedItems();
        }

        private void OnToolStripButtonConvertClick(object sender, EventArgs e)
        {
            _engine.StartConversion();
        }

        private void OnToolStripButtonLogClick(object sender, EventArgs e)
        {
            ShowLogDialog();
        }

        #endregion

        #endregion

        #endregion

        #region inner types

        private class FileDropHandler : IDropHandler
        {
            public string Format
            {
                get { return "FileDrop"; }
            }

            public bool CanHandle(IDataObject data)
            {
                bool handles = data.GetFormats().Contains(Format);
                return handles;
            }

            public bool Handle(IDataObject data, MainForm frm)
            {
                object obj = data.GetData(Format);
                string[] files = obj as string[];

                bool handled;
                if (files != null) {
                    List<string> target = new List<string>();
                    AddFiles(files, target, frm);
                    frm.AddFiles(target);
                    handled = true;
                } else {
                    handled = false;
                }

                return handled;
            }

            private static void AddFiles(IEnumerable<string> source, List<string> target, MainForm frm)
            {
                foreach (string filename in source) {
                    FileAttributes attr = File.GetAttributes(filename);
                    if ((attr & FileAttributes.Directory) == FileAttributes.Directory) {
                        string[] files2 = Directory.GetFiles(filename);
                        AddFiles(files2, target, frm);
                    } else {
                        string extension = Path.GetExtension(filename);
                        extension = extension != null && extension.StartsWith(".") ? extension.Substring(1) : extension;
                        if (frm._engine.InputFormats.Contains(extension)) {
                            target.Add(filename);
                        }
                    }
                }
            }
        }

        #endregion
    }
}
