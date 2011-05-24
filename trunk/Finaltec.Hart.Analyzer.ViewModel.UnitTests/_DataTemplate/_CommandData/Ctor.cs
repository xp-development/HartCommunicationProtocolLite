using System;
using Finaltec.Hart.Analyzer.ViewModel.DataModels;
using Finaltec.Hart.Analyzer.ViewModel.DataTemplate;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._DataTemplate._CommandData
{
    [TestFixture]
    public class Ctor
    {
        [Test]
        public void Usage()
        {
            CommandData commandData = new CommandData(InformationType.Send, 3, 24, "F9-00-A1-B2-C3", 17, new byte[] { 240, 219, 42, 32, 58, 21, 72, 119, 192, 203 }, "05-44", 110);
            Assert.That(commandData, Is.Not.Null);

            Assert.That(commandData.Address, Is.EqualTo("F9-00-A1-B2-C3"));
            Assert.That(commandData.AddressToolTip, Is.EqualTo("Producer: 'F9', Device-Type: '00', Device Identifier: 'A1-B2-C3'"));
            Assert.That(commandData.ByteCount, Is.EqualTo("0A"));
            Assert.That(commandData.ByteCountToolTip, Is.EqualTo("The command contains '10' data bytes.")); 
            Assert.That(commandData.Checksum, Is.EqualTo("6E"));
            Assert.That(commandData.ChecksumToolTip, Is.EqualTo("Checksum value is '110'")); 
            Assert.That(commandData.Command, Is.EqualTo("11"));
            Assert.That(commandData.CommandToolTip, Is.EqualTo("Command number is '17'")); 
            Assert.That(commandData.Data, Is.EqualTo("F0-DB-2A-20-3A-15-48-77-C0-CB"));
            Assert.That(commandData.DataToolTip, Is.EqualTo("Mark the data bytes for more informations on the status bar.")); 
            Assert.That(commandData.DateTime, Is.Not.Null);
            Assert.That(commandData.DateTimeToolTip.StartsWith("Time '"), Is.True);
            Assert.That(commandData.Delimiter, Is.EqualTo("18"));
            Assert.That(commandData.DelimiterToolTip, Is.EqualTo("Delimiter value is '24'"));  
            Assert.That(commandData.Preamble, Is.EqualTo("FF-FF-FF"));
            Assert.That(commandData.PreambleToolTip, Is.EqualTo("This command has '3' preambles.")); 
            Assert.That(commandData.Response, Is.EqualTo("05-44"));
            Assert.That(commandData.ResponseToolTip, Is.EqualTo("Value of first response byte is '5', value of secound response byte is '68'"));
            Assert.That(commandData.Type, Is.EqualTo("Send   "));
            Assert.That(commandData.TypeToolTip, Is.EqualTo("Informations send to device.")); 

            DataTransferModel.GetInstance().Output.Add(commandData);
        }

        [Test]
        public void SmallCtor()
        {
            CommandData commandData = new CommandData(InformationType.Receive, 1, 24, "Invalid", 17, new byte[] { 240 }, 110);
            Assert.That(commandData, Is.Not.Null);

            Assert.That(commandData.Address, Is.EqualTo("Invalid"));
            Assert.That(commandData.AddressToolTip, Is.EqualTo("Address can not identify"));
            Assert.That(commandData.ByteCount, Is.EqualTo("01"));
            Assert.That(commandData.ByteCountToolTip, Is.EqualTo("The command contains '1' data byte."));
            Assert.That(commandData.Checksum, Is.EqualTo("6E"));
            Assert.That(commandData.ChecksumToolTip, Is.EqualTo("Checksum value is '110'"));
            Assert.That(commandData.Command, Is.EqualTo("11"));
            Assert.That(commandData.CommandToolTip, Is.EqualTo("Command number is '17'"));
            Assert.That(commandData.Data, Is.EqualTo("F0"));
            Assert.That(commandData.DataToolTip, Is.EqualTo("Mark the data bytes for more informations on the status bar."));
            Assert.That(commandData.DateTime, Is.Not.Null);
            Assert.That(commandData.DateTimeToolTip.StartsWith("Time '"), Is.True);
            Assert.That(commandData.Delimiter, Is.EqualTo("18"));
            Assert.That(commandData.DelimiterToolTip, Is.EqualTo("Delimiter value is '24'"));
            Assert.That(commandData.Preamble, Is.EqualTo("FF"));
            Assert.That(commandData.PreambleToolTip, Is.EqualTo("This command has '1' preamble."));
            Assert.That(commandData.Response, Is.Null);
            Assert.That(commandData.ResponseToolTip, Is.EqualTo("No response available"));
            Assert.That(commandData.Type, Is.EqualTo("Receive"));
            Assert.That(commandData.TypeToolTip, Is.EqualTo("Informations reseived from device."));

            DataTransferModel.GetInstance().Output.Add(commandData);
        }
    }
}