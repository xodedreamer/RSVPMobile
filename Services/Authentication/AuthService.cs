using RSVPMobile.Dtos;
using RSVPMobile.Services.Base;
using System;
using System.Collections.Generic;
using System.Net.Http.Json;
using System.Text;
using System.Text.Json;

namespace RSVPMobile.Services.Authentication
{
    public class AuthService : BaseApiService, IAuthService
    {
        public AuthService(HttpClient httpClient) : base(httpClient) { }

        public async Task<bool> LoginAsync(LoginRequest loginDto)
        {
            try
            {
                // DEBUG: See exactly what is being serialized
                var json = JsonSerializer.Serialize(loginDto);
                System.Diagnostics.Debug.WriteLine($"MAUI SENDING: {json}");

                var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);

                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                    if (result?.Token != null)
                    {
                        // Securely store the JWT for future requests
                        await SecureStorage.Default.SetAsync("auth_token", result.Token);
                        return true;
                    }
                }
                return false;
            }
            catch (Exception)
            {
                // Handle network or serialization errors
                return false;
            }
        }

        public async Task<bool> SignupAsync(SignupRequest signupDto)
        {
            var response = await _httpClient.PostAsJsonAsync("auth/signup", signupDto);
            return response.IsSuccessStatusCode;
        }

        public async Task LogoutAsync()
        {
            SecureStorage.Default.Remove("auth_token");
            await Shell.Current.GoToAsync("//LoginView");
        }
    }
}
