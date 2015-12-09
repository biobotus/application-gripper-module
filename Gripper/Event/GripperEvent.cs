using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Gripper.Driver;

/* Autor: Dave Plouffe
 * 
 * GripperEvent contains event definitions used to
 * communicate between components.
 * 
 * The event "OnMotorSelectedChange" is used to send the selected motor
 * to every component who are subscribed.
 * 
 * The event "OnMessageBusEvent" is used to exchange information
 * between components like the goal position, angle limits and
 * the current position.
 * 
 * */

namespace Gripper.Events
{
    class GripperEvent
    {

        #region INSTANCE
        private static GripperEvent instance;
        public static GripperEvent Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new GripperEvent();
                }
                return instance;
            }
        }
        #endregion


        #region MOTOR SELECTED CHANGE EVENT - OBSERVER PATTERN
        public event EventHandler<MotorSelectedChangeArgs> OnMotorSelectedChange;
        public void postMotorSelectedChangeEvent(DynamixelData motor)
        {
            if (OnMotorSelectedChange != null)
            {
                OnMotorSelectedChange(this, new MotorSelectedChangeArgs(motor));
            }
        }
        public class MotorSelectedChangeArgs : EventArgs
        {
            public DynamixelData motor { get; private set; }

            public MotorSelectedChangeArgs(DynamixelData motor)
            {
                this.motor = motor;
            }
        }
        #endregion


        #region MESSAGE BUS EVENT (TO SHARE INFORMATION BETWEEN COMPONANT)

        public enum MessageBusType
        {
            PRESENT_POSITION_CHANGE,
            GOAL_POSITION_CHANGE,
            CW_ANGLE_LIMIT_CHANGE,
            CCW_ANGLE_LIMIT_CHANGE
        }

        public event EventHandler<MessageBusArgs> OnMessageBusEvent;
        public void postMessageBusEvent(MessageBusType type, uint value)
        {
            if (OnMessageBusEvent != null)
            {
                OnMessageBusEvent(this, new MessageBusArgs(type, value));
            }
        }
        public class MessageBusArgs : EventArgs
        {
            public MessageBusType type { get; private set; }
            public uint value { get; private set; }

            public MessageBusArgs(MessageBusType type, uint value)
            {
                this.type = type;
                this.value = value;
            }
        }



        #endregion

    }
}
