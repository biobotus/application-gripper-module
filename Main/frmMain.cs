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

using Gripper.UI;
using PCAN.Driver;

/* Autor: Dave Plouffe
 * 
 * frmGripper is the application startup.
 * 
 * */

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

        /*
         * Every message that are sent to the CAN Queue will be sent to 
         * the CAN bus every time the timer "tmrSendCANMessage" ticks.
         * */
        private void tmrSendCANMessage_Tick(object sender, EventArgs e)
        {
            CANQueue.Instance.executeFirst();
        }

        /*
         * This line of code is useful to connect the application
         * on the CAN bus without clicking the "connect" button
         * */
        private void frmGripper_Load(object sender, EventArgs e)
        {
            ctrCanConnector1.btnConnect_Click(sender, e);
        }

    }
}
