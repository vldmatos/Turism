namespace Server.Settings
{
    internal class VaultSettings
    {
        #region Properties

        public string KeyVaultAddress { get; private set; }
        public string TenantId { get; private set; }
        public string ClientId { get; private set; }
        public string ClientSecret { get; private set; }
        public bool Initialized { get; private set; } = false;

        #endregion Properties

        public VaultSettings(string environment)
        {
            if (environment != EnvironmentSettings.Development)
            {
                KeyVaultAddress = "https://turism-vault-staging.vault.azure.net/";
                TenantId = "d6f40208-0e69-4598-a450-1e46d6668a38";
                ClientId = "f6acbd00-b0c2-4696-abd8-3f7bfc5ae541";
                ClientSecret = "cj57Q~R936irWBL-7.EUTnxHXzNohNR41iOSJ";
                Initialized = true;
            }
        }
    }
}