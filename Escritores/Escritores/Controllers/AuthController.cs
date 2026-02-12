namespace Escritores.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(IConfiguration configuration) : ControllerBase
{
    private readonly IConfiguration _configuration = configuration;

    [HttpPost("login")]
    [AllowAnonymous]
    [ProducesResponseType(typeof(LoginResponse), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    public ActionResult<LoginResponse> Login([FromBody] LoginRequest request)
    {
        string? user = _configuration["Jwt:User"];
        string? password = _configuration["Jwt:Password"];
        if (string.IsNullOrEmpty(user) || request.UserName != user || request.Password != password)
            return Unauthorized();

        string token = GenerateJwtToken(request.UserName);
        return Ok(new LoginResponse(token, DateTime.UtcNow.AddHours(24)));
    }

    private string GenerateJwtToken(string userName)
    {
        SymmetricSecurityKey key = new(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"] ?? "ClaveSecretaMinimo32CaracteresParaHS256!!"));
        SigningCredentials credentials = new(key, SecurityAlgorithms.HmacSha256);
        Claim[] claims =
        [
            new Claim(ClaimTypes.Name, userName),
            new Claim(JwtRegisteredClaimNames.Sub, userName),
            new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString())
        ];
        JwtSecurityToken token = new(
            issuer: _configuration["Jwt:Issuer"],
            audience: _configuration["Jwt:Audience"],
            claims: claims,
            expires: DateTime.UtcNow.AddHours(24),
            signingCredentials: credentials);
        return new JwtSecurityTokenHandler().WriteToken(token);
    }
}