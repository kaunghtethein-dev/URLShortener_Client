namespace URLShortener_Client.Services
{
    public class NotifierService
    {
        public event Action<string, object?, string?>? OnEvent;
        public event Func<string, object?, string?, Task>? OnEventAsync;

        public void Emit(string eventName, object? data = null, string? action = null)
        {
            if (OnEvent != null)
            {
                OnEvent?.Invoke(eventName, data, action);
            }
            if(OnEventAsync != null)
            {
                OnEventAsync?.Invoke(eventName, data, action);
            }
        }
    }
}
