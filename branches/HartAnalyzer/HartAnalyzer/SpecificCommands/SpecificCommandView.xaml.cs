using System.ComponentModel.Composition;
using System.Windows.Controls;

namespace HartAnalyzer.SpecificCommands
{
    /// <summary>
    /// Interaction logic for SpecificCommandView.xaml
    /// </summary>
    [Export(typeof(SpecificCommandView))]
    public partial class SpecificCommandView
    {
        public SpecificCommandView()
        {
            InitializeComponent();
        }

        private void OnCommandListBoxSelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (DataContext != null)
                ((SpecificCommandViewModel)DataContext).TextBoxGotFocusAction();
        }
    }
}