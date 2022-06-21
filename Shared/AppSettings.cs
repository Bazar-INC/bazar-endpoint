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
    }
}
