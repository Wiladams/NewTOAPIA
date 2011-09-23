using System;
using System.Collections;
using System.Drawing;
using System.IO;
using System.Xml;
using System.Xml.Serialization;
using System.Net;
using System.Threading;

using TOAPI.Types;

using NewTOAPIA.Drawing;
using NewTOAPIA.UI;

namespace PLC
{
    using NewTOAPIA;


	public class PLCWindow : GraphicWindow
	{
		SortedList fEditionsDictionary;
		//PLCPanel fClientPanel;
		//TreeView fNavTree;

		Point WOrg;
		Point WExt;
		Point VOrg;
		Point VExt;
		


		// Things related to printing
		//PageSetupDialog setupDlg = new PageSetupDialog();
		//PrintDialog printDlg = new PrintDialog();
		//PrintDocument printDoc = new PrintDocument();

		// Opening files
		//OpenFileDialog fFileOpenDialog;
		//string fFileName;

		// Saving files
		//SaveFileDialog fSaveFileDialog;
		//string fSaveFileName;


		public PLCWindow()
			: base("PLC Browser", 0, 10, 1024, 768)
		{
			fEditionsDictionary = new SortedList(25);

            BackgroundColor = RGBColor.White;

			//RootGraphic.GraphicsUnit = GraphicsUnit.Inch;

			WOrg = new Point(0, 0);
			WExt = new Point(1000, 750);

			VOrg = new Point(0, 0);
			VExt = new Point(1024, 768);


			//CreateFileOpenDialog();
			//CreateSaveFileDialog();

			//CreateMenu();
			//CreatePrintDialogs();

		}

        public override void OnResizedBy(int dw, int dh)
        {
          
            VExt.X = ClientRectangle.Width;
            VExt.Y = ClientRectangle.Height;
        }

        //void CreatePrintDialogs()
        //{
        //    setupDlg = new PageSetupDialog();
        //    printDlg = new PrintDialog();
        //    printDoc = new PrintDocument();
        //    printDoc.DocumentName = "PLC Document";
        //    printDoc.DefaultPageSettings.Landscape = true;
        //}

        //void CreateFileOpenDialog()
        //{
        //    fFileOpenDialog = new OpenFileDialog();
        //    fFileOpenDialog.Title = "PLC Viewer Open File Dialog";
        //    fFileOpenDialog.InitialDirectory = @".";
        //    fFileOpenDialog.Filter = "XML Files (*.xml)|*.xml|All Files (*.*)|*.*";
        //    fFileOpenDialog.FilterIndex = 1;
        //    fFileOpenDialog.RestoreDirectory = false;
        //}

        //void CreateSaveFileDialog()
        //{
        //    fSaveFileDialog = new SaveFileDialog();
        //    fSaveFileDialog.Title = "PLC Viewer Save File Dialog";
        //    fSaveFileDialog.InitialDirectory = @".";
        //    fSaveFileDialog.Filter = "Portable Network Graphics (*.png)|*.png|"+
        //        "JPEG File Interchange Format (*.jpg)|*.jpg;*.jpeg;*.jfif|"+
        //        "Graphics Interchange Format (*.gif)|*.gif|"+
        //        "Tagged Image File Format (*.tif)|*.tif;*.tiff|"+
        //        "Windows PixelBuffer (*.bmp)|*.bmp|"+
        //        "XML (*.xml)|*.xml|"+
        //        "XHTML (*.html)|*.html;*.htm|"+
        //        "All Files (*.*)|*.*";
        //    fSaveFileDialog.FilterIndex = 1;
        //    fSaveFileDialog.RestoreDirectory = false;
        //}


        //void CreateMenu()
        //{
        //    // Create the menus
        //    Menu = new MainMenu();

        //    MenuItem fileMenu = new MenuItem("&File");
        //    Menu.MenuItems.Add(fileMenu);

        //    fileMenu.MenuItems.Add("New", new EventHandler(MenuNewClick));
        //    fileMenu.MenuItems.Add("Open", new EventHandler(MenuOpenClick));
        //    //fileMenu.AddSeparator();
        //    fileMenu.MenuItems.Add("Save", new EventHandler(MenuSaveClick));
        //    fileMenu.MenuItems.Add("Save As", new EventHandler(MenuSaveAsClick));
        //    fileMenu.MenuItems.Add("Save All", new EventHandler(MenuSaveAllClick));
        //    //fileMenu.AppendSeparator();
        //    fileMenu.MenuItems.Add("Page Setup", new EventHandler(MenuPageSetupClick));
        //    fileMenu.MenuItems.Add("Print", new EventHandler(MenuPrintClick));
        //    //fileMenu.AppendSeparator();
        //    fileMenu.MenuItems.Add("Exit", new EventHandler(MenuExitClick));

        //    MenuItem editMenu = new MenuItem("&Edit");
        //    Menu.MenuItems.Add(editMenu);
        //    //editMenu.MenuItems.Add("Cut", new EventHandler(MenuCutClick));
        //    editMenu.MenuItems.Add("Copy", new EventHandler(MenuCopyClick));
        //    //editMenu.MenuItems.Add("Paste", new EventHandler(MenuPasteClick));

        //    MenuItem viewMenu = new MenuItem("&View");
        //    Menu.MenuItems.Add(viewMenu);
        //    viewMenu.MenuItems.Add("Software", new EventHandler(MenuChangeView));
        //    viewMenu.MenuItems.Add("Hardware", new EventHandler(MenuChangeView));
        //    viewMenu.MenuItems.Add("IT", new EventHandler(MenuChangeView));


        //    // A scaling menu if we want that
        //    MenuItem zoomMenu = new MenuItem("&Zoom");
        //    Menu.MenuItems.Add(zoomMenu);

        //    zoomMenu.MenuItems.Add("100", new EventHandler(MenuChangeZoom));
        //    zoomMenu.MenuItems.Add("150", new EventHandler(MenuChangeZoom));
        //    zoomMenu.MenuItems.Add("200", new EventHandler(MenuChangeZoom));
        //}

        //void CreateEditionsMenu()
        //{
        //    MenuItem editionMenu = new MenuItem("Editions");


        //    // Try to get the edition information from the seb service
        //    try{
        //        EpssApi.Service _ws = new EpssApi.Service();
        //        _ws.Credentials = CredentialCache.DefaultCredentials;
			
        //        EpssApi.Edition[] editions;
        //        editions = _ws.GetEditionTree();

        //        // First store all the editions in a dictionary
        //        // The key is the GUID, and the value is the 
        //        // Edition object
        //        foreach (EpssApi.Edition e in editions)
        //        {
        //            this.fEditionsDictionary.Add(e.Title, e);
        //        }

        //        // Now populate the menu with the data found
        //        foreach (EpssApi.Edition ed in editions)
        //        {
        //            // If the parentID is empty, then it is a top level
        //            // edition, so we should create it's sub menu, and add
        //            // the whole thing.
        //            if (ed.ParentEditionId == Guid.Empty)
        //            {
        //                MenuItem m = CreateEditionMenu(ed);
        //                editionMenu.MenuItems.Add(m);
        //            }
        //        }
        //    } 
        //    catch 
        //    {
        //        // No connection to service, so create a default menu
        //        editionMenu.MenuItems.Add(new MenuItem("Software"));
        //        editionMenu.MenuItems.Add(new MenuItem("Hardware"));
        //        editionMenu.MenuItems.Add(new MenuItem("IT"));

        //    }
			
        //    Menu.MenuItems.Add(editionMenu);
        //}

        //MenuItem CreateEditionMenu(EpssApi.Edition e)
        //{
        //    MenuItem m = new MenuItem(e.Title, new EventHandler(MenuEditionClick));
			

        //    // If there are sub-Editions, then add them as well
        //    foreach (DictionaryEntry entry in fEditionsDictionary)
        //    {
        //        // If the edition we're looking at in the dictionary matches the 
        //        // one that was passed into this routine, then the one in the
        //        // dictionary is a sub-edition of the one that was passed in.
        //        EpssApi.Edition tmpEd = (EpssApi.Edition)entry.Value;
        //        if (tmpEd.ParentEditionId == e.EditionId)
        //        {
        //            m.MenuItems.Add(CreateEditionMenu(tmpEd));
        //        }

        //        //Console.WriteLine("{0}", e.EditionHierarchy);
        //    }

        //    return m;
        //}

        //void CreateEditionsTree(Control parent)
        //{
            //fNavTree = new TreeView();
            //fNavTree.Parent = parent;
            //fNavTree.Size = new Size(180,768);
            //fNavTree.Dock = DockStyle.Left;


			// Try to get the edition information from the web service
            //try
            //{
            //    EpssApi.Service _ws = new EpssApi.Service();
            //    _ws.Credentials = CredentialCache.DefaultCredentials;
			
            //    EpssApi.Edition[] editions;
            //    editions = _ws.GetEditionTree();

            //    // First store all the editions in a dictionary
            //    // The key is the GUID, and the value is the 
            //    // Edition object
            //    foreach (EpssApi.Edition e in editions)
            //    {
            //        this.fEditionsDictionary.Add(e.Title, e);
            //    }

            //    // Now populate the menu with the data found
            //    foreach (EpssApi.Edition ed in editions)
            //    {
            //        // If the parentID is empty, then it is a top level
            //        // edition, so we should create it's sub menu, and add
            //        // the whole thing.
            //        if (ed.ParentEditionId == Guid.Empty)
            //        {
            //            TreeNode tn = CreateEditionTreeNode(ed);
            //            fNavTree.Nodes.Add(tn);
            //        }
            //    }
            //} 
            //catch 
            //{
            //    EpssApi.Edition ed = new EpssApi.Edition();

            //    // No connection to service, so create a default menu
            //    ed.Title = "Hardware";
            //    fNavTree.Nodes.Add(CreateEditionTreeNode(ed));

            //    ed.Title = "IT";
            //    fNavTree.Nodes.Add(CreateEditionTreeNode(ed));
				
            //    ed.Title = "Software";
            //    fNavTree.Nodes.Add(CreateEditionTreeNode(ed));

            //}
			
            //fNavTree.AfterSelect += new TreeViewEventHandler(fNavTree_AfterSelect);
		//}

        //TreeNode CreateEditionTreeNode(EpssApi.Edition e)
        //{
        //    TreeNode tn = new TreeNode(e.Title);
        //    tn.Tag = e;


        //    // If there are sub-Editions, then add them as well
        //    foreach (DictionaryEntry entry in fEditionsDictionary)
        //    {
        //        // If the edition we're looking at in the dictionary matches the 
        //        // one that was passed into this routine, then the one in the
        //        // dictionary is a sub-edition of the one that was passed in.
        //        EpssApi.Edition tmpEd = (EpssApi.Edition)entry.Value;
        //        if (tmpEd.ParentEditionId == e.EditionId)
        //        {
        //            tn.Nodes.Add(CreateEditionTreeNode(tmpEd));
        //        }

        //        //Console.WriteLine("{0}", e.EditionHierarchy);
        //    }

        //    return tn;
        //}

		void MenuChangeView(object obj, EventArgs ea)
		{
			//MenuItem aMenu = (MenuItem)obj;
			//fClientPanel.DisplayView(aMenu.Text);
		}

		// A Basic dictionary lookup
        //EpssApi.Edition GetEdition(string name)
        //{
        //    EpssApi.Edition edition = null;


        //    if (this.fEditionsDictionary.ContainsKey(name))
        //        edition = (EpssApi.Edition)this.fEditionsDictionary.GetByIndex(fEditionsDictionary.IndexOfKey(name));

        //    return edition;
        //}

        //void MenuEditionClick(object obj, EventArgs ea)
        //{
        //    MenuItem aMenu = (MenuItem)obj;

        //    // Using the title, find the edition 
        //    EpssApi.Edition edition = GetEdition(aMenu.Text);

        //    // See if a view already exists for the edition.
        //    // If it does not, then create a view for the edition
        //    PLCView editionView = this.CreateEditionView(edition);

        //    // Display the view for the specified edition
        //    fClientPanel.SwitchToView(editionView);
        //}
		
        //void MenuChangeZoom(object obj, EventArgs ea)
        //{
        //    MenuItem aMenu = (MenuItem)obj;

        //    switch (aMenu.Text)
        //    {
        //        case "100":
        //            fScaleFactor.Width = 1;
        //            fScaleFactor.Height = 1;
        //            break;
        //        case "150":
        //            fScaleFactor.Width = 150;
        //            fScaleFactor.Height = 150;
        //            break;
        //        case "200":
        //            fScaleFactor.Width = 200;
        //            fScaleFactor.Height = 200;
        //            break;

        //    }

        //    // Draw the whole screen
        //    fClientPanel.ZoomFactor = fScaleFactor.Width;
        //    Size autoSize = new Size((int)Math.Ceiling(fScaleFactor.Width*1024),
        //        (int)Math.Ceiling(fScaleFactor.Height*768));
        //    fClientPanel.AutoScrollMinSize = autoSize;

        //    Invalidate();
        //}

        //void MenuNewClick(object obj, EventArgs ea)
        //{
        //    Console.WriteLine("MenuNewClick");
        //    //fContentBrowser = new ContentBrowser();
        //    //fContentBrowser.DisplayContent("<html><head><body>Hello World!</body></head></html>");
        //    //fContentBrowser.Show();
        //}

		/// <summary>
		/// This delegate routine is called when the 'Open' menu item is clicked.
		/// </summary>
		/// <param name="obj">A reference to the menu item that was clicked.</param>
		/// <param name="ea">The event arguments for the menu click</param>
        //void MenuOpenClick(object obj, EventArgs ea)
        //{
        //    if (fFileOpenDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        fFileName = fFileOpenDialog.FileName;

        //        LoadFile(fFileName);
        //    }
        //}

        //void MenuSaveClick(object obj, EventArgs ea)
        //{
        //    Console.WriteLine("MenuSaveClick");
        //}

        //void MenuSaveAsClick(object obj, EventArgs ea)
        //{
        //    //Console.WriteLine("MenuSaveAsClick");
        //    fSaveFileDialog.AddExtension = true;
        //    fSaveFileDialog.FileName = fClientPanel.EditionName;

        //    if (fSaveFileDialog.ShowDialog() == DialogResult.OK)
        //    {
        //        this.fClientPanel.SaveAs(fSaveFileDialog.FileName);
        //    }
        //}
		

        //void MenuPageSetupClick(object obj, EventArgs ea)
        //{
        //    // Page Setup Dialog settings
        //    setupDlg.Document = printDoc;
        //    setupDlg.PageSettings = printDoc.DefaultPageSettings;
        //    setupDlg.AllowMargins = false;
        //    setupDlg.AllowOrientation = true;
        //    setupDlg.AllowPaper = true;
        //    setupDlg.AllowPrinter = true;

        //    // Now pop up the dialog and capture the settings
        //    if (setupDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        printDoc.DefaultPageSettings = setupDlg.PageSettings;
        //        printDoc.PrinterSettings = setupDlg.PrinterSettings;
        //    }
        //}

        //void MenuPrintClick(object obj, EventArgs ea)
        //{
        //    printDlg.Document = printDoc;

        //    printDlg.PrinterSettings.MinimumPage = 1;
        //    printDlg.PrinterSettings.MaximumPage = 1;
        //    printDlg.PrinterSettings.FromPage = 1;
        //    printDlg.PrinterSettings.ToPage = 1;

        //    if (printDlg.ShowDialog() == DialogResult.OK)
        //    {
        //        printDoc.DocumentName = Text;
        //        printDoc.PrintPage += new PrintPageEventHandler(OnPrintPage);
				
        //        // Do the actual printing thing
        //        printDoc.Print();
        //    }
        //}
		
        //void OnPrintPage(object obj, PrintPageEventArgs ppea)
        //{
        //    // Create the graphics object to match the printer driver
        //    Graphics grfx = ppea.Graphics;
			
        //    // Now draw stuff
        //    RootGraphic.DrawInto(grfx);

        //    ppea.HasMorePages = false;
        //}



		PLCView CreateEditionView(EditionType edition)
		{
			PLCView aView = null;

			// Create a new view based on the stream of XML
			// and hand that back.
			switch (edition)
			{
                //case EditionType.Hardware:
                //    aView = new PLCHardwareView(edition.Title,0,0,WExt.X, WExt.Y);
                //    break;
				case EditionType.Software:
					aView = new PLCSoftwareView("Software",0,0,WExt.X, WExt.Y);
					break;
                //case EditionType.IT:
                //    aView = new PLCITView(edition.Title,0,0,WExt.X, WExt.Y);
                //    break;
			}

            //try {
            //    XmlNode xNode = _ws.GeneratePLCMapXml (edition.EditionId);
            //    aView.XmlData = xNode;
            //} catch 
            //{
            //    Console.WriteLine("Error in loading XML from service: {0}",edition.EditionId);
            //}

			return aView;
		}


	}
}
