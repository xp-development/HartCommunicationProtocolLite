using System;
using System.Windows;
using System.Windows.Controls;

namespace Finaltec.Hart.Analyzer.View.UserControls
{
    /// <summary>
    /// ConnectMenuItem class.
    /// Implements base class MenuItem.
    /// </summary>
    public class ConnectMenuItem : MenuItem
    {
        /// <summary>
        /// DependencyProperty IsConnectedProperty.
        /// </summary>
        public static readonly DependencyProperty IsConnectedProperty =
                DependencyProperty.Register("IsConnected",
                typeof(Boolean), typeof(ConnectMenuItem), new PropertyMetadata(false));

        /// <summary>
        /// Gets or sets a value indicating whether this instance is connected.
        /// </summary>
        /// <value>
        /// 	<c>true</c> if this instance is connected; otherwise, <c>false</c>.
        /// </value>
        public bool IsConnected
        {
            get { return (bool)GetValue(IsConnectedProperty); }
            set { SetValue(IsConnectedProperty, value); }
        }
    }
}