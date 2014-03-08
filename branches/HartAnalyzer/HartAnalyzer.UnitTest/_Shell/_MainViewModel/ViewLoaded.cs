using System;
using FluentAssertions;
using HartAnalyzer.Infrastructure;
using HartAnalyzer.Services;
using HartAnalyzer.Shell;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._Shell._MainViewModel
{
    [TestFixture]
    public class ViewLoaded
    {
        [Test]
        public void ShouldLoadRibbonView()
        {
            ITestCommonServices commonServices = new TestCommonServices();
            commonServices.RegionManager.Regions.Add(new TestRegion(RegionNames.RibbonRegion));
            new MainViewModel(commonServices);

            commonServices.ViewAwareStatus.SimulateViewIsLoadedEvent();

            ((TestRegion) commonServices.RegionManager.Regions[RegionNames.RibbonRegion]).LastRequestedTarget.Should().Be(new Uri(typeof (RibbonView).FullName, UriKind.Relative));
        }

        [Test]
        public void ShouldLoadStatusBarView()
        {
            ITestCommonServices commonServices = new TestCommonServices();
            commonServices.RegionManager.Regions.Add(new TestRegion(RegionNames.StatusBarRegion));
            new MainViewModel(commonServices);

            commonServices.ViewAwareStatus.SimulateViewIsLoadedEvent();

            ((TestRegion)commonServices.RegionManager.Regions[RegionNames.StatusBarRegion]).LastRequestedTarget.Should().Be(new Uri(typeof(StatusBarView).FullName, UriKind.Relative));
        }
    }
}