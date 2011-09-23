using System;
using System.Drawing;
using System.Collections;
using System.ComponentModel;
using System.Windows.Forms;
using mshtml;

//http://www.codeproject.com/dotnet/dwebapp.asp

namespace PLC
{
	/// <summary>
	/// Summary description for WebForm.
	/// </summary>
	public class ContentBrowser : System.Windows.Forms.Form
	{
		private AxSHDocVw.AxWebBrowser axWebBrowser1;
		/// <summary>
		/// Required designer variable.
		/// </summary>
		private System.ComponentModel.Container components = null;

		public ContentBrowser()
		{
			//
			// Required for Windows Form Designer support
			//
			InitializeComponent();

			//
			// TODO: Add any constructor code after InitializeComponent call
			//
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

		#region Windows Form Designer generated code
		/// <summary>
		/// Required method for Designer support - do not modify
		/// the contents of this method with the code editor.
		/// </summary>
		private void InitializeComponent()
		{
			System.Resources.ResourceManager resources = new System.Resources.ResourceManager(typeof(ContentBrowser));
			this.axWebBrowser1 = new AxSHDocVw.AxWebBrowser();
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).BeginInit();
			this.SuspendLayout();
			// 
			// axWebBrowser1
			// 
			this.axWebBrowser1.Dock = System.Windows.Forms.DockStyle.Fill;
			this.axWebBrowser1.Enabled = true;
			this.axWebBrowser1.Location = new System.Drawing.Point(0, 0);
			this.axWebBrowser1.OcxState = ((System.Windows.Forms.AxHost.State)(resources.GetObject("axWebBrowser1.OcxState")));
			this.axWebBrowser1.Size = new System.Drawing.Size(328, 298);
			this.axWebBrowser1.TabIndex = 0;
			this.axWebBrowser1.Enter += new System.EventHandler(this.axWebBrowser1_Enter);
			// 
			// WebForm
			// 
			this.AutoScaleBaseSize = new System.Drawing.Size(5, 13);
			this.ClientSize = new System.Drawing.Size(328, 298);
			this.Controls.Add(this.axWebBrowser1);
			this.Name = "WebForm";
			this.Text = "Content Browser";
			((System.ComponentModel.ISupportInitialize)(this.axWebBrowser1)).EndInit();
			this.ResumeLayout(false);

		}
		#endregion

		private void axWebBrowser1_Enter(object sender, System.EventArgs e)
		{
		
		}

		public void Navigate(string aPage)
		{
			object o = null;
			axWebBrowser1.Navigate(aPage, ref o, ref o, ref o, ref o);
		}
		
		public void DisplayContent(string aString) 
		{ 
			Navigate("about:blank");
			IHTMLDocument2 doc = axWebBrowser1.Document as IHTMLDocument2;
			doc.write(aString);
		}

		public void ClearContent()
		{ 
			IHTMLDocument2 doc = axWebBrowser1.Document as IHTMLDocument2; 
			doc.write(""); 
			doc.close();
			doc.write("");
		}	
	}
}
