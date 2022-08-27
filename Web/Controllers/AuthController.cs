using Application.Features.AuthFeatures.Commands;
using Application.Features.AuthFeatures.Dtos;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using Shared.AuthServices.OAuth;
using Shared.SecurityServices;
using Web.Controllers.Abstract;

namespace Web.Controllers;

public class AuthController : BaseController
{
    private readonly IMediator _mediator;

    public AuthController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("request")]
    public async Task<ActionResult<Unit>> RequestAsync([FromBody] AddCodeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    [HttpPost("confirm")]
    public async Task<ActionResult<ConfirmResponseDto>> ConfirmAsync([FromBody] ConfirmCodeCommand command)
    {
        return Ok(await _mediator.Send(command));
    }

    private const string redirectUrl = "https://localhost:7165/api/Auth/oauth/code";
    private const string pkceSessionKey = "codeVerifier";

    [HttpGet("oauth/redirect-on-oauth-server")]
    public IActionResult RedirectOnOAuthServer()
    {
        // PCKE.
        var codeVerifier = Guid.NewGuid().ToString();
        var codeChellange = Sha256Service.ComputeHash(codeVerifier);

        HttpContext.Session.SetString(pkceSessionKey, codeVerifier);

        var url = OAuthService.GenerateOAuthRequestUrl(redirectUrl, codeChellange);
        return Redirect(url);
    }

    [HttpGet("oauth/code")]
    public async Task<IActionResult> CodeAsync(string code)
    {
        var codeVerifier = HttpContext.Session.GetString(pkceSessionKey);

        var tokenResult = await OAuthService.ExchangeCodeOnTokenAsync(code, codeVerifier!, redirectUrl);

        // refresh access token using refresh token
        //var refreshedTokenResult = await OAuthService.RefreshTokenAsync(tokenResult.RefreshToken!);

        return Redirect("https://rozetka-clone.herokuapp.com/");
    }
}
