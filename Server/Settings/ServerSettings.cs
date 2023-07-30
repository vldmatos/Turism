namespace Server.Settings
{
    public class ServerSettings
    {
        #region Constantes

        internal const string ContentTypeJson = "application/json";
        internal const short HoursFromCache = 20;

        #endregion Constantes

        public string ConnectionString { get; set; }

        public string DatabaseName { get; set; }
    }
}