using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Dynamixel.Driver;
using PCAN;


namespace GripperControler.Main.UI
{
    public partial class ctrGripper : UserControl
    {
        public ctrGripper()
        {
            InitializeComponent();
        }

        private void btnOpenGripper_Click(object sender, EventArgs e)
        {
            CANQueue.Instance.clearQueue();
            Dynamixel2CANQueue.openGripper((byte)txtPourcentage.Value);
            CANQueue.Instance.executeFirst();
        }

        private void btnCloseGripper_Click(object sender, EventArgs e)
        {
            CANQueue.Instance.clearQueue();
            Dynamixel2CANQueue.openGripper(0);
            CANQueue.Instance.executeFirst();
        }

    }
}
