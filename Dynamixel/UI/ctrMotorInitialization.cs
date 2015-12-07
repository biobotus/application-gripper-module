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

/* Autor: Dave Plouffe
 * 
 * ctrMotorInitialization is used to initialize new dynamixel motors.
 * 
 * When you buy a brand new Dynamixel motor, it comes with default parameters 
 * which prevent the application to work correctly and detecting it. To configure your 
 * brand new Dynamixel motor, please select one id that isn’t used by any of your motors 
 * and then click the button “initialize motor”. The id must be between 0 and 253 inclusively. 
 * You’ll be able to change it after the configuration process. If you have more than one new 
 * motor to configure, you'll have to initialize them one by one. The initialization won’t affect 
 * motors already configured even if they are plugged in because they don't respond to the
 * same baudrate.
 * 
 * */

namespace GripperControler.Dynamixel.UI
{
    public partial class ctrMotorInitialization : UserControl
    {
        public ctrMotorInitialization()
        {
            InitializeComponent();
        }

        private void btnInitialize_Click(object sender, EventArgs e)
        {
            /* Initialization steps:
             * 
             *  1. Change the communication rate between the dynamixel and the motor to 1Mbps
             *  2. Set the alarm shutdown to stop the torque only when an overheating error occurs
             *  3. Set the maximum torque in the ROM and in the RAM
             *  4. Set the status return level to 1 which mean the motor will respond only to READ_DATA instructions
             *  5. Set the return delay to 50
             *  5. Set the ID with the value of the component "txtID"
             *  6. Set the baudrate at 0.5Mbps
             *  7. Switch back the communcation rate between the dynamixel and the motor to 0.5Mbps
             */ 
            Dynamixel2CANQueue.sendMessage(DynamixelCANInstruction.SET_CLOCK_8M, 0, 0, 0, 0, 0, 0, 0);
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.ALARM_SHUTDOWN, 1, 0x04);
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.MAX_TORQUE_L, 2, 200);
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.TORQUE_LIMIT_L, 2, 200);
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.RETURN_LEVEL, 1, 1);
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.RETURN_DELAY_TIME, 1, 50);
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.ID, 1, (byte)(txtID.Value));
            Dynamixel2CANQueue.addSetInstruction(DynamixelConst.BROADCAST_ID, DynamixelConst.BAUD_RATE, 1, 0x03);
            Dynamixel2CANQueue.sendMessage(DynamixelCANInstruction.SET_CLOCK_4M, 0, 0, 0, 0, 0, 0, 0);
        }
    }
}
