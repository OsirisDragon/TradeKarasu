using System;
using System.Runtime.InteropServices;

namespace TradeUtility.Card
{
    public class WinSCard
    {
        [StructLayout(LayoutKind.Sequential)]
        public struct SCARD_IO_REQUEST
        {
            public int dwProtocol;
            public int cbPciLength;
        }

        [StructLayout(LayoutKind.Sequential)]
        public struct SCARD_READERSTATE
        {
            public string RdrName;
            public int UserData;
            public int RdrCurrState;
            public int RdrEventState;
            public int ATRLength;

            [MarshalAs(UnmanagedType.ByValArray, SizeConst = 37)]
            public byte[] ATRValue;
        }

        [DllImport("winscard.dll")]
        public static extern int SCardEstablishContext(int dwScope, int pvReserved1, int pvReserved2, ref int phContext);

        [DllImport("winscard.dll")]
        public static extern int SCardReleaseContext(int phContext);

        [DllImport("winscard.dll")]
        public static extern int SCardConnect(int hContext, string szReaderName, int dwShareMode, int dwPrefProtocol, ref int phCard, ref int ActiveProtocol);

        [DllImport("winscard.dll")]
        public static extern int SCardBeginTransaction(int hCard);

        [DllImport("winscard.dll")]
        public static extern int SCardDisconnect(int hCard, int Disposition);

        [DllImport("winscard.dll")]
        public static extern int SCardListReaderGroups(int hContext, ref string mzGroups, ref int pcchGroups);

        [DllImport("winscard.DLL", EntryPoint = "SCardListReadersA", CharSet = CharSet.Ansi)]
        public static extern int SCardListReaders(int hContext, byte[] Groups, byte[] Readers, ref int pcchReaders);

        [DllImport("winscard.dll")]
        public static extern int SCardStatus(int hCard, string szReaderName, ref int pcchReaderLen, ref int State, ref int Protocol, ref byte ATR, ref int ATRLen);

        [DllImport("winscard.dll")]
        public static extern int SCardEndTransaction(int hCard, int Disposition);

        [DllImport("winscard.dll")]
        public static extern int SCardState(int hCard, ref uint State, ref uint Protocol, ref byte ATR, ref uint ATRLen);

        [DllImport("WinScard.dll")]
        public static extern int SCardTransmit(IntPtr hCard, ref SCARD_IO_REQUEST pioSendPci, ref byte[] pbSendBuffer, int cbSendLength, ref SCARD_IO_REQUEST pioRecvPci, ref byte[] pbRecvBuffer, ref int pcbRecvLength);

        [DllImport("winscard.dll")]
        public static extern int SCardTransmit(int hCard, ref SCARD_IO_REQUEST pioSendRequest, ref byte SendBuff, int SendBuffLen, ref SCARD_IO_REQUEST pioRecvRequest, ref byte RecvBuff, ref int RecvBuffLen);

        [DllImport("winscard.dll")]
        public static extern int SCardTransmit(int hCard, ref SCARD_IO_REQUEST pioSendRequest, ref byte[] SendBuff, int SendBuffLen, ref SCARD_IO_REQUEST pioRecvRequest, ref byte[] RecvBuff, ref int RecvBuffLen);

        [DllImport("winscard.dll")]
        public static extern int SCardControl(int hCard, uint dwControlCode, ref byte SendBuff, int SendBuffLen, ref byte RecvBuff, int RecvBuffLen, ref int pcbBytesReturned);

        [DllImport("winscard.dll")]
        public static extern int SCardGetStatusChange(int hContext, int TimeOut, ref SCARD_READERSTATE ReaderState, int ReaderCount);

        public static string GetScardErrMsg(int retCode)
        {
            try
            {
                SCardReturnValues returnEnum = SCardReturnValues.NONE;
                returnEnum = (SCardReturnValues)retCode;

                switch (returnEnum)
                {
                    case SCardReturnValues.ERROR_BAD_COMMAND:
                        return ("讀卡機忙碌中");

                    case SCardReturnValues.SCARD_S_SUCCESS:
                        return ("No error was encountered.");

                    case SCardReturnValues.SCARD_E_NO_READERS_AVAILABLE:
                        return ("找不到讀卡機");

                    case SCardReturnValues.SCARD_W_REMOVED_CARD:
                        return ("讀卡機待命中");

                    case SCardReturnValues.SCARD_E_CANCELLED:
                        return ("The action was canceled by an SCardCancel request.");

                    case SCardReturnValues.SCARD_E_CANT_DISPOSE:
                        return ("The system could not dispose of the media in the requested manner.");

                    case SCardReturnValues.SCARD_E_CARD_UNSUPPORTED:
                        return ("The smart card does not meet minimal requirements for support.");

                    case SCardReturnValues.SCARD_E_DUPLICATE_READER:
                        return ("The reader driver didn't produce a unique reader name.");

                    case SCardReturnValues.SCARD_E_INSUFFICIENT_BUFFER:
                        return ("The data buffer for returned data is too small for the returned data.");

                    case SCardReturnValues.SCARD_E_INVALID_ATR:
                        return ("An ATR string obtained from the registry is not a valid ATR string.");

                    case SCardReturnValues.SCARD_E_INVALID_HANDLE:
                        return ("The supplied handle was invalid.");

                    case SCardReturnValues.SCARD_E_INVALID_PARAMETER:
                        return ("One or more of the supplied parameters could not be properly interpreted.");

                    case SCardReturnValues.SCARD_E_INVALID_TARGET:
                        return ("Registry startup information is missing or invalid.");

                    case SCardReturnValues.SCARD_E_INVALID_VALUE:
                        return ("One or more of the supplied parameter values could not be properly interpreted.");

                    case SCardReturnValues.SCARD_E_NOT_READY:
                        return ("The reader or card is not ready to accept commands.");

                    case SCardReturnValues.SCARD_E_NOT_TRANSACTED:
                        return ("An attempt was made to end a non-existent transaction.");

                    case SCardReturnValues.SCARD_E_NO_MEMORY:
                        return ("Not enough memory available to complete this command.");

                    // The smart card resource manager is not running.
                    case SCardReturnValues.SCARD_E_NO_SERVICE:
                        return ("找不到讀卡機服務");

                    // The operation requires a smart card, but no smart card is currently in the device.
                    case SCardReturnValues.SCARD_E_NO_SMARTCARD:
                        return ("不要一直亂移動卡片好嗎");

                    case SCardReturnValues.SCARD_E_PCI_TOO_SMALL:
                        return ("The PCI receive buffer was too small.");

                    case SCardReturnValues.SCARD_E_PROTO_MISMATCH:
                        return ("The requested protocols are incompatible with the protocol currently in use with the card.");

                    case SCardReturnValues.SCARD_E_READER_UNAVAILABLE:
                        return ("The specified reader is not currently available for use.");

                    case SCardReturnValues.SCARD_E_READER_UNSUPPORTED:
                        return ("The reader driver does not meet minimal requirements for support.");

                    case SCardReturnValues.SCARD_E_SERVICE_STOPPED:
                        return ("The smart card resource manager has shut down.");

                    case SCardReturnValues.SCARD_E_SHARING_VIOLATION:
                        return ("The smart card cannot be accessed because of other outstanding connections.");

                    case SCardReturnValues.SCARD_E_SYSTEM_CANCELLED:
                        return ("The action was canceled by the system, presumably to log off or shut down.");

                    case SCardReturnValues.SCARD_E_TIMEOUT:
                        return ("The user-specified timeout value has expired.");

                    case SCardReturnValues.SCARD_E_UNKNOWN_CARD:
                        return ("The specified smart card name is not recognized.");

                    // The specified reader name is not recognized.
                    case SCardReturnValues.SCARD_E_UNKNOWN_READER:
                        return ("識別讀卡機中");

                    case SCardReturnValues.SCARD_F_COMM_ERROR:
                        return ("An internal communications error has been detected.");

                    case SCardReturnValues.SCARD_F_INTERNAL_ERROR:
                        return ("An internal consistency check failed.");

                    case SCardReturnValues.SCARD_F_UNKNOWN_ERROR:
                        return ("An internal error has been detected, but the source is unknown.");

                    case SCardReturnValues.SCARD_F_WAITED_TOO_LONG:
                        return ("An internal consistency timer has expired.");

                    case SCardReturnValues.SCARD_W_RESET_CARD:
                        return ("The smart card has been reset, so any shared state information is invalid.");

                    case SCardReturnValues.SCARD_W_UNPOWERED_CARD:
                        return ("Power has been removed from the smart card, so that further communication is not possible.");

                    case SCardReturnValues.SCARD_W_UNRESPONSIVE_CARD:
                        return ("The smart card is not responding to a reset.");

                    case SCardReturnValues.SCARD_W_UNSUPPORTED_CARD:
                        return ("The reader cannot communicate with the card, due to ATR string configuration conflicts.");

                    case SCardReturnValues.SCARD_P_SHUTDOWN:
                        return ("The operation has been aborted to allow the server application to exit.");

                    default:
                        return retCode.ToString();
                }
            }
            catch
            {
                return retCode.ToString();
            }
        }
    }
}