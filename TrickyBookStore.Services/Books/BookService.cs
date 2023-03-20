using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Books
{
    internal class BookService : IBookService
    {
        IList<Book> allBooks = (IList<Book>)Store.Books.Data;
        public IList<Book> GetBooks(params long[] ids)
        {
            throw new NotImplementedException();
        }

        public Book GetBook(long id)
        {
            Book book = new Book();
            try
            {
                book = allBooks.SingleOrDefault(bookItem => bookItem.Id == id);
                return book;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
            return null;
        }

        public IList<Book> GetNewBooks(IList<Book> books)
        {
            IList<Book> newBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (!book.IsOld)
                {
                    newBooks.Add(book);
                }
            }
            return newBooks.OrderByDescending(b => b.Price).ToList();
        }

        public IList<Book> GetOldBooks(IList<Book> books)
        {
            IList<Book> oldBooks = new List<Book>();
            foreach (Book book in books)
            {
                if (book.IsOld)
                {
                    oldBooks.Add(book);
                }
            }
            return oldBooks.OrderByDescending(b => b.Price).ToList();
        }

        internal string toString(Book book)
        {
            return "  + Id:" + book.Id + ",Cate " + book.CategoryId + ",isOld " + book.IsOld + ",Price " + book.Price;
        }
    }
}
