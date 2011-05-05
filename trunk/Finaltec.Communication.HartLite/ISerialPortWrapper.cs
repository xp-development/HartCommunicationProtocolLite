using System.IO.Ports;

namespace Finaltec.Communication.HartLite
{
    public interface ISerialPortWrapper
    {
        void Open();
        void Close();

        int Read(byte[] buffer, int offset, int count);
        void Write(byte[] buffer, int offset, int count);

        int BytesToRead { get; }
        string PortName { get; set; }

        bool DtrEnable { get; set; }
        bool RtsEnable { get; set; }
        bool CtsHolding { get; }
        bool CDHolding { get; }

        event SerialDataReceivedEventHandler DataReceived;
        event SerialPinChangedEventHandler PinChanged;
    }
}