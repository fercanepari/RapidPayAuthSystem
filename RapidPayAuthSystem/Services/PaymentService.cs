namespace RapidPayAuthSystem.Services
{
    public class PaymentService : IPaymentService
    {
        private static readonly Random _random = new Random();
        private decimal _currentFee = 0.1m; // Initial fee

        public PaymentService()
        {
            // Start a background task to update fee randomly every hour
            ThreadPool.QueueUserWorkItem(_ =>
            {
                while (true)
                {
                    Thread.Sleep(TimeSpan.FromHours(1));
                    _currentFee *= (decimal)_random.NextDouble() * 2; // Random decimal between 0 and 2
                }
            });
        }

        public decimal GetCurrentFee()
        {
            return _currentFee;
        }
    }
}
