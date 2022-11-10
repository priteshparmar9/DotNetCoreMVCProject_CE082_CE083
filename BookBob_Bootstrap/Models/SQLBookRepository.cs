using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public class SQLBookRepository : IBookRepository
    {
        private readonly AppDbContext context;
        public SQLBookRepository(AppDbContext context)
        {
            this.context = context;
        }
        Book IBookRepository.Add(Book Book)
        {
            context.Books.Add(Book);
            context.SaveChanges();
            return Book;
        }
        Book IBookRepository.Delete(int Id)
        {
            Book Book = context.Books.Find(Id);
            if (Book != null)
            {
                context.Books.Remove(Book);
                context.SaveChanges();
            }
            return Book;
        }

        IEnumerable<Book> IBookRepository.GetAllBooks()
        {
            return context.Books;
        }

        Book IBookRepository.GetBook(int id)
        {
            return context.Books.FirstOrDefault(m => m.BookId == id);
        }

        Book IBookRepository.Update(Book BookChanges)
        {
            var Book = context.Books.Attach(BookChanges);
            Book.State = Microsoft.EntityFrameworkCore.EntityState.Modified;
            context.SaveChanges();
            return BookChanges;
        }
    }
}
