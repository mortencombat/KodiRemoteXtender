using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Reflection;
using System.Diagnostics;
using System.Net;

namespace KodiRemoteXtender
{
    public partial class fSettings : Form
    {
        private AppContext currentContext;
        public AppContext CurrentContext
        { get { return currentContext; } set { currentContext = value; } }

        delegate void ClearLogEntriesCallback();
        delegate void AddLogEntryCallback(ListViewItem Entry);
        delegate void MoveCursorCallback(Point Position);

        public void MoveCursor(Point Position)
        {
            if (this.InvokeRequired)
            {
                MoveCursorCallback d = new MoveCursorCallback(MoveCursor);
                this.Invoke(d, new object[] { Position });
            }
            else
            {
                this.Cursor = new Cursor(Cursor.Current.Handle);
                Cursor.Position = Position;
            }
        }

        public void ClearLogEntries()
        {
            if (this.lstLog.InvokeRequired)
            {
                ClearLogEntriesCallback d = new ClearLogEntriesCallback(ClearLogEntries);
                this.Invoke(d);
            }
            else
            {
                lock (lstLog) { lstLog.Items.Clear(); }
            }
        }

        public void AddLogEntry(ListViewItem Entry)
        {
            if (this.lstLog.InvokeRequired)
            {
                AddLogEntryCallback d = new AddLogEntryCallback(AddLogEntry);
                this.Invoke(d, new object[] { Entry });
            }
            else
            {
                lock (lstLog) 
                { lstLog.Items.Add(Entry);  }
            }
        }

        public fSettings()
        {
            InitializeComponent();
        }

        public bool Running
        {
            get { return miEnabled.Checked; }
            set { miEnabled.Checked = value; }
        }

        private void btnClearLog_Click(object sender, EventArgs e)
        {
            currentContext.Log.Clear();
        }

        private void icoNotify_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            this.Show();
        }

        public void UpdateStatus(bool Started, string Status)
        {
            updRemotePort.Enabled = !Started;
            tslStatus.Text = Status;
        }

        private void fSettings_Load(object sender, EventArgs e)
        {
            chkEnabled.DataBindings.Add(new Binding("Checked", currentContext.Settings, "Enabled", false, DataSourceUpdateMode.OnPropertyChanged));
            chkRunOnStartup.DataBindings.Add(new Binding("Checked", currentContext.Settings, "RunOnStartup", false, DataSourceUpdateMode.OnPropertyChanged));
            updKodiPort.DataBindings.Add(new Binding("Value", currentContext.Settings, "KodiPort", false, DataSourceUpdateMode.OnPropertyChanged));
            txtKodiHostname.DataBindings.Add(new Binding("Text", currentContext.Settings, "KodiHostname", false, DataSourceUpdateMode.OnPropertyChanged));
            updRemotePort.DataBindings.Add(new Binding("Value", currentContext.Settings, "RemotePort", false, DataSourceUpdateMode.OnPropertyChanged));
            this.DataBindings.Add(new Binding("Location", currentContext.Settings, "FormLocation", true, DataSourceUpdateMode.OnPropertyChanged));
            this.Size = currentContext.Settings.FormSize;

            string ipaddr = "", host = Dns.GetHostName();
            
            foreach(IPAddress ip in Dns.GetHostAddresses(host))
            {
                if (ip.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                {
                    ipaddr = ip.ToString();
                    break;
                }
            }
            
            lblIPAddress.Text = host + ((ipaddr.Length > 0) ? " (" + ipaddr + ")" : "");

            string version = Application.ProductVersion;
            if (System.Deployment.Application.ApplicationDeployment.IsNetworkDeployed) 
            {
                System.Deployment.Application.ApplicationDeployment ad = System.Deployment.Application.ApplicationDeployment.CurrentDeployment;
                version = ad.CurrentVersion.ToString();
            }
            tslVersion.Text = "Version " + version;

            chkLogResponseModifications.DataBindings.Add(new Binding("Checked", currentContext.Settings, "LogResponseModifications", false, DataSourceUpdateMode.OnPropertyChanged));
            
            chkMPCHC_Enabled.DataBindings.Add(new Binding("Checked", currentContext.Settings, "MPCHC_Enabled", false, DataSourceUpdateMode.OnPropertyChanged));
            txtMPCHC_WebInterfaceURL.DataBindings.Add(new Binding("Text", currentContext.Settings, "MPCHC_WebInterfaceURL", false, DataSourceUpdateMode.OnPropertyChanged));
            lblMPCHC_Status.DataBindings.Add(new Binding("Text", currentContext.GetTarget("MPC-HC"), "Status", false, DataSourceUpdateMode.OnPropertyChanged));

            updRemotePort.Enabled = !currentContext.Settings.Enabled;
            
            fSettings_ResizeEnd(null, null);
            miEnabled.Checked = chkEnabled.Checked;
        }

        private void fSettings_FormClosing(object sender, FormClosingEventArgs e)
        {
            currentContext.SaveSettings();
            e.Cancel = true;
            this.Hide();
        }

        private void miQuit_Click(object sender, EventArgs e)
        {
            currentContext.Exit();
        }

        private void miSettings_Click(object sender, EventArgs e)
        {
            this.Show();
        }

        private void miEnabled_Click(object sender, EventArgs e)
        {
            currentContext.Settings.Enabled = miEnabled.Checked;
        }

        private void chkMPCHC_Enabled_CheckedChanged(object sender, EventArgs e)
        {
            if (currentContext != null) currentContext.SetTargetStatus("MPC-HC", chkMPCHC_Enabled.Checked);
        }

        private void miEnabled_CheckedChanged(object sender, EventArgs e)
        {
            chkEnabled.Checked = miEnabled.Checked;
            if (currentContext != null) currentContext.Enabled = chkEnabled.Checked;
        }

        private void chkEnabled_Click(object sender, EventArgs e)
        {
            currentContext.Enabled = chkEnabled.Checked;
        }

        private void fSettings_ResizeEnd(object sender, EventArgs e)
        {
            this.columnHeader3.Width = this.lstLog.Width - this.columnHeader1.Width - this.columnHeader2.Width - 30;
        }

        private void chkLogResponseModifications_CheckedChanged(object sender, EventArgs e)
        {
            if (currentContext == null) return;

            currentContext.Settings.LogResponseModifications = chkLogResponseModifications.Checked;
            fSettings_VisibleChanged(null, null);
        }

        private void chkEnabled_CheckedChanged(object sender, EventArgs e)
        {
            miEnabled.Checked = chkEnabled.Checked;
        }

        private void fSettings_VisibleChanged(object sender, EventArgs e)
        {
            if (currentContext == null) return;

            if (this.Visible)
            {
                currentContext.Log.View = this;
                if (currentContext.Settings.LogResponseModifications)
                { currentContext.Log.ListEventTypes = Log.EventType.ForwardToExternalPlayer | Log.EventType.ModifyResponse; }
                else
                { currentContext.Log.ListEventTypes = Log.EventType.ForwardToExternalPlayer; }
                currentContext.Log.ResetView();
            }
            else
            {
                currentContext.Log.View = null;
            }

        }

        private void tslSupport_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://www.adeptweb.dk/remotextender/support.html");
        }

        private void chkRunOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            Microsoft.Win32.RegistryKey key = Microsoft.Win32.Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
            if (chkRunOnStartup.Checked)
            {
                key.SetValue(Application.ProductName, System.Reflection.Assembly.GetExecutingAssembly().Location);
            }
            else
            {
                key.DeleteValue(Application.ProductName, false);
            }

            currentContext.Settings.Save();
        }

        private void txtKodiHostname_Validating(object sender, System.ComponentModel.CancelEventArgs e)
        {
            string host = txtKodiHostname.Text.Trim();
            if (host.Length == 0)
            {
                host = "localhost";
            }
            txtKodiHostname.Text = host;
        }
    }
}
