namespace APIAggregation.Helpers
{
    /// <summary>
    /// Enum file
    /// </summary>
    public class Enum
    {
        /// <summary>
        /// Error Codes
        /// </summary>
        public enum ErrorCodes : int
        {
            /// <summary>
            /// 
            /// </summary>
            NoError = 0,

            /// <summary>
            /// 
            /// </summary>
            SystemError = 9999
        }

        /// <summary>
        /// Languages
        /// </summary>
        public enum Languages : int
        {
            /// <summary>
            /// English
            /// </summary>
            EN = 0,

            /// <summary>
            /// German
            /// </summary>
            DE = 1,

            /// <summary>
            /// Czech
            /// </summary>
            CS = 2,

            /// <summary>
            /// Spanish
            /// </summary>
            ES = 3,

            /// <summary>
            /// French
            /// </summary>
            FR = 4,

            /// <summary>
            /// Portuguese
            /// </summary>
            PT = 5
        }

        /// <summary>
        /// Flags of categories
        /// </summary>
        [Flags]
        public enum Categories
        {
            /// <summary>
            /// Any
            /// </summary>
            Any = 0,

            /// <summary>
            /// Programming
            /// </summary>
            Programming = 1,

            /// <summary>
            /// Misc
            /// </summary>
            Miscellaneous = 2,

            /// <summary>
            /// Dark
            /// </summary>
            Dark = 4,

            /// <summary>
            /// Pun
            /// </summary>
            Pun = 8,

            /// <summary>
            /// Spooky
            /// </summary>
            Spooky = 16,

            /// <summary>
            /// Christmas
            /// </summary>
            Christmas = 32
        }

        /// <summary>
        /// The Units that the weather was available.
        /// </summary>
        public enum WeatherUnit
        {
            /// <summary>
            /// Celsius
            /// </summary>
            C,
            /// <summary>
            /// Fahrenheit 
            /// </summary>
            F
        }
    }
}