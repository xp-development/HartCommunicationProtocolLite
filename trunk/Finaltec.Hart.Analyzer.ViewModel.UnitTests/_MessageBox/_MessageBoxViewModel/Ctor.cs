using System;
using System.Windows;
using Finaltec.Hart.Analyzer.ViewModel.Common;
using Finaltec.Hart.Analyzer.ViewModel.MessageBox;
using NUnit.Framework;

namespace Finaltec.Hart.Analyzer.ViewModel.UnitTests._MessageBox._MessageBoxViewModel
{
    [TestFixture]
    public class Ctor
    {
        private const string MESSAGE = "Is this a test message?";
        private const string CAPTION = "Test caption";
        private ViewProvider _viewProvider;

        [SetUp]
        public void SetUp()
        {
            _viewProvider = new ViewProvider();
            _viewProvider.AddView("MessageBox", typeof(TestView));
        }

        [Test]
        public void Usage()
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel(_viewProvider, MESSAGE, CAPTION, MessageBoxIcon.Warning, MessageBoxButtonType.YesNo, null);
            messageBoxViewModel.NoCommand.Execute(null);

            Assert.That(messageBoxViewModel, Is.Not.Null);
            Assert.That(messageBoxViewModel.ViewProvider, Is.Not.Null);
            Assert.That(messageBoxViewModel.Message, Is.EqualTo(MESSAGE));
            Assert.That(messageBoxViewModel.Caption, Is.EqualTo(CAPTION));
            Assert.That(messageBoxViewModel.YesVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.NoVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.CancelVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.OkVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.YesIsDefault, Is.True);
            Assert.That(messageBoxViewModel.OkIsDefault, Is.False);
            Assert.That(messageBoxViewModel.BoxImage, Is.Not.Null);
            Assert.That(messageBoxViewModel.MessageBoxDialogResult, Is.EqualTo(MessageBoxDialogResult.No));
        }

        [Test]
        public void UsageWithOwner()
        {
            _viewProvider.AddView("TestView", typeof(TestView));
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel(_viewProvider, MESSAGE, CAPTION, MessageBoxIcon.Error, MessageBoxButtonType.OkCancel, _viewProvider.GetView("TestView"));
            messageBoxViewModel.CancelCommand.Execute(null);

            Assert.That(messageBoxViewModel, Is.Not.Null);
            Assert.That(messageBoxViewModel.ViewProvider, Is.Not.Null);
            Assert.That(messageBoxViewModel.Message, Is.EqualTo(MESSAGE));
            Assert.That(messageBoxViewModel.Caption, Is.EqualTo(CAPTION));
            Assert.That(messageBoxViewModel.YesVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.NoVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.CancelVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.OkVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.YesIsDefault, Is.False);
            Assert.That(messageBoxViewModel.OkIsDefault, Is.True);
            Assert.That(messageBoxViewModel.BoxImage, Is.Not.Null);
            Assert.That(messageBoxViewModel.MessageBoxDialogResult, Is.EqualTo(MessageBoxDialogResult.Cancel));
        }

        [Test]
        public void QuestionIconUsage()
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel(_viewProvider, MESSAGE, CAPTION, MessageBoxIcon.Question, MessageBoxButtonType.Ok, null);
            messageBoxViewModel.OkCommand.Execute(null);

            Assert.That(messageBoxViewModel, Is.Not.Null);
            Assert.That(messageBoxViewModel.ViewProvider, Is.Not.Null);
            Assert.That(messageBoxViewModel.Message, Is.EqualTo(MESSAGE));
            Assert.That(messageBoxViewModel.Caption, Is.EqualTo(CAPTION));
            Assert.That(messageBoxViewModel.YesVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.NoVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.CancelVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.OkVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.YesIsDefault, Is.False);
            Assert.That(messageBoxViewModel.OkIsDefault, Is.True);
            Assert.That(messageBoxViewModel.MessageBoxDialogResult, Is.EqualTo(MessageBoxDialogResult.Ok));
        }

        [Test]
        public void NoneIconUsage()
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel(_viewProvider, MESSAGE, CAPTION, MessageBoxIcon.None, MessageBoxButtonType.Ok, null);
            messageBoxViewModel.NoCommand.Execute(null);

            Assert.That(messageBoxViewModel, Is.Not.Null);
            Assert.That(messageBoxViewModel.ViewProvider, Is.Not.Null);
            Assert.That(messageBoxViewModel.Message, Is.EqualTo(MESSAGE));
            Assert.That(messageBoxViewModel.Caption, Is.EqualTo(CAPTION));
            Assert.That(messageBoxViewModel.YesVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.NoVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.CancelVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.OkVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.YesIsDefault, Is.False);
            Assert.That(messageBoxViewModel.OkIsDefault, Is.True);
            Assert.That(messageBoxViewModel.MessageBoxDialogResult, Is.EqualTo(MessageBoxDialogResult.No));
        }

        [Test]
        public void InformationIconUsage()
        {
            MessageBoxViewModel messageBoxViewModel = new MessageBoxViewModel(_viewProvider, MESSAGE, CAPTION, MessageBoxIcon.Information, MessageBoxButtonType.YesNo, null);
            messageBoxViewModel.YesCommand.Execute(null);

            Assert.That(messageBoxViewModel, Is.Not.Null);
            Assert.That(messageBoxViewModel.ViewProvider, Is.Not.Null);
            Assert.That(messageBoxViewModel.Message, Is.EqualTo(MESSAGE));
            Assert.That(messageBoxViewModel.Caption, Is.EqualTo(CAPTION));
            Assert.That(messageBoxViewModel.YesVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.NoVisibility, Is.EqualTo(Visibility.Visible));
            Assert.That(messageBoxViewModel.CancelVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.OkVisibility, Is.EqualTo(Visibility.Collapsed));
            Assert.That(messageBoxViewModel.YesIsDefault, Is.True);
            Assert.That(messageBoxViewModel.OkIsDefault, Is.False);
            Assert.That(messageBoxViewModel.MessageBoxDialogResult, Is.EqualTo(MessageBoxDialogResult.Yes));
        }
    }
}