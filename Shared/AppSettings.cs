namespace Shared;

public class AppSettings
{
    public static class Roles
    {
        public const string Customer = "Customer";
    }

    public static class Sms
    {
        public const string BaseUrl = @"https://api.mobizon.ua/service/message/sendSmsMessage?output=json&api=v1&apiKey=";
        public const string ApiKey = @"";
        public static readonly TimeSpan CodeLifetime = TimeSpan.FromSeconds(30);
    }

    public static class JwtTokenLifetimes
    {
        public static readonly TimeSpan DefaultExpirationTime = TimeSpan.FromHours(12);
        public static readonly TimeSpan LongerExpirationTime = TimeSpan.FromDays(7);
    }

    public static class Constants
    {
        public const string NLogConfigPath = "Logging/nlog.config";
    }
}
