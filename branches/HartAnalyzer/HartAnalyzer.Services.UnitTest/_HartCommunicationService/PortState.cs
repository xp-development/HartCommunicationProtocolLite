using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq.Expressions;
using Communication.Hart;
using FluentAssertions;
using Moq;
using NUnit.Framework;

namespace HartAnalyzer.Services.UnitTest._HartCommunicationService
{
    [TestFixture]
    public class PortState
    {
        [Test]
        public void DefaultPortStateShouldBeClosed()
        {
            var mock = new Mock<IHartCommunication>();
            var service = new HartCommunicationService(mock.Object);
            service.PortState.Should().Be(Services.PortState.Closed);
        }

        [Test]
        public async void ShouldSetPortStateFromOpeningToOpened()
        {
            var mock = new Mock<IHartCommunication>();
            mock.Setup(item => item.Open()).Returns(OpenResult.Opened);
            var service = new HartCommunicationService(mock.Object);
            var states = new List<Services.PortState>();
            service.LogChanges(() => service.PortState, states);

            await service.OpenAsync();

            states.Count.Should().Be(2);
            states[0].Should().Be(Services.PortState.Opening);
            states[1].Should().Be(Services.PortState.Opened);
        }

        [Test]
        public async void ShouldSetPortStateFromOpenedToClosed()
        {
            var mock = new Mock<IHartCommunication>();
            mock.Setup(item => item.Close()).Returns(CloseResult.Closed);
            var service = new HartCommunicationService(mock.Object) {PortState = Services.PortState.Opened};
            
            var states = new Queue<Services.PortState>();
            states.Enqueue(Services.PortState.Closing);
            states.Enqueue(Services.PortState.Closed);
            service.LogChanges(() => service.PortState, states);

            await service.CloseAsync();

            states.Count.Should().Be(0);
        }
    }

    public static class UnitTestHelper
    {
        public static void LogChanges<TNotify, TProperty>(this TNotify notifyObject, Expression<Func<TProperty>> property, List<TProperty> history)
            where TNotify : INotifyPropertyChanged
        {
            var name = ((MemberExpression)property.Body).Member.Name;

            notifyObject.PropertyChanged += (sender, args) =>
                {
                    if (name != args.PropertyName)
                        return;

                    var compile = property.Compile();
                    history.Add(compile());
                };
        }

        public static void LogChanges<TNotify, TProperty>(this TNotify notifyObject, Expression<Func<TProperty>> property, Queue<TProperty> expects)
            where TNotify : INotifyPropertyChanged
        {
            var name = ((MemberExpression)property.Body).Member.Name;

            notifyObject.PropertyChanged += (sender, args) =>
                {
                    if (name != args.PropertyName)
                        return;

                    var compile = property.Compile();
                    Assert.That(compile(), Is.EqualTo(expects.Dequeue()));
                };
        }
    }
}