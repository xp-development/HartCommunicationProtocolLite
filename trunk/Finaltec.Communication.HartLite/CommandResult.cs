namespace Finaltec.Communication.HartLite
{
    public class CommandResult
    {
        private readonly Command _command;

        public byte CommandNumber
        {
            get { return _command.CommandNumber; }
        }

        public byte[] Data
        {
            get { return _command.Data; }
        }

        public ResponseCode ResponseCode
        {
            get { return ResponseCode.ToResponseCode(_command.ResponseCode); }
        }

        public byte[] Address
        {
            get { return _command.Address; }
        }

        public int PreambleLength
        {
            get { return _command.PreambleLength; }
        }

        internal CommandResult(Command command)
        {
            _command = command;
        }
    }
}