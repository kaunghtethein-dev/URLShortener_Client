using Microsoft.JSInterop;
using System.Text.RegularExpressions;

namespace URLShortener_Client.Services
{
    public class CommonFunctionsService
    {
        private readonly LocalStorageService _localStorage;
        private readonly IJSRuntime _js;
        private readonly VariablesService _vs;

        public CommonFunctionsService(LocalStorageService localStorage, IJSRuntime js, VariablesService vs)
        {
            _localStorage = localStorage;
            _js = js;
            _vs = vs;
        }
        public async Task LoadAuthInfoAsync()
        {
            _vs.AuthInfo = await _localStorage.GetItemAsync<AuthInfo>(_vs.AuthInfoKey);
        }
        public async Task SaveAuthInfoAsync(AuthInfo authInfo)
        {
            _vs.AuthInfo = authInfo;
            await _localStorage.SetItemAsync(_vs.AuthInfoKey, authInfo);
        }
        public async Task ClearAuthInfoAsync()
        {
            _vs.AuthInfo = null;
            await _localStorage.RemoveItemAsync(_vs.AuthInfoKey);
        }
        public async Task LogOut()
        {
            await ClearAuthInfoAsync();
            await _js.InvokeVoidAsync("location.reload");
        }
        public bool IsEmailValidFormat(string email)
        {
            string pattern = @"^[a-zA-Z0-9.!#$%&'*+/=?^_`{|}~-]+@[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?(?:\.[a-zA-Z0-9](?:[a-zA-Z0-9-]{0,61}[a-zA-Z0-9])?)*$";
            return Regex.IsMatch(email, pattern, RegexOptions.IgnoreCase);
            
        }
        public bool IsSafeSlug(string? input)
        {
            if (string.IsNullOrWhiteSpace(input))
                return false;

            return Regex.IsMatch(input, @"^[A-Za-z0-9_-]+$");
        }

    }
}
