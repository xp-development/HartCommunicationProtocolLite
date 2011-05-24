namespace Finaltec.Hart.Analyzer.View.MessageBox
{
    /// <summary>
    /// Interaktionslogik für MessageBox.xaml
    /// </summary>
    public partial class MessageBox
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="MessageBox"/> class.
        /// </summary>
        public MessageBox()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Handles the MouseLeftButtonDown event of the Grid control.
        /// </summary>
        /// <param name="sender">The source of the event.</param>
        /// <param name="e">The <see cref="System.Windows.Input.MouseButtonEventArgs"/> instance containing the event data.</param>
        private void Grid_MouseLeftButtonDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            DragMove();
        }
    }
}
