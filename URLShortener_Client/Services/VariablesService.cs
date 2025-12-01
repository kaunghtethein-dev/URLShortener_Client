using URLShortener_Shared.DTOs;

namespace URLShortener_Client.Services
{
    public class VariablesService
    {
        private readonly LocalStorageService _localStorage;

        public VariablesService(LocalStorageService localStorage)
        {
            _localStorage = localStorage;
        }

        public AuthInfo? AuthInfo { get; private set; }

        private const string AuthInfoKey = "auth_info";

        public async Task LoadAuthInfoAsync()
        {
            AuthInfo = await _localStorage.GetItemAsync<AuthInfo>(AuthInfoKey);
        }

        public async Task SaveAuthInfoAsync(AuthInfo authInfo)
        {
            AuthInfo = authInfo;
            await _localStorage.SetItemAsync(AuthInfoKey, authInfo);
        }

        public async Task ClearAuthInfoAsync()
        {
            AuthInfo = null;
            await _localStorage.RemoveItemAsync(AuthInfoKey);
        }
    }

    public class AuthInfo : Dto_AuthResponse
    {
        public bool IsGuest => string.IsNullOrEmpty(Email);
    }
}
