using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

using Peak.Can.Basic;
using TPCANHandle = System.UInt16;

/* Autor: Dave Plouffe
 * 
 * ctrCanConnector is used to configure the connexion over the CAN bus and 
 * to connect the application to the bus.
 * .
 * This component is a simplified version of the PCAN basic API application
 * which can be found at this address:
 *      http://www.peak-system.com/PCAN-USB.199.0.html?L=1
 * 
 * */

namespace PCAN
{
    public partial class ctrCanConnector : UserControl
    {
        PCANCom pCanCom = new PCANCom();

        #region Members
        /// <summary>
        /// Stores the status of received messages for its display
        /// </summary>
        private System.Collections.ArrayList m_LastMsgsList;
        /// <summary>
        /// Read Delegate for calling the function "ReadMessages"
        /// </summary>
        //private ReadDelegateHandler m_ReadDelegate;
        /// <summary>
        /// Receive-Event
        /// </summary>
        private System.Threading.AutoResetEvent m_ReceiveEvent;
        /// <summary>
        /// Thread for message reading (using events)
        /// </summary>
        private System.Threading.Thread m_ReadThread;
        /// <summary>
        /// Handles of the current available PCAN-Hardware
        /// </summary>
        private TPCANHandle[] m_HandlesArray;
        #endregion



        public ctrCanConnector()
        {
            InitializeComponent();
            InitializeBasicComponents();
        }


        private void btnHwRefresh_Click(object sender, EventArgs e)
        {
            UInt32 iBuffer;
            TPCANStatus stsResult;
            bool isFD;

            // Clears the Channel combioBox and fill it again with 
            // the PCAN-Basic handles for no-Plug&Play hardware and
            // the detected Plug&Play hardware
            //
            cbChannel.Items.Clear();
            try
            {
                for (int i = 0; i < m_HandlesArray.Length; i++)
                {
                    // Includes all no-Plug&Play Handles
                    if (m_HandlesArray[i] <= PCANBasic.PCAN_DNGBUS1)
                        cbChannel.Items.Add(FormatChannelName(m_HandlesArray[i]));
                    else
                    {
                        // Checks for a Plug&Play Handle and, according with the return value, includes it
                        // into the list of available hardware channels.
                        //
                        stsResult = PCANBasic.GetValue(m_HandlesArray[i], TPCANParameter.PCAN_CHANNEL_CONDITION, out iBuffer, sizeof(UInt32));
                        if ((stsResult == TPCANStatus.PCAN_ERROR_OK) && ((iBuffer & PCANBasic.PCAN_CHANNEL_AVAILABLE) == PCANBasic.PCAN_CHANNEL_AVAILABLE))
                        {
                            stsResult = PCANBasic.GetValue(m_HandlesArray[i], TPCANParameter.PCAN_CHANNEL_FEATURES, out iBuffer, sizeof(UInt32));
                            isFD = (stsResult == TPCANStatus.PCAN_ERROR_OK) && ((iBuffer & PCANBasic.FEATURE_FD_CAPABLE) == PCANBasic.FEATURE_FD_CAPABLE);
                            cbChannel.Items.Add(FormatChannelName(m_HandlesArray[i], isFD));
                        }
                    }
                }
                cbChannel.SelectedIndex = cbChannel.Items.Count - 1;
                btnConnect.Enabled = cbChannel.Items.Count > 0;
            }
            catch (DllNotFoundException)
            {
                MessageBox.Show("Unable to find the library: PCANBasic.dll !", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                Environment.Exit(-1);
            }

        }


        #region HelpFunction

        /// <summary>
        /// Initialization of PCAN-Basic components
        /// </summary>
        private void InitializeBasicComponents()
        {
            // Creates the list for received messages
            //
            m_LastMsgsList = new System.Collections.ArrayList();
            // Creates the event used for signalize incomming messages 
            //
            m_ReceiveEvent = new System.Threading.AutoResetEvent(false);
            // Creates an array with all possible PCAN-Channels
            //
            m_HandlesArray = new TPCANHandle[]
            {
                PCANBasic.PCAN_ISABUS1,
                PCANBasic.PCAN_ISABUS2,
                PCANBasic.PCAN_ISABUS3,
                PCANBasic.PCAN_ISABUS4,
                PCANBasic.PCAN_ISABUS5,
                PCANBasic.PCAN_ISABUS6,
                PCANBasic.PCAN_ISABUS7,
                PCANBasic.PCAN_ISABUS8,
                PCANBasic.PCAN_DNGBUS1,
                PCANBasic.PCAN_PCIBUS1,
                PCANBasic.PCAN_PCIBUS2,
                PCANBasic.PCAN_PCIBUS3,
                PCANBasic.PCAN_PCIBUS4,
                PCANBasic.PCAN_PCIBUS5,
                PCANBasic.PCAN_PCIBUS6,
                PCANBasic.PCAN_PCIBUS7,
                PCANBasic.PCAN_PCIBUS8,
                PCANBasic.PCAN_PCIBUS9,
                PCANBasic.PCAN_PCIBUS10,
                PCANBasic.PCAN_PCIBUS11,
                PCANBasic.PCAN_PCIBUS12,
                PCANBasic.PCAN_PCIBUS13,
                PCANBasic.PCAN_PCIBUS14,
                PCANBasic.PCAN_PCIBUS15,
                PCANBasic.PCAN_PCIBUS16,
                PCANBasic.PCAN_USBBUS1,
                PCANBasic.PCAN_USBBUS2,
                PCANBasic.PCAN_USBBUS3,
                PCANBasic.PCAN_USBBUS4,
                PCANBasic.PCAN_USBBUS5,
                PCANBasic.PCAN_USBBUS6,
                PCANBasic.PCAN_USBBUS7,
                PCANBasic.PCAN_USBBUS8,
                PCANBasic.PCAN_USBBUS9,
                PCANBasic.PCAN_USBBUS10,
                PCANBasic.PCAN_USBBUS11,
                PCANBasic.PCAN_USBBUS12,
                PCANBasic.PCAN_USBBUS13,
                PCANBasic.PCAN_USBBUS14,
                PCANBasic.PCAN_USBBUS15,
                PCANBasic.PCAN_USBBUS16,
                PCANBasic.PCAN_PCCBUS1,
                PCANBasic.PCAN_PCCBUS2,
                PCANBasic.PCAN_LANBUS1,
                PCANBasic.PCAN_LANBUS2,
                PCANBasic.PCAN_LANBUS3,
                PCANBasic.PCAN_LANBUS4,
                PCANBasic.PCAN_LANBUS5,
                PCANBasic.PCAN_LANBUS6,
                PCANBasic.PCAN_LANBUS7,
                PCANBasic.PCAN_LANBUS8,
                PCANBasic.PCAN_LANBUS9,
                PCANBasic.PCAN_LANBUS10,
                PCANBasic.PCAN_LANBUS11,
                PCANBasic.PCAN_LANBUS12,
                PCANBasic.PCAN_LANBUS13,
                PCANBasic.PCAN_LANBUS14,
                PCANBasic.PCAN_LANBUS15,
                PCANBasic.PCAN_LANBUS16,
            };

            // Fills and configures the Data of several comboBox components
            //
            FillComboBoxData();
        }


        /// <summary>
        /// Gets the formated text for a PCAN-Basic channel handle
        /// </summary>
        /// <param name="handle">PCAN-Basic Handle to format</param>
        /// <param name="isFD">If the channel is FD capable</param>
        /// <returns>The formatted text for a channel</returns>
        private string FormatChannelName(TPCANHandle handle, bool isFD)
        {
            TPCANDevice devDevice;
            byte byChannel;

            // Gets the owner device and channel for a 
            // PCAN-Basic handle
            //
            if (handle < 0x100)
            {
                devDevice = (TPCANDevice)(handle >> 4);
                byChannel = (byte)(handle & 0xF);
            }
            else
            {
                devDevice = (TPCANDevice)(handle >> 8);
                byChannel = (byte)(handle & 0xFF);
            }

            // Constructs the PCAN-Basic Channel name and return it
            //
            if (isFD)
                return string.Format("{0}:FD {1} ({2:X2}h)", devDevice, byChannel, handle);
            else
                return string.Format("{0} {1} ({2:X2}h)", devDevice, byChannel, handle);
        }

        /// <summary>
        /// Gets the formated text for a PCAN-Basic channel handle
        /// </summary>
        /// <param name="handle">PCAN-Basic Handle to format</param>
        /// <returns>The formatted text for a channel</returns>
        private string FormatChannelName(TPCANHandle handle)
        {
            return FormatChannelName(handle, false);
        }


        /// <summary>
        /// Configures the data of all ComboBox components of the main-form
        /// </summary>
        private void FillComboBoxData()
        {
            // Channels will be check
            //
            btnHwRefresh_Click(this, new EventArgs());

            // Baudrates 
            //
            cbBaudrates.SelectedIndex = 3; // 250kbps

            // Hardware Type for no plugAndplay hardware
            //
            cbHwType.SelectedIndex = 0;

            // Interrupt for no plugAndplay hardware
            //
            cbInterrupt.SelectedIndex = 0;

            // IO Port for no plugAndplay hardware
            //
            cbIO.SelectedIndex = 0;

            // Parameters for GetValue and SetValue function calls
            //
            //cbParameter.SelectedIndex = 0;
        }



        /// <summary>
        /// Activates/deaactivates the different controls of the main-form according
        /// with the current connection status
        /// </summary>
        /// <param name="bConnected">Current status. True if connected, false otherwise</param>
        private void SetConnectionStatus(bool bConnected)
        {
            // Buttons
            btnConnect.Enabled = !bConnected;
            btnDisconnect.Enabled = bConnected;
            btnHwRefresh.Enabled = !bConnected;

            // ComboBoxs
            cbChannel.Enabled = !bConnected;
            cbBaudrates.Enabled = !bConnected;
        }

        /// <summary>
        /// Help Function used to get an error as text
        /// </summary>
        /// <param name="error">Error code to be translated</param>
        /// <returns>A text with the translated error</returns>
        private string GetFormatedError(TPCANStatus error)
        {
            StringBuilder strTemp;

            // Creates a buffer big enough for a error-text
            //
            strTemp = new StringBuilder(256);
            // Gets the text using the GetErrorText API function
            // If the function success, the translated error is returned. If it fails,
            // a text describing the current error is returned.
            //
            if (PCANBasic.GetErrorText(error, 0, strTemp) != TPCANStatus.PCAN_ERROR_OK)
                return string.Format("An error occurred. Error-code's text ({0:X}) couldn't be retrieved", error);
            else
                return strTemp.ToString();
        }

        private TPCANBaudrate getBaudrate()
        {
            switch (cbBaudrates.SelectedIndex)
            {
                case 0:
                    return TPCANBaudrate.PCAN_BAUD_1M;
                case 1:
                    return TPCANBaudrate.PCAN_BAUD_800K;
                case 2:
                    return TPCANBaudrate.PCAN_BAUD_500K;
                case 3:
                    return TPCANBaudrate.PCAN_BAUD_250K;
                case 4:
                    return TPCANBaudrate.PCAN_BAUD_125K;
                case 5:
                    return TPCANBaudrate.PCAN_BAUD_100K;
                case 6:
                    return TPCANBaudrate.PCAN_BAUD_95K;
                case 7:
                    return TPCANBaudrate.PCAN_BAUD_83K;
                case 8:
                    return TPCANBaudrate.PCAN_BAUD_50K;
                case 9:
                    return TPCANBaudrate.PCAN_BAUD_47K;
                case 10:
                    return TPCANBaudrate.PCAN_BAUD_33K;
                case 11:
                    return TPCANBaudrate.PCAN_BAUD_20K;
                case 12:
                    return TPCANBaudrate.PCAN_BAUD_10K;
                case 13:
                    return TPCANBaudrate.PCAN_BAUD_5K;
            }
            return 0;
        }

        private TPCANType getCanType()
        {
            switch (cbHwType.SelectedIndex)
            {
                case 0:
                    return TPCANType.PCAN_TYPE_ISA;
                case 1:
                    return TPCANType.PCAN_TYPE_ISA_SJA;
                case 2:
                    return TPCANType.PCAN_TYPE_ISA_PHYTEC;
                case 3:
                    return TPCANType.PCAN_TYPE_DNG;
                case 4:
                    return TPCANType.PCAN_TYPE_DNG_EPP;
                case 5:
                    return TPCANType.PCAN_TYPE_DNG_SJA;
                case 6:
                    return TPCANType.PCAN_TYPE_DNG_SJA_EPP;
            }
            return 0;
        }

        #endregion


        public void btnConnect_Click(object sender, EventArgs e)
        {
            TPCANStatus stsResult;

            // Get the handle fromt he text being shown
            //
            String strTemp = cbChannel.Text;
            strTemp = strTemp.Substring(strTemp.IndexOf('(') + 1, 3);
            strTemp = strTemp.Replace('h', ' ').Trim(' ');

            // Connects a selected PCAN-Basic channel
            //
            stsResult = PCANCom.Instance.connect(
                            Convert.ToUInt16(strTemp, 16),
                            getBaudrate(), getCanType(),
                            Convert.ToUInt32(cbIO.Text, 16),
                            Convert.ToUInt16(cbInterrupt.Text));

            if (stsResult != TPCANStatus.PCAN_ERROR_OK)
            { 
                if (stsResult != TPCANStatus.PCAN_ERROR_CAUTION)
                    MessageBox.Show(GetFormatedError(stsResult));
                else
                {
                    stsResult = TPCANStatus.PCAN_ERROR_OK;
                }

            }




            // Sets the connection status of the main-form
            //
            SetConnectionStatus(stsResult == TPCANStatus.PCAN_ERROR_OK);
        }

        private void btnDisconnect_Click(object sender, EventArgs e)
        {
            PCANCom.Instance.disconnect();
            SetConnectionStatus(false);
        }






    }
}
