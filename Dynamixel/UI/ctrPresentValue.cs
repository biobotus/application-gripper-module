using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Diagnostics;

using Dynamixel.Driver;
using Dynamixel.Events; 
using PCAN;

namespace GripperControler.Dynamixel.UI
{
    public partial class ctrPresentValue : UserControl
    {

        #region MEMBER
        private const byte MAX_DELAY_DATA = 50;
        private long[] delay = new long[MAX_DELAY_DATA];
        private byte curDelayIndex = 0;
        private byte nbOfDelayData = 0;
        private Stopwatch stopwatch = new Stopwatch();
        private uint counter = 0;

        private DynamixelData motor;
        #endregion


        #region INITIALIZATION
        public ctrPresentValue()
        {
            InitializeComponent();
            addPresentProperties();
            initDelay();
            DynamixelEvents.Instance.OnMotorSelectedChange += MotorDataReceived;
            PCANCom.Instance.OnMessageReceived += CANMessageReceived;
        }
        #endregion


        #region NEW MOTOR SELECTED MESSAGE (OBSERVER PATTERN)
        private void MotorDataReceived(object sender, DynamixelEvents.MotorSelectedChangeArgs e)
        {
            motor = e.motor;
            tmrGetPresentValue.Start();
        }
        #endregion


        #region CAN MESSAGE QUEUE GENERATION

        private void tmrGetPresentValue_Tick(object sender, EventArgs e)
        {
            generateCanQueueMessage(motor.id);
        }

        private void generateCanQueueMessage(byte motorID)
        {
            switch(counter) 
            {
                case 0:
                    stopwatch.Reset();
                    // RAM
                    // get the Present position and the present speed
                    Dynamixel2CANQueue.getPresentPositionAndPresentSpeed(DynamixelConst.PRESENT_POSITION_L, motorID);
                    CANQueue.Instance.executeLast();
                    stopwatch.Start();
                    break;
                case 1:
                    // get the present load, the present voltage and the present temperature
                    Dynamixel2CANQueue.getPresentLoadAndPresentVoltageAndPresentTemperature(DynamixelConst.PRESENT_LOAD_L, motorID);
                    break;
                case 2:
                    // get the present led status
                    Dynamixel2CANQueue.getLedStatus(DynamixelConst.LED, motorID);
                    break;
            }
            counter = (counter + 1) % 3;
        }
        #endregion


        #region CAN MESSAGE RECEIVED
        private void CANMessageReceived(object sender, PCANComEventArgs e)
        {
            if (e.CanMsg.DATA[1] == CANDeviceConstant.HARDWARE_FILTER_GRIPPER)
            {
                if (e.CanMsg.DATA[2] == DynamixelConst.PRESENT_POSITION_L || e.CanMsg.DATA[2] == DynamixelConst.PRESENT_LOAD_L)
                {
                    packetDecoder(e.CanMsg.DATA);
                }
                else if (e.CanMsg.DATA[2] == DynamixelConst.LED && e.CanMsg.DATA[3] == motor.id)
                {       
                    showError(e.CanMsg.DATA[4]);
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
                // fill the right componant with the right property value
                switch (packet[2])
                {
                    case DynamixelConst.PRESENT_POSITION_L:
                        displayCommunicationDelay();
                        dataGrid.Rows[0].Cells[2].Value = ((packet[5] << 8) | packet[4]).ToString(); // present position
                        dataGrid.Rows[1].Cells[2].Value = ((packet[7] << 8) | packet[6]).ToString(); // present speed
                        DynamixelEvents.Instance.postMessageBusEvent(DynamixelEvents.MessageBusType.PRESENT_POSITION_CHANGE, (uint)((packet[5] << 8) + packet[4]));
                        break;
                    case DynamixelConst.PRESENT_LOAD_L:
                        dataGrid.Rows[2].Cells[2].Value = (((packet[5] << 8) | packet[4]) & 0x3ff).ToString(); // present load
                        dataGrid.Rows[3].Cells[2].Value = (packet[6]/10).ToString() + "V"; // present voltage
                        dataGrid.Rows[4].Cells[2].Value = packet[7].ToString() + "°C"; // present temperature
                        break;
                }
            }
        }
        #endregion


        #region HELPER

        private void displayCommunicationDelay()
        {
            long mean = 0;
            stopwatch.Stop();
            delay[curDelayIndex++ % MAX_DELAY_DATA] = stopwatch.ElapsedMilliseconds;
            nbOfDelayData++;
            if (nbOfDelayData > MAX_DELAY_DATA) nbOfDelayData = MAX_DELAY_DATA;
            for (byte i = 0; i < MAX_DELAY_DATA; i++)
            {
                mean += delay[i];
            }
            mean /= nbOfDelayData;
            dataGrid.Rows[6].Cells[2].Value = mean.ToString() + "ms";
        }

        private void initDelay()
        {
            for (byte i = 0; i < MAX_DELAY_DATA; i++)
            {
                delay[i] = 0;
            }
        }

        private void addPresentProperties()
        {
            String memory = "RAM";
            addProperty(memory, "Present Position");
            addProperty(memory, "Present Speed");
            addProperty(memory, "Present Load");
            addProperty(memory, "Present Voltage");
            addProperty(memory, "Present Temperature");
            addProperty("", "Current Error");
            addProperty("", "Communication delay");
            dataGrid.AllowUserToAddRows = false;
        }

        private void addProperty(String memory, String propertyName)
        {
            DataGridViewRow row = (DataGridViewRow)dataGrid.Rows[0].Clone();
            row.Cells[0].Value = memory;
            row.Cells[1].Value = propertyName;
            row.ReadOnly = true;

            row.Cells[2].Value = "";    // value
            dataGrid.Rows.Add(row);
        }

        private void showError(byte error)
        {
            dataGrid.Rows[5].Cells[2].Value = motor.getErrorCodeMeaning(error);
        }

        #endregion

    }
}
