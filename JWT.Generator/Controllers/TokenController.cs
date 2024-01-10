namespace JWT.Generator.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TokenController : Controller
{
    private readonly ITokenService _tokenService;

    public TokenController(ITokenService tokenService)
    {
        _tokenService = tokenService;
    }

    [HttpPost]
    public async Task<string> CriarToken(JsonWebToken jsonWebToken) => await _tokenService.CriarTokenAsync(jsonWebToken);
}
