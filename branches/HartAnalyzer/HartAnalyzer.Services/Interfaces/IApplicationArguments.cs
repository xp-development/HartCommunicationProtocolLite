namespace HartAnalyzer.Services
{
    public interface IApplicationArguments
    {
        bool IsIsolatedTestModeEnabled { get; }
    }
}