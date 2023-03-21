using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TestCRUD.Entities;

namespace TestCRUD.Controllers;

[ApiController]
[Route("[controller]")]
public class BookController : ControllerBase
{
    private readonly LibraryContext _context;
    private readonly ILogger _logger;

    public BookController(LibraryContext context, ILogger<BookController> logger)
    {
        _context = context;
        _logger = logger;
    }

    [HttpGet(Name = "GetAllBook")]
    public async Task<List<Book>> Index()
    {
        return await _context.Books.ToListAsync();
    }

    [HttpGet("{id:int}", Name = "GetBook")]
    public IActionResult Get(int id)
    {
        var result = _context.Books.Find(id);

        _logger.LogInformation($"Get Book with ID: {id}");

        if (result == null) return NotFound();

        return Ok(result);
    }

    [HttpPost(Name = "StoreBook")]
    public IActionResult Store(Book book)
    {
        _context.Books.Add(book);
        _context.SaveChanges();

        _logger.LogInformation("Create Book");
        
        var routeValue = new { id = book.BookId };

        return CreatedAtRoute("GetBook", routeValue, book);
    }

    [HttpPut("{id:int}", Name = "UpdateBook")]
    public IActionResult Update(int id, Book book)
    {
        var result = _context.Books.Find(id);
        
        if (result == null) return NotFound();

        result.Title = book.Title;
        result.Author = book.Author;
        result.Publisher = book.Publisher;

        _context.SaveChanges();

        return NoContent();
    }

    [HttpDelete("{id:int}", Name = "DeleteBook")]
    public IActionResult Delete(int id)
    {
        var result = _context.Books.Find(id);

        if (result == null) return NotFound();

        _context.Books.Remove(result);
        _context.SaveChanges();

        return NoContent();
    }
}