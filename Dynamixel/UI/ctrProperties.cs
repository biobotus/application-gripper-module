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
 * ctrProperties is used to show current ROM and RAM values of the selected motor
 * that can be modified (exept for the model number and the firmware number).
 * 
 * This component sends CAN messages to retrieve the information needed which are:
 *  
 * ROM:
 *  - Model Number
 *  - Version of Firmware
 *  - ID
 *  - Baud Rate
 *  - Return Delay Time
 *  - CW Angle Limit
 *  - CCW Angle Limit
 *  - Temperature Limit
 *  - Lowest Voltage Limit
 *  - Highest Voltage Limit
 *  - Max Torque
 *  - Status Return Level
 *  - Alarm LED
 *  - Alarm Shutdown
 *  
 * RAM:
 *  - Torque Enable
 *  - CW Compliance Margin
 *  - CCW Compliance Margin
 *  - CW Compliance Slope
 *  - CCW Compliance Slope
 *  - Goal Position
 *  - Moving Speed
 *  - Torque Limit
 *  - Lock
 *  - Punch
 * 
 * PSoC EEPROM:
 *  - Motor Type
 * 
 * To work properly, the motor type should be set before everything else. After
 * that the motor type has been set, a command should be sent to define the
 * angle limits associated with the motor type. This task can be done by simply
 * modifying the angle limits properties in the ROM. Those things must be done
 * in the right order to work, otherwise the "open gripper" CAN message won't
 * work.
 * 
 * */

namespace GripperControler.Dynamixel.UI
{
    public partial class ctrProperties : UserControl
    {

        #region MEMBER
        private const byte DATA_MODEL_FIRMWARE_ID = 0;
        private const byte DATA_BAUDRATE_RETURNDELAY_CWANGLE = 1;
        private const byte DATA_CCWANGLE_TEMPERATURE = 2;
        private const byte DATA_LOWESTVOLTAGE_HIGHESTVOLTAGE_MAXTORQUE = 3;
        private const byte DATA_STATUSLEVEL_ALARMLED_ALARMSHUTDOWN = 4;
        private const byte DATA_TORQUEENABLE_LED_CWMARGIN_CCWMARGIN = 5;
        private const byte DATA_CWSLOPE_CCWSLOPE_GOALPOSITION = 6;
        private const byte DATA_MOVINGSPEED_TORQUELIMIT = 7;
        private const byte DATA_LOCK_PUNCH = 8;
        private const byte DATA_GRIPPER = 9;

        private DynamixelData motor;
        #endregion

        #region INITIALIZAION
        public ctrProperties()
        {
            InitializeComponent();
        }

        private void ctrProperties_Load(object sender, EventArgs e)
        {
            addEEPROMProperties();
            addRAMProperties();
            addPSoCProperties();
            dataGrid.AllowUserToAddRows = false;
            dataGrid.Rows[0].ReadOnly = true;
            dataGrid.Rows[1].ReadOnly = true;

            PCANCom.Instance.OnCANMessageReceived += CANMessageReceived;
            DynamixelEvents.Instance.OnMotorSelectedChange += MotorDataReceived;
            DynamixelEvents.Instance.OnMessageBusEvent += OnMessageBusEventReceived;
        }
        #endregion

        #region NEW MOTOR SELECTED MESSAGE (OBSERVER PATTERN)
        private void MotorDataReceived(object sender, DynamixelEvents.MotorSelectedChangeArgs e)
        {
            motor = e.motor;
            generateCanQueueMessage(motor.id);
        }

        private void OnMessageBusEventReceived(object sender, DynamixelEvents.MessageBusArgs e)
        {
            if (e.type == DynamixelEvents.MessageBusType.GOAL_POSITION_CHANGE)
            {
                if (e.value != int.Parse(dataGrid.Rows[19].Cells[2].Value.ToString()))
                    Dynamixel2CANQueue.sendInstruction(DynamixelCANInstruction.MOVE, motor.id, (ushort)e.value);

                dataGrid.Rows[19].Cells[2].Value = (e.value).ToString(); // goal position
            }
        }
        #endregion

        #region CAN MESSAGE RECEIVED
        private void CANMessageReceived(object sender, PCANComEventArgs e)
        {
            if (e.CanMsg.DATA[1] == CANDeviceConstant.HARDWARE_FILTER_GRIPPER)
            {
                packetDecoder(e.CanMsg.DATA);
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
                    case DynamixelConst.MODEL_NUMBER_L:
                        dataGrid.Rows[0].Cells[2].Value = ((packet[5] << 8) | packet[4]).ToString(); // model number
                        dataGrid.Rows[1].Cells[2].Value = packet[6].ToString(); // version firmware
                        dataGrid.Rows[2].Cells[2].Value = packet[3].ToString(); // id
                        break;
                    case DynamixelConst.BAUD_RATE:
                        dataGrid.Rows[3].Cells[2].Value = packet[4].ToString(); // baudrate
                        dataGrid.Rows[4].Cells[2].Value = packet[5].ToString(); // return delay time
                        dataGrid.Rows[5].Cells[2].Value = ((packet[7] << 8) | packet[6]).ToString(); // CW angle limit
                        DynamixelEvents.Instance.postMessageBusEvent(DynamixelEvents.MessageBusType.CW_ANGLE_LIMIT_CHANGE, (uint)((packet[7] << 8) + packet[6]));
                        break;
                    case DynamixelConst.CCW_ANGLE_LIMIT_L:
                        dataGrid.Rows[6].Cells[2].Value = ((packet[5] << 8) | packet[4]).ToString(); // CCW angle limit
                        dataGrid.Rows[7].Cells[2].Value = packet[7].ToString(); // temperature limit
                        DynamixelEvents.Instance.postMessageBusEvent(DynamixelEvents.MessageBusType.CCW_ANGLE_LIMIT_CHANGE, (uint)((packet[5] << 8) + packet[4]));
                        break;
                    case DynamixelConst.LOWEST_LIMIT_VOLTAGE:
                        dataGrid.Rows[8].Cells[2].Value = packet[4].ToString(); // lowest limit voltage
                        dataGrid.Rows[9].Cells[2].Value = packet[5].ToString(); // highest limit voltage
                        dataGrid.Rows[10].Cells[2].Value = ((packet[7] << 8) | packet[6]).ToString(); // max torque
                        break;
                    case DynamixelConst.RETURN_LEVEL:
                        dataGrid.Rows[11].Cells[2].Value = packet[4].ToString(); // status return level
                        dataGrid.Rows[12].Cells[2].Value = packet[5].ToString(); // alarm led
                        dataGrid.Rows[13].Cells[2].Value = packet[6].ToString(); // alarm shutdown
                        break;
                    case DynamixelConst.TORQUE_ENABLE:
                        dataGrid.Rows[14].Cells[2].Value = packet[4].ToString(); // torque enable
                        //dataGrid.Rows[15].Cells[2].Value = packet[5].ToString(); // led
                        dataGrid.Rows[15].Cells[2].Value = packet[6].ToString(); // cw compliance margin
                        dataGrid.Rows[16].Cells[2].Value = packet[7].ToString(); // ccw compliance margin
                        break;
                    case DynamixelConst.CW_COMPLIANCE_SLOPE:
                        dataGrid.Rows[17].Cells[2].Value = packet[4].ToString(); // cw compliance slope
                        dataGrid.Rows[18].Cells[2].Value = packet[5].ToString(); // ccw compliance slope
                        dataGrid.Rows[19].Cells[2].Value = ((packet[7] << 8) + packet[6]).ToString(); // goal position
                        DynamixelEvents.Instance.postMessageBusEvent(DynamixelEvents.MessageBusType.GOAL_POSITION_CHANGE, (uint)((packet[7] << 8) + packet[6]));
                        break;
                    case DynamixelConst.MOVING_SPEED_L:
                        dataGrid.Rows[20].Cells[2].Value = ((packet[5] << 8) + packet[4]).ToString(); // moving speed
                        dataGrid.Rows[21].Cells[2].Value = ((packet[7] << 8) + packet[6]).ToString(); // torque limit
                        break;
                    case DynamixelConst.LOCK:
                        dataGrid.Rows[22].Cells[2].Value = (packet[4]).ToString(); // lock
                        dataGrid.Rows[23].Cells[2].Value = ((packet[6] << 8) + packet[5]).ToString(); // punch
                        break;
                    case DynamixelCANInstruction.GET_MOTOR_TYPE:
                        switch (packet[4])
                        {
                            case 0:
                                dataGrid.Rows[24].Cells[2].Value = "None";
                                break;
                            case 1:
                                dataGrid.Rows[24].Cells[2].Value = "Rotate Motor";
                                break;
                            case 2:
                                dataGrid.Rows[24].Cells[2].Value = "Tilt Motor";
                                break;
                            case 3:
                                dataGrid.Rows[24].Cells[2].Value = "Right Motor";
                                break;
                            case 4:
                                dataGrid.Rows[24].Cells[2].Value = "Left Motor";
                                break;
                        }
                        break;
                }
            }
        }
        #endregion

        #region CAN MESSAGE QUEUE GENERATION

        private void generateCanQueueMessage(byte motorID)
        {
            for (byte i = 0; i < 10; i++)
            {
                addToCanQueue(motorID, i);
            }
        }

        private void addToCanQueue(byte motorID, byte constData)
        {
            switch (constData)
            {
    /* ROM */   case DATA_MODEL_FIRMWARE_ID: // to get the "model number", the "version firmware"
                    Dynamixel2CANQueue.getModelAndFirmware(DynamixelConst.MODEL_NUMBER_L, motorID);
                    break;
                case DATA_BAUDRATE_RETURNDELAY_CWANGLE: // to get the "baudrate", "return delay time" and "CW angle limit" 
                    Dynamixel2CANQueue.getBaudrateAndReturnDelayTimeAndCWAngleLimit(DynamixelConst.BAUD_RATE, motorID);
                    break;
                case DATA_CCWANGLE_TEMPERATURE: // to get the "CCW angle limit" and "temperature limit"
                    Dynamixel2CANQueue.getCCWAngleLimitAndTemperatureLimit(DynamixelConst.CCW_ANGLE_LIMIT_L, motorID);
                    break;
                case DATA_LOWESTVOLTAGE_HIGHESTVOLTAGE_MAXTORQUE: // to get the "lowest limit voltage", "highest limit voltage" and "max torque"
                    Dynamixel2CANQueue.getLowestLimitVoltageAndHighestLimitVoltageAndMaxTorque(DynamixelConst.LOWEST_LIMIT_VOLTAGE, motorID);
                    break;
                case DATA_STATUSLEVEL_ALARMLED_ALARMSHUTDOWN: // to get the "status return level", "alarm led" and "alarm shutdown"
                    Dynamixel2CANQueue.getStatusReturnLevelAndAlarmLedAndAlarmShutdown(DynamixelConst.RETURN_LEVEL, motorID);
                    break;

    /* RAM */   case DATA_TORQUEENABLE_LED_CWMARGIN_CCWMARGIN: // to get the "torque enable", "led", "cw compliance margin" and "ccw compliance margin"
                    Dynamixel2CANQueue.getTorqueEnableAndLedAndCWComplianceMarginAndCCWComplianceMargin(DynamixelConst.TORQUE_ENABLE, motorID);
                    break;
                case DATA_CWSLOPE_CCWSLOPE_GOALPOSITION:// to get the "cw compliance slope", "ccw compliance slope" and "goal position"
                    Dynamixel2CANQueue.getCWComplianceSlopeAndCCWComplianceSlopeAndGoalPosition(DynamixelConst.CW_COMPLIANCE_SLOPE, motorID);
                    break;
                case DATA_MOVINGSPEED_TORQUELIMIT: // to get the "moving speed" and "torque limit"
                    Dynamixel2CANQueue.getMovingSpeedAndTorqueLimit(DynamixelConst.MOVING_SPEED_L, motorID);
                    break;
                case DATA_LOCK_PUNCH: // to get "lock" and "punch" properties
                    Dynamixel2CANQueue.getLockAndPunch(DynamixelConst.LOCK, motorID);
                    break;

   /* PSOC */  case DATA_GRIPPER: // to get the motor type (rotate, tilt, right, left)
                    Dynamixel2CANQueue.addGetMotorType(DynamixelCANInstruction.GET_MOTOR_TYPE, motorID);
                    break;
            }
        }


        #endregion

        #region REFRESH PROPERTY DATA

        private void dataGrid_ColumnHeaderMouseDoubleClick(object sender, DataGridViewCellMouseEventArgs e)
        {
            if (e.ColumnIndex == 1) // column of properties
            {
                generateCanQueueMessage(motor.id);
            }
        }

        #endregion

        #region Adding Properties To The DataGrid

        private void addEEPROMProperties()
        {
            String memory = "ROM";
            addProperty(memory, "Model Number");
            addProperty(memory, "Version of Firmware");
            addProperty(memory, "ID", "1");

            addProperty(memory, "Baud Rate", "1 (which means 1Mbps)");
            addProperty(memory, "Return Delay Time", "250 (The delay time is given by 2uSec * value)");
            addProperty(memory, "CW Angle Limit");

            addProperty(memory, "CCW Angle Limit");
            addProperty(memory, "Temperature Limit", "85");

            addProperty(memory, "Lowest Voltage Limit", "60");
            addProperty(memory, "Highest Voltage Limit", "190");
            addProperty(memory, "Max Torque", "1023");

            addProperty(memory, "Status Return Level", "2");
            addProperty(memory, "Alarm LED", "4");
            addProperty(memory, "Alarm Shutdown", "4");
        }

        private void addRAMProperties()
        {
            String memory = "RAM";
            addProperty(memory, "Torque Enable");
            addProperty(memory, "CW Compliance Margin", "0");
            addProperty(memory, "CCW Compliance Margin", "0");

            addProperty(memory, "CW Compliance Slope", "32");
            addProperty(memory, "CCW Compliance Slope", "32");
            addProperty(memory, "Goal Position", "The value of Present Position");

            addProperty(memory, "Moving Speed", "0");
            addProperty(memory, "Torque Limit", "The value of Max Torque");

            addProperty(memory, "Lock", "0");
            addProperty(memory, "Punch", "32");

        }

        private void addPSoCProperties()
        {
            int index = addProperty("PSOC5", "Motor Type");

            DataGridViewComboBoxCell cmb = new DataGridViewComboBoxCell();
            cmb.ValueMember = "ID";
            cmb.DisplayMember = "Item";
            cmb.Items.Add("None");
            cmb.Items.Add("Rotate Motor");
            cmb.Items.Add("Tilt Motor");
            cmb.Items.Add("Right Motor");
            cmb.Items.Add("Left Motor");
            dataGrid.Rows[index].Cells[2] = cmb;
            dataGrid.Rows[index].Cells[2].Value = "None";
        }

        private int addProperty(String memory, String propertyName)
        {
            DataGridViewRow row = (DataGridViewRow)dataGrid.Rows[0].Clone();
            row.Cells[0].Value = memory;
            row.Cells[1].Value = propertyName;
            
            row.Cells[2].Value = "";    // value
            return dataGrid.Rows.Add(row);
        }

        private int addProperty(String memory, String propertyName, String initialValue)
        {
            DataGridViewRow row = (DataGridViewRow)dataGrid.Rows[0].Clone();
            row.Cells[0].Value = memory;
            row.Cells[1].Value = propertyName;

            row.Cells[2].Value = "";    // value
            row.Cells[2].ToolTipText = "Initial Value: " + initialValue;

            return dataGrid.Rows.Add(row);
        }

        #endregion

        #region PROPERTY CHANGED

        private void dataGrid_CellEndEdit(object sender, DataGridViewCellEventArgs e)
        {
            ushort value = 0;
            if (e.RowIndex >= 0 && e.RowIndex < 25)
            {
                if (dataGrid.Rows[e.RowIndex].Cells[e.ColumnIndex].Value != null)
                {
                    try
                    {
                        if (e.RowIndex == 24)
                        {
                            switch (dataGrid.Rows[e.RowIndex].Cells[2].Value.ToString())
                            {
                                case "None": value = 0; break;
                                case "Rotate Motor": value = 1; break;
                                case "Tilt Motor": value = 2; break;
                                case "Right Motor": value = 3; break;
                                case "Left Motor": value = 4; break;
                            }
                        }
                        else
                        {
                            value = ushort.Parse(dataGrid.Rows[e.RowIndex].Cells[2].Value.ToString());
                        }
                        if (value > 4095) throw new FormatException();

                        switch (e.RowIndex)
                        {
                            case 2: // id
                                if (value > 254)
                                    MessageBox.Show("The value must be a number between 0 and 253.", "Error");
                                else
                                {
                                    Dynamixel2CANQueue.setMotorID(motor.id, (byte)value);
                                    addToCanQueue(motor.id, DATA_MODEL_FIRMWARE_ID);
                                }
                                break;
                            case 3: // baudrate
                                if (MessageBox.Show(this, "WARNING: Changing the baudrate will cause the motor to stop responding unless you modify the microcontroler communication rate also. The new baudrate will be " + ((2000000) / (value + 1)).ToString() + "baud.", "WARNING", MessageBoxButtons.OKCancel) == DialogResult.OK)
                                {
                                    Dynamixel2CANQueue.setBaudrate(motor.id, (byte)value);
                                    addToCanQueue(motor.id, DATA_BAUDRATE_RETURNDELAY_CWANGLE);
                                }
                                break;
                            case 4: // return delay time                                
                                Dynamixel2CANQueue.setReturnDelayTime(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_BAUDRATE_RETURNDELAY_CWANGLE);
                                break;
                            case 5: // CW angle limit
                                Dynamixel2CANQueue.setCWAngleLimit(motor.id, value);
                                addToCanQueue(motor.id, DATA_BAUDRATE_RETURNDELAY_CWANGLE);
                                break;
                            case 6: // CCW angle limit
                                Dynamixel2CANQueue.setCCWAngleLimit(motor.id, value);
                                addToCanQueue(motor.id, DATA_CCWANGLE_TEMPERATURE);
                                break;
                            case 7: // temperature limit
                                Dynamixel2CANQueue.setTemperatureLimit(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_CCWANGLE_TEMPERATURE);
                                break;
                            case 8: // lowest limit voltage
                                Dynamixel2CANQueue.setLowestLimitVoltage(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_LOWESTVOLTAGE_HIGHESTVOLTAGE_MAXTORQUE);
                                break;
                            case 9: // highest limit voltage
                                Dynamixel2CANQueue.setHighestLimitVoltage(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_LOWESTVOLTAGE_HIGHESTVOLTAGE_MAXTORQUE);
                                break;
                            case 10:// max torque
                                Dynamixel2CANQueue.setMaxTorque(motor.id, value);
                                addToCanQueue(motor.id, DATA_LOWESTVOLTAGE_HIGHESTVOLTAGE_MAXTORQUE);
                                break;
                            case 11:// status return level
                                Dynamixel2CANQueue.setStatusReturnLevel(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_STATUSLEVEL_ALARMLED_ALARMSHUTDOWN);
                                break;
                            case 12:// alarm led
                                Dynamixel2CANQueue.setAlarmLed(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_STATUSLEVEL_ALARMLED_ALARMSHUTDOWN);
                                break;
                            case 13:// alarm shutdown
                                Dynamixel2CANQueue.setAlarmShutdown(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_STATUSLEVEL_ALARMLED_ALARMSHUTDOWN);
                                break;
                            case 14:// torque enable
                                Dynamixel2CANQueue.setTorqueEnable(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_TORQUEENABLE_LED_CWMARGIN_CCWMARGIN);
                                break;
                            case 15:// cw compliance margin
                                Dynamixel2CANQueue.setCWComplianceMargin(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_TORQUEENABLE_LED_CWMARGIN_CCWMARGIN);
                                break;
                            case 16:// ccw compliance margin
                                Dynamixel2CANQueue.setCCWComplianceMargin(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_TORQUEENABLE_LED_CWMARGIN_CCWMARGIN);
                                break;
                            case 17:// cw compliance slope
                                Dynamixel2CANQueue.setCWComplianceSlope(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_CWSLOPE_CCWSLOPE_GOALPOSITION);
                                break;
                            case 18:// ccw compliance slope
                                Dynamixel2CANQueue.setCCWComplianceSlope(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_CWSLOPE_CCWSLOPE_GOALPOSITION);
                                break;
                            case 19:// goal position
                                Dynamixel2CANQueue.setGoalPosition(motor.id, value);
                                addToCanQueue(motor.id, DATA_CWSLOPE_CCWSLOPE_GOALPOSITION);
                                break;
                            case 20:// moving speed
                                Dynamixel2CANQueue.setMovingSpeed(motor.id, value);
                                addToCanQueue(motor.id, DATA_MOVINGSPEED_TORQUELIMIT);
                                break;
                            case 21:// torque limit
                                Dynamixel2CANQueue.setTorqueLimit(motor.id, value);
                                addToCanQueue(motor.id, DATA_MOVINGSPEED_TORQUELIMIT);
                                break;
                            case 22: // lock
                                Dynamixel2CANQueue.setLock(motor.id, (byte)value);
                                addToCanQueue(motor.id, DATA_LOCK_PUNCH);
                                break;
                            case 23: // punch
                                Dynamixel2CANQueue.setPunch(motor.id, value);
                                addToCanQueue(motor.id, DATA_LOCK_PUNCH);
                                break;
                            case 24:
                                Dynamixel2CANQueue.sendInstruction(DynamixelCANInstruction.SET_MOTOR_TYPE, motor.id, value);
                                addToCanQueue(motor.id, DATA_GRIPPER);
                                break;
                        }
                    }
                    catch (FormatException ex)
                    {
                        MessageBox.Show("The value must be a number.", "Error");
                    }
                }
            }
        }

        #endregion

    }
}