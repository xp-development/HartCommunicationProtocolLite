using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading;
using Finaltec.Communication.HartLite;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;
using Finaltec.Hart.Analyzer.ViewModel.DataTemplate;
using Finaltec.Hart.Analyzer.ViewModel.MessageBox;
using Finaltec.Hart.Analyzer.ViewModel.Properties;

namespace Finaltec.Hart.Analyzer.ViewModel
{
    /// <summary>
    /// MainViewModel class.
    /// Implements base class ViewModelBase.
    /// </summary>
    public class MainViewModel : ViewModelBase
    {
        private string _value;
        private HartCommunicationLite _hartCommunicationLite;
        private readonly SynchronizationContext _synchronizationContext;

        private string _selectedOutput = "";
        private bool _isConnected;
        private bool _swapBytes;

        /// <summary>
        /// Gets or sets the data transfer model.
        /// </summary>
        /// <value>The data transfer model.</value>
        public DataTransferModel DataTransferModel { get; set; }
        /// <summary>
        /// Gets or sets the filter.
        /// </summary>
        /// <value>The filter.</value>
        public Filter Filter { get; set; }

        /// <summary>
        /// Gets or sets the selected output.
        /// </summary>
        /// <value>The selected output.</value>
        public string SelectedOutput
        {
            get { return _selectedOutput; }
            set { _selectedOutput = value; SelectedOutputChanged(); }
        }
        /// <summary>
        /// Gets or sets the value.
        /// </summary>
        /// <value>The value.</value>
        public string Value
        {
            get { return _value; }
            set { _value = value; InvokePropertyChanged("Value"); }
        }
        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get { return _isConnected; }
            set { _isConnected = value; InvokePropertyChanged("IsConnected"); }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [swap bits].
        /// </summary>
        /// <value><c>true</c> if [swap bits]; otherwise, <c>false</c>.</value>
        public bool SwapBytes
        {
            get { return _swapBytes; }
            set
            {
                _swapBytes = value;
                InvokePropertyChanged("SwapBytes");

                Settings.Default.BitSwapping = value;
                Settings.Default.Save();

                SelectedOutputChanged();
            }
        }

        /// <summary>
        /// Gets or sets the connect disconnect command.
        /// </summary>
        /// <value>The connect disconnect command.</value>
        public UiCommand ConnectDisconnectCommand { get; private set; }
        /// <summary>
        /// Gets or sets the send command.
        /// </summary>
        /// <value>The send command.</value>
        public UiCommand SendCommand { get; private set; }
        /// <summary>
        /// Gets or sets the display connection settings command.
        /// </summary>
        /// <value>The display connection settings command.</value>
        public UiCommand DisplayConnectionSettingsCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="MainViewModel"/> class.
        /// </summary>
        /// <param name="viewProvider">The view provider.</param>
        public MainViewModel(ViewProvider viewProvider) : base(viewProvider)
        {
            ReadSettings();
            DataTransferModel = DataTransferModel.GetInstance();
            _synchronizationContext = SynchronizationContext.Current;

            InitCommands();
        }

        /// <summary>
        /// Inits the view.
        /// </summary>
        public void InitView()
        {
            View = ViewProvider.GetView("MainView", this);
            View.Closing += ViewClosingEventHandle;

            View.Width = Settings.Default.Width;
            View.Height = Settings.Default.Height;
            View.WindowState = Settings.Default.WindowState;

            if (Settings.Default.ShowOnStartup)
            {
                new SettingsDialogModel(ViewProvider);
            }

            View.Show();
        }

        /// <summary>
        /// Inits the commands.
        /// </summary>
        private void InitCommands()
        {
            ConnectDisconnectCommand = new UiCommand(ConnectDisconnectCommandExecute);
            SendCommand = new UiCommand(SendCommandExecute, obj => IsConnected); 
            DisplayConnectionSettingsCommand = new UiCommand(DisplayConnectionSettingsCommandExecute, obj => !IsConnected);
        }

        /// <summary>
        /// Reads the settings.
        /// </summary>
        private void ReadSettings()
        {
            SettingsDataModel settings = SettingsDataModel.GetInstance();
            settings.ComPort = Settings.Default.COM_Port;
            settings.Preamble = Settings.Default.Preambles;
            settings.ShowSettingsOnStart = Settings.Default.ShowOnStartup;

            SwapBytes = Settings.Default.BitSwapping;
            Filter = new Filter();
        }

        /// <summary>
        /// Views the closing event handle.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.ComponentModel.CancelEventArgs"/> instance containing the event data.</param>
        private void ViewClosingEventHandle(object sender, CancelEventArgs e)
        {
            View.Closing -= ViewClosingEventHandle;

            Settings.Default.Width = View.Width;
            Settings.Default.Height = View.Height;
            Settings.Default.WindowState = View.WindowState;
            Settings.Default.Save();
        }

        /// <summary>
        /// Send command execute.
        /// </summary>
        /// <param name="o">The o.</param>
        private void SendCommandExecute(object o)
        {
            SendCommandModel sendCommandModel = new SendCommandModel(ViewProvider, View);
            if(sendCommandModel.Response)
            {
                List<byte> databytes = new List<byte>();

                string[] dataParts = sendCommandModel.Data.Split(new[] { ';' }, StringSplitOptions.RemoveEmptyEntries);
                foreach (string dataPart in dataParts)
                {
                    string value = dataPart.Trim();
                    if (string.IsNullOrEmpty(value))
                        continue;

                    byte[] bytes = TypeParser.TryParse(value).ByteArray;
                    if (bytes != null)
                    {
                        databytes.AddRange(bytes);
                        continue;
                    }
                    
                    return;
                }

                _hartCommunicationLite.SendAsync(sendCommandModel.Command, (databytes.Count > 0) ? databytes.ToArray() : new byte[0]);
            }
        }

        /// <summary>
        /// Connect and disconnect command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void ConnectDisconnectCommandExecute(object obj)
        {
            if (!IsConnected)
            {
                try
                {
                    _hartCommunicationLite = new HartCommunicationLite(SettingsDataModel.GetInstance().ComPort);
                    OpenResult result = _hartCommunicationLite.Open();

                    switch (result)
                    {
                        case OpenResult.UnknownComPortError:
                            new MessageBoxViewModel(ViewProvider, "Unknown COM port error while connecting to device.", "Error",
                                           MessageBoxIcon.Error, MessageBoxButtonType.Ok, View);
                            return;
                        case OpenResult.ComPortNotExisting:
                            new MessageBoxViewModel(ViewProvider, "The selected COM port does not exists.", "Error",
                                           MessageBoxIcon.Error, MessageBoxButtonType.Ok, View);
                            return;
                        case OpenResult.ComPortIsOpenAlreadyOpen:
                            new MessageBoxViewModel(ViewProvider, "The COM port is already opened.", "Error",
                                           MessageBoxIcon.Error, MessageBoxButtonType.Ok, View);
                            return;
                    }

                    _hartCommunicationLite.PreambleLength = Convert.ToInt32(SettingsDataModel.GetInstance().Preamble);
                    _hartCommunicationLite.Receive += ReceiveValueHandle;
                    _hartCommunicationLite.SendingCommand += SendingValueHandle;

                    IsConnected = true;
                }
                catch (Exception e)
                {
                    new MessageBoxViewModel(ViewProvider, "Error on connecting to device.\n\n" + e, "Error",
                                            MessageBoxIcon.Error, MessageBoxButtonType.Ok, View);
                }
            }
            else
            {
                try
                {
                    CloseResult result = _hartCommunicationLite.Close();

                    switch (result)
                    {
                        case CloseResult.PortIsNotOpen:
                            new MessageBoxViewModel(ViewProvider, "The port is not open.", "Error",
                                           MessageBoxIcon.Error, MessageBoxButtonType.Ok, View);
                            return;
                    }

                    IsConnected = false;

                    _hartCommunicationLite.Receive -= ReceiveValueHandle;
                    _hartCommunicationLite.SendingCommand -= SendingValueHandle;
                    _hartCommunicationLite = null;
                }
                catch (Exception e)
                {
                    new MessageBoxViewModel(ViewProvider, "Error on disconnecting from device.\n\n" + e, "Error",
                                            MessageBoxIcon.Error, MessageBoxButtonType.Ok, View);
                }
            }
        }

        /// <summary>
        /// Sendings the value handle.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void SendingValueHandle(object sender, CommandRequest args)
        {
            _synchronizationContext.Send(delegate
            {
                DataTransferModel.Output.Add(new CommandData(InformationType.Send, args.PreambleLength, args.Delimiter, BitConverter.ToString(args.Address.ToByteArray()), args.CommandNumber, args.Data,
                    args.Checksum));
            }, null);
        }

        /// <summary>
        /// Receives the value handle.
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="args">The args.</param>
        private void ReceiveValueHandle(object sender, CommandResult args)
        {
            _synchronizationContext.Send(delegate
            {
                DataTransferModel.Output.Add(new CommandData(InformationType.Receive, args.PreambleLength, args.Delimiter, BitConverter.ToString(args.Address.ToByteArray()), args.CommandNumber, args.Data,
                   BitConverter.ToString(new[] { args.ResponseCode.FirstByte, args.ResponseCode.SecondByte }), args.Checksum));
            }, null);
        }

        /// <summary>
        /// Selecteds the output changed.
        /// </summary>
        private void SelectedOutputChanged()
        {
            string[] outputParts = SelectedOutput.Split(new[] { '-' }, StringSplitOptions.RemoveEmptyEntries);
            if (outputParts.Any(outputPart => outputPart.Trim().Length > 2) || outputParts.Any(outputPart => string.IsNullOrEmpty(outputPart) || outputPart == " "))
            {
                return;
            }

            List<byte> selectedBytes = outputParts.Select(outputPart => byte.Parse(outputPart, NumberStyles.HexNumber)).ToList();

            string byteValue = string.Empty;
            string shortValue = string.Empty;
            string uShortValue = string.Empty;
            string int24Value = string.Empty;
            string uInt24Value = string.Empty;
            string intValue = string.Empty;
            string uIntValue = string.Empty;
            string floatValue = string.Empty;
            string stringValue = string.Empty;

            if (selectedBytes.Count == 1)
                byteValue = string.Format("Byte Value: '{0}' - ", selectedBytes[0]);

            if(selectedBytes.Count == 2)
                shortValue = string.Format("Short Value: '{0}' - ", (SwapBytes) ? BitConverter.ToInt16(selectedBytes.ToArray().Reverse().ToArray(), 0) : BitConverter.ToInt16(selectedBytes.ToArray(), 0));

            if(selectedBytes.Count == 2)
                uShortValue = string.Format("UShort Value: '{0}' - ", (SwapBytes) ? BitConverter.ToUInt16(selectedBytes.ToArray().Reverse().ToArray(), 0) : BitConverter.ToUInt16(selectedBytes.ToArray(), 0));

            if (selectedBytes.Count == 3) 
                int24Value = string.Format("Int24 Value: '{0}' - ", (SwapBytes) ? new Int24(BitConverter.ToInt32(new byte[] { selectedBytes[2], selectedBytes[1], selectedBytes[0], 0 }, 0)).Value : new Int24(BitConverter.ToInt32(new byte[] { selectedBytes[0], selectedBytes[1], selectedBytes[2], 0 }, 0)).Value);
            
            if (selectedBytes.Count == 3)
                uInt24Value = string.Format("UInt24 Value: '{0}' - ", (SwapBytes) ? BitConverter.ToUInt32(new byte[] { selectedBytes[0], selectedBytes[1], selectedBytes[2], 0 }, 0) : BitConverter.ToUInt32(new byte[] { selectedBytes[2], selectedBytes[1], selectedBytes[0], 0 }, 0));
            
            if (selectedBytes.Count == 4)
                intValue = string.Format("Int32 Value: '{0}' - ", (SwapBytes) ? BitConverter.ToInt32(selectedBytes.ToArray().Reverse().ToArray(), 0) : BitConverter.ToInt32(selectedBytes.ToArray(), 0));

            if (selectedBytes.Count == 4)
                uIntValue = string.Format("UInt32 Value: '{0}' - ", (SwapBytes) ? BitConverter.ToUInt32(selectedBytes.ToArray().Reverse().ToArray(), 0) : BitConverter.ToUInt32(selectedBytes.ToArray(), 0));

            if (selectedBytes.Count == 4)
                floatValue = string.Format("Float Value: '{0}' - ", (SwapBytes) ? BitConverter.ToSingle(selectedBytes.ToArray().Reverse().ToArray(), 0) : BitConverter.ToSingle(selectedBytes.ToArray(), 0));

            if(selectedBytes.Count > 0)
                stringValue = string.Format("String Value: '{0}'", Encoding.ASCII.GetString(selectedBytes.ToArray()));

            Value = string.Format("{0}{1}{2}{3}{4}{5}{6}{7}{8}", (!string.IsNullOrEmpty(byteValue)) ? byteValue : "",
                                    (!string.IsNullOrEmpty(shortValue)) ? shortValue : "",
                                    (!string.IsNullOrEmpty(uShortValue)) ? uShortValue : "",
                                    (!string.IsNullOrEmpty(int24Value)) ? int24Value : "",
                                    (!string.IsNullOrEmpty(uInt24Value)) ? uInt24Value : "",
                                    (!string.IsNullOrEmpty(intValue)) ? intValue : "",
                                    (!string.IsNullOrEmpty(uIntValue)) ? uIntValue : "",
                                    (!string.IsNullOrEmpty(floatValue)) ? floatValue : "",
                                    (!string.IsNullOrEmpty(stringValue)) ? stringValue : "");
        }

        /// <summary>
        /// Displays the connection settings command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void DisplayConnectionSettingsCommandExecute(object obj)
        {
            new SettingsDialogModel(ViewProvider, View);
        }
    }
}