namespace Escritores.Models;

public record RegisterAuthorRequest(
    string FullName,
    DateTime BirthDate,
    string CityOfOrigin,
    string Email);