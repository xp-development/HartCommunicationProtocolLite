using Cinch;
using HartAnalyzer.Infrastructure;

namespace HartAnalyzer.ConnectionConfiguration
{
    /// <summary>
    /// Interaction logic for ConnectionConfigurationView.xaml
    /// </summary>
    [PopupNameToViewLookupKeyMetadata(ViewNames.ConnectionConfigurationView, typeof(ConnectionConfigurationView))]
    public partial class ConnectionConfigurationView
    {
        public ConnectionConfigurationView()
        {
            InitializeComponent();
        }
    }
}
