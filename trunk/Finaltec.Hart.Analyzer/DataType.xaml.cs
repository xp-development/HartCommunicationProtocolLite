namespace Finaltec.Hart.Analyzer.View
{
    /// <summary>
    /// Interaktionslogik für DataType.xaml
    /// </summary>
    public partial class DataType
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="DataType"/> class.
        /// </summary>
        public DataType()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Sets the data context.
        /// </summary>
        /// <param name="dataContext">The data context.</param>
        public void SetDataContext(object dataContext)
        {
            DataContext = dataContext;
        }
    }
}
