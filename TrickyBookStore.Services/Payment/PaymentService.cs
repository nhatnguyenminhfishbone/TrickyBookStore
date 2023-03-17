using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;
using TrickyBookStore.Services.Books;
using TrickyBookStore.Services.Customers;
using TrickyBookStore.Services.PurchaseTransactions;
using TrickyBookStore.Services.Subscriptions;

namespace TrickyBookStore.Services.Payment
{
    internal class PaymentService : IPaymentService
    {
        ICustomerService CustomerService { get; }
        IPurchaseTransactionService PurchaseTransactionService { get; }
        ISubscriptionService SubscriptionService { get; }

        public PaymentService(ICustomerService customerService, 
            IPurchaseTransactionService purchaseTransactionService,
            ISubscriptionService subscriptionService)
        {
            CustomerService = customerService;
            PurchaseTransactionService = purchaseTransactionService;
            SubscriptionService = subscriptionService;
        }

        public double GetPaymentAmount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            IList<Book> customerBooks = PurchaseTransactionService.GetAllCustomerBooks(customerId, fromDate, toDate);
            Console.WriteLine("- List books: ");
            foreach (Book book in customerBooks)
            {
                Console.WriteLine("  + Id:" + book.Id + ",Cate " + book.CategoryId + ",isOld " + book.IsOld + ",Price " + book.Price);
            }

            double TotalBeforeDiscount = GetTotalBeforeDiscount(customerBooks);
            double TotalDiscount = GetTotalDiscount(customerId, fromDate, toDate);
            double TotalAfterDiscount = TotalBeforeDiscount - TotalDiscount;
            double TotalSubscriptions = GetTotalSubscriptions(customerId, fromDate, toDate);

            Console.WriteLine("Price books before discount: " + TotalBeforeDiscount);
            Console.WriteLine("Price books discount: " + TotalDiscount);
            Console.WriteLine("Price books after discount: " + TotalAfterDiscount);
            Console.WriteLine("Price subscriptions: " + TotalSubscriptions);

            return TotalAfterDiscount + TotalSubscriptions;
        }

        private double GetTotalDiscount(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            Customer customer = CustomerService.GetCustomerById(customerId);
            IList<Subscription> customerSubscriptions = CustomerService.GetSubscriptions(customer);
            IList<Book> customerOldBooks = PurchaseTransactionService.GetCustomerOldBooks(customerId, fromDate, toDate);
            IList<Book> customerNewBooks = PurchaseTransactionService.GetCustomerNewBooks(customerId, fromDate, toDate);

            double totalDiscountOldBook = GetDiscountOldBook(customerOldBooks, customerSubscriptions);
            double totalDiscountNewBook = GetDiscountNewBook(customerNewBooks, customerSubscriptions);

            Console.WriteLine("Discount old books: " + totalDiscountOldBook);
            Console.WriteLine("Discount new books: " + totalDiscountNewBook);

            return totalDiscountOldBook + totalDiscountNewBook;
        }

        private double GetTotalSubscriptions(long customerId, DateTimeOffset fromDate, DateTimeOffset toDate)
        {
            Customer customer = CustomerService.GetCustomerById(customerId);
            IList<Subscription> customerSubscriptions = CustomerService.GetSubscriptions(customer);
            double total = 0;
            Console.WriteLine("- Subscriptions user");
            foreach (Subscription subscription in customerSubscriptions)
            {
                Console.WriteLine("  + " + subscription.SubscriptionType.ToString() + " " + subscription.BookCategoryId);
                total += subscription.PriceDetails["SubscriptionFee"];
            }
            return total;
        }

        private double GetTotalBeforeDiscount(IList<Book> customerBooks)
        {
            double totalBeforeDiscount = 0;
            foreach (Book customerBook in customerBooks)
            {
                totalBeforeDiscount += customerBook.Price;
            }
            return totalBeforeDiscount;
        }

        private double GetDiscountOldBook(IList<Book> listOldBooks, IList<Subscription> customerSubscriptions)
        {
            double discountPrice = 0;
            bool isCalculated = false;

            var isPremium = customerSubscriptions.FirstOrDefault(s => s.SubscriptionType == SubscriptionTypes.Premium);

            if (isPremium != null)
            {
                return 0;
            }

            foreach (Book book in listOldBooks)
            {
                isCalculated = false;
                foreach (var subscription in customerSubscriptions)
                {
                    if (book.CategoryId == subscription.BookCategoryId)
                    {
                        discountPrice += book.Price;
                        isCalculated = true;
                    }
                    if (subscription.BookCategoryId == null)
                    {
                        discountPrice += book.Price * subscription.PriceDetails["DiscountReadOldBook"];
                        isCalculated = true;
                    }
                    if (isCalculated)
                    {
                        break;
                    }
                }
                if (!isCalculated)
                {
                    discountPrice += book.Price * 0.1;
                }
            }
            return discountPrice;
        }

        private double GetDiscountNewBook(IList<Book> listNewBooks, IList<Subscription> customerSubscriptions)
        {
            double discountPrice = 0;
            bool isCalculated = false;

            foreach (Book book in listNewBooks)
            {
                isCalculated = false;
                foreach (var subscription in customerSubscriptions)
                {
                    if (book.CategoryId == subscription.BookCategoryId)
                    {
                        if (subscription.PriceDetails["LimitBookBuyWithDiscount"] > 0)
                        {
                            discountPrice += book.Price * subscription.PriceDetails["DiscountBuyNewBook"];
                            subscription.PriceDetails["LimitBookBuyWithDiscount"]--;
                            isCalculated = true;
                        }
                    }

                    if (subscription.BookCategoryId == null)
                    {
                        if (subscription.PriceDetails["LimitBookBuyWithDiscount"] > 0)
                        {
                            discountPrice += book.Price * subscription.PriceDetails["DiscountBuyNewBook"];
                            subscription.PriceDetails["LimitBookBuyWithDiscount"]--;
                            isCalculated = true;

                        }
                    }

                    if (isCalculated)
                    {
                        break;
                    }
                }
                if (!isCalculated)
                {
                    discountPrice += 0;
                }
            }
            return discountPrice;
        }
    }
}
