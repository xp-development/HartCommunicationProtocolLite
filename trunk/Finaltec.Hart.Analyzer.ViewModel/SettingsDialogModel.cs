using System.Collections.Generic;
using System.IO.Ports;
using System.Windows;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;
using Finaltec.Hart.Analyzer.ViewModel.Properties;

namespace Finaltec.Hart.Analyzer.ViewModel
{
    /// <summary>
    /// SettingsDialogModel class.
    /// Implements base class ViewModelBase.
    /// </summary>
    public class SettingsDialogModel : ViewModelBase
    {
        /// <summary>
        /// Gets or sets the COM ports.
        /// </summary>
        /// <value>The COM ports.</value>
        public List<string> ComPorts { get; set; }
        /// <summary>
        /// Gets or sets the preamble counts.
        /// </summary>
        /// <value>The preamble counts.</value>
        public List<string> PreambleCounts { get; set; }

        /// <summary>
        /// Gets or sets the settings data model.
        /// </summary>
        /// <value>The settings data model.</value>
        public SettingsDataModel SettingsDataModel { get; set; }

        /// <summary>
        /// Gets or sets the confirm command.
        /// </summary>
        /// <value>The confirm command.</value>
        public UiCommand ConfirmCommand { get; private set; }
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public UiCommand CancelCommand { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsDialogModel"/> class.
        /// </summary>
        public SettingsDialogModel(ViewProvider viewProvider, IView owner = null)
            : base(viewProvider)
        {
            SettingsDataModel = SettingsDataModel.GetInstance();

            InitComPorts();
            InitPreambles();
            InitCommands();

            View = ViewProvider.GetView("SettingsView", this);
            if(owner != null)
            {
                View.Owner = owner;
                View.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                View.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            View.ShowDialog();
        }

        /// <summary>
        /// Inits the commands.
        /// </summary>
        private void InitCommands()
        {
            ConfirmCommand = new UiCommand(ConfirmCommandExecute, CanExecuteConfirmCommand);
            CancelCommand = new UiCommand(CancelCommandExecute);
        }

        /// <summary>
        /// Inits the preambles.
        /// </summary>
        private void InitPreambles()
        {
            PreambleCounts = new List<string>();
            for (int i = 5; i < 20; i++)
            {
                PreambleCounts.Add(i.ToString());
            }
        }

        /// <summary>
        /// Inits the COM ports.
        /// </summary>
        private void InitComPorts()
        {
            ComPorts = new List<string>();
            foreach (string portName in SerialPort.GetPortNames())
            {
                ComPorts.Add(portName);
            }
        }

        /// <summary>
        /// Cancel command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void CancelCommandExecute(object obj)
        {
            if (View.Owner == null)
                Application.Current.Shutdown(0);

            View.DialogResult = false;
        }

        /// <summary>
        /// Confirms the command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void ConfirmCommandExecute(object obj)
        {
            SettingsDataModel settings = SettingsDataModel.GetInstance();
            Settings.Default.COM_Port = settings.ComPort;
            Settings.Default.Preambles = settings.Preamble;
            Settings.Default.ShowOnStartup = settings.ShowSettingsOnStart;
            Settings.Default.Save();

            View.DialogResult = true;
        }

        /// <summary>
        /// Determines whether this instance [can execute confirm command] the specified obj.
        /// </summary>
        /// <param name="obj">The obj.</param>
        /// <returns>
        /// 	<c>true</c> if this instance [can execute confirm command] the specified obj; otherwise, <c>false</c>.
        /// </returns>
        private bool CanExecuteConfirmCommand(object obj)
        {
            return !string.IsNullOrEmpty(SettingsDataModel.ComPort) && !string.IsNullOrEmpty(SettingsDataModel.Preamble);
        }
    }
}