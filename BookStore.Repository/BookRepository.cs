using BookStore.Models.Models;
using BookStore.Models.ViewModels;

namespace BookStore.Repository
{
    public  class BookRepository : BaseRepository
    {
        public ListResponse<Book> GetBooks(int pageIndex=1,int pageSize=10, string? keyword = "",int categoryid=0)
        {
            keyword = keyword?.ToLower()?.Trim();
            var query = _context.Books.Where(c => keyword == null || c.Name.ToLower().Contains(keyword)).AsQueryable();
            if (categoryid != 0)
            {
                query = query.Where(c => c.Categoryid == categoryid);
            }
            int totalRecords = query.Count();
            List<Book> books = query.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();

            return new ListResponse<Book>()
            {
                Results = books,
                Totalrecords = totalRecords,
            };
        }

        public Book GetBook(int id)
        {
            return _context.Books.FirstOrDefault(c => c.Id == id);
        }

        public Book AddBook(Book book)
        {
            var entry = _context.Books.Add(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public Book UpdateBook(Book book)
        {
            var entry = _context.Books.Update(book);
            _context.SaveChanges();
            return entry.Entity;
        }

        public bool DeleteBook(int id)
        {
            var book = _context.Books.FirstOrDefault(c => c.Id == id);
            var entry = _context.Books.Remove(book);
            _context.SaveChanges();
            return true;
        }
    }
}
