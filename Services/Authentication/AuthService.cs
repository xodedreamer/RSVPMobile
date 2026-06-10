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
        private readonly IUserSessionService _session;
        public AuthService(HttpClient httpClient, IUserSessionService session)
    : base(httpClient)
        {
            _session = session;
        }

        public async Task<bool> LoginAsync(LoginRequest loginDto)
        {
            try
            {
                // DEBUG: See exactly what is being serialized
                var json = JsonSerializer.Serialize(loginDto);
                System.Diagnostics.Debug.WriteLine($"MAUI SENDING LOGIN: {json}");

                var response = await _httpClient.PostAsJsonAsync("auth/login", loginDto);


                if (response.IsSuccessStatusCode)
                {
                    var result = await response.Content.ReadFromJsonAsync<AuthResponse>();

                    if (result?.Token != null)
                    {
                        // Securely store the JWT for future requests
                        await SecureStorage.Default.SetAsync("auth_token", result.Token);

                        _session.LoadFromLoginResponse(result);

                        if (!string.IsNullOrEmpty(result.FullName))
                        {
                            Preferences.Default.Set("user_name", result.FullName);
                            Preferences.Default.Set("user_email", result.Email);
                            Preferences.Default.Set("user_role", result.Role);

                        }

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
            try
            {
                // Resulting URL: http://10.0.2.2:5148/api/auth/register
               // DEBUG: See exactly what is being serialized
                 var json = JsonSerializer.Serialize(signupDto);
                 System.Diagnostics.Debug.WriteLine($"MAUI SENDING SIGNUP: {json}");

                 var response = await _httpClient.PostAsJsonAsync("auth/register", signupDto);
                 return response.IsSuccessStatusCode;
            }
            catch (Exception)
            {

                return false;
            }
        }

        public async Task LogoutAsync()
        {
            SecureStorage.Default.Remove("auth_token");
            await Shell.Current.GoToAsync("//LoginView");
        }
    }
}
