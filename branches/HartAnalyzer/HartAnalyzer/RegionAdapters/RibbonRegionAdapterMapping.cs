using System;
using System.Collections;
using System.ComponentModel.Composition;
using System.Linq;
using Fluent;
using Microsoft.Practices.Prism.Regions;

namespace HartAnalyzer.RegionAdapters
{
    [Export(typeof(RibbonRegionAdapter))]
    [PartCreationPolicy(CreationPolicy.Shared)]
    public class RibbonRegionAdapter : RegionAdapterBase<Ribbon>
    {
        [ImportingConstructor]
        public RibbonRegionAdapter(IRegionBehaviorFactory regionBehaviorFactory)
            : base(regionBehaviorFactory)
        {}

        protected override void Adapt(IRegion region, Ribbon ribbon)
        {
            if (region == null)
                throw new ArgumentNullException("region");
            if (ribbon == null)
                throw new ArgumentNullException("ribbon");

            if (ribbon.Tabs.Count > 0)
            {
                foreach (var view in (IEnumerable)ribbon.Tabs)
                    region.Add(view);
                ribbon.Tabs.Clear();
            }

            region.Views.ToList().ForEach(item => ribbon.Tabs.Add((RibbonTabItem)item));
        }

        protected override IRegion CreateRegion()
        {
            return new AllActiveRegion();
        }
    }
}