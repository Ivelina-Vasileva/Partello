namespace Partello.Config
{
    public class TestSettings
    {
        public string BaseUrl { get; set; }
        public string EventPageUrl { get; set; }
        public string Browser { get; set; }
        public string LoginEmail { get; set; } = string.Empty;
        public string LoginPassword { get; set; } = string.Empty;
        public string GoogleEmail { get; set; } = string.Empty;
        public string GooglePassword { get; set; } = string.Empty;
    }
}
