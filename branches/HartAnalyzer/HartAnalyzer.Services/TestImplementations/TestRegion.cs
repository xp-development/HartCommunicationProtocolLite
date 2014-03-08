using System;
using System.ComponentModel;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Services
{
    public class TestRegion : IRegion
    {
        public Uri LastRequestedTarget { get; set; }

        public IRegionManager Add(object view)
        {
            return _region.Add(view);
        }

        public IRegionManager Add(object view, string viewName)
        {
            return _region.Add(view, viewName);
        }

        public IRegionManager Add(object view, string viewName, bool createRegionManagerScope)
        {
            return _region.Add(view, viewName, createRegionManagerScope);
        }

        public void Remove(object view)
        {
            _region.Remove(view);
        }

        public void Activate(object view)
        {
            _region.Activate(view);
        }

        public void Deactivate(object view)
        {
            _region.Deactivate(view);
        }

        public object GetView(string viewName)
        {
            return _region.GetView(viewName);
        }

        public void RequestNavigate(Uri target, Action<NavigationResult> navigationCallback)
        {
            LastRequestedTarget = target;
        }

        public IRegionBehaviorCollection Behaviors
        {
            get { return _region.Behaviors; }
        }

        public object Context
        {
            get { return _region.Context; }
            set { _region.Context = value; }
        }

        public string Name
        {
            get { return _region.Name; }
            set { _region.Name = value; }
        }

        public IViewsCollection Views
        {
            get { return _region.Views; }
        }

        public IViewsCollection ActiveViews
        {
            get { return _region.ActiveViews; }
        }

        public Comparison<object> SortComparison
        {
            get { return _region.SortComparison; }
            set { _region.SortComparison = value; }
        }

        public IRegionManager RegionManager
        {
            get { return _region.RegionManager; }
            set { _region.RegionManager = value; }
        }

        public IRegionNavigationService NavigationService
        {
            get { return _region.NavigationService; }
            set { _region.NavigationService = value; }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            add { _region.PropertyChanged += value; }
            remove { _region.PropertyChanged -= value; }
        }

        public TestRegion(string name)
        {
            Name = name;
        }

        private readonly Region _region = new Region();
    }
}