using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BookBob_Bootstrap.Models
{
    public interface IBookRepository
    {
        Book GetBook(int Id);
        IEnumerable<Book> GetAllBooks();
        Book Add(Book Book);
        Book Update(Book BookChanges);
        Book Delete(int Id);
    }
}
