using Book_Inventory_System.Data;
using Book_Inventory_System.DTO_Model;
using Book_Inventory_System.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Book_Inventory_System.Controllers
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly BookDBContext bookDBContext;

        public BookController(BookDBContext dBContext)
        {
            bookDBContext= dBContext;

        }

        [HttpPost]
        public ActionResult CreateBook([FromBody] BookDTO bookDto)
        {
            if (bookDto == null)
            {
                return BadRequest("Check Book Details Again !");
            }
            else
            {
                Book books = new Book()
                {
                    Title = bookDto.Title,
                    ISBN = bookDto.ISBN,
                    Author = bookDto.Author,
                    PublicationDate = bookDto.PublicationDate
                };

                bookDBContext.Book.Add(books);
                bookDBContext.SaveChanges();

                return Ok("Book Details Saved !");
            }
        }

        [HttpGet]
        public ActionResult<List<BookDTO>> GetBooks()
        {
            var books = bookDBContext.Book.Select(b =>new BookDTO()
            {
                Id=b.Id,
                Title=b.Title,
                ISBN=b.ISBN,
                Author=b.Author,
                PublicationDate=b.PublicationDate
            }).ToList();

            return Ok(books);
        }

        [HttpGet("{id:int}")]
        public ActionResult GetBook(int id)
        {
            if (id <= 0)
            {
                return BadRequest($"Invalid Book Id {id}");
            }

            var bookDetails = bookDBContext.Book.Where(b => b.Id == id).FirstOrDefault();

            if(bookDetails == null)
            {
                return BadRequest($"The Book Details With Id:{id} Not Found !");
            }
            else
            {
                var bookDto = new BookDTO()
                {
                    Id=bookDetails.Id,
                    Title = bookDetails.Title,
                    ISBN = bookDetails.ISBN,
                    Author=bookDetails.Author,
                    PublicationDate=bookDetails.PublicationDate
                    
                };

                return Ok(bookDto);
            }
        }

        [HttpPut]
        public ActionResult UpdateBook([FromBody] BookDTO bookDto)
        {
            var bookDetails = bookDBContext.Book.Where(b => b.Id == bookDto.Id).FirstOrDefault();

            if (bookDetails == null)
            {
                return BadRequest($"The Book Details With Id:{bookDto.Id} Not Found !");
            }
            else
            {
                bookDetails.Title = bookDto.Title;
                bookDetails.ISBN = bookDto.ISBN;
                bookDetails.Author = bookDto.Author;
                bookDetails.PublicationDate = bookDto.PublicationDate;

                bookDBContext.SaveChanges();

                return Ok(bookDetails);
            }

        }

        [HttpDelete("{id:int}")]
        public ActionResult DeleteBook(int id)
        {
            var bookDetails = bookDBContext.Book.Where(b => b.Id ==id).FirstOrDefault();

            if (bookDetails == null)
            {
                return BadRequest($"The Book Details With Id:{id} Not Found !");
            }
            else
            {
                bookDBContext.Remove(bookDetails);
                bookDBContext.SaveChanges();

                return Ok("Book Details Deleted !");
            }
        }
    }
}
