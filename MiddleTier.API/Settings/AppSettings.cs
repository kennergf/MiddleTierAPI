namespace MiddleTier.API.Settings
{
    public class AppSettings
    {
        public string DB_API_URL { get; set; }

        public string Secret { get; set; }
        public int ExpirationInHours { get; set; }
        public string Issuer { get; set; }
        public string ValidIn { get; set; }
    }
}