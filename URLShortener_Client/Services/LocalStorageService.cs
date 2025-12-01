using Microsoft.JSInterop;
using System.Text.Json;

namespace URLShortener_Client.Services
{
    public class LocalStorageService
    {
        private readonly IJSRuntime _jsRuntime;

        public LocalStorageService(IJSRuntime jsRuntime)
        {
            _jsRuntime = jsRuntime;
        }

        // Set an item in localStorage
        public async Task SetItemAsync<T>(string key, T item)
        {
            var json = JsonSerializer.Serialize(item);
            await _jsRuntime.InvokeVoidAsync("localStorage.setItem", key, json);
        }

        // Get an item from localStorage
        public async Task<T?> GetItemAsync<T>(string key)
        {
            var json = await _jsRuntime.InvokeAsync<string>("localStorage.getItem", key);
            if (string.IsNullOrEmpty(json))
            {
                return default;
            }
            return JsonSerializer.Deserialize<T>(json);
        }

        // Remove an item from localStorage
        public async Task RemoveItemAsync(string key)
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.removeItem", key);
        }

        // Clear all localStorage
        public async Task ClearAsync()
        {
            await _jsRuntime.InvokeVoidAsync("localStorage.clear");
        }
    }
}
