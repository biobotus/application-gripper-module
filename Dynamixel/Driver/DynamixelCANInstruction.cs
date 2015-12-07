using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dynamixel.Driver
{
    class DynamixelCANInstruction
    {
        // Dynamixel instruction: [BYTE 1] [BYTE 2] [...] [BYTE 8]
        // An instruction sent to the Dynamixel always begin
        // with the instruction id and is followed by some data.
        //
        // [BYTE 1] = instruction id 
        // [BYTE 2] = id
        // [BYTE 3] = data
        // [BYTE n] = data


        public const byte SET_Z_AXIS_HOME = 0;
        public const byte SET_Z_AXIS_POSITION = 1;

        public const byte OPEN_GRIPPER      = 2;    // [OPEN_GRIPPER]

        public const byte SET_MOVING_SPEED  = 3;    // [SET_MOVING_SPEED] [id] [speed]
        public const byte SET_TORQUE        = 4;    // [SET_TORQUE] [ID] [torque]
        public const byte SET_TORQUE_ENABLE = 5;    // [SET_TORQUE_ENABLE] [ID]

        public const byte TILT              = 6;    // [TILT] [position]
        public const byte ROTATE            = 7;    // [ROTATE] [position]
        public const byte MOVE              = 8;    // [MOVE] [ID] [position]

        public const byte GET_DATA          = 9;    // [GET_DATA] [TREATING FUNC] [ID] [ADDRESS] [#OfByte (max = 4)]
        public const byte SET_DATA          = 10;

        public const byte SET_CLOCK_4M      = 11;   // [SET_CLOCK_4M]
        public const byte SET_CLOCK_8M      = 12;   // [SET_CLOCK_8M]

        public const byte GET_MOTOR_TYPE    = 13;  // [GET_MOTOR_TYPE] [TREADING FUNCTION] [ID]
        public const byte SET_MOTOR_TYPE    = 14;  // [SET_MOTOR_TYPE] [ID] [MOTOR TYPE]


    }
}
