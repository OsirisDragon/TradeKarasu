using System;
using System.Collections.Generic;
using System.Linq;

namespace TradeUtility.Card
{
    public class CardReader
    {
        public int retCode, hCard, Protocol, hContext;
        public bool connActive = false;
        public WinSCard.SCARD_IO_REQUEST pioSendRequest;

        public CardReader()
        {
        }

        public bool Connect(string readerName)
        {
            connActive = true;
            retCode = WinSCard.SCardConnect(hContext, readerName, (int)SCardShareModes.Shared,
                                 (int)SCardProtocol.T0 | (int)SCardProtocol.T1, ref hCard, ref Protocol);

            if (retCode != (int)SCardReturnValues.SCARD_S_SUCCESS)
            {
                connActive = false;
                throw new Exception(WinSCard.GetScardErrMsg(retCode));
            }
            else
                return true;
        }

        public void DisconnectAndRelease()
        {
            if (connActive)
            {
                retCode = WinSCard.SCardDisconnect(hCard, (int)SCardReaderDisposition.Unpower);
            }
            retCode = WinSCard.SCardReleaseContext(hContext);
        }

        public List<string> GetReadersList()
        {
            string ReaderList = "" + Convert.ToChar(0);
            int indx;
            int pcchReaders = 0;
            string rName = "";
            List<string> lstReaders = new List<string>();

            // Establish Context
            retCode = WinSCard.SCardEstablishContext((int)SCardScopes.User, 0, 0, ref hContext);

            if (retCode != (int)SCardReturnValues.SCARD_S_SUCCESS)
                throw new Exception(WinSCard.GetScardErrMsg(retCode));

            // List card readers in the system
            retCode = WinSCard.SCardListReaders(this.hContext, null, null, ref pcchReaders);

            if (retCode != (int)SCardReturnValues.SCARD_S_SUCCESS)
                throw new Exception(WinSCard.GetScardErrMsg(retCode));

            byte[] readersList = new byte[pcchReaders];

            // Fill reader list
            retCode = WinSCard.SCardListReaders(this.hContext, null, readersList, ref pcchReaders);

            if (retCode != (int)SCardReturnValues.SCARD_S_SUCCESS)
                throw new Exception("Error SCardListReaders");

            rName = "";
            indx = 0;

            while (readersList[indx] != 0)
            {
                while (readersList[indx] != 0)
                {
                    rName += (char)readersList[indx];
                    indx++;
                }

                lstReaders.Add(rName);
                rName = "";
                indx++;
            }

            return lstReaders;
        }

        public string GetNXPPR533Readers()
        {
            List<string> readerList = GetReadersList();
            return readerList.Where(x => x.Contains("NXP PR533")).SingleOrDefault();
        }

        public string GetCardUID()
        {
            string cardUID = "";
            byte[] receivedUID = new byte[256];
            var request = new WinSCard.SCARD_IO_REQUEST
            {
                dwProtocol = (int)SCardProtocol.T1,
                cbPciLength = System.Runtime.InteropServices.Marshal.SizeOf(typeof(WinSCard.SCARD_IO_REQUEST))
            };

            /*
            +------+------+------+------+------+
            | CLA  | INS  |  P1  |  P2  |  Le  |
            +------+------+------+------+------+
            | 0xFF | 0xCA | 0x00 | 0x00 | 0x00 |
            +------+------+------+------+------+
             */

            byte[] sendBytes = new byte[] { 0xFF, 0xCA, 0x00, 0x00, 0x00 };
            int outBytes = receivedUID.Length;
            int status = WinSCard.SCardTransmit(hCard, ref request, ref sendBytes[0], sendBytes.Length, ref request, ref receivedUID[0], ref outBytes);

            if (status != (int)SCardReturnValues.SCARD_S_SUCCESS)
                cardUID = "";
            else
                cardUID = BitConverter.ToString(receivedUID.Take(4).ToArray()).Replace("-", string.Empty).ToLower();
            return cardUID.ToUpper();
        }
    }
}