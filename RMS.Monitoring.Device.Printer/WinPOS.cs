using System;
using System.IO.Ports;

namespace RMS.Monitoring.Device.Printer
{
    public class WinPOS : Printer
    {
        public WinPOS(string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort)
            : base("WinPOS", model, deviceManagerName, deviceManagerID, printerName, useCOMPort)
        {
        }

        public WinPOS(string model, string deviceManagerName, string deviceManagerID, string printerName, bool useCOMPort, string comPort)
            : base("WinPOS", model, deviceManagerName, deviceManagerID, printerName, useCOMPort, comPort)
        {
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns>-1 Cannot Check, 0 OK, 3 Near Paper End </returns>
        public override int[] CheckPaperStatus()
        {
            if (!useCOMPort || string.IsNullOrEmpty(comPort)) return null;

            int counter = 3;

            SerialPort serialPort = new SerialPort(comPort, 9600, Parity.None, 8, StopBits.One);

            do
            {
                try
                {
                    // send ESC v
                    var esc = new char[2] {Convert.ToChar(27), Convert.ToChar(118)};

                    serialPort.Open();
                    serialPort.Write(esc, 0, 2);
                    //label1.Text = serialPort.ReadExisting();
                    serialPort.ReadTimeout = 1500;
                    int status = serialPort.ReadByte();
                    int[] ret = new int[2];
                    if (status == 0)
                    {
                        ret[1] = 0;
                        ret[2] = 0;

                    }
                    if (status == 3)
                    {
                        ret[1] = 1;
                        ret[2] = 0;
                    }
                    else
                    {
                        ret[1] = 1;
                        ret[2] = 1;
                    }
                    return ret;
                }
                catch (Exception ex)
                {
                    if (ex.Message.IndexOf("timeout") < 0) return null;
                    System.Threading.Thread.Sleep(1500);
                    counter--;
                }
                finally
                {
                    serialPort.Close();
                    serialPort.Dispose();
                }
            } while (counter > 0);

            return null;
        }
    }
}
