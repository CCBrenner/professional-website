using ProfessionalWebsite.Client.Services.Contracts;

namespace ProfessionalWebsite.Client.Services
{
    public class CounterService : ICounterService
    {
        public int Count { get; set; } = 0;
        public int Amount { get; set; } = 1;

        public void IncrementCount() => Count += Amount;
        public void DecrementCount() => Count -= Amount;
        public void ResetCount() => Count = 0;
    }
}
