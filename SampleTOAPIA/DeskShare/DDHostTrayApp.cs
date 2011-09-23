using System;
using System.Net;
using System.Drawing;
using System.Windows.Forms;

using NewTOAPIA.Net.Rtp;

namespace DistributedDesktop
{
    public class DDHostTrayApp : Form
    {
        IPEndPoint fGroupEndPoint;          // The group address
        MultiSession fSession;              // Session used to communicate
        PayloadChannel fGraphicsChannel;
        PayloadChannel fUserIOChannel;

        private NotifyIcon trayIcon;
        private ContextMenu trayMenu;

        private MenuItem menu_RemoteControl;
        private MenuItem menu_Settings;
        private MenuItem menu_Sharing;
        private MenuItem menu_Gray;
        private MenuItem menu_Exit;
        
        DesktopSnapper fSnapper;    // The object that managers the snapshots

        public DDHostTrayApp()
        {
            // Create a simple tray menu with only one item.   
            trayMenu = new ContextMenu();

            menu_Settings=new MenuItem("Settings", OnSettingsSelect);
            menu_Sharing = new MenuItem("Sharing", OnChangeSharing);
            menu_RemoteControl = new MenuItem("Remote Control", OnRemoteControl);
            menu_Gray = new MenuItem("Gray", OnChangeGray);
            menu_Exit = new MenuItem("Exit", OnExit);

            trayMenu.MenuItems.Add(menu_Settings);
            trayMenu.MenuItems.Add(menu_RemoteControl);
            trayMenu.MenuItems.Add(menu_Sharing);
            trayMenu.MenuItems.Add(menu_Gray);
            trayMenu.MenuItems.Add(new MenuItem("-"));
            trayMenu.MenuItems.Add(menu_Exit);
            
            // Create a tray icon. In this example we use a   
            // standard system icon for simplicity, but you   
            // can of course use your own custom icon too.   
            trayIcon = new NotifyIcon();
            trayIcon.Text = "DD Host";
            trayIcon.Icon = new Icon("computer.ico");

            // Add menu to tray icon and show it.   
            trayIcon.ContextMenu = trayMenu;
            trayIcon.Visible = true;
            trayIcon.MouseDown += new System.Windows.Forms.MouseEventHandler(DDHostTrayApp_MouseDown);

            trayIcon.ShowBalloonTip(0, "DD Host", "Right Click for Settings", ToolTipIcon.Info);
            trayIcon.BalloonTipShown += new EventHandler(trayIcon_BalloonTipShown);

            // Create the communications sessions
            fGroupEndPoint = new IPEndPoint(IPAddress.Parse("234.5.7.15"), 5004);
            fSession = new MultiSession(Guid.NewGuid().ToString(), fGroupEndPoint);

            fGraphicsChannel = fSession.CreateChannel(PayloadType.dynamicPresentation);
            fUserIOChannel = fSession.CreateChannel(PayloadType.xApplication2);

            
            fSnapper = new DesktopSnapper(fGraphicsChannel, fUserIOChannel, true);

            menu_Gray.Checked = fSnapper.SendLuminance;
            menu_RemoteControl.Checked = fSnapper.AllowRemoteControl;
            menu_Sharing.Checked = fSnapper.ActivelySharing;
        }


        void trayIcon_BalloonTipShown(object sender, EventArgs e)
        {
        }

        void DDHostTrayApp_MouseDown(object sender, MouseEventArgs e)
        {
        }

        #region Reacting to various events
        private void OnExit(object sender, EventArgs e)
        {
            // Dispose the snapper
            fSnapper.Stop();

            Application.ExitThread();
        }

        protected override void OnLoad(EventArgs e)
        {
            Visible = false; // Hide form window.   
            ShowInTaskbar = false; // Remove from taskbar.   

            base.OnLoad(e);
        }

        private void OnChangeGray(object sender, EventArgs e)
        {
            fSnapper.SendLuminance = !fSnapper.SendLuminance;
            menu_Gray.Checked = fSnapper.SendLuminance;
        }

        private void OnRemoteControl(object sender, EventArgs e)
        {
            fSnapper.AllowRemoteControl = !fSnapper.AllowRemoteControl;
            menu_RemoteControl.Checked = fSnapper.AllowRemoteControl;
        }

        private void OnChangeSharing(object sender, EventArgs e)
        {
            fSnapper.ActivelySharing = !fSnapper.ActivelySharing;
            menu_Sharing.Checked = fSnapper.ActivelySharing;
        }

        private void OnSettingsSelect(object sender, EventArgs e)
        {
            // Show a form so we can capture the desired group IP and port number
            ServerForm groupForm = new ServerForm();
            groupForm.ShowDialog();

        }

        #endregion

        #region Main Program Startup
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        /// <remarks>You only need to use the [STAThread] attribute
        /// if the application needs to be single threaded from a Windows Forms, or other frameworks.
        /// This is the case if you intend to capture screen shots to the Clipboard and Windows.Forms.Clipboard
        /// will only work in a single threaded environment.
        /// </remarks>
        //[STAThread]
        public static void Main()
        {
            Application.EnableVisualStyles();
            Application.Run(new DDHostTrayApp());

            //// Create an instance of the window
            //DesktopSnapper win = new DesktopSnapper();

            //// Use the NewTOAPIA application object just for fun
            //User32Application app = new User32Application();

            //// This will autmatically show the window, and when the 
            //// window is closed, the application will discontinue.
            //app.Run(win);
        }
        #endregion
    }
}
