namespace Finaltec.Hart.Analyzer.ViewModel.DataModels
{
    /// <summary>
    /// SettingsDataModel class.
    /// </summary>
    public class SettingsDataModel
    {
        private static SettingsDataModel _instance;

        /// <summary>
        /// Gets or sets the COM port.
        /// </summary>
        /// <value>The COM port.</value>
        public string ComPort { get; set; }
        /// <summary>
        /// Gets or sets the preamble count.
        /// </summary>
        /// <value>The preamble.</value>
        public string Preamble { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [show settings on start].
        /// </summary>
        /// <value>
        /// 	<c>true</c> if [show settings on start]; otherwise, <c>false</c>.
        /// </value>
        public bool ShowSettingsOnStart { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="SettingsDataModel"/> class.
        /// </summary>
        private SettingsDataModel() {}

        /// <summary>
        /// Gets the instance.
        /// </summary>
        /// <returns></returns>
        public static SettingsDataModel GetInstance()
        {
            return _instance ?? (_instance = new SettingsDataModel());
        }
    }
}