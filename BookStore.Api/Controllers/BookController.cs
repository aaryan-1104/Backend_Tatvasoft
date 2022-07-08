using BookStore.Models.Models;
using BookStore.Models.ViewModels;
using BookStore.Repository;
using Microsoft.AspNetCore.Mvc;

namespace BookStore.Api.Controllers
{
    [Route("books")]
    [ApiController]
    public class BookController : ControllerBase
    {

        BookRepository _bookRepository = new BookRepository();
        [HttpGet("getBooks")]
        public IActionResult GetBooks(int pageIndex = 1, int pageSize = 10, string? keyword = "")
        {
            var books =_bookRepository.GetBooks(pageIndex, pageSize, keyword);
            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)).ToList(),
                Totalrecords = books.Totalrecords
            };

            return Ok(listResponse);
        }

        [HttpGet("getBooksByCategory")]
        public IActionResult GetBooksByCategory(int pageIndex = 1, int pageSize = 10, string? keyword = "", int categoryid=0)
        {
            var books = _bookRepository.GetBooks(pageIndex, pageSize, keyword, categoryid);
            ListResponse<BookModel> listResponse = new ListResponse<BookModel>()
            {
                Results = books.Results.Select(c => new BookModel(c)).ToList(),
                Totalrecords = books.Totalrecords
            };

            return Ok(listResponse);
        }

        [HttpGet("getBook/{id}")]
        [ProducesResponseType(typeof(BookModel), 200)]
        [ProducesResponseType(typeof(NotFoundResult), 404)]
        public IActionResult AddBook(int id)
        {
            var book = _bookRepository.GetBook(id);

            if (book == null)
            {
                return NotFound();
            }

            BookModel bookModel = new BookModel(book);

            return Ok(bookModel);
        }

        [HttpPost("addBook")]
        [ProducesResponseType(typeof(BookModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult AddBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest("No data found to be added");
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity
            };
            var entry = _bookRepository.AddBook(book);

            BookModel bookModel = new BookModel(entry);

            return Ok(bookModel);
        }

        [HttpPut("updateBook")]
        [ProducesResponseType(typeof(BookModel), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult UpdateBook(BookModel model)
        {
            if (model == null)
            {
                return BadRequest("No data found to be added");
            }
            Book book = new Book()
            {
                Id = model.Id,
                Name = model.Name,
                Price = model.Price,
                Description = model.Description,
                Base64image = model.Base64image,
                Categoryid = model.Categoryid,
                Publisherid = model.Publisherid,
                Quantity = model.Quantity
            };
            var entry = _bookRepository.UpdateBook(book);

            BookModel bookModel = new BookModel(entry);

            return Ok(bookModel);
        }

        [HttpDelete("deleteBook/{id}")]
        [ProducesResponseType(typeof(bool), 200)]
        [ProducesResponseType(typeof(BadRequestObjectResult), 400)]
        public IActionResult DeleteBook(int id)
        {
            if (id == 0)
            {
                return BadRequest("Please enter valid Id");
            }
            var entry = _bookRepository.DeleteBook(id);
            return Ok(entry);
        }               
    }
}
