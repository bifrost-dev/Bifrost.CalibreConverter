using System.Windows.Forms;

using Bifrost.SimpleLog;

namespace Bifrost.CalibreConverter
{
    public partial class ApplicationLogForm : Form
    {
        private static readonly ILogger _logger = Logger.GetLogger(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType);

        #region ctors

        public ApplicationLogForm()
        {
            _logger.EnterFunction();

            InitializeComponent();

            Handle.GetType(); // force creating handle on current thread
            Icon = Resources.Instance.Icons.Log;

            _logger.LeaveFunction();
        }

        #endregion

        #region methods

        protected override void OnFormClosing(FormClosingEventArgs e)
        {
            switch (e.CloseReason) {
            case CloseReason.UserClosing:
                e.Cancel = true;
                Hide();
                break;
            }

            base.OnFormClosing(e);
        }

        public LogTabPage AddLogPage(string text)
        {
            LogTabPage page = new LogTabPage();
            page.Text = text;
            _tabControlLogContainer.TabPages.Add(page);

            return page;
        }

        private void OnButtonCloseClick(object sender, System.EventArgs e)
        {
            DialogResult = DialogResult.OK;
            Hide();
        }

        #endregion
    }
}
