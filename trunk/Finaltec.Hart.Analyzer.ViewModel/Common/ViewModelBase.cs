using System.ComponentModel;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// ViewModelBase class.
    /// Implements Interface INotifyPropertyChanged.
    /// </summary>
    public class ViewModelBase : INotifyPropertyChanged
    {
        /// <summary>
        /// The View for the Viewmodel.
        /// </summary>
        protected IView View;

        /// <summary>
        /// Gets or sets the view provider.
        /// </summary>
        /// <value>The view provider.</value>
        public ViewProvider ViewProvider { get; private set; }

        public ViewModelBase(ViewProvider viewProvider)
        {
            ViewProvider = viewProvider;
        }

        /// <summary>
        /// Invokes the property changed.
        /// </summary>
        /// <param name="propertyName">Name of the property.</param>
        public void InvokePropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }

        /// <summary>
        /// Occurs on changes.
        /// </summary>
        public event PropertyChangedEventHandler PropertyChanged;
    }
}