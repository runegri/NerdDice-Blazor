using Microsoft.AspNetCore.Blazor.Browser.Interop;
using System.Threading.Tasks;

namespace no.rag.NerdDice.Services
{
    public static class LocalStorage
    {
        public static async Task<string> GetItem(string key)
        {
            return await Task.FromResult(RegisteredFunction.Invoke<string>("localStorage.getItem", key));
        }

        public static async Task<T> GetItem<T>(string key) 
        {
            var serialized = await GetItem(key);
            if(string.IsNullOrEmpty(serialized))
            {
                return default(T);
            }
            return JsonParser.FromJson<T>(serialized);
        }

        public static async Task SetItem(string key, string item)
        {
            await Task.FromResult(RegisteredFunction.Invoke<object>("localStorage.setItem", key, item));
        }

        public static async Task SetItem<T>(string key, T item)
        {
            var serialized = JsonWriter.ToJson(item);
            await SetItem(key, serialized);
        }
    }
}
