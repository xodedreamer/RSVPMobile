using System;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Text;

namespace RSVPMobile.Services.Base
{
    public abstract class BaseApiService
    {
        protected readonly HttpClient _httpClient;

        protected BaseApiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        protected async Task AddAuthHeader()
        {
            var token = await SecureStorage.Default.GetAsync("auth_token");
            if (!string.IsNullOrEmpty(token))
            {
                _httpClient.DefaultRequestHeaders.Authorization =
                    new AuthenticationHeaderValue("Bearer", token);
            }
        }
    }
}
