namespace Finaltec.Hart.Analyzer.ViewModel.Common
{
    /// <summary>
    /// ParseReturnValue struct.
    /// </summary>
    public struct ParseReturnValue
    {
        /// <summary>
        /// Gets or sets the error message.
        /// </summary>
        /// <value>The error message.</value>
        public string ErrorMessage { get; set; }
        /// <summary>
        /// Gets or sets a value indicating whether [parse result].
        /// </summary>
        /// <value><c>true</c> if [parse result]; otherwise, <c>false</c>.</value>
        public bool ParseResult { get; set; }
        /// <summary>
        /// Gets or sets the parse value.
        /// </summary>
        /// <value>The parse value.</value>
        public object ParseValue { get; set; }
        /// <summary>
        /// Gets or sets the byte array.
        /// </summary>
        /// <value>The byte array.</value>
        public byte[] ByteArray { get; set; }
    }
}