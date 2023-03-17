using System.Collections.Generic;
using TrickyBookStore.Models;

// KeepIt
namespace TrickyBookStore.Services.Books
{
    public interface IBookService
    {
        IList<Book> GetBooks(params long[] ids);
        IList<Book> GetNewBooks(IList<Book> books);
        IList<Book> GetOldBooks(IList<Book> books);
        Book GetBook(long id);
    }
}
