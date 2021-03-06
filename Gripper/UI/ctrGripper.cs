﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Gripper.Driver;
using PCAN.Driver;

/* Autor: Dave Plouffe
 * 
 * ctrGripper is used for testing purpose.
 * 
 * Depending on the button clicked, it sends
 * a CAN message to the gripper to open the
 * gripper according to the percentage written
 * in the component "txtPercentage" value.
 * 
 * It can also send a CAN message to close the
 * gripper. Closing the gripper is the same as
 * opening it but with a 0 percent value.
 * 
 * */

namespace Gripper.UI
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
            Gripper2CANQueue.openGripper((byte)txtPercentage.Value);
            CANQueue.Instance.executeFirst();
        }

        private void btnCloseGripper_Click(object sender, EventArgs e)
        {
            CANQueue.Instance.clearQueue();
            Gripper2CANQueue.openGripper(0);
            CANQueue.Instance.executeFirst();
        }

    }
}
