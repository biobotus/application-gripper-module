using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO.Ports;

using PCAN;
using Peak.Can.Basic;

namespace Dynamixel.Driver
{
    public class DynamixelCom
    {

        #region MEMBER

        private DynamixelPacket dynamixel = new DynamixelPacket();
        private List<byte[]> packetQueue = new List<byte[]>();

        #endregion

        #region CONSTRUCTOR
        public DynamixelCom()
        {

        }
        #endregion


        #region INSTANCE
        private static DynamixelCom instance;
        public static DynamixelCom Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new DynamixelCom();
                }
                return instance;
            }
        }
        #endregion


        #region SEND PACKET FUNCTION
        public void printPacket(byte[] packet)
        {
            int N = packet[3] + 4;
            Console.Write("N={0:X}   -   ",N-2);
            for (int n = 2; n < N; n++)
            {
                Console.Write("[{0:X}] ", packet[n]);
            }
        }


        private void sendPacket(byte[] packet)
        {
            if (packet.Length > 2)
            {
                try
                {
                    TPCANMsg CANMsg = new TPCANMsg();
                    CANMsg.DATA = new byte[8];
                    Array.Copy(packet, 2, CANMsg.DATA, 1, packet.Length-2);
                    CANMsg.ID = CANDeviceConstant.HARDWARE_FILTER_GRIPPER;
                    PCANCom.Instance.send(CANMsg);

                } catch(Exception e){
                    Console.WriteLine("Can not transmit the command");
                }

            }
        }
        #endregion


        #region QUEUE CONTROL

        private void addToQueue(byte[] packet)
        {
            packetQueue.Add(packet);
        }


        #endregion



        /**
	        To obtain informations from the Control Table of Dynamixels Motors.

	        @param id id of the motor
	        @param address address of the memory to read
	        @param numberOfData number of data to read from the address.
        */
        public void get(byte id, byte address, byte numberOfData)
        {
            addToQueue(dynamixel.getReadPacket(id, address, numberOfData));
        }


        #region SET MOTOR DATA

        public void setByte(byte id, byte address, byte data)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, address, data));
        }


        public void set2Bytes(byte id, byte address, UInt16 value)
        {
            byte[] param = new byte[3];
            param[0] = address;
            param[1] = (byte)((0xff) & value);
            param[2] = (byte)((0xff) & (value >> 8));
            addToQueue(dynamixel.getWritePacket(id, param, 3));
        }


        public void setMultiBytes(byte id, byte address, byte[] data)
        {
            byte N = (byte)(data.Length+1);
            byte[] param = new byte[N];
            param[0] = address;
            for (int n = 1; n < N; n++)
                param[n] = data[n-1];
            addToQueue(dynamixel.getWritePacket(id, param, N));
        }

        #endregion

        #region PING COMMAND

        public void ping(byte id)
        {
            addToQueue(dynamixel.getPingPacket(id));
        }

        #endregion

        // ------------------------------------
        //  EEPROM AERA
        // ------------------------------------

        #region EEPROM
        public void setId(byte id)
        {
            addToQueue(dynamixel.getWritePacketSimple(DynamixelConst.BROADCAST_ID, DynamixelConst.ID, id));
        }

        public void setBaudrate(byte id, byte baudrate)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.BAUD_RATE, baudrate));
        }

        public void setReturnDelayTime(byte id, byte delay)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.RETURN_DELAY_TIME, delay));
        }

        public void setCWAngleLimit(byte id, UInt16 value)
        {
            set2Bytes(id, DynamixelConst.CW_ANGLE_LIMIT_L, value);
        }

        public void setCCWAngleLimit(byte id, UInt16 value)
        {
            set2Bytes(id, DynamixelConst.CCW_ANGLE_LIMIT_L, value);
        }

        public void setTemperatureLimit(byte id, byte temperature)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.TEMPERATURE_LIMIT, temperature));
        }

        public void setLowestVoltageLimit(byte id, byte temperature)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.LOWEST_LIMIT_VOLTAGE, temperature));
        }

        public void setHighestVoltageLimit(byte id, byte temperature)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.HIGHEST_LIMIT_VOLTAGE, temperature));
        }

        public void setMaxTorque(byte id, UInt16 value)
        {
            set2Bytes(id, DynamixelConst.MAX_TORQUE_L, value);
        }

        public void setStatusReturnLevel(byte id, byte returnLevel)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.RETURN_LEVEL, returnLevel));
        }

        public void setAlarmLed(byte id, byte value)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.ALARM_LED, value));
        }

        public void setAlarmShutdown(byte id, byte value)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.ALARM_SHUTDOWN, value));
        }


        public void setMultiTurnOffset(byte id, UInt16 value)
        {
            set2Bytes(id, DynamixelConst.MULTI_TURN_OFFSET_L, value);
        }


        public void setResolutionDivider(byte id, byte value)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.RESOLUTION_DIVIDER, value));
        }

        #endregion

        // ------------------------------------
        //  RAM AERA
        // ------------------------------------

        #region RAM

        public void setTorqueEnable(byte id, byte enabled)
        {
            addToQueue(dynamixel.getWritePacketSimple(id, DynamixelConst.TORQUE_ENABLE, enabled));
        }
   

        public void setCompliance(byte id, byte cw_margin, byte ccw_margin, byte cw_slope, byte ccw_slope)
        {
            byte[] param = new byte[5];
            param[0] = DynamixelConst.CW_COMPLIANCE_MARGIN;
            param[1] = cw_margin;
            param[2] = ccw_margin;
            param[3] = cw_slope;
            param[4] = ccw_slope;
            addToQueue(dynamixel.getWritePacket(id, param, 5));
        }


        public void setPosition(byte id, UInt16 position)
        {
            set2Bytes(id, DynamixelConst.GOAL_POSITION_L, position);
        }


        public void setMovingSpeed(byte id, UInt16 speed)
        {
            set2Bytes(id, DynamixelConst.MOVING_SPEED_L, speed);
        }

        #endregion

    }
}
