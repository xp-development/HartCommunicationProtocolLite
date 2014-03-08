using System.ComponentModel.Composition;

namespace HartAnalyzer.Shell
{
    /// <summary>
    /// Interaction logic for StatusBarView.xaml
    /// </summary>
    [Export(typeof(StatusBarView))]
    public partial class StatusBarView
    {
        public StatusBarView()
        {
            InitializeComponent();
        }
    }
}
