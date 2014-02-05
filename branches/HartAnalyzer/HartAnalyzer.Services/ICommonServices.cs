using Cinch;

namespace HartAnalyzer.Services
{
    public interface ICommonServices
    {
        IMessageBoxService MessageBoxService { get; }
        IUIVisualizerService UiVisualizerService { get; }
    }
}