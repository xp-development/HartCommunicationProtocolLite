using System.Windows;

namespace HartAnalyzer
{
    public interface ISettingsService
    {
        WindowState MainViewWindowState { get; set; }
        int MainViewLeft { get; set; }
        int MainViewTop { get; set; }
        int MainViewHeight { get; set; }
        int MainViewWidth { get; set; }
        bool ShowChecksumFilter { get; set; }
        bool ShowDataFilter { get; set; }
        bool ShowLengthFilter { get; set; }
        bool ShowCommandFilter { get; set; }
        bool ShowAddressFilter { get; set; }
        bool ShowDelimiterFilter { get; set; }
        bool ShowPreamblesFilter { get; set; }
        bool ShowTypeFilter { get; set; }
        bool ShowTimeFilter { get; set; }
        bool ShowResponseCodeFilter { get; set; }
        string CommandFilter { get; set; }
    }
}