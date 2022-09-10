﻿namespace Shared;

public class AppSettings
{
    /// <summary>
    /// user roles
    /// </summary>
    public static class Roles
    {
        public const string Customer = "Customer";
        public const string Seller = "Seller";
        public const string Admin = "Admin";
        public const string Manager = "Manager";
    }

    public static class Users
    {
        public const string AdminUserPhone = "673630000";
        public const string ManagerUserPhone = "673631100";
        public const string SellerUserPhone = "673632200";
    }


    /// <summary>
    /// sms service constants
    /// </summary>
    public static class Sms
    {
        /// <summary>
        /// base url to sms api service
        /// </summary>
        public const string BaseUrl = @"https://api.mobizon.ua/service/message/sendSmsMessage?output=json&api=v1&apiKey=";
        /// <summary>
        /// user`s api key to send a message
        /// </summary>
        public const string ApiKey = @"";
        /// <summary>
        /// autogenerated code lifetime - 30 seconds
        /// </summary>
        public static readonly TimeSpan CodeLifetime = TimeSpan.FromSeconds(30);
        /// <summary>
        /// phone length in format [67 363 00 11]
        /// </summary>
        public const int PhoneLength = 9;
        /// <summary>
        /// autogenerated code length [1234]
        /// </summary>
        public const int CodeLength = 4;
    }

    /// <summary>
    /// jwt token lifetimes
    /// </summary>
    public static class JwtTokenLifetimes
    {
        /// <summary>
        /// 12 hours
        /// </summary>
        public static readonly TimeSpan DefaultExpirationTime = TimeSpan.FromHours(12);
        /// <summary>
        /// 7 days
        /// </summary>
        public static readonly TimeSpan LongerExpirationTime = TimeSpan.FromDays(7);
    }

    public static class Constants
    {
        /// <summary>
        /// path to NLog config file to configure logging
        /// </summary>
        public const string NLogConfigPath = "Logging/nlog.config";
        /// <summary>
        /// default pagination items count per page
        /// </summary>
        public const int DefaultPerPage = 12;
        /// <summary>
        /// default pagination page
        /// </summary>
        public const int DefaultPage = 1;
        /// <summary>
        /// min count of stars for feedback
        /// </summary>
        public const int MinStarsCount = 1;
        /// <summary>
        /// max count of stars for feedback
        /// </summary>
        public const int MaxStarsCount = 5;
    }

    public static class CountryCodes
    {
        public const string Ukraine = "380";
    }

    public static class Formats
    {
        public const string CommentDateFormat = "dd.MM.yyyy";
    }

    public static class CdnPaths
    {
        // Item1 - folder path
        // Item2 - request path
        public static Tuple<string, string> CdnDirectory = Tuple.Create(@"Cdn\", @"/cdn");
        public static Tuple<string, string> CategoryIcons = Tuple.Create(@"Categories\Icons\", @"/categories/icons");
        public static Tuple<string, string> CategoryImages = Tuple.Create(@"Categories\Images\", @"/categories/images");
        public static Tuple<string, string> ProductImages = Tuple.Create(@"Products\Images\", @"/products/images");
        public static Tuple<string, string> UsersAvatars = Tuple.Create(@"Users\Avatars\", @"/users/avatars");
    }
}
