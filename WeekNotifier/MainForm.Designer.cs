namespace WeekNotifier
{
	partial class MainForm
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

		#region Windows Form Designer generated code

		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
            this.components = new System.ComponentModel.Container();
            System.Windows.Forms.Label label1;
            System.Windows.Forms.Label label2;
            System.Windows.Forms.Label label3;
            this._offsetYUpDown = new System.Windows.Forms.NumericUpDown();
            this._okButton = new System.Windows.Forms.Button();
            this._cancelButton = new System.Windows.Forms.Button();
            this._offsetXUpDown = new System.Windows.Forms.NumericUpDown();
            this._colorDialog = new System.Windows.Forms.ColorDialog();
            this._fontDialog = new System.Windows.Forms.FontDialog();
            this._backColorButton = new System.Windows.Forms.Button();
            this._fontColorButton = new System.Windows.Forms.Button();
            this._fontTypeButton = new System.Windows.Forms.Button();
            this._iconSample = new System.Windows.Forms.PictureBox();
            this._weekSampleUpDown = new System.Windows.Forms.NumericUpDown();
            this._weekNumberIcon = new WeekNotifier.WeekNumberIcon(this.components);
            label1 = new System.Windows.Forms.Label();
            label2 = new System.Windows.Forms.Label();
            label3 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this._offsetYUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._offsetXUpDown)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconSample)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this._weekSampleUpDown)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            label1.AutoSize = true;
            label1.Location = new System.Drawing.Point(12, 15);
            label1.Name = "label1";
            label1.Size = new System.Drawing.Size(48, 13);
            label1.TabIndex = 1;
            label1.Text = "Offset X:";
            // 
            // label2
            // 
            label2.AutoSize = true;
            label2.Location = new System.Drawing.Point(12, 41);
            label2.Name = "label2";
            label2.Size = new System.Drawing.Size(48, 13);
            label2.TabIndex = 1;
            label2.Text = "Offset Y:";
            // 
            // label3
            // 
            label3.AutoSize = true;
            label3.Location = new System.Drawing.Point(12, 67);
            label3.Name = "label3";
            label3.Size = new System.Drawing.Size(77, 13);
            label3.TabIndex = 10;
            label3.Text = "Sample Week:";
            // 
            // _offsetYUpDown
            // 
            this._offsetYUpDown.Location = new System.Drawing.Point(96, 39);
            this._offsetYUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._offsetYUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this._offsetYUpDown.Name = "_offsetYUpDown";
            this._offsetYUpDown.Size = new System.Drawing.Size(37, 20);
            this._offsetYUpDown.TabIndex = 0;
            this._offsetYUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._offsetYUpDown.ValueChanged += new System.EventHandler(this._offsetYUpDown_ValueChanged);
            // 
            // _okButton
            // 
            this._okButton.Location = new System.Drawing.Point(174, 116);
            this._okButton.Name = "_okButton";
            this._okButton.Size = new System.Drawing.Size(75, 23);
            this._okButton.TabIndex = 2;
            this._okButton.Text = "&Ok";
            this._okButton.UseVisualStyleBackColor = true;
            this._okButton.Click += new System.EventHandler(this._okButton_Click);
            // 
            // _cancelButton
            // 
            this._cancelButton.DialogResult = System.Windows.Forms.DialogResult.Cancel;
            this._cancelButton.Location = new System.Drawing.Point(255, 116);
            this._cancelButton.Name = "_cancelButton";
            this._cancelButton.Size = new System.Drawing.Size(75, 23);
            this._cancelButton.TabIndex = 3;
            this._cancelButton.Text = "&Cancel";
            this._cancelButton.UseVisualStyleBackColor = true;
            this._cancelButton.Click += new System.EventHandler(this._cancelButton_Click);
            // 
            // _offsetXUpDown
            // 
            this._offsetXUpDown.Location = new System.Drawing.Point(96, 13);
            this._offsetXUpDown.Maximum = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this._offsetXUpDown.Minimum = new decimal(new int[] {
            10,
            0,
            0,
            -2147483648});
            this._offsetXUpDown.Name = "_offsetXUpDown";
            this._offsetXUpDown.Size = new System.Drawing.Size(37, 20);
            this._offsetXUpDown.TabIndex = 0;
            this._offsetXUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._offsetXUpDown.ValueChanged += new System.EventHandler(this._offsetXUpDown_ValueChanged);
            // 
            // _fontDialog
            // 
            this._fontDialog.ShowColor = true;
            // 
            // _backColorButton
            // 
            this._backColorButton.Location = new System.Drawing.Point(160, 10);
            this._backColorButton.Name = "_backColorButton";
            this._backColorButton.Size = new System.Drawing.Size(75, 23);
            this._backColorButton.TabIndex = 4;
            this._backColorButton.Text = "Back Color";
            this._backColorButton.Click += new System.EventHandler(this._backColorButton_Click);
            // 
            // _fontColorButton
            // 
            this._fontColorButton.Location = new System.Drawing.Point(160, 36);
            this._fontColorButton.Name = "_fontColorButton";
            this._fontColorButton.Size = new System.Drawing.Size(75, 23);
            this._fontColorButton.TabIndex = 6;
            this._fontColorButton.Text = "Font Color";
            this._fontColorButton.UseVisualStyleBackColor = true;
            this._fontColorButton.Click += new System.EventHandler(this._fontColorButton_Click);
            // 
            // _fontTypeButton
            // 
            this._fontTypeButton.Location = new System.Drawing.Point(160, 62);
            this._fontTypeButton.Name = "_fontTypeButton";
            this._fontTypeButton.Size = new System.Drawing.Size(75, 23);
            this._fontTypeButton.TabIndex = 7;
            this._fontTypeButton.Text = "Font Type";
            this._fontTypeButton.UseVisualStyleBackColor = true;
            this._fontTypeButton.Click += new System.EventHandler(this._fontTypeButton_Click);
            // 
            // _iconSample
            // 
            this._iconSample.Location = new System.Drawing.Point(255, 10);
            this._iconSample.Name = "_iconSample";
            this._iconSample.Size = new System.Drawing.Size(63, 50);
            this._iconSample.TabIndex = 8;
            this._iconSample.TabStop = false;
            // 
            // _weekSampleUpDown
            // 
            this._weekSampleUpDown.Location = new System.Drawing.Point(96, 65);
            this._weekSampleUpDown.Maximum = new decimal(new int[] {
            53,
            0,
            0,
            0});
            this._weekSampleUpDown.Minimum = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._weekSampleUpDown.Name = "_weekSampleUpDown";
            this._weekSampleUpDown.Size = new System.Drawing.Size(37, 20);
            this._weekSampleUpDown.TabIndex = 9;
            this._weekSampleUpDown.TextAlign = System.Windows.Forms.HorizontalAlignment.Right;
            this._weekSampleUpDown.Value = new decimal(new int[] {
            1,
            0,
            0,
            0});
            this._weekSampleUpDown.ValueChanged += new System.EventHandler(this._weekSampleUpDown_ValueChanged);
            // 
            // _weekNumberIcon
            // 
            this._weekNumberIcon.BackgroundColor = System.Drawing.Color.Yellow;
            this._weekNumberIcon.FontColor = System.Drawing.Color.Red;
            this._weekNumberIcon.FontType = new System.Drawing.Font("Courier New", 18F, System.Drawing.FontStyle.Bold);
            this._weekNumberIcon.OffsetX = -3;
            this._weekNumberIcon.OffsetY = 6;
            this._weekNumberIcon.ExitSelected += new System.EventHandler(this._weekNumberIcon_ExitSelected);
            this._weekNumberIcon.AboutSelected += new System.EventHandler(this._weekNumberIcon_AboutSelected);
            this._weekNumberIcon.SettingsSelected += new System.EventHandler(this._weekNumberIcon_SettingsSelected);
            // 
            // MainForm
            // 
            this.AcceptButton = this._okButton;
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.AutoSize = true;
            this.AutoSizeMode = System.Windows.Forms.AutoSizeMode.GrowAndShrink;
            this.CancelButton = this._cancelButton;
            this.ClientSize = new System.Drawing.Size(408, 259);
            this.Controls.Add(label3);
            this.Controls.Add(this._weekSampleUpDown);
            this.Controls.Add(this._iconSample);
            this.Controls.Add(this._fontTypeButton);
            this.Controls.Add(this._fontColorButton);
            this.Controls.Add(this._backColorButton);
            this.Controls.Add(this._cancelButton);
            this.Controls.Add(this._okButton);
            this.Controls.Add(label2);
            this.Controls.Add(label1);
            this.Controls.Add(this._offsetYUpDown);
            this.Controls.Add(this._offsetXUpDown);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.Padding = new System.Windows.Forms.Padding(10);
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Week Number Settings";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            ((System.ComponentModel.ISupportInitialize)(this._offsetYUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._offsetXUpDown)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._iconSample)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this._weekSampleUpDown)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

		}

		#endregion

		private System.Windows.Forms.NumericUpDown _offsetXUpDown;
		private System.Windows.Forms.NumericUpDown _offsetYUpDown;
		private System.Windows.Forms.Button _okButton;
		private System.Windows.Forms.Button _cancelButton;
		private WeekNumberIcon _weekNumberIcon;
		private System.Windows.Forms.ColorDialog _colorDialog;
		private System.Windows.Forms.FontDialog _fontDialog;
		private System.Windows.Forms.Button _backColorButton;
		private System.Windows.Forms.Button _fontColorButton;
		private System.Windows.Forms.Button _fontTypeButton;
		private System.Windows.Forms.PictureBox _iconSample;
        private System.Windows.Forms.NumericUpDown _weekSampleUpDown;

	}
}

