using System;
using System.Globalization;
using System.Threading;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;

namespace Finaltec.Hart.Analyzer.ViewModel.DataTemplate
{
    /// <summary>
    /// CommandData class.
    /// </summary>
    public class CommandData
    {
        private const string TIME_PATTERN = "HH:mm:ss.fff";

        private readonly InformationType _type;
        private readonly int _preamble;

        /// <summary>
        /// Gets or sets the date time.
        /// </summary>
        /// <value>The date time.</value>
        public string DateTime { get; private set; }
        /// <summary>
        /// Gets the type.
        /// </summary>
        /// <value>The type.</value>
        public string Type { get { return string.Format("{0,-7}", _type); } }
        /// <summary>
        /// Gets or sets the preamble.
        /// </summary>
        /// <value>The preamble.</value>
        public string Preamble { get; private set; }
        /// <summary>
        /// Gets or sets the delimiter.
        /// </summary>
        /// <value>The delimiter.</value>
        public string Delimiter { get; private set; }
        /// <summary>
        /// Gets or sets the address.
        /// </summary>
        /// <value>The address.</value>
        public string Address { get; private set; }
        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public string Command { get; private set; }
        /// <summary>
        /// Gets or sets the byte count.
        /// </summary>
        /// <value>The byte count.</value>
        public string ByteCount { get; private set; }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; private set; }
        /// <summary>
        /// Gets or sets the response.
        /// </summary>
        /// <value>The response.</value>
        public string Response { get; private set; }
        /// <summary>
        /// Gets or sets the checksum.
        /// </summary>
        /// <value>The checksum.</value>
        public string Checksum { get; private set; }

        /// <summary>
        /// Gets the date time tool tip.
        /// </summary>
        /// <value>The date time tool tip.</value>
        public string DateTimeToolTip
        {
            get { return CreateDateTimeToolTip(); }
        }
        /// <summary>
        /// Gets the type tool tip.
        /// </summary>
        /// <value>The type tool tip.</value>
        public string TypeToolTip
        {
            get { return (_type == InformationType.Receive) ? "Informations reseived from device." : "Informations send to device."; }
        }
        /// <summary>
        /// Gets the preamble tool tip.
        /// </summary>
        /// <value>The preamble tool tip.</value>
        public string PreambleToolTip
        {
            get
            {
                return (_preamble > 1) ? string.Format("This command has '{0}' preambles.", _preamble) : string.Format("This command has '{0}' preamble.", _preamble);
            }
        }
        /// <summary>
        /// Gets the delimiter tool tip.
        /// </summary>
        /// <value>The delimiter tool tip.</value>
        public string DelimiterToolTip
        {
            get { return string.Format("Delimiter value is '{0}'", Convert.ToInt32(Delimiter, 16)); }
        }
        /// <summary>
        /// Gets the address tool tip.
        /// </summary>
        /// <value>The address tool tip.</value>
        public string AddressToolTip
        {
            get { return CreateAddressToolTip(); }
        }
        /// <summary>
        /// Gets the command tool tip.
        /// </summary>
        /// <value>The command tool tip.</value>
        public string CommandToolTip
        {
            get { return string.Format("Command number is '{0}'", Convert.ToByte(Command, 16)); }
        }
        /// <summary>
        /// Gets the byte count tool tip.
        /// </summary>
        /// <value>The byte count tool tip.</value>
        public string ByteCountToolTip
        {
            get
            {
                byte bytes = Convert.ToByte(ByteCount, 16);
                return (bytes > 1) ? string.Format("The command contains '{0}' data bytes.", bytes) : string.Format("The command contains '{0}' data byte.", bytes);
            }
        }
        /// <summary>
        /// Gets the data tool tip.
        /// </summary>
        /// <value>The data tool tip.</value>
        public string DataToolTip
        {
            get { return "Mark the data bytes for more informations on the status bar."; }
        }
        /// <summary>
        /// Gets the response tool tip.
        /// </summary>
        /// <value>The response tool tip.</value>
        public string ResponseToolTip
        {
            get { return CreateResponseToolTip(); }
        }
        /// <summary>
        /// Gets the checksum tool tip.
        /// </summary>
        /// <value>The checksum tool tip.</value>
        public string ChecksumToolTip
        {
            get { return string.Format("Checksum value is '{0}'", Convert.ToByte(Checksum, 16)); }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandData"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="preamble">The preamble.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="address">The address.</param>
        /// <param name="command">The command.</param>
        /// <param name="data">The data.</param>
        /// <param name="checksum">The checksum.</param>
        public CommandData(InformationType type, int preamble, byte delimiter, string address, byte command, byte[] data, byte checksum)
            : this(type, preamble, delimiter, address, command, data, null, checksum)
        {}

        /// <summary>
        /// Initializes a new instance of the <see cref="CommandData"/> class.
        /// </summary>
        /// <param name="type">The type.</param>
        /// <param name="preamble">The preamble.</param>
        /// <param name="delimiter">The delimiter.</param>
        /// <param name="address">The address.</param>
        /// <param name="command">The command.</param>
        /// <param name="data">The data.</param>
        /// <param name="response">The response.</param>
        /// <param name="checksum">The checksum.</param>
        public CommandData(InformationType type, int preamble, byte delimiter, string address, byte command, byte[] data, string response, byte checksum)
        {
            DateTime = string.Format("{0}", System.DateTime.Now.ToString(TIME_PATTERN));
            _type = type;
            _preamble = preamble;

            for (int i = 0; i < preamble; i++)
            {
                if (!string.IsNullOrEmpty(Preamble))
                    Preamble += "-FF";
                else
                    Preamble = "FF";
            }

            Delimiter = string.Format("{0:X2}", delimiter);
            Address = address;
            Command = string.Format("{0:X2}", command);
            ByteCount = string.Format("{0:X2}", data.Length);
            Data = BitConverter.ToString(data);
            Response = response;
            Checksum = string.Format("{0:X2}", checksum);
        }

        /// <summary>
        /// Creates the date time tool tip.
        /// </summary>
        /// <returns></returns>
        private string CreateDateTimeToolTip()
        {
            DataTransferModel dataTransferModel = DataTransferModel.GetInstance();
            string time = DateTime;

            if(dataTransferModel.Output.Count > 1)
            {
                DateTime dateTime = System.DateTime.Parse(time);
                TimeSpan timeDiffToFirst = dateTime.Subtract(System.DateTime.Parse(dataTransferModel.Output[0].DateTime));
                TimeSpan timeDiffToLast = dateTime.Subtract(System.DateTime.Parse(dataTransferModel.Output[dataTransferModel.Output.Count - 2].DateTime));

                if (timeDiffToFirst.Milliseconds > 0)
                    return string.Format("Time '{0}', time span to the first entry '{1}', time span to the entry before '{2}'", time, timeDiffToFirst.ToString().Remove(TIME_PATTERN.Length), timeDiffToLast.ToString().Remove(TIME_PATTERN.Length));
            }

            return string.Format("Time '{0}'", time);
        }

        /// <summary>
        /// Creates the address tool tip.
        /// </summary>
        /// <returns></returns>
        private string CreateAddressToolTip()
        {
            if(Address != null)
            {
                string[] addressParts = Address.Split(new[] {'-'});
                if(addressParts.Length == 5)
                {
                    string deviceIdentifier = string.Format("{0}-{1}-{2}", addressParts[2], addressParts[3], addressParts[4]);

                    //todo implement correct producer
                    return string.Format("Producer: '{0}', Device-Type: '{1}', Device Identifier: '{2}'", addressParts[0], addressParts[1], deviceIdentifier);
                }
            }

            return "Address can not identify";
        }

        /// <summary>
        /// Creates the response tool tip.
        /// </summary>
        /// <returns></returns>
        private string CreateResponseToolTip()
        {
            if(Response != null)
            {
                string[] responseParts = Response.Split(new[] {'-'});
                if(responseParts.Length == 2)
                {
                    return string.Format("Value of first response byte is '{0}', value of secound response byte is '{1}'", 
                        Convert.ToByte(responseParts[0], 16), Convert.ToByte(responseParts[1], 16));
                }
            }

            return "No response available";
        }
    }
}