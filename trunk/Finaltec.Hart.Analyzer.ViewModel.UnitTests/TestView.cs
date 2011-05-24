using System.ComponentModel;
using System.Windows;
using Finaltec.Hart.Analyzer.ViewModel.Common;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests
{
    public class TestView : IView
    {
        private bool? _dialogResult;

        public event RoutedEventHandler Loaded;
        public event CancelEventHandler Closing;

        public bool? DialogResult
        {
            set { _dialogResult = value; }
        }

        public object DataContext { get; set; }
        public IView Owner { get; set; }

        public WindowStartupLocation WindowStartupLocation { get; set; }
        public WindowState WindowState { get; set; }

        public double Width { get; set; }
        public double Height { get; set; }

        public static bool WasShow { get; private set; }
        public static bool WasShowDialog { get; private set; }
        public static bool WasClose { get; private set; }
        
        public TestView()
        {
            WasShow = false;
            WasShowDialog = false;
            WasClose = false;
        }

        public void Close()
        {
            WasClose = true;
        }

        public void Show()
        {
            WasShow = true;
        }

        public bool? ShowDialog()
        {
            WasShowDialog = true;
            return _dialogResult;
        }
    }
}