namespace ProfessionalWebsite.Client.Services.CounterService.Contracts;

public interface ICounterService
{
    public int Count { get; set; }
    public int Amount { get; set; }
    public void IncrementCount();
    public void DecrementCount();
    public void ResetCount();
}
