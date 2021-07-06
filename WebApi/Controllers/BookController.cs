using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using WebApi.Data;
using WebApi.Data.Models;

namespace WebApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ApplicationDbContext data;

        public BookController(ApplicationDbContext data) => this.data = data;

        [HttpGet]
        public IEnumerable<Book> Get()
            => this.data.Books.ToArray();

        [HttpPost]
        public async Task<JsonResult> Post(Book book)
        {
            if (!ModelState.IsValid || book.Year <= 0 || book.IMDB <= 0)
            {
                return new JsonResult("Model is invalid. The fields are required!");
            }

            await this.data.Books.AddAsync(new Book
            {
                Name = book.Name,
                Author = book.Author,
                ImagePath = book.ImagePath,
                Year = book.Year,
                IMDB = book.IMDB,
            });
            await this.data.SaveChangesAsync();

            return new JsonResult("Added Successfully");
        }

        [HttpPut]
        public async Task<JsonResult> Put(Book book)
        {
            if (!ModelState.IsValid || book.Year <= 0 || book.IMDB <= 0)
            {
                return new JsonResult("Model is invalid. The fields are required!");
            }

            var entity = await this.data.Books.FindAsync(book.Id);
            if (entity == null)
            {
                return new JsonResult("Book not exist!");
            }

            entity.Name = book.Name;
            entity.Author = book.Author;
            entity.Year = book.Year;
            entity.IMDB = book.IMDB;

            await this.data.SaveChangesAsync();

            return new JsonResult("Book updated successfully.");
        }

        [HttpDelete("{id}")]
        public async Task<JsonResult> Delete(int id)
        {
            var book = await this.data.Books.FindAsync(id);
            if (book == null)
            {
                return new JsonResult("Book not exist!");
            }

            this.data.Books.Remove(book);
            await this.data.SaveChangesAsync();

            return new JsonResult("Book deleted successfully.");
        }
    }
}
