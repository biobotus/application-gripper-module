using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Autor: Dave Plouffe
 * 
 * DynamixelConst contains all instructions of a dynamixel motor.
 * 
 * */

namespace Dynamixel.Driver
{
    public enum DynamixelModel
    {
        AX12A = 12, MX12W = 360
    }

    public class DynamixelConst
    {

        public const byte BROADCAST_ID = 0xFE;


        // ERROR BITS
        public const byte ERROR_INSTRUCTION = 64;
        public const byte ERROR_OVERLOAD = 32;
        public const byte ERROR_CHECKSUM = 16;
        public const byte ERROR_RANGE = 8;
        public const byte ERROR_OVERHEATING = 4;
        public const byte ERROR_ANGLE_LIMIT = 2;
        public const byte ERROR_INPUT_VOLTAGE = 1;




        // INSTRUCTIONS
        public const byte INST_PING = 0x01;             // Used to obtain a Status Packet
        public const byte INST_READ = 0x02;             // Reading values in Control Table
        public const byte INST_WRITE = 0x03;            // Writing values in Control Table
        public const byte INST_REG_WRITE = 0x04;        // Write data byte to REGISTERED_INSTRUCION
        public const byte INST_ACTION = 0x05;           // Triggers the action stored byteo REGISTERED_INSTRUCTION
        public const byte INST_RESET = 0x06;            // Change the control table values to the factory default values
        public const byte INST_SYNC_WRITE = 0x83;       // Used to control many Dynamixel actuators at the same time


        public const byte INST_DIGITAL_RESET = 0x07;    // Datasheet exemple function
        public const byte INST_SYSTEM_READ = 0x0C;      // Datasheet exemple function
        public const byte INST_SYSTEM_WRITE = 0x0D;     // Datasheet exemple function
        public const byte INST_SYNC_REG_WRITE = 0x84;   // Datasheet exemple function


        // EEPROM ADDRESS
        public const byte MODEL_NUMBER_L = 0;           // RD, Model Number, 0x000C for AX-12
        public const byte MODEL_NUMBER_H = 1;           // RD

        public const byte FIRMWARE = 2;                  // RD, Firmware Version

        public const byte ID = 3;                       // RD-WR, Unique ID
        public const byte BAUD_RATE = 4;                // RD-WR, Baud Rate, see page 14/38 of Datasheet
        public const byte RETURN_DELAY_TIME = 5;        // RD-WR, Return Delay Time, time is given by (2usec * RETURN_VALUE_TIME)

        public const byte CW_ANGLE_LIMIT_L = 6;         // RD-WR, Clock-Wise Angle Limit
        public const byte CW_ANGLE_LIMIT_H = 7;         // RD-WR
        public const byte CCW_ANGLE_LIMIT_L = 8;        // RD-WR, Counter-Clock-Wise Angle Limit
        public const byte CCW_ANGLE_LIMIT_H = 9;        // RD-WR

        public const byte TEMPERATURE_LIMIT = 11;         // RD-WR, Highest Limit Temperature, If Temperature goes higher than the specficied value, Over Heating Error Bit = 1 (Bit 2 of the Status Packet)and an alarm will be set by address 17 and 18.
        public const byte LOWEST_LIMIT_VOLTAGE = 12;      // RD-WR, Lowest Limit Voltage, If voltage out of specified range, Voltage Range Error Bit = 1 (Bit 0 of Status Packet) and an alarm will be set by address 17 and 18.
        public const byte HIGHEST_LIMIT_VOLTAGE = 13;        // RD-WR, Highest Limit Voltage, Values are 10 times the actual voltage

        public const byte MAX_TORQUE_L = 14;            // RD-WR, Maximum torque output, when set to 0, actuator goes in Free Run mode
        public const byte MAX_TORQUE_H = 15;            // RD-WR, Torque limited by value in the RAM Torque_Limit

        public const byte RETURN_LEVEL = 16;            // RD-WR, Determine if the Dynamixel return a Status Packet after receiving an Instruction Packet, 0 = No Respond, 1 = Respond only to READ_DATA INST, 2 = Respond to all instruction

        public const byte ALARM_LED = 17;               // RD-WR, Set if LED should blink for the different errors, see table page 16/38 of datasheet
        public const byte ALARM_SHUTDOWN = 18;          // RD-WR, Set if Actuator should shutdown when different alarm occurs, see table page 16/38 of datasheet

        public const byte MULTI_TURN_OFFSET_L = 20;     // RD-WR, Adjusts position (zeroing). This value gets included in Present Position (36).
        public const byte MULTI_TURN_OFFSET_H = 21;     // RD-WR, Present position + multi-turn offset.
        public const byte RESOLUTION_DIVIDER = 22;      // RD-WR, It allows the user to change Dynamixel’s resolution.

        public const byte DOWN_CALIBRATION_L = 20;      // RD, Data used for compensating for the differences betwwen the potentiometers used in the Dynamixel
        public const byte DOWN_CALIBRATION_H = 21;      // RD
        public const byte UP_CALIBRATION_L = 22;        // RD
        public const byte UP_CALIBRATION_H = 23;        // RD


        //RAM AREA
        public const byte TORQUE_ENABLE = (24);         // RD-WR, Initial value = 0 = Free Run Mode, to Enable Torque Control set value to 1
        public const byte LED = (25);                   // RD-WR, LED turns on if value = 1, turns off when value = 0

        public const byte CW_COMPLIANCE_MARGIN = (26);  // RD-WR, See graph page 17/38 of datasheet
        public const byte CCW_COMPLIANCE_MARGIN = (27); // RD-WR, See graph page 17/38 of datasheet
        public const byte CW_COMPLIANCE_SLOPE = (28);   // RD-WR, See graph page 17/38 of datasheet
        public const byte CCW_COMPLIANCE_SLOPE = (29);  // RD-WR, See graph page 17/38 of datasheet

        public const byte GOAL_POSITION_L = (30);       // RD-WR, Requested angular position between 0 and 300 degree, 0x0 = 0 degree, 0x1ff = 150 degree, 0x3ff = 300 degree
        public const byte GOAL_POSITION_H = (31);       // RD-WR, Values range between 0 and 1023 (0x3ff)

        public const byte MOVING_SPEED_L = (32);          // RD-WR, Set the angular velocity pf the output moving to the goal position. 0x3ff = max velocity = 114 rpm, lowest velocity = 0x1, 0x0 = highest velocity for supplied voltage
        public const byte MOVING_SPEED_H = (33);          // RD-WR, See table page 19/38 of datasheet

        public const byte TORQUE_LIMIT_L = (34);        // RD-WR, When turned on, MAX_TORQUE_L/H is copied byteo TORQUE_LIMIT_L/H
        public const byte TORQUE_LIMIT_H = (35);        // RD-WR

        public const byte PRESENT_POSITION_L = (36);    // RD, Current angular position
        public const byte PRESENT_POSITION_H = (37);    // RD

        public const byte PRESENT_SPEED_L = (38);       // RD, Current angular velocity
        public const byte PRESENT_SPEED_H = (39);       // RD

        public const byte PRESENT_LOAD_L = (40);        // RD, Magnitude of the load, see table page 18/38 of datasheet
        public const byte PRESENT_LOAD_H = (41);        // RD

        public const byte PRESENT_VOLTAGE = (42);       // RD, Current voltage applied, value is 10 times the actual voltage
        public const byte PRESENT_TEMPERATURE = (43);   // RD, Current byteernal temperature in degree
        public const byte REGISTERED_INSTRUCTION = (44);// RD-WR, Set to 1 when an insctruction is assignedd by REG_WRITE instruction. Set to 0 after it completes the assigned insctruction by the ACTION instruction

        public const byte MOVING = (46);        // RD, Set to 1 when the actuator is movin by its own power
        public const byte LOCK = (47);          // RD-WR, Value = 1, only Address 0x18 tp 0x23 can be written to. When locked, can only be unloakc by turning the device power off
        public const byte PUNCH_L = (48);       // RD-WR, Minimum current supplied to the motor during operation, initial value = 0x20, maximum value = 0x3ff
        public const byte PUNCH_H = (49);       // RD-WR

    }
}
