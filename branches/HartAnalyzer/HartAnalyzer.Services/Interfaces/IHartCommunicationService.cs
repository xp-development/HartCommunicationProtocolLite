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
        string PortName { get; }
    }
}