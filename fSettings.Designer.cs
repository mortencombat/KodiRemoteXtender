namespace KodiRemoteXtender
{
    partial class fSettings
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(fSettings));
            this.updRemotePort = new System.Windows.Forms.NumericUpDown();
            this.chkEnabled = new System.Windows.Forms.CheckBox();
            this.tabMPCHC = new System.Windows.Forms.TabPage();
            this.txtMPCHC_WebInterfaceURL = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.lblMPCHC_Status = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.chkMPCHC_Enabled = new System.Windows.Forms.CheckBox();
            this.tabControl1 = new System.Windows.Forms.TabControl();
            this.tabLog = new System.Windows.Forms.TabPage();
            this.chkLogResponseModifications = new System.Windows.Forms.CheckBox();
            this.btnClearLog = new System.Windows.Forms.Button();
            this.lstLog = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader2 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.columnHeader3 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabPDVD = new System.Windows.Forms.TabPage();
            this.txtPDVD_Passcode = new System.Windows.Forms.MaskedTextBox();
            this.label8 = new System.Windows.Forms.Label();
            this.txtPDVD_Host = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.lblPDVD_Status = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.chkPDVD_Enabled = new System.Windows.Forms.CheckBox();
            this.tabTMT = new System.Windows.Forms.TabPage();
            this.label6 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.chkTMT_Enabled = new System.Windows.Forms.CheckBox();
            this.ssStatus = new System.Windows.Forms.StatusStrip();
            this.tslStatus = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslSupport = new System.Windows.Forms.ToolStripStatusLabel();
            this.tslVersion = new System.Windows.Forms.ToolStripStatusLabel();
            this.label5 = new System.Windows.Forms.Label();
            this.updKodiPort = new System.Windows.Forms.NumericUpDown();
            this.icoNotify = new System.Windows.Forms.NotifyIcon(this.components);
            this.mnuNotify = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.miEnabled = new System.Windows.Forms.ToolStripMenuItem();
            this.miSettings = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.miQuit = new System.Windows.Forms.ToolStripMenuItem();
            this.chkRunOnStartup = new System.Windows.Forms.CheckBox();
            this.tabGeneral = new System.Windows.Forms.TabPage();
            this.txtKodiHostname = new System.Windows.Forms.TextBox();
            this.label10 = new System.Windows.Forms.Label();
            this.groupBox1 = new System.Windows.Forms.GroupBox();
            this.label11 = new System.Windows.Forms.Label();
            this.groupBox2 = new System.Windows.Forms.GroupBox();
            this.groupBox3 = new System.Windows.Forms.GroupBox();
            this.label12 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.lblIPAddress = new System.Windows.Forms.Label();
            this.label4 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.updRemotePort)).BeginInit();
            this.tabMPCHC.SuspendLayout();
            this.tabControl1.SuspendLayout();
            this.tabLog.SuspendLayout();
            this.tabPDVD.SuspendLayout();
            this.tabTMT.SuspendLayout();
            this.ssStatus.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updKodiPort)).BeginInit();
            this.mnuNotify.SuspendLayout();
            this.tabGeneral.SuspendLayout();
            this.groupBox1.SuspendLayout();
            this.groupBox2.SuspendLayout();
            this.groupBox3.SuspendLayout();
            this.SuspendLayout();
            // 
            // updRemotePort
            // 
            this.updRemotePort.Location = new System.Drawing.Point(137, 55);
            this.updRemotePort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.updRemotePort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updRemotePort.Name = "updRemotePort";
            this.updRemotePort.Size = new System.Drawing.Size(66, 23);
            this.updRemotePort.TabIndex = 0;
            this.updRemotePort.Value = new decimal(new int[] {
            8081,
            0,
            0,
            0});
            // 
            // chkEnabled
            // 
            this.chkEnabled.AutoSize = true;
            this.chkEnabled.Checked = true;
            this.chkEnabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkEnabled.Location = new System.Drawing.Point(9, 22);
            this.chkEnabled.Name = "chkEnabled";
            this.chkEnabled.Size = new System.Drawing.Size(68, 19);
            this.chkEnabled.TabIndex = 2;
            this.chkEnabled.Text = "Enabled";
            this.chkEnabled.UseVisualStyleBackColor = true;
            this.chkEnabled.CheckedChanged += new System.EventHandler(this.chkEnabled_CheckedChanged);
            this.chkEnabled.Click += new System.EventHandler(this.chkEnabled_Click);
            // 
            // tabMPCHC
            // 
            this.tabMPCHC.Controls.Add(this.txtMPCHC_WebInterfaceURL);
            this.tabMPCHC.Controls.Add(this.label3);
            this.tabMPCHC.Controls.Add(this.lblMPCHC_Status);
            this.tabMPCHC.Controls.Add(this.label1);
            this.tabMPCHC.Controls.Add(this.chkMPCHC_Enabled);
            this.tabMPCHC.Location = new System.Drawing.Point(4, 24);
            this.tabMPCHC.Name = "tabMPCHC";
            this.tabMPCHC.Padding = new System.Windows.Forms.Padding(3);
            this.tabMPCHC.Size = new System.Drawing.Size(632, 354);
            this.tabMPCHC.TabIndex = 0;
            this.tabMPCHC.Text = "MPC-HC";
            this.tabMPCHC.UseVisualStyleBackColor = true;
            // 
            // txtMPCHC_WebInterfaceURL
            // 
            this.txtMPCHC_WebInterfaceURL.Location = new System.Drawing.Point(137, 50);
            this.txtMPCHC_WebInterfaceURL.Name = "txtMPCHC_WebInterfaceURL";
            this.txtMPCHC_WebInterfaceURL.Size = new System.Drawing.Size(329, 23);
            this.txtMPCHC_WebInterfaceURL.TabIndex = 4;
            this.txtMPCHC_WebInterfaceURL.Text = "http://localhost:13579";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 53);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(107, 15);
            this.label3.TabIndex = 3;
            this.label3.Text = "Web Interface URL:";
            // 
            // lblMPCHC_Status
            // 
            this.lblMPCHC_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblMPCHC_Status.AutoEllipsis = true;
            this.lblMPCHC_Status.Location = new System.Drawing.Point(134, 29);
            this.lblMPCHC_Status.Name = "lblMPCHC_Status";
            this.lblMPCHC_Status.Size = new System.Drawing.Size(492, 18);
            this.lblMPCHC_Status.TabIndex = 2;
            this.lblMPCHC_Status.Text = "<statustext>";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(6, 29);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(42, 15);
            this.label1.TabIndex = 1;
            this.label1.Text = "Status:";
            // 
            // chkMPCHC_Enabled
            // 
            this.chkMPCHC_Enabled.AutoSize = true;
            this.chkMPCHC_Enabled.Checked = true;
            this.chkMPCHC_Enabled.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkMPCHC_Enabled.Location = new System.Drawing.Point(7, 7);
            this.chkMPCHC_Enabled.Name = "chkMPCHC_Enabled";
            this.chkMPCHC_Enabled.Size = new System.Drawing.Size(68, 19);
            this.chkMPCHC_Enabled.TabIndex = 0;
            this.chkMPCHC_Enabled.Text = "Enabled";
            this.chkMPCHC_Enabled.UseVisualStyleBackColor = true;
            this.chkMPCHC_Enabled.CheckedChanged += new System.EventHandler(this.chkMPCHC_Enabled_CheckedChanged);
            // 
            // tabControl1
            // 
            this.tabControl1.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.tabControl1.Controls.Add(this.tabGeneral);
            this.tabControl1.Controls.Add(this.tabMPCHC);
            this.tabControl1.Controls.Add(this.tabPDVD);
            this.tabControl1.Controls.Add(this.tabTMT);
            this.tabControl1.Controls.Add(this.tabLog);
            this.tabControl1.Location = new System.Drawing.Point(14, 12);
            this.tabControl1.Name = "tabControl1";
            this.tabControl1.SelectedIndex = 0;
            this.tabControl1.Size = new System.Drawing.Size(608, 363);
            this.tabControl1.TabIndex = 1;
            // 
            // tabLog
            // 
            this.tabLog.Controls.Add(this.chkLogResponseModifications);
            this.tabLog.Controls.Add(this.btnClearLog);
            this.tabLog.Controls.Add(this.lstLog);
            this.tabLog.Location = new System.Drawing.Point(4, 24);
            this.tabLog.Name = "tabLog";
            this.tabLog.Size = new System.Drawing.Size(632, 305);
            this.tabLog.TabIndex = 1;
            this.tabLog.Text = "Log";
            this.tabLog.UseVisualStyleBackColor = true;
            // 
            // chkLogResponseModifications
            // 
            this.chkLogResponseModifications.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Left)));
            this.chkLogResponseModifications.AutoSize = true;
            this.chkLogResponseModifications.Location = new System.Drawing.Point(3, 274);
            this.chkLogResponseModifications.Name = "chkLogResponseModifications";
            this.chkLogResponseModifications.Size = new System.Drawing.Size(194, 19);
            this.chkLogResponseModifications.TabIndex = 2;
            this.chkLogResponseModifications.Text = "Include Response Modifications";
            this.chkLogResponseModifications.UseVisualStyleBackColor = true;
            this.chkLogResponseModifications.CheckedChanged += new System.EventHandler(this.chkLogResponseModifications_CheckedChanged);
            // 
            // btnClearLog
            // 
            this.btnClearLog.Anchor = ((System.Windows.Forms.AnchorStyles)((System.Windows.Forms.AnchorStyles.Bottom | System.Windows.Forms.AnchorStyles.Right)));
            this.btnClearLog.Location = new System.Drawing.Point(543, 274);
            this.btnClearLog.Name = "btnClearLog";
            this.btnClearLog.Size = new System.Drawing.Size(86, 28);
            this.btnClearLog.TabIndex = 1;
            this.btnClearLog.Text = "Clear All";
            this.btnClearLog.UseVisualStyleBackColor = true;
            this.btnClearLog.Click += new System.EventHandler(this.btnClearLog_Click);
            // 
            // lstLog
            // 
            this.lstLog.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom) 
            | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lstLog.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2,
            this.columnHeader3});
            this.lstLog.Location = new System.Drawing.Point(3, 3);
            this.lstLog.Name = "lstLog";
            this.lstLog.Size = new System.Drawing.Size(626, 265);
            this.lstLog.TabIndex = 0;
            this.lstLog.UseCompatibleStateImageBehavior = false;
            this.lstLog.View = System.Windows.Forms.View.Details;
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Date/Time";
            this.columnHeader1.Width = 120;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Event";
            this.columnHeader2.Width = 120;
            // 
            // columnHeader3
            // 
            this.columnHeader3.Text = "Action";
            this.columnHeader3.Width = 350;
            // 
            // tabPDVD
            // 
            this.tabPDVD.Controls.Add(this.txtPDVD_Passcode);
            this.tabPDVD.Controls.Add(this.label8);
            this.tabPDVD.Controls.Add(this.txtPDVD_Host);
            this.tabPDVD.Controls.Add(this.label2);
            this.tabPDVD.Controls.Add(this.lblPDVD_Status);
            this.tabPDVD.Controls.Add(this.label7);
            this.tabPDVD.Controls.Add(this.chkPDVD_Enabled);
            this.tabPDVD.Location = new System.Drawing.Point(4, 24);
            this.tabPDVD.Name = "tabPDVD";
            this.tabPDVD.Size = new System.Drawing.Size(632, 305);
            this.tabPDVD.TabIndex = 2;
            this.tabPDVD.Text = "PowerDVD";
            this.tabPDVD.UseVisualStyleBackColor = true;
            // 
            // txtPDVD_Passcode
            // 
            this.txtPDVD_Passcode.Location = new System.Drawing.Point(137, 79);
            this.txtPDVD_Passcode.Mask = "0000";
            this.txtPDVD_Passcode.Name = "txtPDVD_Passcode";
            this.txtPDVD_Passcode.Size = new System.Drawing.Size(44, 23);
            this.txtPDVD_Passcode.TabIndex = 12;
            this.txtPDVD_Passcode.Text = "8346";
            this.txtPDVD_Passcode.Visible = false;
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Location = new System.Drawing.Point(6, 82);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(56, 15);
            this.label8.TabIndex = 10;
            this.label8.Text = "Passcode";
            this.label8.Visible = false;
            // 
            // txtPDVD_Host
            // 
            this.txtPDVD_Host.Location = new System.Drawing.Point(137, 50);
            this.txtPDVD_Host.Name = "txtPDVD_Host";
            this.txtPDVD_Host.Size = new System.Drawing.Size(329, 23);
            this.txtPDVD_Host.TabIndex = 9;
            this.txtPDVD_Host.Text = "localhost";
            this.txtPDVD_Host.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(6, 53);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(80, 15);
            this.label2.TabIndex = 8;
            this.label2.Text = "Hostname/IP:";
            this.label2.Visible = false;
            // 
            // lblPDVD_Status
            // 
            this.lblPDVD_Status.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.lblPDVD_Status.AutoEllipsis = true;
            this.lblPDVD_Status.Location = new System.Drawing.Point(134, 29);
            this.lblPDVD_Status.Name = "lblPDVD_Status";
            this.lblPDVD_Status.Size = new System.Drawing.Size(492, 18);
            this.lblPDVD_Status.TabIndex = 7;
            this.lblPDVD_Status.Text = "Not yet supported.";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Location = new System.Drawing.Point(6, 29);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(42, 15);
            this.label7.TabIndex = 6;
            this.label7.Text = "Status:";
            // 
            // chkPDVD_Enabled
            // 
            this.chkPDVD_Enabled.AutoSize = true;
            this.chkPDVD_Enabled.Enabled = false;
            this.chkPDVD_Enabled.Location = new System.Drawing.Point(7, 7);
            this.chkPDVD_Enabled.Name = "chkPDVD_Enabled";
            this.chkPDVD_Enabled.Size = new System.Drawing.Size(68, 19);
            this.chkPDVD_Enabled.TabIndex = 5;
            this.chkPDVD_Enabled.Text = "Enabled";
            this.chkPDVD_Enabled.UseVisualStyleBackColor = true;
            // 
            // tabTMT
            // 
            this.tabTMT.Controls.Add(this.label6);
            this.tabTMT.Controls.Add(this.label9);
            this.tabTMT.Controls.Add(this.chkTMT_Enabled);
            this.tabTMT.Location = new System.Drawing.Point(4, 24);
            this.tabTMT.Name = "tabTMT";
            this.tabTMT.Size = new System.Drawing.Size(632, 305);
            this.tabTMT.TabIndex = 3;
            this.tabTMT.Text = "TotalMedia Theatre";
            this.tabTMT.UseVisualStyleBackColor = true;
            // 
            // label6
            // 
            this.label6.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.label6.AutoEllipsis = true;
            this.label6.Location = new System.Drawing.Point(134, 29);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(492, 18);
            this.label6.TabIndex = 9;
            this.label6.Text = "Not yet supported.";
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Location = new System.Drawing.Point(6, 29);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(42, 15);
            this.label9.TabIndex = 8;
            this.label9.Text = "Status:";
            // 
            // chkTMT_Enabled
            // 
            this.chkTMT_Enabled.AutoSize = true;
            this.chkTMT_Enabled.Enabled = false;
            this.chkTMT_Enabled.Location = new System.Drawing.Point(7, 7);
            this.chkTMT_Enabled.Name = "chkTMT_Enabled";
            this.chkTMT_Enabled.Size = new System.Drawing.Size(68, 19);
            this.chkTMT_Enabled.TabIndex = 6;
            this.chkTMT_Enabled.Text = "Enabled";
            this.chkTMT_Enabled.UseVisualStyleBackColor = true;
            // 
            // ssStatus
            // 
            this.ssStatus.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.tslStatus,
            this.tslSupport,
            this.tslVersion});
            this.ssStatus.Location = new System.Drawing.Point(0, 389);
            this.ssStatus.Name = "ssStatus";
            this.ssStatus.Padding = new System.Windows.Forms.Padding(1, 0, 16, 0);
            this.ssStatus.Size = new System.Drawing.Size(634, 22);
            this.ssStatus.TabIndex = 3;
            // 
            // tslStatus
            // 
            this.tslStatus.Name = "tslStatus";
            this.tslStatus.Size = new System.Drawing.Size(474, 17);
            this.tslStatus.Spring = true;
            this.tslStatus.Text = "<Status>";
            this.tslStatus.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tslSupport
            // 
            this.tslSupport.AutoSize = false;
            this.tslSupport.IsLink = true;
            this.tslSupport.Name = "tslSupport";
            this.tslSupport.Size = new System.Drawing.Size(70, 17);
            this.tslSupport.Text = "Support";
            this.tslSupport.Click += new System.EventHandler(this.tslSupport_Click);
            // 
            // tslVersion
            // 
            this.tslVersion.Name = "tslVersion";
            this.tslVersion.Size = new System.Drawing.Size(73, 17);
            this.tslVersion.Text = "Version 0.1.0";
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(6, 29);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(125, 15);
            this.label5.TabIndex = 6;
            this.label5.Text = "Hostname/IP-address:";
            // 
            // updKodiPort
            // 
            this.updKodiPort.Location = new System.Drawing.Point(137, 55);
            this.updKodiPort.Maximum = new decimal(new int[] {
            65536,
            0,
            0,
            0});
            this.updKodiPort.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this.updKodiPort.Name = "updKodiPort";
            this.updKodiPort.Size = new System.Drawing.Size(66, 23);
            this.updKodiPort.TabIndex = 5;
            this.updKodiPort.Value = new decimal(new int[] {
            8080,
            0,
            0,
            0});
            // 
            // icoNotify
            // 
            this.icoNotify.ContextMenuStrip = this.mnuNotify;
            this.icoNotify.Icon = ((System.Drawing.Icon)(resources.GetObject("icoNotify.Icon")));
            this.icoNotify.Text = "Kodi RemoteXtender";
            this.icoNotify.Visible = true;
            this.icoNotify.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.icoNotify_MouseDoubleClick);
            // 
            // mnuNotify
            // 
            this.mnuNotify.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.miEnabled,
            this.miSettings,
            this.toolStripSeparator1,
            this.miQuit});
            this.mnuNotify.Name = "mnuNotify";
            this.mnuNotify.Size = new System.Drawing.Size(126, 76);
            // 
            // miEnabled
            // 
            this.miEnabled.CheckOnClick = true;
            this.miEnabled.Name = "miEnabled";
            this.miEnabled.Size = new System.Drawing.Size(125, 22);
            this.miEnabled.Text = "Enabled";
            this.miEnabled.CheckedChanged += new System.EventHandler(this.miEnabled_CheckedChanged);
            this.miEnabled.Click += new System.EventHandler(this.miEnabled_Click);
            // 
            // miSettings
            // 
            this.miSettings.Name = "miSettings";
            this.miSettings.Size = new System.Drawing.Size(125, 22);
            this.miSettings.Text = "Settings...";
            this.miSettings.Click += new System.EventHandler(this.miSettings_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            this.toolStripSeparator1.Size = new System.Drawing.Size(122, 6);
            // 
            // miQuit
            // 
            this.miQuit.Name = "miQuit";
            this.miQuit.Size = new System.Drawing.Size(125, 22);
            this.miQuit.Text = "Quit";
            this.miQuit.Click += new System.EventHandler(this.miQuit_Click);
            // 
            // chkRunOnStartup
            // 
            this.chkRunOnStartup.AutoSize = true;
            this.chkRunOnStartup.Checked = true;
            this.chkRunOnStartup.CheckState = System.Windows.Forms.CheckState.Checked;
            this.chkRunOnStartup.Location = new System.Drawing.Point(9, 47);
            this.chkRunOnStartup.Name = "chkRunOnStartup";
            this.chkRunOnStartup.Size = new System.Drawing.Size(128, 19);
            this.chkRunOnStartup.TabIndex = 7;
            this.chkRunOnStartup.Text = "Start with Windows";
            this.chkRunOnStartup.UseVisualStyleBackColor = true;
            this.chkRunOnStartup.CheckedChanged += new System.EventHandler(this.chkRunOnStartup_CheckedChanged);
            // 
            // tabGeneral
            // 
            this.tabGeneral.Controls.Add(this.groupBox3);
            this.tabGeneral.Controls.Add(this.groupBox2);
            this.tabGeneral.Controls.Add(this.groupBox1);
            this.tabGeneral.Location = new System.Drawing.Point(4, 24);
            this.tabGeneral.Name = "tabGeneral";
            this.tabGeneral.Size = new System.Drawing.Size(600, 335);
            this.tabGeneral.TabIndex = 4;
            this.tabGeneral.Text = "General";
            this.tabGeneral.UseVisualStyleBackColor = true;
            // 
            // txtKodiHostname
            // 
            this.txtKodiHostname.Location = new System.Drawing.Point(137, 26);
            this.txtKodiHostname.Name = "txtKodiHostname";
            this.txtKodiHostname.Size = new System.Drawing.Size(163, 23);
            this.txtKodiHostname.TabIndex = 9;
            this.txtKodiHostname.Text = "localhost";
            this.txtKodiHostname.Validating += new System.ComponentModel.CancelEventHandler(this.txtKodiHostname_Validating);
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Location = new System.Drawing.Point(6, 57);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(32, 15);
            this.label10.TabIndex = 10;
            this.label10.Text = "Port:";
            // 
            // groupBox1
            // 
            this.groupBox1.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox1.Controls.Add(this.label4);
            this.groupBox1.Controls.Add(this.label11);
            this.groupBox1.Controls.Add(this.label5);
            this.groupBox1.Controls.Add(this.label10);
            this.groupBox1.Controls.Add(this.txtKodiHostname);
            this.groupBox1.Controls.Add(this.updKodiPort);
            this.groupBox1.Location = new System.Drawing.Point(7, 185);
            this.groupBox1.Name = "groupBox1";
            this.groupBox1.Size = new System.Drawing.Size(584, 88);
            this.groupBox1.TabIndex = 11;
            this.groupBox1.TabStop = false;
            this.groupBox1.Text = "Kodi";
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Location = new System.Drawing.Point(209, 57);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(368, 15);
            this.label11.TabIndex = 11;
            this.label11.Text = "(port number as set under Settings -> Services -> Webserver in Kodi)";
            // 
            // groupBox2
            // 
            this.groupBox2.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox2.Controls.Add(this.chkEnabled);
            this.groupBox2.Controls.Add(this.chkRunOnStartup);
            this.groupBox2.Location = new System.Drawing.Point(7, 10);
            this.groupBox2.Name = "groupBox2";
            this.groupBox2.Size = new System.Drawing.Size(584, 75);
            this.groupBox2.TabIndex = 12;
            this.groupBox2.TabStop = false;
            this.groupBox2.Text = "RemoteXtender";
            // 
            // groupBox3
            // 
            this.groupBox3.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
            | System.Windows.Forms.AnchorStyles.Right)));
            this.groupBox3.Controls.Add(this.lblIPAddress);
            this.groupBox3.Controls.Add(this.label12);
            this.groupBox3.Controls.Add(this.label13);
            this.groupBox3.Controls.Add(this.label14);
            this.groupBox3.Controls.Add(this.updRemotePort);
            this.groupBox3.Location = new System.Drawing.Point(7, 91);
            this.groupBox3.Name = "groupBox3";
            this.groupBox3.Size = new System.Drawing.Size(584, 88);
            this.groupBox3.TabIndex = 13;
            this.groupBox3.TabStop = false;
            this.groupBox3.Text = "Remote control";
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Location = new System.Drawing.Point(209, 57);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(271, 15);
            this.label12.TabIndex = 11;
            this.label12.Text = "(port number as set in remote control application)";
            // 
            // label13
            // 
            this.label13.AutoSize = true;
            this.label13.Location = new System.Drawing.Point(6, 29);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(125, 15);
            this.label13.TabIndex = 6;
            this.label13.Text = "Hostname/IP-address:";
            // 
            // label14
            // 
            this.label14.AutoSize = true;
            this.label14.Location = new System.Drawing.Point(6, 57);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(32, 15);
            this.label14.TabIndex = 10;
            this.label14.Text = "Port:";
            // 
            // lblIPAddress
            // 
            this.lblIPAddress.AutoSize = true;
            this.lblIPAddress.Location = new System.Drawing.Point(134, 29);
            this.lblIPAddress.Name = "lblIPAddress";
            this.lblIPAddress.Size = new System.Drawing.Size(73, 15);
            this.lblIPAddress.TabIndex = 12;
            this.lblIPAddress.Text = "<unknown>";
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(306, 29);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(271, 15);
            this.label4.TabIndex = 12;
            this.label4.Text = "(use \'localhost\' if Kodi is running on this machine)";
            // 
            // fSettings
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(634, 411);
            this.Controls.Add(this.ssStatus);
            this.Controls.Add(this.tabControl1);
            this.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.MinimumSize = new System.Drawing.Size(650, 450);
            this.Name = "fSettings";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.Text = "Kodi RemoteXtender";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.fSettings_FormClosing);
            this.Load += new System.EventHandler(this.fSettings_Load);
            this.ResizeEnd += new System.EventHandler(this.fSettings_ResizeEnd);
            this.VisibleChanged += new System.EventHandler(this.fSettings_VisibleChanged);
            ((System.ComponentModel.ISupportInitialize)(this.updRemotePort)).EndInit();
            this.tabMPCHC.ResumeLayout(false);
            this.tabMPCHC.PerformLayout();
            this.tabControl1.ResumeLayout(false);
            this.tabLog.ResumeLayout(false);
            this.tabLog.PerformLayout();
            this.tabPDVD.ResumeLayout(false);
            this.tabPDVD.PerformLayout();
            this.tabTMT.ResumeLayout(false);
            this.tabTMT.PerformLayout();
            this.ssStatus.ResumeLayout(false);
            this.ssStatus.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.updKodiPort)).EndInit();
            this.mnuNotify.ResumeLayout(false);
            this.tabGeneral.ResumeLayout(false);
            this.groupBox1.ResumeLayout(false);
            this.groupBox1.PerformLayout();
            this.groupBox2.ResumeLayout(false);
            this.groupBox2.PerformLayout();
            this.groupBox3.ResumeLayout(false);
            this.groupBox3.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.NumericUpDown updRemotePort;
        private System.Windows.Forms.CheckBox chkEnabled;
        private System.Windows.Forms.TabPage tabMPCHC;
        private System.Windows.Forms.TabControl tabControl1;
        private System.Windows.Forms.TextBox txtMPCHC_WebInterfaceURL;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Label lblMPCHC_Status;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.CheckBox chkMPCHC_Enabled;
        private System.Windows.Forms.TabPage tabLog;
        private System.Windows.Forms.ListView lstLog;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.ColumnHeader columnHeader3;
        private System.Windows.Forms.StatusStrip ssStatus;
        private System.Windows.Forms.ToolStripStatusLabel tslStatus;
        private System.Windows.Forms.Button btnClearLog;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.NumericUpDown updKodiPort;
        private System.Windows.Forms.NotifyIcon icoNotify;
        private System.Windows.Forms.ContextMenuStrip mnuNotify;
        private System.Windows.Forms.ToolStripMenuItem miSettings;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem miQuit;
        private System.Windows.Forms.ToolStripMenuItem miEnabled;
        private System.Windows.Forms.TabPage tabPDVD;
        private System.Windows.Forms.TabPage tabTMT;
        private System.Windows.Forms.MaskedTextBox txtPDVD_Passcode;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.TextBox txtPDVD_Host;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label lblPDVD_Status;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.CheckBox chkPDVD_Enabled;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.CheckBox chkTMT_Enabled;
        private System.Windows.Forms.CheckBox chkLogResponseModifications;
        private System.Windows.Forms.ToolStripStatusLabel tslSupport;
        private System.Windows.Forms.ToolStripStatusLabel tslVersion;
        private System.Windows.Forms.CheckBox chkRunOnStartup;
        private System.Windows.Forms.TabPage tabGeneral;
        private System.Windows.Forms.GroupBox groupBox1;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.TextBox txtKodiHostname;
        private System.Windows.Forms.GroupBox groupBox2;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.GroupBox groupBox3;
        private System.Windows.Forms.Label lblIPAddress;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label4;
    }
}

