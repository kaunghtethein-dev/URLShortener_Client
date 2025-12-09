using Microsoft.JSInterop;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using URLShortener_Shared.DTOs;
using URLShortener_Shared.Wrappers;

namespace URLShortener_Client.Services
{
    public class ApiClient
    {
        private readonly HttpClient _httpClient;
        private readonly VariablesService _variablesService;

        public ApiClient(HttpClient httpClient, VariablesService variablesService)
        {
            _httpClient = httpClient;
            _variablesService = variablesService;
        }

        // Generic GET request
        public async Task<T?> GetAsync<T>(string uri)
        {
            await AttachTokenAsync();
            return await _httpClient.GetFromJsonAsync<T>(uri);
        }
        // Generic POST request
        public async Task<T?> PostAsync<T>(string uri, object data)
        {
            await AttachTokenAsync();
            var response = await _httpClient.PostAsJsonAsync(uri, data);
            response.EnsureSuccessStatusCode();
            return await response.Content.ReadFromJsonAsync<T>();
        }
        //Seperate method for byteArray response
        public async Task<byte[]?> GetBytesAsync(string uri)
        {
            await AttachTokenAsync();
            var response = await _httpClient.GetAsync(uri);

            if (!response.IsSuccessStatusCode) return null;
            return await response.Content.ReadAsByteArrayAsync();
        }

        // Attach JWT token from localStorage if available
        private async Task AttachTokenAsync()
        {
            if (_variablesService.AuthInfo != null &&
                _variablesService.AuthInfo.AccessTokenExpiresAt <= DateTime.UtcNow)
            {
                await RefreshTokenAsync();
            }

            var auth = _variablesService.AuthInfo;
            if (auth != null && !string.IsNullOrWhiteSpace(auth.AccessToken))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", auth.AccessToken);
            }
        }
        // Call backend refresh endpoint
        private async Task RefreshTokenAsync()
        {
            var auth = _variablesService.AuthInfo;
            if (auth == null || string.IsNullOrWhiteSpace(auth.RefreshToken))
            {
                return;
            }

            try
            {
                var refreshRequest = new Dto_RefreshRequest
                {
                    RefreshToken = auth.RefreshToken
                };

                var response = await _httpClient.PostAsJsonAsync("api/user/refresh", refreshRequest);
                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<DataResult<Dto_AuthResponse>>();
                    if (result != null && result.Success && result.Data != null)
                    {
                        // Convert Dto_AuthResponse to AuthInfo
                        var newAuth = new AuthInfo
                        {
                            AccessToken = result.Data.AccessToken,
                            RefreshToken = result.Data.RefreshToken,
                            AccessTokenExpiresAt = result.Data.AccessTokenExpiresAt,
                            UserName = result.Data.UserName,
                            Email = result.Data.Email
                        };

                        await _variablesService.SaveAuthInfoAsync(newAuth);
                        
                    }
                    else
                    {
                        // Failed refresh, clear auth
                        await _variablesService.ClearAuthInfoAsync();
                    }
                }
                else
                {
                    // Failed refresh, clear auth
                    await _variablesService.ClearAuthInfoAsync();
                }
            }
            catch
            {
                await _variablesService.ClearAuthInfoAsync();
            }
        }

    }
}
