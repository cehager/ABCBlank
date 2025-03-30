using Common.Wrapper;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace BankUI.Extensions
{
    internal static  class ResponseExtensions
    {
        public static async Task<ResponseWrapper<T>> ToResponse<T>(this HttpResponseMessage responseMessage)
        {
            var responseString = await responseMessage.Content.ReadAsStringAsync();
            var responseObject = JsonSerializer.Deserialize<ResponseWrapper<T>>(responseString, new JsonSerializerOptions
                {
                      PropertyNameCaseInsensitive = true,
                      ReferenceHandler = ReferenceHandler.Preserve
                });

            return responseObject;
        }
    }
}
