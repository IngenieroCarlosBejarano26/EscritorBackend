namespace Escritores.Models;

public record LoginResponse(string Token, DateTime ExpiresAt);