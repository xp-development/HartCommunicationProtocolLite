using System;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._DataModels._SettingsDataModel
{
    [TestFixture]
    public class GetInstance
    {
        [Test]
        public void Usage()
        {
            SettingsDataModel settingsDataModel = SettingsDataModel.GetInstance();
            Assert.That(settingsDataModel, Is.Not.Null);
        }
    }
}