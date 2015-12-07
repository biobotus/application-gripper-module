using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


/* Autor: Dave Plouffe
 * 
 * DynamixelData contains only basic information about a dynamixel motor.
 * 
 * This class is used to transfer information between components. Each
 * component that work with dynamixel motor should know at least the
 * model of the motor, the firmware and the id.
 * 
 * With the motor model, it is possible to know all properties that can
 * be modified. The firmware model is not used but it could be useful in
 * the futur.
 * 
 * */

namespace Dynamixel.Driver
{
    public struct DynamixelData
    {
        public UInt16 model;
        public byte firmware;
        public byte id;

        public double getAngleStep()
        {
            double step;

            switch (model)
            {
                case (ushort)DynamixelModel.MX12W:
                    step = (double)360 / (double)4096;
                    break;
                case (ushort)DynamixelModel.AX12A:
                default:
                    step = (double)300 / (double)1024;
                    break;
            }

            return step;
        }

        public double getAngleFromPosition(ushort position)
        {
            return (getAngleStep() * position) / 180 * Math.PI;
        }

        public ushort getPositionFromAngle(double angle)
        {
            ushort pos = (ushort)Math.Round(angle/getAngleStep());

            switch (model)
            {
                case (ushort)DynamixelModel.MX12W:
                    if (pos > 0xFFF) pos = 0xFFF;
                    break;
                case (ushort)DynamixelModel.AX12A:
                default:
                    if (pos > 0x3FF) pos = 0x3FF;
                    break;
            }

            return pos;
        }


        public String getModelName()
        {
            switch(model)
            {
                case (ushort)DynamixelModel.AX12A:
                    return "AX-12A";

                case (ushort)DynamixelModel.MX12W:
                    return "MX-12W";

                default:
                    return "Unknown";
            }
        }


        public String getErrorCodeMeaning(byte error)
        {
            String msg = "";

            if (error == 0)
            {
                msg = "No Error";
            }
            else
            {
                if ((error & DynamixelConst.ERROR_INPUT_VOLTAGE) == DynamixelConst.ERROR_INPUT_VOLTAGE)
                {
                    msg += "Input Voltage Error";
                }
                if ((error & DynamixelConst.ERROR_ANGLE_LIMIT) == DynamixelConst.ERROR_ANGLE_LIMIT)
                {
                    if (msg.Length > 0) msg += " - ";
                    msg += "Angle Limit Error";
                }
                if ((error & DynamixelConst.ERROR_OVERHEATING) == DynamixelConst.ERROR_OVERHEATING)
                {
                    if (msg.Length > 0) msg += " - ";
                    msg += "Overheating Error";
                }
                if ((error & DynamixelConst.ERROR_RANGE) == DynamixelConst.ERROR_RANGE)
                {
                    if (msg.Length > 0) msg += " - ";
                    msg += "Range Error";
                }
                if ((error & DynamixelConst.ERROR_CHECKSUM) == DynamixelConst.ERROR_CHECKSUM)
                {
                    if (msg.Length > 0) msg += " - ";
                    msg += "Checksum Error";
                }
                if ((error & DynamixelConst.ERROR_OVERLOAD) == DynamixelConst.ERROR_OVERLOAD)
                {
                    if (msg.Length > 0) msg += " - ";
                    msg += "Overload Error";
                }
                if ((error & DynamixelConst.ERROR_INSTRUCTION) == DynamixelConst.ERROR_INSTRUCTION)
                {
                    if (msg.Length > 0) msg += " - ";
                    msg += "Instruction Error";
                }
            }

            return msg;
        }


    }
}
