using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

/* Autor: Dave Plouffe
 * 
 * This class should be removed because the Hardware filter of the gripper
 * could be anything between 0x00 and 0xFF.
 * 
 * */

namespace PCAN.Driver
{
    class CANDeviceConstant
    {
        #region GRIPPER CONSTANTS
        public const byte HARDWARE_FILTER_GRIPPER = 0x30;

        #endregion
    }
}
