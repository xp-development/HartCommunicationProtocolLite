using System.IO.Ports;

namespace Finaltec.Communication.HartLite
{
    internal class SerialPortWrapper : ISerialPortWrapper
    {
        private readonly SerialPort _serialPort;

        public SerialPortWrapper(string comPort, int baudrate, Parity parity, int dataBits, StopBits stopBits)
        {
            _serialPort = new SerialPort(comPort, baudrate, parity, dataBits, stopBits);
        }

        public void Open()
        {
            _serialPort.Open();
        }

        public void Close()
        {
            _serialPort.Close();
        }

        public int Read(byte[] buffer, int offset, int count)
        {
            return _serialPort.Read(buffer, offset, count);
        }

        public void Write(byte[] buffer, int offset, int count)
        {
            _serialPort.Write(buffer, offset, count);
        }

        public int BytesToRead
        {
            get { return _serialPort.BytesToRead; }
        }

        public bool DtrEnable
        {
            get { return _serialPort.DtrEnable; }
            set { _serialPort.DtrEnable = value; }
        }

        public bool RtsEnable
        {
            get { return _serialPort.RtsEnable; }
            set { _serialPort.RtsEnable = value; }
        }

        public bool CtsHolding
        {
            get { return _serialPort.CtsHolding; }
        }

        public bool CDHolding
        {
            get { return _serialPort.CDHolding; }
        }

        public string PortName
        {
            get { return _serialPort.PortName; }
            set { _serialPort.PortName = value; }
        }

        public event SerialDataReceivedEventHandler DataReceived
        {
            add { _serialPort.DataReceived += value; }
            remove { _serialPort.DataReceived -= value; }
        }

        public event SerialPinChangedEventHandler PinChanged
        {
            add { _serialPort.PinChanged += value; }
            remove { _serialPort.PinChanged -= value; }
        }
    }
}