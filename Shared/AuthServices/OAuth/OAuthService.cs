

using Microsoft.AspNetCore.WebUtilities;
using Shared.HttpServices;

namespace Shared.AuthServices.OAuth;

public class OAuthService
{
    private const string ClientId = "802607933858-e85sf3lvh622bot3dd3gok37rdf29f9d.apps.googleusercontent.com";
    private const string ClientSecret = "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";

    private const string OAuthServerEndpoint = "https://accounts.google.com/o/oauth2/v2/auth";
    private const string TokenServerEndpoint = "https://oauth2.googleapis.com/token";

    public static string GenerateOAuthRequestUrl(string redirectUrl, string codeChellange)
    {
        var scope = getAllScopes();
        var queryParams = new Dictionary<string, string>
            {
                { "client_id", ClientId},
                { "redirect_uri", redirectUrl },
                { "response_type", "code" },
                { "scope", scope },
                { "code_challenge", codeChellange },
                { "code_challenge_method", "S256" },
                { "access_type", "offline" }
            };

        var url = QueryHelpers.AddQueryString(OAuthServerEndpoint, queryParams);
        return url;
    }

    public static async Task<TokenResult> ExchangeCodeOnTokenAsync(string code, string codeVerifier, string redirectUrl)
    {
        var authParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "code", code },
                { "code_verifier", codeVerifier },
                { "grant_type", "authorization_code" },
                { "redirect_uri", redirectUrl }
            };

        var tokenResult = await HttpClientService.PostAsync<TokenResult>(TokenServerEndpoint, authParams);
        return tokenResult;
    }

    public static async Task<TokenResult> RefreshTokenAsync(string refreshToken)
    {
        var refreshParams = new Dictionary<string, string>
            {
                { "client_id", ClientId },
                { "client_secret", ClientSecret },
                { "grant_type", "refresh_token" },
                { "refresh_token", refreshToken }
            };

        var tokenResult = await HttpClientService.PostAsync<TokenResult>(TokenServerEndpoint, refreshParams);

        return tokenResult;
    }

    private static string getAllScopes()
    {
        Type type = typeof(AppSettings.OAuth.Scopes);

        var scopes = new List<string>();

        foreach (var scope in type.GetFields().Where(f => f.IsPublic))
        {
            scopes.Add(scope.GetValue(scope)?.ToString() ?? "");
        }

        return string.Join(" ", scopes);
    }
}
