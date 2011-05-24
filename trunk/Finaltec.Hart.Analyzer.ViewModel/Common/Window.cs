using System;
using System.Windows;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    public class Window : System.Windows.Window, IView
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Window"/> class.
        /// </summary>
        protected Window()
        {
            base.Loaded += OnViewLoaded;
            base.Unloaded += OnViewUnloaded;

            Closed += OnViewClosed;
        }

        /// <summary>
        /// Called when [view unloaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnViewUnloaded(object sender, RoutedEventArgs e)
        {
            if (Unloaded != null)
                Unloaded(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [view loaded].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.Windows.RoutedEventArgs"/> instance containing the event data.</param>
        private void OnViewLoaded(object sender, RoutedEventArgs e)
        {
            if (Loaded != null)
                Loaded(this, EventArgs.Empty);
        }

        /// <summary>
        /// Called when [view closed].
        /// </summary>
        /// <param name="sender">The sender.</param>
        /// <param name="e">The <see cref="System.EventArgs"/> instance containing the event data.</param>
        private void OnViewClosed(object sender, EventArgs e)
        {
            Closed -= OnViewClosed;

            base.Loaded -= OnViewLoaded;
            base.Unloaded -= OnViewUnloaded;
        }

        /// <summary>
        /// Gets the <see cref="T:System.Windows.Window"/> that is the owner to this.
        /// </summary>
        /// <value></value>
        /// <returns>
        /// The owner <see cref="T:System.Windows.Window"/>-Objekt.
        /// </returns>
        /// <exception cref="T:System.ArgumentException">
        /// A window try to own himself.
        /// - or -
        /// Two windows try to own the other one.
        /// </exception>
        /// <exception cref="T:System.InvalidOperationException">
        /// The <see cref="P:System.Windows.Window.Owner"/>-Property was set for a <see cref="T:System.Windows.Window"/> that was called with <see cref="M:System.Windows.Window.ShowDialog"/>
        /// – or –
        /// The <see cref="P:System.Windows.Window.Owner"/>-Property was set to a no displayed window.
        /// </exception>
        public new IView Owner
        {
            get { return (IView)base.Owner; }
            set { base.Owner = (System.Windows.Window)value; }
        }

        /// <summary>
        /// Gets or sets the view startup location.
        /// </summary>
        /// <value>The view startup location.</value>
        public WindowStartupLocation ViewStartupLocation
        {
            get { return WindowStartupLocation; }
            set { WindowStartupLocation = value; }
        }

        /// <summary>
        /// Was loaded event.
        /// </summary>
        public new event EventHandler Loaded;
        /// <summary>
        /// Was unloaded event.
        /// </summary>
        public new event EventHandler Unloaded;
    }
}