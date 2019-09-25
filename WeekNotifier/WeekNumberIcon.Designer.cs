namespace WeekNotifier
{
	partial class WeekNumberIcon
	{
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.IContainer components = null;

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		/// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
		protected override void Dispose( bool disposing )
		{
			if ( disposing && ( components != null ) )
			{
				components.Dispose();
			}
			base.Dispose( disposing );
		}

		#region Component Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
            this._weekNumberNotifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this._contextMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this._menuRefresh = new System.Windows.Forms.ToolStripMenuItem();
            this._menuSettings = new System.Windows.Forms.ToolStripMenuItem();
            this._menuAbout = new System.Windows.Forms.ToolStripMenuItem();
            this._menuExit = new System.Windows.Forms.ToolStripMenuItem();
            toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this._contextMenu.SuspendLayout();
            // 
            // toolStripSeparator1
            // 
            toolStripSeparator1.Name = "toolStripSeparator1";
            toolStripSeparator1.Size = new System.Drawing.Size(97, 6);
            // 
            // _weekNumberNotifyIcon
            // 
            this._weekNumberNotifyIcon.BalloonTipText = "Balloon Tip Text?";
            this._weekNumberNotifyIcon.BalloonTipTitle = "Balloon Tip Title?";
            this._weekNumberNotifyIcon.Text = "Week Number";
            this._weekNumberNotifyIcon.Visible = true;
            this._weekNumberNotifyIcon.MouseClick += new System.Windows.Forms.MouseEventHandler(this._weekNumberNotifyIcon_MouseClick);
            // 
            // _contextMenu
            // 
            this._contextMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this._menuRefresh,
            this._menuSettings,
            this._menuAbout,
            toolStripSeparator1,
            this._menuExit});
            this._contextMenu.Name = "_contextMenu";
            this._contextMenu.ShowImageMargin = false;
            this._contextMenu.Size = new System.Drawing.Size(101, 98);
            this._contextMenu.Text = "Week Number";
            this._contextMenu.ItemClicked += new System.Windows.Forms.ToolStripItemClickedEventHandler(this._contextMenu_ItemClicked);
            // 
            // _menuRefresh
            // 
            this._menuRefresh.Font = new System.Drawing.Font("Segoe UI", 9F, System.Drawing.FontStyle.Bold);
            this._menuRefresh.Name = "_menuRefresh";
            this._menuRefresh.Size = new System.Drawing.Size(100, 22);
            this._menuRefresh.Tag = "Refresh";
            this._menuRefresh.Text = "Refresh";
            this._menuRefresh.ToolTipText = "Select to manually refresh the current week number";
            // 
            // _menuSettings
            // 
            this._menuSettings.Font = new System.Drawing.Font("Segoe UI", 9F);
            this._menuSettings.Name = "_menuSettings";
            this._menuSettings.Size = new System.Drawing.Size(100, 22);
            this._menuSettings.Tag = "Settings";
            this._menuSettings.Text = "Settings...";
            this._menuSettings.ToolTipText = "Select to change the settings";
            // 
            // _menuAbout
            // 
            this._menuAbout.Name = "_menuAbout";
            this._menuAbout.Size = new System.Drawing.Size(100, 22);
            this._menuAbout.Tag = "About";
            this._menuAbout.Text = "About...";
            // 
            // _menuExit
            // 
            this._menuExit.Name = "_menuExit";
            this._menuExit.Size = new System.Drawing.Size(100, 22);
            this._menuExit.Tag = "Exit";
            this._menuExit.Text = "Exit";
            this._menuExit.ToolTipText = "Exit Week Number Application";
            this._contextMenu.ResumeLayout(false);

		}

		#endregion

		private System.Windows.Forms.NotifyIcon _weekNumberNotifyIcon;
		private System.Windows.Forms.ContextMenuStrip _contextMenu;
		private System.Windows.Forms.ToolStripMenuItem _menuExit;
		private System.Windows.Forms.ToolStripMenuItem _menuSettings;
		private System.Windows.Forms.ToolStripMenuItem _menuAbout;
        private System.Windows.Forms.ToolStripMenuItem _menuRefresh;
	}
}
