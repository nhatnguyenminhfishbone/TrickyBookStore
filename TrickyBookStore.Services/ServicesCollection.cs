using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.Datetime;
using TrickyBookStore.Services.Payment;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services
{
    public class ServicesCollection
    {
        IPaymentService paymentService = new PaymentService(new CustomerService(new SubscriptionService()), new PurchaseTransactionService(new BookService()), new SubscriptionService());
        IDatetimeService getDateService = new DatetimeService();
        public double getPayment(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            return paymentService.GetPaymentAmount(customerId, fromDate, toDate);
        }

        public DateTimeOffset getStartDate(int year, int month)
        {
            return getDateService.getStartDate(year, month);
        }

        public DateTimeOffset getEndDate(int year, int month)
        {
            return getDateService.getEndDate(year, month);
        }
    }
}
