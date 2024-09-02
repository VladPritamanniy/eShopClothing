namespace Core.Exceptions.Subscription
{
    public class SubscriptionExeption : Exception
    {
        public SubscriptionExeption() : base("You are already subscribed to this seller.")
        {
        }
    }
}
