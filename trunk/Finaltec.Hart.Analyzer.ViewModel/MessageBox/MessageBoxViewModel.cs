using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Reflection;
using System.Windows;
using System.Windows.Media.Imaging;
using IView = Finaltec.Hart.Analyzer.ViewModel.Common.IView;
using UiCommand = Finaltec.Hart.Analyzer.ViewModel.Common.UiCommand;
using ViewModelBase = Finaltec.Hart.Analyzer.ViewModel.Common.ViewModelBase;
using ViewProvider = Finaltec.Hart.Analyzer.ViewModel.Common.ViewProvider;

namespace Finaltec.Hart.Analyzer.ViewModel.MessageBox
{
    /// <summary>
    /// MessageBoxViewModel class.
    /// Implements base class ViewModelBase.
    /// </summary>
    public class MessageBoxViewModel : ViewModelBase
    {
        #region Fields

        private readonly IView _view;

        private string _caption;
        private string _message;
        private BitmapImage _boxImage;

        private bool _okIsDefault;
        private bool _yesIsDefault;

        private Visibility _okVisibility;
        private Visibility _yesVisibility;
        private Visibility _noVisibility;
        private Visibility _cancelVisibility;

        private MessageBoxDialogResult _messageBoxDialogResult;

        #endregion

        #region Propertys

        /// <summary>
        /// Gets or sets the caption.
        /// </summary>
        /// <value>The caption.</value>
        public string Caption
        {
            get { return _caption; }
            private set { _caption = value; InvokePropertyChanged("Caption"); }
        }
        /// <summary>
        /// Gets or sets the message.
        /// </summary>
        /// <value>The message.</value>
        public string Message
        {
            get { return _message; }
            private set { _message = value; InvokePropertyChanged("Message"); }
        }
        /// <summary>
        /// Gets or sets the box image.
        /// </summary>
        /// <value>The box image.</value>
        public BitmapImage BoxImage
        {
            get { return _boxImage; }
            private set { _boxImage = value; InvokePropertyChanged("BoxImage"); }
        }

        /// <summary>
        /// Gets or sets a value indicating whether [ok is default].
        /// </summary>
        /// <value><c>true</c> if [ok is default]; otherwise, <c>false</c>.</value>
        public bool OkIsDefault
        {
            get { return _okIsDefault; }
            private set { _okIsDefault = value; InvokePropertyChanged("OkIsDefault"); }
        }
        /// <summary>
        /// Gets or sets a value indicating whether [yes is default].
        /// </summary>
        /// <value><c>true</c> if [yes is default]; otherwise, <c>false</c>.</value>
        public bool YesIsDefault
        {
            get { return _yesIsDefault; }
            private set { _yesIsDefault = value; InvokePropertyChanged("YesIsDefault"); }
        }

        /// <summary>
        /// Gets or sets the ok visibility.
        /// </summary>
        /// <value>The ok visibility.</value>
        public Visibility OkVisibility
        {
            get { return _okVisibility; }
            private set { _okVisibility = value; InvokePropertyChanged("OkVisibility"); }
        }
        /// <summary>
        /// Gets or sets the yes visibility.
        /// </summary>
        /// <value>The yes visibility.</value>
        public Visibility YesVisibility
        {
            get { return _yesVisibility; }
            private set { _yesVisibility = value; InvokePropertyChanged("YesVisibility"); }
        }
        /// <summary>
        /// Gets or sets the no visibility.
        /// </summary>
        /// <value>The no visibility.</value>
        public Visibility NoVisibility
        {
            get { return _noVisibility; }
            private set { _noVisibility = value; InvokePropertyChanged("NoVisibility"); }
        }
        /// <summary>
        /// Gets or sets the cancel visibility.
        /// </summary>
        /// <value>The cancel visibility.</value>
        public Visibility CancelVisibility
        {
            get { return _cancelVisibility; }
            private set { _cancelVisibility = value; InvokePropertyChanged("CancelVisibility"); }
        }

        /// <summary>
        /// Gets or sets the ok command.
        /// </summary>
        /// <value>The ok command.</value>
        public UiCommand OkCommand { get; private set; }
        /// <summary>
        /// Gets or sets the yes command.
        /// </summary>
        /// <value>The yes command.</value>
        public UiCommand YesCommand { get; private set; }
        /// <summary>
        /// Gets or sets the no command.
        /// </summary>
        /// <value>The no command.</value>
        public UiCommand NoCommand { get; private set; }
        /// <summary>
        /// Gets or sets the cancel command.
        /// </summary>
        /// <value>The cancel command.</value>
        public UiCommand CancelCommand { get; private set; }

        /// <summary>
        /// Gets or sets the message box dialog result.
        /// </summary>
        /// <value>The message box dialog result.</value>
        public MessageBoxDialogResult MessageBoxDialogResult
        {
            get { return _messageBoxDialogResult; }
            set { _messageBoxDialogResult = value; InvokePropertyChanged("MessageBoxDialogResult"); }
        }

        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBoxViewModel"/> class.
        /// </summary>
        /// <param name="viewProvider">The view provider.</param>
        /// <param name="message">The message.</param>
        /// <param name="caption">The caption.</param>
        /// <param name="icon">The icon.</param>
        /// <param name="buttons">The buttons.</param>
        /// <param name="owner">The owner.</param>
        public MessageBoxViewModel(ViewProvider viewProvider, string message, string caption, MessageBoxIcon icon, MessageBoxButtonType buttons, IView owner) : base(viewProvider)
        {
            _view = viewProvider.GetView("MessageBox", this);

            if(owner != null)
            {
                _view.Owner = owner;
                _view.WindowStartupLocation = WindowStartupLocation.CenterOwner;
            }
            else
            {
                _view.WindowStartupLocation = WindowStartupLocation.CenterScreen;
            }

            Message = message;
            Caption = caption;

            SetVisibility(buttons);
            SetImage(icon);

            InitCommands();

            _view.ShowDialog();
        }

        /// <summary>
        /// Inits the commands.
        /// </summary>
        private void InitCommands()
        {
            OkCommand = new UiCommand(OkCommandExecute);
            YesCommand = new UiCommand(YesCommandExecute);
            NoCommand = new UiCommand(NoCommandExecute);
            CancelCommand = new UiCommand(CancelCommandExecute);
        }

        #region Command Executes

        /// <summary>
        /// Ok command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void OkCommandExecute(object obj)
        {
            MessageBoxDialogResult = MessageBoxDialogResult.Ok;
            _view.DialogResult = true;
        }

        /// <summary>
        /// Yes command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void YesCommandExecute(object obj)
        {
            MessageBoxDialogResult = MessageBoxDialogResult.Yes;
            _view.DialogResult = true;
        }

        /// <summary>
        /// No command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void NoCommandExecute(object obj)
        {
            MessageBoxDialogResult = MessageBoxDialogResult.No;
            _view.DialogResult = true;
        }

        /// <summary>
        /// Cancel command execute.
        /// </summary>
        /// <param name="obj">The obj.</param>
        private void CancelCommandExecute(object obj)
        {
            MessageBoxDialogResult = MessageBoxDialogResult.Cancel;
            _view.DialogResult = true;
        }

        #endregion
        
        #region Set Methods

        /// <summary>
        /// Sets the visibility.
        /// </summary>
        /// <param name="button">The button.</param>
        private void SetVisibility(MessageBoxButtonType button)
        {
            switch (button)
            {
                case MessageBoxButtonType.Ok:
                    OkIsDefault = true;
                    CancelVisibility = Visibility.Collapsed;
                    YesVisibility = Visibility.Collapsed;
                    NoVisibility = Visibility.Collapsed;
                    OkVisibility = Visibility.Visible;
                    break;
                case MessageBoxButtonType.OkCancel:
                    OkIsDefault = true;
                    CancelVisibility = Visibility.Visible;
                    YesVisibility = Visibility.Collapsed;
                    NoVisibility = Visibility.Collapsed;
                    OkVisibility = Visibility.Visible;
                    break;
                case MessageBoxButtonType.YesNo:
                    YesIsDefault = true;
                    CancelVisibility = Visibility.Collapsed;
                    YesVisibility = Visibility.Visible;
                    NoVisibility = Visibility.Visible;
                    OkVisibility = Visibility.Collapsed;
                    break;
            }
        }

        /// <summary>
        /// Sets the image.
        /// </summary>
        /// <param name="image">The image.</param>
        private void SetImage(MessageBoxIcon image)
        {
            Assembly executingAssembly = Assembly.GetExecutingAssembly();
            if (executingAssembly != null)
            {
                switch (image)
                {
                    // ReSharper disable AssignNullToNotNullAttribute
                    case MessageBoxIcon.Error:
                        BoxImage = ConvertToWpf(new Bitmap(executingAssembly.GetManifestResourceStream("Finaltec.Hart.Analyzer.ViewModel.MessageBox.Icons.Error.png")));
                        break;
                    case MessageBoxIcon.Warning:
                        BoxImage = ConvertToWpf(new Bitmap(executingAssembly.GetManifestResourceStream("Finaltec.Hart.Analyzer.ViewModel.MessageBox.Icons.Warning.png")));
                        break;
                    case MessageBoxIcon.Question:
                        BoxImage = ConvertToWpf(new Bitmap(executingAssembly.GetManifestResourceStream("Finaltec.Hart.Analyzer.ViewModel.MessageBox.Icons.Question.png")));
                        break;
                    case MessageBoxIcon.Information:
                        BoxImage = ConvertToWpf(new Bitmap(executingAssembly.GetManifestResourceStream("Finaltec.Hart.Analyzer.ViewModel.MessageBox.Icons.Information.png")));
                        break;
                    // ReSharper restore AssignNullToNotNullAttribute
                }
            }
        }

        /// <summary>
        /// Converts to WPF.
        /// </summary>
        /// <param name="bmp">The BMP.</param>
        /// <returns></returns>
        public BitmapImage ConvertToWpf(Bitmap bmp)
        {
            if (bmp == null)
                return null;

            MemoryStream streamSource = new MemoryStream();
            bmp.Save(streamSource, ImageFormat.Png);
            streamSource.Position = 0;
            BitmapImage bitmapImage = new BitmapImage();
            bitmapImage.BeginInit();
            bitmapImage.StreamSource = streamSource;
            bitmapImage.EndInit();

            return bitmapImage;
        }

        #endregion
    }
}