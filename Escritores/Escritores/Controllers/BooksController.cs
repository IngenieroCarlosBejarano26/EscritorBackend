namespace Escritores.Controllers;

[ApiController]
[Route("api/[controller]")]
[Authorize]
public class BooksController(IMediator mediator) : ControllerBase
{
    private readonly IMediator _mediator = mediator;

    [HttpGet]
    [ProducesResponseType(typeof(IReadOnlyList<BookDto>), StatusCodes.Status200OK)]
    public async Task<ActionResult<IReadOnlyList<BookDto>>> GetAll(CancellationToken cancellationToken)
    {
        IReadOnlyList<BookDto> result = await _mediator.Send(new GetBooksQuery(), cancellationToken);
        return Ok(result);
    }

    [HttpGet("{id:guid}")]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<ActionResult<BookDto>> GetById(Guid id, CancellationToken cancellationToken)
    {
        BookDto? result = await _mediator.Send(new GetBookByIdQuery(id), cancellationToken);
        return result is null ? NotFound() : Ok(result);
    }

    [HttpPost]
    [ProducesResponseType(typeof(BookDto), StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<ActionResult<BookDto>> Register(
        [FromBody] RegisterBookRequest request,
        CancellationToken cancellationToken)
    {
        RegisterBookCommand command = new(
            request.Title,
            request.Year,
            request.GenreId,
            request.NumberOfPages,
            request.AuthorId);
        BookDto result = await _mediator.Send(command, cancellationToken);
        return CreatedAtAction(nameof(GetById), new { id = result.Id }, result);
    }
}