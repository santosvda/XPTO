using System;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;
using XPTO.Domain.FakeStore;

namespace XPTO.Repository.FakeStore
{
    public static class FakeStoreRepository
    {
        private static string baseURL = "https://fakestoreapi.com/";
        private static HttpClient client = new HttpClient();

        public static bool PostProduct(FakeStoreProduct product)
        {
            try
            {
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(baseURL);
                }
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var data = JsonConvert.SerializeObject(product);

                var request = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                string route = "products";

                var postResponse = client.PostAsync(route, request).Result;

                if (postResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool UpdateProduct(FakeStoreProduct product, int productId)
        {
            try
            {
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(baseURL);
                }
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                var data = JsonConvert.SerializeObject(product);

                var request = new StringContent(JsonConvert.SerializeObject(product), Encoding.UTF8, "application/json");

                string route = "products/"+productId;

                var postResponse = client.PutAsync(route, request).Result;

                if (postResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
        public static bool DeleteProduct(int productId)
        {
            try
            {
                if (client.BaseAddress == null)
                {
                    client.BaseAddress = new Uri(baseURL);
                }
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(
                    new MediaTypeWithQualityHeaderValue("application/json"));

                string route = "products/"+productId;

                var postResponse = client.DeleteAsync(route).Result;

                if (postResponse.IsSuccessStatusCode)
                {
                    return true;
                }
                return false;
            }
            catch
            {
                return false;
            }
        }
    }
}