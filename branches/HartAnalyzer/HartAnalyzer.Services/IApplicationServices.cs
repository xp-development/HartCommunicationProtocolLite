namespace HartAnalyzer.Services
{
    public interface IApplicationServices
    {
        IHartCommunicationService HartCommunicationService { get; }
    }
}