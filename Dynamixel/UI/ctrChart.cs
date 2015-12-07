using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.DataVisualization.Charting;

using Dynamixel.Driver;
using Dynamixel.Events;
using PCAN;

/* Autor: Dave Plouffe
 * 
 * ctrChart is used to plot the current temperature and load of a motor.
 * 
 * This component isn't used anymore but have not been suppressed as
 * it could be useful in some way in the future.
 * 
 * */

namespace GripperControler.Dynamixel.UI
{
    public partial class ctrChart : UserControl
    {
        #region MEMBER
        private int x = 0;
        private DynamixelData motor;
        private bool bPlotChartData = false;
        #endregion

        #region INITIALIZATION
        public ctrChart()
        {
            InitializeComponent();
            lblCurrentInfo.Text = "";
            PCANCom.Instance.OnCANMessageReceived += CANMessageReceived;
            DynamixelEvents.Instance.OnMotorSelectedChange += MotorDataReceived;
        }
        
        private void ctrChart_Load(object sender, EventArgs e)
        {
            chart.Series[0].ChartType = SeriesChartType.Spline;
            chart.Series[0].Color = Color.Red;

            chart.Series[1].ChartType = SeriesChartType.Spline;
            chart.Series[1].Color = Color.Blue;
            clearChart();
        }
        #endregion

        #region NEW MOTOR SELECTED MESSAGE (OBSERVER PATTERN)
        private void MotorDataReceived(object sender, DynamixelEvents.MotorSelectedChangeArgs e)
        {
            motor = e.motor;
            clearChart();
            tmrGetTemperatureAndLoad.Start();
        }
        #endregion

        #region CHART CONTROL (ADD DATA, CLEAR)
        public void addTemperature(float value)
        {
            chart.Series[0].Points.AddXY(x, value);
        }

        public void addMotorLoad(float value)
        {
            chart.Series[1].Points.AddXY(x, value);
        }

        public void increaseX()
        {
            x++;
        }

        public void clearChart()
        {
            chart.Series[0].Points.Clear();
            chart.Series[1].Points.Clear();
            x = 0;
            addTemperature(0);
            addMotorLoad(0);
            increaseX();
        }

        private void chart_Click(object sender, EventArgs e)
        {
            if (bPlotChartData == false) clearChart();
            bPlotChartData = !bPlotChartData;
        }

        #endregion

        #region CAN MESSAGE RECEIVED
        private void CANMessageReceived(object sender, PCANComEventArgs e)
        {
            if (e.CanMsg.DATA[0] == CANDeviceConstant.HARDWARE_FILTER_GRIPPER)
            {
                if (e.CanMsg.DATA[2] == DynamixelConst.PRESENT_TEMPERATURE)
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
                ushort curLoad = (ushort)((0x3ff) & ((packet[5] << 8) + packet[4]));
                if (bPlotChartData)
                {
                    addMotorLoad(curLoad);
                    addTemperature(packet[7]);
                    increaseX();
                }
                lblCurrentInfo.Text = "Current Temperature: " + packet[7].ToString() + "°C - Current Load: " + curLoad.ToString();
            }
        }
        #endregion

        #region SEND GET TEMPERATURE AND LOAD COMMAND
        private void tmrGetTemperatureAndLoad_Tick(object sender, EventArgs e)
        {
            Dynamixel2CANQueue.addGetInstruction(DynamixelConst.PRESENT_TEMPERATURE, motor.id, DynamixelConst.PRESENT_LOAD_L, 4);
            //CANQueue.Instance.executeLast();
        }
        #endregion

    }
}
