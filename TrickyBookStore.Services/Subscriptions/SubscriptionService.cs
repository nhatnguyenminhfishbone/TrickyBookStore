using System;
using System.Collections.Generic;
using System.Linq;
using TrickyBookStore.Models;

namespace TrickyBookStore.Services.Subscriptions
{
    internal class SubscriptionService : ISubscriptionService
    {
        IList<Subscription> allSubscriptions = (IList<Subscription>)Store.Subscriptions.Data;

        public IList<Subscription> GetSubscriptions(params int[] ids)
        {
            IList<Subscription> targetSubscriptions = new List<Subscription>();
            Subscription subscription = new Subscription();

            foreach (int id in ids)
            {
                subscription = GetSubscription(id);
                if (subscription != null)
                {
                    targetSubscriptions.Add(subscription);
                }
            }
            return targetSubscriptions.OrderByDescending(c => c.Priority).ToList();
        }

        private Subscription GetSubscription(int id)
        {
            Subscription subscription = new Subscription();
            try
            {
                subscription = allSubscriptions.FirstOrDefault(subscriptionItem => subscriptionItem.Id == id);
                return subscription;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }

            return null;
        }
    }
}
