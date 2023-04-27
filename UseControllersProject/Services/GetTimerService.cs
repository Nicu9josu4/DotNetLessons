namespace UseMVCProject.Services
{
    public class GetTimerService : IGetTimerService
    {
        public string GetTime() => $"Time now is: {DateTime.Now:hh:mm:ss}";
    }
}