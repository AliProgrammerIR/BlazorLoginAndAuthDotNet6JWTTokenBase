﻿using System.ComponentModel.Design;
using System.Text.Json;
using System.Text;
using System.Net.Http;

namespace LoginDC6.Client.Helpers
{
    public interface IHttpService
    {
        Task<HttpResponseWrapper<object>> Post<T>(string url, T data);
        Task<HttpResponseWrapper<object>> Put<T>(string url, T data);
        Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data);
        Task<HttpResponseWrapper<T>> Get<T>(string url);
    }
    public class HttpService : IHttpService
    {
        private readonly HttpClient httpClient;
        protected JsonSerializerOptions defaultJsonSerializerOptions => new JsonSerializerOptions { PropertyNameCaseInsensitive = true };

        public HttpService(HttpClient httpClient)
        {
            this.httpClient = httpClient;
        }

        public async Task<HttpResponseWrapper<object>> Post<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync(url, stringContent);

            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<object>> Put<T>(string url, T data)
        {
            var dataJson = JsonSerializer.Serialize(data);
            var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
            var response = await httpClient.PutAsync(url, stringContent);

            return new HttpResponseWrapper<object>(null, response.IsSuccessStatusCode, response);
        }

        public async Task<HttpResponseWrapper<TResponse>> Post<T, TResponse>(string url, T data)
        {
            try
            {
                var dataJson = JsonSerializer.Serialize(data);
                var stringContent = new StringContent(dataJson, Encoding.UTF8, "application/json");
                var response = await httpClient.PostAsync(url, stringContent);

                if (response.IsSuccessStatusCode)
                {
                    var deserializedResponse = await Deserialize<TResponse>(response, defaultJsonSerializerOptions);
                    return new HttpResponseWrapper<TResponse>(deserializedResponse, true, response);
                }
                else
                {
                    return new HttpResponseWrapper<TResponse>(default, false, response);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return new HttpResponseWrapper<TResponse>(default, false, null);
            }
        }

        public async Task<HttpResponseWrapper<T>> Get<T>(string url)
        {
            var responseHTTP = await httpClient.GetAsync(url);
            if (responseHTTP.IsSuccessStatusCode)
            {
                var response = await Deserialize<T>(responseHTTP, defaultJsonSerializerOptions);
                return new HttpResponseWrapper<T>(response, true, responseHTTP);
            }
            else
            {
                return new HttpResponseWrapper<T>(default, false, responseHTTP);
            }
        }

        private async Task<T> Deserialize<T>(HttpResponseMessage httpResponse, JsonSerializerOptions options)
        {
            var responseString = await httpResponse.Content.ReadAsStringAsync();
            return JsonSerializer.Deserialize<T>(responseString, options);
        }
    }
}