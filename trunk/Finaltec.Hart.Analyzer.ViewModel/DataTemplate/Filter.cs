using System.ComponentModel;
using Finaltec.Hart.Analyzer.ViewModel.Properties;

namespace Finaltec.Hart.Analyzer.ViewModel.DataTemplate
{
    /// <summary>
    /// Filter class.
    /// Implements interface INotifyPropertyChanged.
    /// </summary>
    public class Filter : INotifyPropertyChanged
    {
        private bool _displayTime;
        private bool _displaySendOrRecived;
        private bool _displayPreamble;
        private bool _displayDelimiter;
        private bool _displayAddress;
        private bool _displayCommand;
        private bool _displayByteCount;
        private bool _displayData;
        private bool _displayResponse;
        private bool _displayChecksum;

        /// <summary>
        /// Gets or sets a value indicating whether [display time].
        /// </summary>
        /// <value><c>true</c> if [display time]; otherwise, <c>false</c>.</value>
        public bool DisplayTime
        {
            get { return _displayTime; }
            set
            {
                _displayTime = value;
                InvokePropertyChanged("DisplayTime");

                Settings.Default.Display_Time = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display send or recived].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [display send or recived]; otherwise, <c>false</c>.
        /// </value>
        public bool DisplaySendOrRecived
        {
            get { return _displaySendOrRecived; }
            set
            {
                _displaySendOrRecived = value;
                InvokePropertyChanged("DisplaySendOrRecived");

                Settings.Default.Display_SendOrRecived = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display preamble].
        /// </summary>
        /// <value><c>true</c> if [display preamble]; otherwise, <c>false</c>.</value>
        public bool DisplayPreamble
        {
            get { return _displayPreamble; }
            set
            {
                _displayPreamble = value;
                InvokePropertyChanged("DisplayPreamble");

                Settings.Default.Display_Preamble = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display delimiter].
        /// </summary>
        /// <value><c>true</c> if [display delimiter]; otherwise, <c>false</c>.</value>
        public bool DisplayDelimiter
        {
            get { return _displayDelimiter; }
            set
            {
                _displayDelimiter = value;
                InvokePropertyChanged("DisplayDelimiter");

                Settings.Default.Display_Delimiter = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display address].
        /// </summary>
        /// <value><c>true</c> if [display address]; otherwise, <c>false</c>.</value>
        public bool DisplayAddress
        {
            get { return _displayAddress; }
            set
            {
                _displayAddress = value;
                InvokePropertyChanged("DisplayAddress");

                Settings.Default.Display_Address = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display command].
        /// </summary>
        /// <value><c>true</c> if [display command]; otherwise, <c>false</c>.</value>
        public bool DisplayCommand
        {
            get { return _displayCommand; }
            set
            {
                _displayCommand = value;
                InvokePropertyChanged("DisplayCommand");

                Settings.Default.Display_Command = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display byte count].
        /// </summary>
        /// <value><c>true</c> if [display byte count]; otherwise, <c>false</c>.</value>
        public bool DisplayByteCount
        {
            get { return _displayByteCount; }
            set
            {
                _displayByteCount = value;
                InvokePropertyChanged("DisplayByteCount");

                Settings.Default.Display_ByteCount = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display data].
        /// </summary>
        /// <value><c>true</c> if [display data]; otherwise, <c>false</c>.</value>
        public bool DisplayData
        {
            get { return _displayData; }
            set
            {
                _displayData = value;
                InvokePropertyChanged("DisplayData");

                Settings.Default.Display_Data = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display response].
        /// </summary>
        /// <value><c>true</c> if [display response]; otherwise, <c>false</c>.</value>
        public bool DisplayResponse
        {
            get { return _displayResponse; }
            set
            {
                _displayResponse = value;
                InvokePropertyChanged("DisplayResponse");

                Settings.Default.Display_Response = value;
                Settings.Default.Save();
            }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [display checksum].
        /// </summary>
        /// <value><c>true</c> if [display checksum]; otherwise, <c>false</c>.</value>
        public bool DisplayChecksum
        {
            get { return _displayChecksum; }
            set
            {
                _displayChecksum = value;
                InvokePropertyChanged("DisplayChecksum");

                Settings.Default.Display_Checksum = value;
                Settings.Default.Save();
            }
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Filter"/> class.
        /// </summary>
        public Filter()
        {
            DisplayTime = Settings.Default.Display_Time;
            DisplaySendOrRecived = Settings.Default.Display_SendOrRecived;
            DisplayPreamble = Settings.Default.Display_Preamble;
            DisplayDelimiter = Settings.Default.Display_Delimiter;
            DisplayAddress = Settings.Default.Display_Address;
            DisplayCommand = Settings.Default.Display_Command;
            DisplayByteCount = Settings.Default.Display_ByteCount;
            DisplayData = Settings.Default.Display_Data;
            DisplayResponse = Settings.Default.Display_Response;
            DisplayChecksum = Settings.Default.Display_Checksum;
        }

        /// <summary>
        /// Invokes the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs on changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}