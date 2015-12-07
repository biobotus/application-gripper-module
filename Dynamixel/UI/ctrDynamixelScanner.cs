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
using Dynamixel.Events;
using PCAN;

/* Autor: Dave Plouffe
 * 
 * ctrDynamixelScanner is used to detect all motors that are connected together
 * on the board.
 * 
 * Basicly, when the button "scan" is clicked, a timer is set to enable.
 * Then, each time the timer ticks, a CAN command is sent
 * to retrieve the Model, the Firmware of the motor id. If the motor id
 * exist, a CAN message will be received with the information wanted.
 * 
 * The list of the existing motors are added to the component "lstDynamixel"
 * according to the group they belong.
 * 
 * When a motor is selected, a message is posted to update all the
 * components they need to know with which motor we are dealing with.
 * 
 * */

namespace GripperControler.Dynamixel.UI
{

    public partial class ctrDynamixelScanner : UserControl
    {
        #region MEMBER
        private byte MotorSearchID;
        private List<DynamixelData> motors = new List<DynamixelData>();
        #endregion

        #region INITIALIZATION
        public ctrDynamixelScanner()
        {
            InitializeComponent();
            PCANCom.Instance.OnCANMessageReceived += CANMessageReceived;
        }
        #endregion

        #region SCAN CONTROL
        private void btnScan_Click(object sender, EventArgs e)
        {
            if (timerScan.Enabled)
                stopSearchMotor();
            else
                startSearchMotor();
        }

        private void timerScan_Tick(object sender, EventArgs e)
        {
            if (MotorSearchID == 255)
                stopSearchMotor();
            else
            {
                getMotorInfo(MotorSearchID++);
                progressBar.Value = MotorSearchID;
            }
        }
        #endregion

        #region CAN MESSAGE RECEIVED
        private void CANMessageReceived(object sender, PCANComEventArgs e)
        {
            if (e.CanMsg.DATA[1] == CANDeviceConstant.HARDWARE_FILTER_GRIPPER && timerScan.Enabled == true)
            {
                if (e.CanMsg.DATA[2] == DynamixelConst.MODEL_NUMBER_L)
                {
                    packetDecoder(e.CanMsg.DATA);
                }
            }
        }
        #endregion

        #region CAN MESSAGE TREATMENT

        private void packetDecoder(byte[] packet)
        {
            if (this.InvokeRequired)
            {
                this.Invoke(new Action<byte[]>(packetDecoder), packet);
            }
            else
            {
                DynamixelData motor = new DynamixelData();
                motor.id = packet[3];
                motor.model = (ushort)((packet[5] << 8) + packet[4]);
                motor.firmware = packet[6];
                motors.Add(motor);

                addMotorToListView(motor);
            }
        }
        #endregion

        #region POST THE NEW MOTOR ID TO LISTENNERS (OBSERVER PATTERN)
        private void lstDynamixel_SelectedIndexChanged(object sender, EventArgs e)
        {
            try
            {
                int index = lstDynamixel.SelectedItems[0].Index;
                CANQueue.Instance.clearQueue();
                DynamixelEvents.Instance.postMotorSelectedChangeEvent(motors[index]);
            }
            catch (Exception ex) { }
        }
        #endregion

        #region HELPERS

        private void addMotorToListView(DynamixelData motor)
        {
            int group;
            switch (motor.model)
            {
                case (ushort)DynamixelModel.AX12A:
                    group = 0;
                    break;
                case (ushort)DynamixelModel.MX12W:
                    group = 1;
                    break;
                default:
                    group = 2;
                    break;
            }

            ListViewItem i = new ListViewItem(lstDynamixel.Groups[group]);
            i.Text = "Motor " + motor.id.ToString();
            lstDynamixel.Items.Add(i);
        }

        private void startSearchMotor()
        {
            lstDynamixel.Items.Clear();
            motors.Clear();
            MotorSearchID = 0;
            progressBar.Value = 0;
            progressBar.Visible = true;
            timerScan.Start();
            btnScan.Text = "Stop";
        }


        private void stopSearchMotor()
        {
            timerScan.Stop();
            progressBar.Visible = false;
            btnScan.Text = "Scan";
        }


        private void getMotorInfo(byte motorID)
        {
            // Retreive Model, Firmware if the motor exist
            Dynamixel2CANQueue.getModelAndFirmware(DynamixelConst.MODEL_NUMBER_L, motorID);
        }

        #endregion
    }
    
}
