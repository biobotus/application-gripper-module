using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using PCAN;
using Peak.Can.Basic;

/* Autor: Dave Plouffe
 * 
 * Dynamixel2CANQueue is used to send CAN message to the CAN queue.
 * 
 * This class contains every CAN message that can be sent to
 * the gripper module.
 * 
 * */

namespace Dynamixel.Driver
{
    class Dynamixel2CANQueue
    {

        #region ROM

        #region GET PROPERTIES
        public static void getModelAndFirmware(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.MODEL_NUMBER_L, 3);
        }

        public static void getLedStatus(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.LED, 1);
        }

        public static void getBaudrateAndReturnDelayTimeAndCWAngleLimit(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.BAUD_RATE, 4);
        }

        public static void getCCWAngleLimitAndTemperatureLimit(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.CCW_ANGLE_LIMIT_L, 4);
        }

        public static void getLowestLimitVoltageAndHighestLimitVoltageAndMaxTorque(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.LOWEST_LIMIT_VOLTAGE, 4);
        }

        public static void getStatusReturnLevelAndAlarmLedAndAlarmShutdown(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.RETURN_LEVEL, 3);
        }
        #endregion

        #region SET PROPERTIES

        public static void setMotorID(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.ID, 1, value);
        }

        public static void setBaudrate(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.BAUD_RATE, 1, value);
        }

        public static void setReturnDelayTime(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.RETURN_DELAY_TIME, 1, value);
        }

        public static void setCWAngleLimit(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.CW_ANGLE_LIMIT_L, 2, value);
        }

        public static void setCCWAngleLimit(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.CCW_ANGLE_LIMIT_L, 2, value);
        }

        public static void setTemperatureLimit(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.TEMPERATURE_LIMIT, 1, value);
        }

        public static void setLowestLimitVoltage(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.LOWEST_LIMIT_VOLTAGE, 1, value);
        }

        public static void setHighestLimitVoltage(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.HIGHEST_LIMIT_VOLTAGE, 1, value);
        }

        public static void setMaxTorque(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.MAX_TORQUE_L, 2, value);
        }

        public static void setStatusReturnLevel(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.RETURN_LEVEL, 1, value);
        }

        public static void setAlarmLed(byte motorID, byte value) 
        {
            addSetInstruction(motorID, DynamixelConst.ALARM_LED, 1, value);
        }

        public static void setAlarmShutdown(byte motorID, byte value) 
        {
            addSetInstruction(motorID, DynamixelConst.ALARM_SHUTDOWN, 1, value);
        }

        #endregion

        #endregion

        #region RAM

        #region GET PROPERTIES
        public static void getTorqueEnableAndLedAndCWComplianceMarginAndCCWComplianceMargin(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.TORQUE_ENABLE, 4);
        }

        public static void getCWComplianceSlopeAndCCWComplianceSlopeAndGoalPosition(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.CW_COMPLIANCE_SLOPE, 4);
        }

        public static void getMovingSpeedAndTorqueLimit(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.MOVING_SPEED_L, 4);
        }

        public static void getLockAndPunch(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.LOCK, 3);
        }

        public static void getPresentPositionAndPresentSpeed(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.PRESENT_POSITION_L, 4);
        }

        public static void getPresentLoadAndPresentVoltageAndPresentTemperature(byte treatingFunctionID, byte motorID)
        {
            addGetInstruction(treatingFunctionID, motorID, DynamixelConst.PRESENT_LOAD_L, 4);
        }
        #endregion

        #region SET PROPERTIES
        public static void setTorqueEnable(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.TORQUE_ENABLE, 1, value);
        }

        public static void setCWComplianceMargin(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.CW_COMPLIANCE_MARGIN, 1, value);
        }

        public static void setCCWComplianceMargin(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.CCW_COMPLIANCE_MARGIN, 1, value);
        }

        public static void setCWComplianceSlope(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.CW_COMPLIANCE_SLOPE, 1, value);
        }

        public static void setCCWComplianceSlope(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.CCW_COMPLIANCE_SLOPE, 1, value);
        }

        public static void setGoalPosition(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.GOAL_POSITION_L, 2, value);
        }

        public static void setMovingSpeed(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.MOVING_SPEED_L, 2, value);
        }

        public static void setTorqueLimit(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.TORQUE_LIMIT_L, 2, value);
        }

        public static void setLock(byte motorID, byte value)
        {
            addSetInstruction(motorID, DynamixelConst.LOCK, 1, value);
        }

        public static void setPunch(byte motorID, ushort value)
        {
            addSetInstruction(motorID, DynamixelConst.PUNCH_L, 2, value);
        }

        #endregion

        #endregion

        #region GRIPPER CONTROL

        public static void openGripper(byte percent)
        {
            if (percent > 100) percent = 100;
            sendMessage(DynamixelCANInstruction.OPEN_GRIPPER, percent, 0, 0, 0, 0, 0, 0);
        }

        public static void tiltGripper(byte percent)
        {
            if (percent > 100) percent = 100;
            sendMessage(DynamixelCANInstruction.TILT, percent, 0, 0, 0, 0, 0, 0);
        }

        public static void rotateGripper(byte percent)
        {
            if (percent > 100) percent = 100;
            sendMessage(DynamixelCANInstruction.ROTATE, percent, 0, 0, 0, 0, 0, 0);
        }

        #endregion

        #region HELPER

        public static void sendMessage(byte instruction, byte byte1, byte byte2, byte byte3, byte byte4, byte byte5, byte byte6, byte byte7)
        {
            TPCANMsg CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.DATA[0] = instruction;
            CANMsg.DATA[1] = byte1;
            CANMsg.DATA[2] = byte2;
            CANMsg.DATA[3] = byte3;
            CANMsg.DATA[4] = byte4;
            CANMsg.DATA[5] = byte5;
            CANMsg.DATA[6] = byte6;
            CANMsg.DATA[7] = byte7;
            CANMsg.ID = CANDeviceConstant.HARDWARE_FILTER_GRIPPER;
            CANQueue.Instance.add(CANMsg);
        }
        
        public static void addGetInstruction(byte treatingFunction, byte id, byte adress, byte nbOfByte)
        {
            TPCANMsg CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.DATA[0] = DynamixelCANInstruction.GET_DATA;
            CANMsg.DATA[1] = id;
            CANMsg.DATA[2] = adress;
            CANMsg.DATA[3] = (byte)(nbOfByte % 5);
            CANMsg.ID = CANDeviceConstant.HARDWARE_FILTER_GRIPPER;
            CANQueue.Instance.add(CANMsg);
        }

        public static void addGetMotorType(byte treatingFunction, byte id)
        {
            TPCANMsg CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.DATA[0] = DynamixelCANInstruction.GET_MOTOR_TYPE;
            CANMsg.DATA[1] = id;
            CANMsg.DATA[2] = treatingFunction;
            CANMsg.ID = CANDeviceConstant.HARDWARE_FILTER_GRIPPER;
            CANQueue.Instance.add(CANMsg);
        }


        public static void addSetInstruction(byte id, byte adress, byte nbOfByte, UInt16 data)
        {
            TPCANMsg CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.DATA[0] = DynamixelCANInstruction.SET_DATA;
            CANMsg.DATA[1] = id;
            CANMsg.DATA[2] = adress;
            CANMsg.DATA[3] = (byte)(nbOfByte % 3);
            CANMsg.DATA[4] = (byte)data;
            CANMsg.DATA[5] = (byte)(data >> 8);
            CANMsg.ID = CANDeviceConstant.HARDWARE_FILTER_GRIPPER;
            CANQueue.Instance.add(CANMsg);
        }


        public static void sendInstruction(byte instruction, byte id, UInt16 data)
        {
            TPCANMsg CANMsg = new TPCANMsg();
            CANMsg.DATA = new byte[8];
            CANMsg.DATA[0] = instruction;
            CANMsg.DATA[1] = id;
            CANMsg.DATA[2] = (byte)data;
            CANMsg.DATA[3] = (byte)(data >> 8);
            CANMsg.ID = CANDeviceConstant.HARDWARE_FILTER_GRIPPER;
            CANQueue.Instance.add(CANMsg);
        }

        //*/
        #endregion

    }
}
