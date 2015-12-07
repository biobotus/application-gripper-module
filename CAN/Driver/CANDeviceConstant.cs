using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PCAN
{
    class CANDeviceConstant
    {
        #region TAC CONSTANTS
        public const byte HARDWARE_FILTER_TAC = 0x70;
        #endregion

        #region GRIPPER CONSTANTS
        public const byte HARDWARE_FILTER_GRIPPER = 0x30;

        // gripper treating function
        /*public const byte GRIPPER_TF_SCAN = 0x20;
        public const byte GRIPPER_TF_PROPERTIES = 0x40;
        public const byte GRIPPER_TF_POSITION = 0x60;
        public const byte GRIPPER_TF_TEMPERATURE = 0x80;
        public const byte GRIPPER_TF_ANGLELIMIT = 0xA0;
        public const byte GRIPPER_TF_LED_STATUS = 0xC0;
        public const byte GRIPPER_TF_PRESENT_VALUE = 0xE0;//*/
        #endregion
    }
}
