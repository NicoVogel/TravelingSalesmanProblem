namespace TSP.Exceptions
{
    /// <summary>
    /// This class contain the event numbers for the entites project
    /// </summary>
    public static class DiagnosticEvents
    {

        #region Heder numbers


        /// <summary>
        /// The base number for the application
        /// </summary>
        public const int Base = 10000;
        /// <summary>
        /// The base number for the bussines layer
        /// </summary>
        public const int BusinessBase = Base + 100;
        /// <summary>
        /// The base number for the data layer
        /// </summary>
        public const int DataBase = Base + 200;
        /// <summary>
        /// The base number for the presentation layer
        /// </summary>
        public const int PresentationBase = Base + 300;


        #endregion

        #region Data Layer


        public const int ConvertMapToIndexList = DataBase + 1;
        public const int MapLinesStructureError = DataBase + 2;


        public const int ReadPointStringEmpty = DataBase + 10;
        public const int ReadPointStructureError = DataBase + 11;
        public const int ReadPointNoNumber = DataBase + 12;


        public const int ReadMapStringEmpty = DataBase + 20;
        public const int ReadMapAgeSturctureError = DataBase + 21;
        public const int ReadMapAgeNoNumberError = DataBase + 22;
        public const int ReadMapIndicesEmpty = DataBase + 23;
        public const int ReadMapIndiceNoNumberError = DataBase + 24;
        public const int ReadMapFirstIndexDoesNotExist = DataBase + 25;
        public const int ReadMapPointIndexNotFound = DataBase + 26;


        public const int LoadPointError = DataBase + 30;
        public const int LoadPointEmpty = DataBase + 31;
        public const int LoadPointNoNumber = DataBase + 32;


        public const int LoadMapError = DataBase + 40;
        public const int SaveMapError = DataBase + 41;
        public const int SavePointError = DataBase + 42;

        #endregion

    }
}
