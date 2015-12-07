using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Timers;


using Peak.Can.Basic;
using TPCANHandle = System.UInt16;


namespace PCAN
{
    class PCANCom
    {

        #region MEMBER

        /// <summary>
        /// Saves the handle of a PCAN hardware
        /// </summary>
        private static TPCANHandle m_PcanHandle;

        #endregion


        #region CONSTRUCTOR
        public PCANCom(){


        }
        #endregion


        #region INSTANCE
        private static PCANCom instance;
        public static PCANCom Instance
        {
            get
            {
                if (instance == null)
                { 
                    instance = new PCANCom();
                }
                return instance;
            }
        }
        #endregion


        #region CAN CONNEXION / DECONNEXION
        public TPCANStatus connect(TPCANHandle handler, TPCANBaudrate baudrate, TPCANType type, UInt32 io, UInt16 interrupt)
        {
            TPCANStatus stsResult;
            m_PcanHandle = handler;

            // Connects a selected PCAN-Basic channel
            //
            stsResult = PCANBasic.Initialize(
                    m_PcanHandle,
                    baudrate,
                    type,
                    io,
                    interrupt);

            readCanTimer = new Timer(50);
            readCanTimer.Start();
            readCanTimer.Elapsed += OnTimedEvent;

            return stsResult;
        }


        public void disconnect()
        {
            // Releases a current connected PCAN-Basic channel
            PCANBasic.Uninitialize(m_PcanHandle);
        }

        #endregion


        #region SEND MESSAGE
        public TPCANStatus send(TPCANMsg CANMsg)
        {
            CANMsg.LEN = 8;
            CANMsg.MSGTYPE = TPCANMessageType.PCAN_MESSAGE_STANDARD;
            return PCANBasic.Write(m_PcanHandle, ref CANMsg);
        }
        #endregion


        #region READ CAN MESSAGE

        private Timer readCanTimer;
        private void OnTimedEvent(Object source, System.Timers.ElapsedEventArgs e)
        {
            ReadMessage();
        }


        /// <summary>
        /// Function for reading PCAN-Basic messages
        /// </summary>
        private void ReadMessages()
        {
            TPCANStatus stsResult;

            // We read at least one time the queue looking for messages.
            // If a message is found, we look again trying to find more.
            // If the queue is empty or an error occurr, we get out from
            // the dowhile statement.
            //			
            do
            {
                stsResult = ReadMessage();
                if (stsResult == TPCANStatus.PCAN_ERROR_ILLOPERATION)
                    break;
            } while (!Convert.ToBoolean(stsResult & TPCANStatus.PCAN_ERROR_QRCVEMPTY));
        }


        /// <summary>
        /// Function for reading CAN messages on normal CAN devices
        /// </summary>
        /// <returns>A TPCANStatus error code</returns>
        private TPCANStatus ReadMessage()
        {
            TPCANMsg CANMsg;
            TPCANStatus stsResult;

            // We execute the "Read" function of the PCANBasic                
            stsResult = PCANBasic.Read(m_PcanHandle, out CANMsg);
            if (stsResult == TPCANStatus.PCAN_ERROR_OK)
            {
                postMessage(CANMsg);
            }
               
            return stsResult;
        }

        public void printPacket(byte[] packet)
        {
            int N = 8;
            Console.Write("\nN={0:X}   -   ", N);
            for (int n = 0; n < N; n++)
            {
                Console.Write("[{0:X}] ", packet[n]);
            }
        }

        #endregion


        #region OBSERVER PATTERN
        private void postMessage(TPCANMsg CANMsg)
        {
            OnMessageReceivedEvent(new PCANComEventArgs(CANMsg));
        }

        protected virtual void OnMessageReceivedEvent(PCANComEventArgs e)
        {
            EventHandler<PCANComEventArgs> handler = OnMessageReceived;
            if (handler != null)
            {
                handler(this, e);
            }
        }
        public event EventHandler<PCANComEventArgs> OnMessageReceived;
        #endregion

    }


    #region OBSERVER PATTERN (EVENTARGS)
    public class PCANComEventArgs : EventArgs
    {
        public PCANComEventArgs(TPCANMsg CanMsg)
        {
            this.CanMsg = CanMsg;
        }

        public TPCANMsg CanMsg { get; private set; }
    }
    #endregion

}
