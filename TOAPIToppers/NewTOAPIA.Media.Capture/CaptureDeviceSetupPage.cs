

using System;
using System.Collections;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Windows.Forms;

using NewTOAPIA.DirectShow;
using NewTOAPIA.DirectShow.Core;
using NewTOAPIA.Media;

namespace NewTOAPIA.Media.Capture
{
	/// <summary>
	/// Summary description for CaptureDeviceSetupPage.
	/// </summary>
	public class CaptureDeviceSetupPage : System.Windows.Forms.UserControl, IVideoSourcePage
	{
		FilterCollection filters;
		private bool completed = false;
		private Label label1;
		private ComboBox deviceCombo;
		
        /// <summary> 
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		// state changed event
		public event EventHandler StateChanged;

		// Constructor
		public CaptureDeviceSetupPage()
		{
			// This call is required by the Windows.Forms Form Designer.
			InitializeComponent();

			//
			filters = new FilterCollection(FilterCategory.VideoInputDevice);

			if (filters.Count == 0)
			{
				deviceCombo.Items.Add("No local capture devices");
				deviceCombo.Enabled = false;
			}
			else
			{
				// add all devices to combo
				foreach (Filter filter in filters)
				{
					deviceCombo.Items.Add(filter.FriendlyName);
				}
				completed = true;
			}
			deviceCombo.SelectedIndex = 0;
		}

		/// <summary> 
		/// Clean up any resources being used.
		/// </summary>
		protected override void Dispose( bool disposing )
		{
			if( disposing )
			{
				if(components != null)
				{
					components.Dispose();
				}
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
			this.label1 = new System.Windows.Forms.Label();
			this.deviceCombo = new System.Windows.Forms.ComboBox();
			this.SuspendLayout();
			// 
			// label1
			// 
			this.label1.Location = new System.Drawing.Point(10, 13);
			this.label1.Name = "label1";
			this.label1.Size = new System.Drawing.Size(50, 15);
			this.label1.TabIndex = 0;
			this.label1.Text = "&Device:";
			// 
			// deviceCombo
			// 
			this.deviceCombo.Anchor = ((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left) 
				| System.Windows.Forms.AnchorStyles.Right);
			this.deviceCombo.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
			this.deviceCombo.Location = new System.Drawing.Point(60, 10);
			this.deviceCombo.Name = "deviceCombo";
			this.deviceCombo.Size = new System.Drawing.Size(230, 21);
			this.deviceCombo.TabIndex = 1;
			// 
			// CaptureDeviceSetupPage
			// 
			this.Controls.AddRange(new System.Windows.Forms.Control[] {
																		  this.deviceCombo,
																		  this.label1});
			this.Name = "CaptureDeviceSetupPage";
			this.Size = new System.Drawing.Size(300, 150);
			this.ResumeLayout(false);

		}
		#endregion

		// Completed property
		public bool Completed
		{
			get { return completed; }
		}

		// Show the page
		public void Display()
		{
			deviceCombo.Focus();
		}

		// Apply the page
		public bool Apply()
		{
			return true;
		}

		// Get configuration object
		public object GetConfiguration()
		{
			LocalConfiguration config = new LocalConfiguration();

			config.source = filters[deviceCombo.SelectedIndex].MonikerString;

			return (object) config;
		}

		// Set configuration
		public void SetConfiguration(object config)
		{
			LocalConfiguration cfg = (LocalConfiguration) config;

			if (cfg != null)
			{
				for (int i = 0; i < filters.Count; i++)
				{
					if (filters[i].MonikerString == cfg.source)
					{
						deviceCombo.SelectedIndex = i;
						break;
					}
				}
			}
		}
	}
}
