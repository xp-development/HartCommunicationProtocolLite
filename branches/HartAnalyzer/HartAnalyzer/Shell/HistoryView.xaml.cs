using System.Collections.Specialized;
using System.ComponentModel.Composition;

namespace HartAnalyzer.Shell
{
    /// <summary>
    /// Interaction logic for HistoryView.xaml
    /// </summary>
    [Export(typeof(HistoryView))]
    public partial class HistoryView
    {
        public HistoryView()
        {
            InitializeComponent();

            ((INotifyCollectionChanged) HistoryListView.Items).CollectionChanged += (sender, args) =>
                {
                    if (HistoryListView.SelectedIndex != HistoryListView.Items.Count - 2)
                        return;

                    var item = HistoryListView.Items[HistoryListView.Items.Count - 1];
                    HistoryListView.ScrollIntoView(item);
                    HistoryListView.SelectedItem = item;
                };
        }
    }
}