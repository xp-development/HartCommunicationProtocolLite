using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Threading;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.Services
{
    public class TestRegionManager : IRegionManager
    {
        public TestRegionManager()
        {
            _regionCollection = new TestRegionCollection(this);
        }

        public IRegionManager CreateRegionManager()
        {
            return new TestRegionManager();
        }

        public IRegionCollection Regions { get { return _regionCollection; } }

        private readonly IRegionCollection _regionCollection;
    }

    public class TestRegionCollection : IRegionCollection
    {
        private readonly IRegionManager _regionManager;
        private readonly List<IRegion> _regions;
        private NotifyCollectionChangedEventHandler _collectionChanged;

        public IRegion this[string regionName]
        {
            get
            {
                RegionManager.UpdateRegions();
                var regionByName = GetRegionByName(regionName);
                if (regionByName != null)
                    return regionByName;
                throw new Exception();
            }
        }

        event NotifyCollectionChangedEventHandler INotifyCollectionChanged.CollectionChanged
        {
            add
            {
                NotifyCollectionChangedEventHandler changedEventHandler = _collectionChanged;
                NotifyCollectionChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler =
                        Interlocked.CompareExchange(ref _collectionChanged, comparand + value, comparand);
                } while (changedEventHandler != comparand);
            }
            remove
            {
                NotifyCollectionChangedEventHandler changedEventHandler = _collectionChanged;
                NotifyCollectionChangedEventHandler comparand;
                do
                {
                    comparand = changedEventHandler;
                    changedEventHandler = Interlocked.CompareExchange(ref _collectionChanged, comparand - value, comparand);
                } while (changedEventHandler != comparand);
            }
        }

        public TestRegionCollection(IRegionManager regionManager)
        {
            _regionManager = regionManager;
            _regions = new List<IRegion>();
        }

        public IEnumerator<IRegion> GetEnumerator()
        {
            RegionManager.UpdateRegions();
            return _regions.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }

        public void Add(IRegion region)
        {
            if (region == null)
                throw new ArgumentNullException("region");
            RegionManager.UpdateRegions();
            if (region.Name == null)
                throw new InvalidOperationException();
            if (GetRegionByName(region.Name) != null)
                throw new ArgumentException();
            _regions.Add(region);
            region.RegionManager = _regionManager;
            OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, region, 0));
        }

        public bool Remove(string regionName)
        {
            RegionManager.UpdateRegions();
            var flag = false;
            var regionByName = GetRegionByName(regionName);
            if (regionByName != null)
            {
                flag = true;
                _regions.Remove(regionByName);
                regionByName.RegionManager = null;
                OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, regionByName, 0));
            }
            return flag;
        }

        public bool ContainsRegionWithName(string regionName)
        {
            RegionManager.UpdateRegions();
            return GetRegionByName(regionName) != null;
        }

        private IRegion GetRegionByName(string regionName)
        {
            return _regions.FirstOrDefault(r => r.Name == regionName);
        }

        private void OnCollectionChanged(NotifyCollectionChangedEventArgs notifyCollectionChangedEventArgs)
        {
            var changedEventHandler = _collectionChanged;
            if (changedEventHandler == null)
                return;
            changedEventHandler(this, notifyCollectionChangedEventArgs);
        }
    }
}