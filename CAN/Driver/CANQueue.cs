using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Peak.Can.Basic;

namespace PCAN
{
    class CANQueue
    {
    
        #region MEMBER
        private List<TPCANMsg> canQueue = new List<TPCANMsg>();
        #endregion

        #region INSTANCE
        private static CANQueue instance;
        public static CANQueue Instance
        {
            get
            {
                if (instance == null)
                {
                    instance = new CANQueue();
                }
                return instance;
            }
        }
        #endregion

        #region QUEUE CONTROL

        public void add(TPCANMsg msg)
        {
            canQueue.Add(msg);
        }

        // execute the first CAN message on the queue
        public void executeFirst()
        {
            if (canQueue.Count > 0)
            {
                PCANCom.Instance.send(canQueue[0]);
                //printTransmittedPacket(canQueue[0]);
                canQueue.RemoveAt(0);
            }
        }

        public void executeLast()
        {
            if (canQueue.Count > 0)
            {
                PCANCom.Instance.send(canQueue[canQueue.Count-1]);
                //printTransmittedPacket(canQueue[canQueue.Count - 1]);
                canQueue.RemoveAt(canQueue.Count - 1);
            }
        }

        public int countQueue()
        {
            return canQueue.Count();
        }

        public void clearQueue()
        {
            canQueue.Clear();
        }

        #endregion

        #region Debugging function
        public static void printTransmittedPacket(TPCANMsg msg)
        {
            Console.WriteLine(" ");
            Console.WriteLine("---------------------------------------");
            Console.Write("T: ");
            for (int n = 0; n < 8; n++)
            {
                Console.Write("[{0:X}] ", msg.DATA[n]);
            }
        }
        public static void printReceivedPacket(TPCANMsg msg)
        {
            Console.WriteLine(" ");
            Console.WriteLine("---------------------------------------");
            Console.Write("R: ");
            for (int n = 0; n < 8; n++)
            {
                Console.Write("[{0:X}] ", msg.DATA[n]);
            }
        }
        #endregion
    }
}