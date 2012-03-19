using System.Diagnostics;
using System.Windows.Documents;
using System.Windows.Navigation;
using Finaltec.Hart.Analyzer.ViewModel;

namespace Finaltec.Hart.Analyzer.View
{
    /// <summary>
    /// Interaction logic for AboutDialog.xaml
    /// </summary>
    public partial class AboutDialog
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void Hyperlink_OnRequestNavigate(object sender, RequestNavigateEventArgs e)
        {
            string navigateUri = ((Hyperlink) sender).NavigateUri.ToString();
            Process.Start(new ProcessStartInfo(navigateUri));
            e.Handled = true;
        }
    }
}
