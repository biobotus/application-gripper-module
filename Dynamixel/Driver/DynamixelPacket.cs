using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamixel.Driver
{
    class DynamixelPacket
    {

        /*
	        Used for requesting a status packet or to check the existence of a
	        Dynamixel actuator with a specific ID.
        */
        public byte[] getPingPacket(byte id)
        {
            return createInstructionPacket(DynamixelConst.INST_PING, id, null, 0);
        }


        /*
            Read data from the control table of a Dynamixel actuator
        */
        public byte[] getReadPacket(byte id, byte address, byte nbOfDataToBeRead)
        {
            return createSimpleInstructionPacket(DynamixelConst.INST_READ, id, address, nbOfDataToBeRead);
        }


        /*
            To write data into the control table of the Dynamixel actuator
            The first parameter must be the starting addresse of the location where
            the data is to be written.
        */
        public byte[] getWritePacket(byte id, byte[] param, byte nbOfParam)
        {
            return createInstructionPacket(DynamixelConst.INST_WRITE, id, param, nbOfParam);
        }


        public byte[] getWritePacketSimple(byte id, byte address, byte data)
        {
            return createSimpleInstructionPacket(DynamixelConst.INST_WRITE, id, address, data);
        }


        /*
            The REG_WRITE instruction is similar to the WRITE_DATA instruction, but the
            execution timing is different. When the instruction packet is received, the
            values are stored in the buffer and the write instruction is under a stanby
            status. At this time, the registered instruction register (address 0x2C) is
            set to 1. After the action instruction packet is received, the registered
            write instruction is finally executed.
        */
        public byte[] getRegWritePacket(byte id, byte address, byte data)
        {
            return createSimpleInstructionPacket(DynamixelConst.INST_REG_WRITE, id, address, data);
        }


        /*
            Triggers the action registered by the REG_WRITE instruction
        */
        public byte[] getActionPacket(byte id)
        {
            return createInstructionPacket(DynamixelConst.INST_ACTION, id, null, 0);
        }


        /*
            Changes the control table values of the Dynamixel actuator to the Factory
            Default Value.
        */
        public byte[] getResetPacket(byte id)
        {
            return createInstructionPacket(DynamixelConst.INST_RESET, id, null, 0);
        }




        #region HELPER

        private byte[] createSimpleInstructionPacket(byte instruction, byte id, byte param1, byte param2)
        {
            byte[] param = new byte[2];
            param[0] = param1;
            param[1] = param2;

            return createInstructionPacket(instruction, id, param, 2);
        }


        private byte[] createInstructionPacket(byte instruction, byte id, byte[] param, byte nbOfParam)
        {
            byte[] packet = new byte[3 + nbOfParam + 3];
            byte i = 0;

            packet[i++] = 0xff;
            packet[i++] = 0xff;
            packet[i++] = id;				        // motor id
            packet[i++] = (byte)(nbOfParam + 2);	// length
            packet[i++] = instruction;		        // instruction

            for (int n = 0; n < nbOfParam; n++)
            {
                packet[i++] = param[n];
            }
            packet[i] = calculateChecksum(packet); // checksum

            return packet;
        }


        public static byte calculateChecksum(byte[] packet)
        {
            byte N = (byte)(packet[3] + 1);
            byte checksum = 0;
            for (byte n = 0; n < N; n++)
            {
                checksum += packet[2 + n];
            }
            return (byte)(~checksum);
        }

        #endregion





    }
}
