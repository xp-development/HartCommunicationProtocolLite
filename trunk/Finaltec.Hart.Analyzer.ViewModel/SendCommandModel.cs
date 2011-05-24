using System;
using System.Globalization;
using System.Threading;
using System.Windows;
using Finaltec.Hart.Analyzer.ViewModel.Common;

namespace Finaltec.Hart.Analyzer.ViewModel
{
    /// <summary>
    /// SendCommandModel class.
    /// Implements base class ViewModelBase.
    /// </summary>
    public class SendCommandModel : ViewModelBase
    {
        private byte _command;
        private bool _enableData;
        private string _errorMessage;

        /// <summary>
        /// Gets or sets the command.
        /// </summary>
        /// <value>The command.</value>
        public byte Command
        {
            get { return _command; }
            set { _command = value; EnableData = value > 0; }
        }
        /// <summary>
        /// Gets or sets the data.
        /// </summary>
        /// <value>The data.</value>
        public string Data { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [enable data].
        /// </summary>
        /// <value><c>true</c> if [enable data]; otherwise, <c>false</c>.</value>
        public bool EnableData
        {
            get { return _enableData; }
            set { _enableData = value; InvokePropertyChanged("EnableData"); }
        }

        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage
        {
            get { return _errorMessage; }
            set
            {
                _errorMessage = value;
                InvokePropertyChanged("ErrorMessage");
                InvokePropertyChanged("ShowError");
            }
        }
        /// <summary>
        /// Gets the show error.
        /// </summary>
        /// <value>The show error.</value>
        public Visibility ShowError { get { return (string.IsNullOrEmpty(ErrorMessage)) ? Visibility.Collapsed : Visibility.Visible; }}

        /// <summary>
        /// Gets or sets a value indicating whether [command valid].
        /// </summary>
        /// <value><c>true</c> if [command valid]; otherwise, <c>false</c>.</value>
        public bool CommandValid { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this <see cref="SendCommandModel"/> is response.
        /// </summary>
        /// <value><c>true</c> if response; otherwise, <c>false</c>.</value>
        public bool Response { get; set; }

        /// <summary>
        /// Gets or sets the send command.
        /// </summary>
        /// <value>The send command.</value>
        public UiCommand SendCommand { get; private set; }
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public UiCommand CancelCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SendCommandModel"/> class.
        /// </summary>
        /// <param name="viewProvider">The view provider.</param>
        /// <param name="owner">The owner.</param>
        public SendCommandModel(ViewProvider viewProvider, IView owner = null) : base(viewProvider)
        {
            Data = string.Empty;

            View = viewProvider.GetView("SendCommand", this);
            if (owner != null)
            {
                View.Owner = owner;
                View.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                View.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            InitCommands();

            Response = false;
            View.ShowDialog();
        }

        /// <summary>
        /// Inits the commands.
        /// </summary>
        private void InitCommands()
        {
            SendCommand = new UiCommand(SendCommandExecute, CanExecuteSendCommand);
            CancelCommand = new UiCommand(obj => View.DialogResult = false);
        }

        /// <summary>
        /// Send command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void SendCommandExecute(object obj)
        {
            Response = true;
            View.DialogResult = Response;
        }

        /// <summary>
        /// Determines whether this instance [can execute send command] the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can execute send command] the specified obj; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecuteSendCommand(object obj)
        {
            if (!CheckData()) 
                return false;

            ErrorMessage = string.Empty;
            return CommandValid;
        }

        /// <summary>
        /// Checks the data.
        /// </summary>
        /// <returns></returns>
        private bool CheckData()
        {
            string[] values = Data.Split(new[] {';'}, StringSplitOptions.RemoveEmptyEntries);
            if(values.Length > 0)
            {
                foreach (string str in values)
                {
                    ParseReturnValue parseReturnValue = TypeParser.TryParse(str);
                    
                    if (!parseReturnValue.ParseResult)
                    {
                        ErrorMessage = parseReturnValue.ErrorMessage;
                        return false;
                    }

                    continue;
                }
            }
            return true;
        }
    }
}