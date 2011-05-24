using System;
using System.Collections.Generic;

namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// ViewProvider class.
    /// </summary>
    public class ViewProvider
    {
        private readonly Dictionary<string, Type> _viewContainer;

        /// <summary>
        /// Initializes a new instance of the <see cref="ViewProvider"/> class.
        /// </summary>
        public ViewProvider()
        {
            _viewContainer = new Dictionary<string, Type>();
        }

        /// <summary>
        /// Adds the view.
        /// </summary>
        /// <param name="viewId">The view id.</param>
        /// <param name="viewType">Type of the view.</param>
        public void AddView(string viewId, Type viewType)
        {
            if (string.IsNullOrEmpty(viewId))
            {
                throw new ArgumentNullException("viewId");
            }

            if (viewType == null)
            {
                throw new ArgumentNullException("viewType");
            }

            if (viewType.GetInterface("Finaltec.Hart.Analyzer.ViewModel.Common.IView") != typeof(IView))
            {
                throw new InterfaceIsNotImplementedException("Type does not implement interface IView.");
            }

            _viewContainer.Add(viewId, viewType);
        }

        /// <summary>
        /// Gets the view.
        /// </summary>
        /// <param name="viewId">The view id.</param>
        /// <param name="dataContext">The data context.</param>
        /// <returns></returns>
        public IView GetView(string viewId, object dataContext)
        {
            IView view = GetView(viewId);
            view.DataContext = dataContext;
            return view;
        }

        /// <summary>
        /// Get the view with the corosponding viewId.
        /// </summary>
        /// <param name="viewId"></param>
        /// <returns></returns>
        /// <exception cref="ArgumentException">Will be thrown if the view with the submitted viewId does not exist.</exception>
        public IView GetView(string viewId)
        {
            if (!_viewContainer.ContainsKey(viewId))
            {
                throw new ArgumentException("ViewID does not exists.");
            }

            Type viewType = _viewContainer[viewId];
            IView view = (IView)Activator.CreateInstance(viewType);

            return view;
        }
    }
}