using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO.Ports;

using Dynamixel.Driver;
using PCAN;

namespace GripperControler
{
    public partial class frmGripper : Form
    {

        #region INITIALIZATION
        public frmGripper()
        {
            InitializeComponent();
        }
        #endregion


        private void frmGripper_FormClosing(object sender, FormClosingEventArgs e)
        {
            PCANCom.Instance.disconnect();
        }

        private void tmrSendCANMessage_Tick(object sender, EventArgs e)
        {
            CANQueue.Instance.executeFirst();
        }

        private void frmGripper_Load(object sender, EventArgs e)
        {
            ctrCanConnector1.btnConnect_Click(sender, e);
        }

    }
}
