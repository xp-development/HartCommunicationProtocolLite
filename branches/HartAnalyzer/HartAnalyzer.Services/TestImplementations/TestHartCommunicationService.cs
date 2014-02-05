using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;
using System.Threading.Tasks;
using Communication.Hart;
using HartAnalyzer.Services.Annotations;

namespace HartAnalyzer.Services
{
    public class TestHartCommunicationService : IHartCommunicationService
    {
        private PortState _portState;
        public Queue<Func<OpenResult>> OpenAsyncResponders { get; set; }
        public Queue<Func<CloseResult>> CloseAsyncResponders { get; set; }

        public PortState PortState
        {
            get { return _portState; }
            set
            {
                _portState = value;
                NotifyPropertyChanged();
            }
        }

        public string PortName
        {
            get { return _portName; }
            set
            {
                _portName = value;
                NotifyPropertyChanged();
            }
        }

        public TestHartCommunicationService()
            : this("COM1")
        {}

        public TestHartCommunicationService(string portName)
        {
            _portName = portName;

            OpenAsyncResponders = new Queue<Func<OpenResult>>();
            CloseAsyncResponders = new Queue<Func<CloseResult>>();
        }

        public async Task<OpenResult> OpenAsync()
        {
            if (OpenAsyncResponders.Count == 0)
                throw new ApplicationException("TestHartCommunicationService OpenAsync method expects a Func<OpenResult> callback \r\ndelegate to be enqueued for each OpenAsync call");

            return await Task.Run(() => OpenAsyncResponders.Dequeue()());
        }

        public async Task<CloseResult> CloseAsync()
        {
            if (CloseAsyncResponders.Count == 0)
                throw new ApplicationException("TestHartCommunicationService CloseAsync method expects a Func<CloseResult> callback \r\ndelegate to be enqueued for each CloseAsync call");

            return await Task.Run(() => CloseAsyncResponders.Dequeue()());
        }

        public event PropertyChangedEventHandler PropertyChanged;

        [NotifyPropertyChangedInvocator]
        protected virtual void NotifyPropertyChanged([CallerMemberName] string propertyName = null)
        {
            var handler = PropertyChanged;
            if (handler != null) handler(this, new PropertyChangedEventArgs(propertyName));
        }

        private string _portName;
    }
}