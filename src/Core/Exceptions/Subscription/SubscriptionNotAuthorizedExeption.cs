namespace Core.Exceptions.Subscription
{
    public class SubscriptionNotAuthorizedExeption : Exception
    {
        public SubscriptionNotAuthorizedExeption() : base("You are not authorized to make subscriptions.")
        {
        }
    }
}
