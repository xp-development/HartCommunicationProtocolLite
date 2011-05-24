using System;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// InterfaceIsNotImplementedException exception.
    /// Implements base class NotImplementedException.
    /// </summary>
    public class InterfaceIsNotImplementedException : NotImplementedException
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="InterfaceIsNotImplementedException"/> class.
        /// </summary>
        /// <param name="message">The message.</param>
        public InterfaceIsNotImplementedException(string message) : base(message)
        {}
    }
}