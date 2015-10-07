namespace UPS
{
    using System;
    using System.IO.Ports;
    using System.Linq;
    using System.Threading;

    public static class UPSModule
    {
        private static SerialPort UPS_RS232;
        public static int WaitForResponseTime;

        static UPSModule()
        {
            SerialPort port = new SerialPort {
                Parity = Parity.None,
                StopBits = StopBits.One,
                NewLine = "\r",
                ReadTimeout = 500,
                WriteTimeout = 250,
                BaudRate = 0x960,
                DataBits = 8
            };
            UPS_RS232 = port;
            WaitForResponseTime = 500;
        }

        public static void CloseRS232Port()
        {
            if (!UPS_RS232.IsOpen)
            {
                throw new Exception("RS232 is already closed!");
            }
            UPS_RS232.Close();
        }

        public static void OpenRS232Port(string PortName)
        {
            UPS_RS232.PortName = PortName;
            if (UPS_RS232.IsOpen)
            {
                throw new Exception("RS232 is already open!");
            }
            UPS_RS232.Open();
        }

        public static string ReadFromUPS()
        {
            return UPS_RS232.ReadExisting();
        }

        private static byte[] StringHexToByteArray(string hex)
        {
            return (from x in Enumerable.Range(0, hex.Length)
                where (x % 2) == 0
                select Convert.ToByte(hex.Substring(x, 2), 0x10)).ToArray<byte>();
        }

        public static string WriteAndReadFromUPS(string message)
        {
            UPS_RS232.Write(message);
            UPS_RS232.Write(StringHexToByteArray("0d"), 0, 1);
            Thread.Sleep(WaitForResponseTime);
            return UPS_RS232.ReadExisting();
        }

        public static void WriteToUPS(string message)
        {
            UPS_RS232.Write(message);
            UPS_RS232.Write(StringHexToByteArray("0d"), 0, 1);
        }
    }
}

