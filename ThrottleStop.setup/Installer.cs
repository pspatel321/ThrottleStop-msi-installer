using System.Collections;
using System.ComponentModel;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using System.Text;

namespace ThrottleStop.setup
{
    [RunInstaller(true)]
    public class Installer : System.Configuration.Install.Installer
    {
        protected override void OnCommitted(IDictionary savedState)
        {
            base.OnCommitted(savedState);

            if (Context.Parameters["rwdrv"] == "1")
            {
                string outpath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RwDrv.sys");
                File.WriteAllBytes(outpath, Properties.Resources.RwDrv);
            }
            if (Context.Parameters["start_auto"] == "1")
            {
                // Prepare temporary file describing a task (xml) in Task Scheduler from resource template
                string xml = Properties.Resources.task;
                string exe = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product + ".exe"); ;
                xml = xml.Replace("{Author}", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyCompanyAttribute>().Company);
                xml = xml.Replace("{Description}", Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyDescriptionAttribute>().Description);
                xml = xml.Replace("{URI}", "\"" + "\\" + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product + "\"");
                xml = xml.Replace("{Command}", "\"" + exe + "\"");
                xml = xml.Replace("{Arguments}", "");
                var file = Path.GetTempFileName();
                File.WriteAllText(file, xml, Encoding.Unicode);

                // Add startup task
                DoCmd("schtasks", string.Format("/Create /TN {0} /F /XML {1}", "\"" + "\\" + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product + "\"", file), true);
                File.Delete(file);

                // Start now
                DoCmd("schtasks", string.Format("/Run /TN {0}", "\"" + "\\" + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product + "\""));
            }
        }
        protected override void OnBeforeUninstall(IDictionary savedState)
        {
            base.OnBeforeUninstall(savedState);

            // Delete RwDrv.sys file
            string outpath = Path.Combine(Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location), "RwDrv.sys");
            File.Delete(outpath);

            // Delete task
            DoCmd("schtasks", string.Format("/Delete /TN {0} /F", "\"" + "\\" + Assembly.GetExecutingAssembly().GetCustomAttribute<AssemblyProductAttribute>().Product + "\""), true);
        }
        public static int DoCmd(string cmd, string args, bool asAdmin = false)
        {
            var proc = new Process();
            proc.StartInfo.FileName = cmd;
            proc.StartInfo.Arguments = args;
            proc.StartInfo.Verb = asAdmin ? "runas" : "";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
            proc.WaitForExit();
            return proc.ExitCode;
        }
    }
}
