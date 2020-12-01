using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



namespace BookClub.Controllers
{
    [Authorize]
    [ApiController]
    [Route("api/[controller]")]
    public class BookController : ControllerBase
    {
        private readonly ILogger<BookController> _logger;

        public BookController(ILogger<BookController> logger)
        {
            _logger = logger;
        }

        [HttpPost]
        [Route("booklist")]
        public IEnumerable<Models.BookModel> BookList(Models.BookModel search)
        {
            var id = HttpContext.User.FindFirst("id");
            if (id == null)
                throw new Exception("Ошибка аутентификации");
            return GetBookList(int.Parse(id.Value), search.bookName, true);
        }

        [HttpPost]
        [Route("mybooklist")]
        public IEnumerable<Models.BookModel> MyBookList(Models.BookModel search)
        {
            var id = HttpContext.User.FindFirst("id");
            if (id == null)
                throw new Exception("Ошибка аутентификации");
            return GetBookList(int.Parse(id.Value), search.bookName, false);
        }

        private IEnumerable<Models.BookModel> GetBookList(int userId, string bookName, bool full)
        {
            using (BookDb.BookDbContext db = new BookDb.BookDbContext())
            {
                if (full)
                    return db.Book.Where(x => bookName == null ? true : x.Name.ToLower().Contains(bookName.ToLower()))
                        .Select(x => new Models.BookModel
                        {
                            bookId = x.Id,
                            lnkId = x.LnkUserBook.Any(y=>y.UserId == userId)? -1: (int?)null,
                            bookName = x.Name
                        }).ToArray();
                else
                    return db.LnkUserBook.Where(x => x.UserId == userId)
                        .Select(x => new Models.BookModel
                        {
                            bookId = x.BookId,
                            lnkId = x.Id,
                            bookName = x.Book.Name
                        }).Where(x=> bookName == null? true: x.bookName.ToLower().Contains(bookName.ToLower()))
                        .ToArray();
            }
        }



        /// <summary>
        /// Добавление связки пользователь-книга
        /// </summary>
        /// <param name="bookId">ид книги</param>
        [HttpPost]
        [Route("addbook")]
        public void AddBook(Models.BookModel search)
        {
            var id = HttpContext.User.FindFirst("id");
            if (id == null)
                throw new Exception("Ошибка аутентификации");

            using (BookDb.BookDbContext db = new BookDb.BookDbContext())
            {
                using(var scope = db.Database.BeginTransaction())
                {
                    var lnk = new BookDb.LnkUserBook();
                    lnk.BookId = search.bookId.Value;
                    lnk.UserId = int.Parse(id.Value);

                    db.LnkUserBook.Add(lnk);
                    db.SaveChanges();
                    scope.Commit();
                }
            }
        }

        /// <summary>
        /// Удаление связи пользователь-книга
        /// </summary>
        /// <param name="lnkId">ид связки</param>
        [HttpPost]
        [Route("removebook")]
        public void RemoveBook(Models.BookModel search)
        {
            var id = HttpContext.User.FindFirst("id");
            if (id == null)
                throw new Exception("Ошибка аутентификации");

            using (BookDb.BookDbContext db = new BookDb.BookDbContext())
            {
                using (var scope = db.Database.BeginTransaction())
                {
                    var lnk = db.LnkUserBook.Find(search.lnkId.Value);
                    if (lnk == null)
                        throw new Exception("Связка не найдена");
                    
                    db.LnkUserBook.Remove(lnk);
                    db.SaveChanges();
                    scope.Commit();
                }
            }
        }
    }
}
