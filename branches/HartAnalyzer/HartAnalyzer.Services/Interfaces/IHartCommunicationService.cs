using System.Collections.Generic;
using System.ComponentModel;
using System.Threading.Tasks;
using Communication.Hart;

namespace HartAnalyzer.Services
{
    public interface IHartCommunicationService : INotifyPropertyChanged
    {
        Task<OpenResult> OpenAsync();
        Task<CloseResult> CloseAsync();
        PortState PortState { get; }
        string PortName { get; set; }

        ICollection<string> PossiblePortNames { get; }
        event SendingCommandHandler SendingCommand;
        event ReceiveHandler Receive;
        Task Send(byte command);
        Task Send(byte command, byte[] data);
    }
}