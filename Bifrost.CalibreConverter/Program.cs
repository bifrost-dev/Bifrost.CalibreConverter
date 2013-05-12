using System;
using System.IO;
using System.Text;
using System.Windows.Forms;

using Bifrost.SimpleLog;
using Bifrost.SimpleLog.Appenders;

namespace Bifrost.CalibreConverter
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            try {
                StartApplication();
            } catch (Exception e) {
                HandleException(e);
            }
        }

        private static void StartApplication()
        {
            LogManager logman = LogManager.Instance;
            logman.LoadConfiguration(Path.Combine(FolderProvider.Profile, Constants.LoggerFile));
            FileAppender appender = (FileAppender)logman.Appenders[FileAppender.ID];
            if (appender != null) {
                string filename = Path.GetFileName(appender.Filename);
                appender.Filename = Path.Combine(FolderProvider.Profile, filename);
            }
            logman.Appenders.Add(new AppLogAppender());

            Configuration configuration = new Configuration();
            configuration.Load();

            using (Engine engine = new Engine(configuration)) {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new MainForm(engine, configuration));
            }

            logman.Close();
        }

        private static void HandleException(Exception e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat("{0}: {1}{2}{3}{2}", e.GetType().Name, e.Message, Environment.NewLine, e.StackTrace);

            Exception inner = e.InnerException;
            while (inner != null) {
                sb.AppendFormat("INNER EXCEPTION: {0}: {1}{2}{3}{2}", inner.GetType().Name, inner.Message, Environment.NewLine, inner.StackTrace);
                inner = inner.InnerException;
            }
            string message = sb.ToString();
            Console.WriteLine(message);
            try {
                string filename = Path.Combine(FolderProvider.Profile, Constants.UnhandledErrorFile);
                StringBuilder sb2 = new StringBuilder();
                sb2.AppendFormat("=== BEGIN MESSAGE ====== {0} ==={1}", DateTime.Now.ToString("yyyy-MM-dd HH:mm:ss,ffff"), Environment.NewLine);
                sb2.Append(message);
                sb2.AppendFormat("=== END MESSAGE ====================================={0}", Environment.NewLine);
                File.AppendAllText(filename, sb2.ToString());
            } catch {
                // die in peace
            }

            try {
                MessageBox.Show(message, "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            } catch {
                // die in peace
            }
        }
    }
}
