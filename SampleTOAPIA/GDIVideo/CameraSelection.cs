using System.Windows.Forms;

using NewTOAPIA.Media.Capture;

namespace GDIVideo
{
    public partial class CameraSelection : Form
    {
        CaptureDeviceSetupPage fSetupControl;

        public CameraSelection()
        {
            InitializeComponent();

            fSetupControl = new CaptureDeviceSetupPage();
            this.Controls.Add(fSetupControl);
        }

        public CaptureDeviceSetupPage SetupPage
        {
            get { return fSetupControl; }
        }
    }
}
