using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;

namespace TrickyBookStore.Services.PurchaseTransactions
{
    internal class PurchaseTransactionService : IPurchaseTransactionService
    {
        IBookService BookService { get; }

        IList<PurchaseTransaction> allPurchaseTransactionsFromStore = (IList<PurchaseTransaction>)Store.PurchaseTransactions.Data;

        public PurchaseTransactionService(IBookService bookService)
        {
            BookService = bookService;
        }

        public IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<PurchaseTransaction> purchaseTransactions = new List<PurchaseTransaction>();
            foreach (var transaction in allPurchaseTransactionsFromStore)
            {
                if (transaction.CustomerId == customerId && transaction.CreatedDate >= fromDate && transaction.CreatedDate <= toDate)
                {
                    purchaseTransactions.Add(transaction);
                }
            }

            return purchaseTransactions;
        }

        public IList<Book> GetAllCustomerBooks(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<Book> books = new List<Book>();
            IList<PurchaseTransaction> purchaseTransactions = GetPurchaseTransactions(customerId, fromDate, toDate);
            foreach (var transaction in purchaseTransactions)
            {
                books.Add(BookService.GetBook(transaction.BookId));
            }

            return books.ToList();
        }

        public IList<Book> GetCustomerOldBooks(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            return BookService.GetOldBooks(GetAllCustomerBooks(customerId, fromDate, toDate));
        }

        public IList<Book> GetCustomerNewBooks(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            return BookService.GetNewBooks(GetAllCustomerBooks(customerId, fromDate, toDate));
        }
    }
}
