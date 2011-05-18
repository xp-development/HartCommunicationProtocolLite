namespace Finaltec.Communication.HartLite
{
    public delegate void SendingCommandHandler(object sender, CommandRequest args);

    public class CommandRequest
    {
        private readonly Command _command;

        public int PreambleLength
        {
            get { return _command.PreambleLength; }
        }

        public byte Delimiter
        {
            get { return _command.StartDelimiter; }
        }

        public byte[] Address
        {
            get { return _command.Address; }
        }

        public byte CommandNumber
        {
            get { return _command.CommandNumber; }
        }

        public byte[] Data
        {
            get { return _command.Data; }
        }

        public byte Checksum
        {
            get { return _command.CalculateChecksum(); }
        }

        internal CommandRequest(Command command)
        {
            _command = command;
        }
    }
}