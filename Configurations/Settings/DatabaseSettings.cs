namespace Configurations.Settings
{
    public class DatabaseSettings : IDatabaseSettings
    {
        public string Host { get; set; }
        public string Key { get; set; }
        public string Name { get; set; }
    }

    public interface IDatabaseSettings
    {
        string Host { get; set; }
        string Key { get; set; }
        string Name { get; set; }
    }
}