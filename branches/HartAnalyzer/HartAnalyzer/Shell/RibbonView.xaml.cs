using System.ComponentModel.Composition;

namespace HartAnalyzer.Shell
{
    /// <summary>
    /// Interaction logic for RibbonView.xaml
    /// </summary>
    [Export(typeof(RibbonView))]
    public partial class RibbonView
    {
        public RibbonView()
        {
            InitializeComponent();
        }
    }
}
