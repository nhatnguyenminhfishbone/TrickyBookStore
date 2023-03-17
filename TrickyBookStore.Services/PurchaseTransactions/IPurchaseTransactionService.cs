using System;
using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.PurchaseTransactions
{
    // KeepIt
    public interface IPurchaseTransactionService
    {
        IList<PurchaseTransaction> GetPurchaseTransactions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate);
        IList<Book> GetAllCustomerBooks(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate);
        IList<Book> GetCustomerOldBooks(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate);
        IList<Book> GetCustomerNewBooks(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate);

    }
}
