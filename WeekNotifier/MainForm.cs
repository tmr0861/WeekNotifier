using System;
using System.Windows.Forms;
using WeekNotifier.Properties;

namespace WeekNotifier
{
    public partial class MainForm : Form
	{
		public MainForm()
		{
			InitializeComponent();
			LoadSettings();
		}

		private void LoadSettings()
		{
			_weekNumberIcon.BackgroundColor = Settings.Default.BackgroundColor;
			_weekNumberIcon.FontColor = Settings.Default.FontColor;
			_weekNumberIcon.FontType = Settings.Default.FontType;
			
			_offsetXUpDown.Value = Settings.Default.OffsetX;
			_offsetYUpDown.Value = Settings.Default.OffsetY;
			_weekSampleUpDown.Value = _weekNumberIcon.CurrentWeek;

			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

		private void SaveSettings()
		{
			Settings.Default.BackgroundColor = _weekNumberIcon.BackgroundColor;
			Settings.Default.FontColor = _weekNumberIcon.FontColor;
			Settings.Default.FontType = _weekNumberIcon.FontType;
			Settings.Default.OffsetX = _weekNumberIcon.OffsetX;
			Settings.Default.OffsetY = _weekNumberIcon.OffsetY;

			Properties.Settings.Default.Save();
		}

		private void MainForm_FormClosing( object sender, FormClosingEventArgs e )
		{
			// Don't close the app, just hide this window
			if ( e.CloseReason == CloseReason.UserClosing )
			{
				e.Cancel = true;
				Hide();
			}
		}

		private void _cancelButton_Click( object sender, EventArgs e )
		{
			LoadSettings();
			Hide();
		}

		private void _okButton_Click( object sender, EventArgs e )
		{
			SaveSettings();
			Hide();
		}

        private void _backColorButton_Click(object sender, EventArgs e)
		{
			_colorDialog.Color = _weekNumberIcon.BackgroundColor;
			_colorDialog.ShowDialog();

			_weekNumberIcon.BackgroundColor = _colorDialog.Color;
			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

		private void _fontColorButton_Click( object sender, EventArgs e )
		{
			_colorDialog.Color = _weekNumberIcon.FontColor;
			_colorDialog.ShowDialog();

			_weekNumberIcon.FontColor = _colorDialog.Color;
			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

		private void _fontTypeButton_Click( object sender, EventArgs e )
		{
			_fontDialog.Font = _weekNumberIcon.FontType;
			_fontDialog.ShowDialog();

			_weekNumberIcon.FontType = _fontDialog.Font;
			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

		private void _offsetXUpDown_ValueChanged( object sender, EventArgs e )
		{
			_weekNumberIcon.OffsetX = (int)_offsetXUpDown.Value;
			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

		private void _offsetYUpDown_ValueChanged( object sender, EventArgs e )
		{
			_weekNumberIcon.OffsetY = (int)_offsetYUpDown.Value;
			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

		private void _weekNumberIcon_ExitSelected( object sender, EventArgs e )
		{
			Application.Exit();
		}

		private void _weekNumberIcon_SettingsSelected( object sender, EventArgs e )
		{
			Icon = Resources.AppIcon;
            if ( !Visible )
            {
                ShowDialog();
            }
		}

		private void _weekNumberIcon_AboutSelected( object sender, EventArgs e )
		{
			using ( AboutBox about = new AboutBox() )
			{
				about.ShowDialog();
			}
		}

		private void _weekSampleUpDown_ValueChanged( object sender, EventArgs e )
		{
			_iconSample.Image = _weekNumberIcon.GetIcon( (int)_weekSampleUpDown.Value ).ToBitmap();
		}

	}
}
