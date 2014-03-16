using System;
using FluentAssertions;
using HartAnalyzer.Infrastructure;
using HartAnalyzer.Services;
using HartAnalyzer.Shell;
using HartAnalyzer.SpecificCommands;
using NUnit.Framework;

namespace HartAnalyzer.UnitTest._Shell._MainViewModel
{
    [TestFixture]
    public class ViewLoaded
    {
        [Test]
        public void ShouldLoadStatusBarView()
        {
            ITestCommonServices commonServices = new TestCommonServices();
            commonServices.RegionManager.Regions.Add(new TestRegion(RegionNames.StatusBarRegion));
            new MainViewModel(commonServices);

            commonServices.ViewAwareStatus.SimulateViewIsLoadedEvent();

            ((TestRegion)commonServices.RegionManager.Regions[RegionNames.StatusBarRegion]).LastRequestedTarget.Should().Be(new Uri(typeof(StatusBarView).FullName, UriKind.Relative));
        }

        [Test]
        public void ShouldLoadHistoryView()
        {
            ITestCommonServices commonServices = new TestCommonServices();
            commonServices.RegionManager.Regions.Add(new TestRegion(RegionNames.MainRegion));
            new MainViewModel(commonServices);

            commonServices.ViewAwareStatus.SimulateViewIsLoadedEvent();

            ((TestRegion)commonServices.RegionManager.Regions[RegionNames.MainRegion]).LastRequestedTarget.Should().Be(new Uri(typeof(HistoryView).FullName, UriKind.Relative));
        }

        [Test]
        public void ShouldLoadSpecificCommandView()
        {
            ITestCommonServices commonServices = new TestCommonServices();
            commonServices.RegionManager.Regions.Add(new TestRegion(RegionNames.MainRegion));
            new MainViewModel(commonServices);

            commonServices.ViewAwareStatus.SimulateViewIsLoadedEvent();

            ((TestRegion)commonServices.RegionManager.Regions[RegionNames.SpecificCommandRegion]).LastRequestedTarget.Should().Be(new Uri(typeof(SpecificCommandView).FullName, UriKind.Relative));
        }
    }
}