using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using TrustGuard_API.Data;
using TrustGuard_API.DTOs;
using TrustGuard_API.Models;

namespace TrustGuard_API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class BookController : ControllerBase
{
    public readonly ApplicationDbContext _context;

    public BookController(ApplicationDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<ActionResult<IEnumerable<Book>>> Get()
    {
        return await _context.Books.ToListAsync();
        // var lecturers = await _context.Lecturers.Include(l => l.University).ToListAsync();
        // return lecturers;
    }

    [HttpPost]
    public async Task<ActionResult<Book>> Create(BookDTO bookDto)
    {
        // if (!_context.Authors.Any(u => u.AuthorId == bookDto.AuthorId))
        // {
        //     return BadRequest("Invalid UniversityId");
        // }

        var book = new Book
        {
            Title = bookDto.Title,
            PublicationYear = bookDto.PublicationYear,
            AuthorId = bookDto.AuthorId
        };

        await _context.Books.AddAsync(book);
        await _context.SaveChangesAsync();
        return Ok(book);
    }

    [HttpPut("{id}")]
    public async Task<ActionResult<Book>> Update(int id, BookDTO updatedBookDto)
    {
        var book = await _context.Books.Include(l => l.Author)
            .FirstOrDefaultAsync(l => l.BookId == id);
        if (book == null)
        {
            return NotFound();
        }

        book.Title = updatedBookDto.Title;
        book.PublicationYear = updatedBookDto.PublicationYear;
        book.AuthorId = updatedBookDto.AuthorId;

        await _context.SaveChangesAsync();

        return Ok(book);
    }


    [HttpDelete("{id}")]
    public async Task<ActionResult<Book>> Delete(int id)
    {
        var book = await _context.Books.FindAsync(id);
        if (book == null)
        {
            return NotFound();
        }

        _context.Books.Remove(book);
        await _context.SaveChangesAsync();
        var booksList = await _context.Books.ToListAsync();
        return Ok(booksList);
    }
}