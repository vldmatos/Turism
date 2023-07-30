using Shared.Entities;
using Shared.Views;
using System.Net.Http.Json;

namespace Shared
{
    public interface IFunction
    {
        #region Methods

        Task<TView> Search<TView>(Filter filter) where TView : View;

        Task<TContext> Execute<TContext>(Argument argument) where TContext : Entity;

        #endregion Methods
    }

    public class Function : IFunction
    {
        public readonly HttpClient _httpClient;
        public Function(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<TView> Search<TView>(Filter filter) where TView : View
        {
            filter.ResultType = typeof(TView).FullName;
            var result = await _httpClient.PostAsJsonAsync($"api/search", filter);
            return await result.Content.ReadFromJsonAsync<TView>();
        }

        public async Task<TContext> Execute<TContext>(Argument argument) where TContext : Entity
        {
            argument.Type = typeof(TContext).FullName;
            var result = await _httpClient.PostAsJsonAsync($"api/execution", argument);
            return await result.Content.ReadFromJsonAsync<TContext>();
        }
    }

    public class Filter
    {
        public string Operation { get; init; }

        public Dictionary<string, object> Parameters { get; init; } = new();

        public string Source { get; init; }

        public string ResultType { get; set; }
    }

    public class Argument
    {
        public string Operation { get; init; }

        public dynamic Context { get; init; }

        public string Type { get; set; }        

        public string Source { get; init; }
    }
}