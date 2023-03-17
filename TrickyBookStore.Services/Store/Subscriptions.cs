using System.Collections.Generic;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Store
{
    public static class Subscriptions
    {
        public static readonly IEnumerable<Subscription> Data = new List<Subscription>
        {
            new Subscription { Id = 1, SubscriptionType = SubscriptionTypes.Paid, Priority = 2,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 50 },
                    { "LimitBookBuyWithDiscount", 3 },
                    { "DiscountBuyNewBook", 0.05 },
                    { "DiscountReadOldBook", 0.95 }
                }
            },
            new Subscription { Id = 2, SubscriptionType = SubscriptionTypes.Free, Priority = 1,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 0 },
                    { "LimitBookBuyWithDiscount", 0 },
                    { "DiscountBuyNewBook", 0 },
                    { "DiscountReadOldBook", 0.1 }
                }
            },
            new Subscription { Id = 3, SubscriptionType = SubscriptionTypes.Premium, Priority = 4,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 200 },
                    { "LimitBookBuyWithDiscount", 3 },
                    { "DiscountBuyNewBook", 0.15 },
                    { "DiscountReadOldBook", 1 }
                }
            },
            new Subscription { Id = 4, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 75 },
                    { "LimitBookBuyWithDiscount", 3 },
                    { "DiscountBuyNewBook", 0.15 },
                    { "DiscountReadOldBook", 1 }
                },
                BookCategoryId = 1
            },
            new Subscription { Id = 5, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 75 },
                    { "LimitBookBuyWithDiscount", 3 },
                    { "DiscountBuyNewBook", 0.15 },
                    { "DiscountReadOldBook", 1 }
                },
                BookCategoryId = 2
            },
            new Subscription { Id = 6, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 75 },
                    { "LimitBookBuyWithDiscount", 3 },
                    { "DiscountBuyNewBook", 0.15 },
                    { "DiscountReadOldBook", 1 }
                },
                BookCategoryId = 3
            },
            new Subscription { Id = 7, SubscriptionType = SubscriptionTypes.CategoryAddicted, Priority = 3,
                PriceDetails = new Dictionary<string, double>
                {
                    { "SubscriptionFee", 75 },
                    { "LimitBookBuyWithDiscount", 3 },
                    { "DiscountBuyNewBook", 0.15 },
                    { "DiscountReadOldBook", 1 }
                },
                BookCategoryId = 4
            },
        };
    }
}
